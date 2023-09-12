using AP.ChevronCoop.AppDomain.Security.Approvals.ApprovalNotifications;
using AP.ChevronCoop.AppDomain.Security.Approvals.Approvals;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using AP.ChevronCoop.Entities.Security;
using AP.ChevronCoop.Entities.Security.Approvals;
using AP.ChevronCoop.Entities.Security.Approvals.Approvals;
using AP.ChevronCoop.Entities.Security.Auth.ApplicationUserRoles;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace AP.ChevronCoop.AppCore.Services.ApprovalServices.ApprovalFactory;

public class RetireeSwitchApprovalFactory : IApprovalFactory
{
    private readonly ChevronCoopDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public RetireeSwitchApprovalFactory(ChevronCoopDbContext dbContext, IMapper mapper, IMediator mediator)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _mediator = mediator;
    }

    public async Task<CommandResult<bool>> Initiate(Approval request)
    {
        var response = new CommandResult<bool>();

        response.Response = await _mediator.Send(new SendApprovalRequestNotificationCommand()
        {
            ApprovalId = request.Id
        });

        return response;
    }

    public async Task<CommandResult<bool>> Process(Approval request, string? approvedById, string? comment, ApprovalStatus status)
    {
        var response = new CommandResult<bool>();
        try
        {
            if (status != ApprovalStatus.APPROVED)
            {
                response.Response = false;
                return response;
            }
            
            var switchEmployee = await SwitchEmployee(request.EntityId);
            
            // customer info using memberProfileId
            var memberProfile = await _dbContext.MemberProfiles
                .FirstOrDefaultAsync(x => x.Id == request.EntityId);
            var customer = await _dbContext.Customers
                .FirstOrDefaultAsync(x => x.ApplicationUserId == memberProfile!.ApplicationUserId);
            
            // Check loans attached to member
            var loanApplicationIds = await _dbContext.LoanApplicationGuarantors
                .Where(x => x.GuarantorId == customer.Id)
                .Select(l => l.LoanApplicationId).ToListAsync();
            
            // Get loan accounts
            var loanAccounts = await _dbContext.LoanAccounts
                .Where(x => loanApplicationIds.Contains(x.LoanApplicationId) && !x.IsClosed)
                .ToListAsync();

            response.Response = switchEmployee.Item1;
            response.Message = switchEmployee.Item2;
            return response;
        }
        catch (Exception e)
        {
            response.Response = false;
            response.Message = e.Message;
        }

        return response;
    }


    private async Task<(bool, string)> SwitchEmployee(string memberProfileId)
    {
        string message = string.Empty;

        var memberProfileEntity = await _dbContext.MemberProfiles.FindAsync(memberProfileId);
        var memberShipNumber = memberProfileEntity!.MembershipId;

        if (string.IsNullOrEmpty(memberShipNumber))
        {
            return await Task.FromResult((false, "MemberShip Number not found."));
        }

        if (memberShipNumber.LastOrDefault() == 'E')
            memberShipNumber.Remove(memberShipNumber.Length - 1, 1);
        
        if (memberShipNumber.FirstOrDefault() == 'E')
            memberShipNumber.Remove(0, 1);

        // else if (IsDigit(memberShipNumber.LastOrDefault()))
        // {
        //   memberShipNumber = memberShipNumber + "R";
        // }
        // else
        
        memberShipNumber = "R" + memberShipNumber;

        memberProfileEntity.RetireeNumber = memberShipNumber;
        memberProfileEntity.MembershipId = memberShipNumber;
        memberProfileEntity.MemberType = MemberType.RETIREE;
        
        _dbContext.MemberProfiles.Update(memberProfileEntity);
        
        // Update customer table
        var customerEntity = await _dbContext.Customers.FirstOrDefaultAsync(x => x.ApplicationUserId == memberProfileEntity.ApplicationUserId);
        customerEntity.RetireeNumber = memberShipNumber;
        customerEntity.MemberId = memberShipNumber;
        customerEntity.MemberType = MemberType.RETIREE;
        _dbContext.Customers.Update(customerEntity);
        
        
        try
        {
            var user = await _dbContext.ApplicationUsers.FirstOrDefaultAsync(x =>
              x.Id == memberProfileEntity.ApplicationUserId);
            if (user == null)
            {
                return await Task.FromResult((false, "Member application user record not found."));
            }

            //Put member to Retiree roles
            // Get corporate roles
            var corporateRoles = new List<string> { "retiree", "regular", "expatriate" };
            
            var corporateRoleEntities = await _dbContext.ApplicationRoles
              .Where(x => x.Code != null && corporateRoles.Contains(x.Code.ToLower())).ToListAsync();

            // Get user role
            var userRoles =
              _dbContext.ApplicationUserRoles.Where(x => x.UserId == memberProfileEntity.ApplicationUserId);

            if (userRoles.Any())
            {
                // Remove other corporate roles from user roles
                var userRoleHash = new HashSet<string>(userRoles.Select(x => x.RoleId));
                var corporateRoleHash = new HashSet<string>(corporateRoleEntities.Select(x => x.Id));
                corporateRoleHash.IntersectWith(userRoleHash);

                _dbContext.ApplicationUserRoles.RemoveRange(userRoles.Where(x => corporateRoleHash.Contains(x.RoleId)));
            }

            // Update user roles
            var retireeRole = corporateRoleEntities.FirstOrDefault(x => x.Code?.ToLower() == "retiree")!;

            _dbContext.ApplicationUserRoles.Add(new ApplicationUserRole
            {
                UserId = memberProfileEntity.ApplicationUserId,
                RoleId = retireeRole.Id
            });

        }
        catch (Exception exp)
        {
            return await Task.FromResult((true, exp.Message));
        }
        
        await _dbContext.SaveChangesAsync();
        return await Task.FromResult((true, "Employee Member switch to Retiree completed successfully."));
    }
}