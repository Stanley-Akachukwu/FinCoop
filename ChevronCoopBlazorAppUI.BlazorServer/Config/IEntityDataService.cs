using Microsoft.AspNetCore.Authorization;
using Refit;

namespace ChevronCoop.Web.AppUI.BlazorServer.Config
{
    [Headers("Authorization: Bearer")]
    public interface IEntityDataService
    {

        //C,U,D=>Command objects for Cread, Update & Delete
        //V=>ViewModel for response type

        [Post("/{entity}/create")]
        Task<ApiResponse<V>> Create<C, V>(string entity, [Body] C createPayload);

        [Post("/{entity}/create/{command}")]
        Task<ApiResponse<V>> Create<C, V>(string entity, string command, [Body] C createPayload);


        [Post("/{entity}/update")]
        Task<ApiResponse<V>> Update<U, V>(string entity, [Body] U updatePayload);

        [Post("/{entity}/update/{command}")]
        Task<ApiResponse<V>> Update<U, V>(string entity, string command, [Body] U createPayload);

        [Post("/{entity}/delete")]
        Task<ApiResponse<V>> Delete<D, V>(string entity, [Body] D deletePayload);

        [Post("/{entity}/delete/{command}")]
        Task<ApiResponse<V>> Delete<D, V>(string entity, string command, [Body] D createPayload);

        [Post("/{entity}/process/{command}")]
        Task<ApiResponse<V>> Process<P, V>(string entity, string command, [Body] P processPayload);
        [AllowAnonymous]
        [Post("/ApplicationUserLogin/{command}")]
        Task<ApiResponse<V>> Login<P, V>(string command, [Body] P processPayload);

        [Post("/MemberProfile/enrollment/")]
        Task<ApiResponse<V>> CreateEnrolment<P, V>([Body] P processPayload);

        [Post("/enrollment/update")]
        Task<ApiResponse<V>> UpdateEnrolment<P, V>([Body] P processPayload);
        [Post("/EnrollmentPaymentInfo/check/")]
        Task<ApiResponse<V>> CheckEnrolmentPaymentInfo<P, V>([Body] P processPayload);
        [Get("/ApplicationRole/{key}")]
        Task<ApiResponse<V>> GetRolesPermissions<V>(string key);


        [Post("/ApplicationUser/changeStatus/")]
        Task<ApiResponse<V>> ApproveEnrolment<P, V>([Body] P processPayload);


        [Post("/MemberProfile/approveKyc")]
        Task<ApiResponse<V>> ApproveKYC<P, V>([Body] P processPayload);

        [Get("/{entity}")]
        Task<ApiResponse<V>> GetResponse<V>(string entity, string P);
        [Post("/ApplicationUser/ChangeStatus/")]
        Task<ApiResponse<V>> ChangeStatus<P, V>([Body] P processPayload);

        [Post("/{entity}/{command}")]
        Task<ApiResponse<V>> ProcessRequest<P, V>(string entity, string command, [Body] P processPayload);
        [Get("/{entity}")]
        Task<ApiResponse<V>> GetMasterView<V>(string entity);


        [Post("/{entity}/{command}/")]
        Task<ApiResponse<V>> ForgotPassword<P, V>(string entity, string command, [Body] P processPayload);

        [Post("/MemberBeneficiary/create")]
        Task<ApiResponse<V>> AddNewBeneficiary<P, V>([Body] P processPayload);

        [Get("/MemberProfile")]
        Task<ApiResponse<V>> GetMemberProfiles<V>();

        [Get("/MemberBeneficiary")]
        Task<ApiResponse<V>> GetMemberBeneficiaries<V>();

        [Post("/MemberProfile/update/")]
        Task<ApiResponse<V>> UpdateMemberProfile<P, V>([Body] P processPayload);

        [Get("/MemberProfileMasterView")]
        Task<ApiResponse<V>> GetMemberProfileViewResult<V>();

        [Get("/Bank")]
        Task<ApiResponse<V>> GetBanks<V>();

        [Post("/MemberNextOfKin/create/")]
        Task<ApiResponse<V>> AddNewNextOfKin<P, V>([Body] P processPayload);

        [Get("/MemberNextOfKinMasterView")]
        Task<ApiResponse<V>> GetMemberNextOfKinMasterView<V>();

        [Get("/AuditTrailMasterView")]
        Task<ApiResponse<V>> GetAuditTrailMasterView<V>();


        [Post("/MemberNextOfKin/delete/")]
        Task<ApiResponse<V>> DeleteNextOfKin<P, V>([Body] P processPayload);

        [Post("/MemberBeneficiary/delete/")]
        Task<ApiResponse<V>> DeleteBeneficiary<P, V>([Body] P processPayload);

        [Get("/{entity}?filter=applicationUserId eq '{payload}'")]
        Task<ApiResponse<V>> GetValue<V>(string entity, string payload);

        [Get("/{entity}?filter={column} eq '{payload}'")]
        Task<ApiResponse<V>> GetApprovedValue<V>(string entity, string column, string payload);

        [Get("/{entity}")]
        Task<ApiResponse<V>> GetAllValue<V>(string entity);

        [Get("/{entity}?filter={filterValue} eq '{payload}' &orderby eq '{orderby}' {asc_desc} & count eq {count}")]
        Task<ApiResponse<V>> GetValueWithOrderAndCount<V>(string entity, string filterValue, string payload, string orderby, int count, string asc_desc);

        [Get("/{entity}?filter={filterValue} eq '{payload}'")]
        Task<ApiResponse<V>> GetValue<V>(string entity, string filterValue, string payload);

        [Get("/{entity}?filter={filterValue1} eq '{payload1}' and {filterValue2} eq '{payload2}'")]
        Task<ApiResponse<V>> GetValue<V>(string entity, string filterValue1, string payload1, string filterValue2, string payload2);

        [Get("/{entity}?filter={filter1} eq '{payload1}' and {filter2} eq '{payload2}' and {filter3} eq '{payload3}'")]
        Task<ApiResponse<V>> GetValue<V>(string entity, string filter1, string payload1, string filter2, string payload2, string filter3, string payload3);

        [Post("/MemberNextOfKin/{command}/")]
        Task<ApiResponse<V>> NextOfKin<P, V>(string command, [Body] P processPayload);
        [Get("/{entity}?filter=profileId eq '{payload}'")]
        Task<ApiResponse<V>> GetNextOfKinValue<V>(string entity, string payload);
        [Post("/MemberBeneficiary/{command}/")]
        Task<ApiResponse<V>> Beneficiary<P, V>(string command, [Body] P processPayload);
        [Get("/{entity}?filter=profileId eq '{payload}'")]
        Task<ApiResponse<V>> GetBeneficiaryValue<V>(string entity, string payload);
        [Get("/odata/{entity}?filter={filterParameter} eq '{payload}' and isDeleted eq false")]
        Task<ApiResponse<V>> GetCustomerBankAccountValue<V>(string entity, string filterParameter, string payload);
        [Get("/{entity}?filter=locationType eq '{payload}'")]
        Task<ApiResponse<V>> Location<V>(string entity, string payload);
        [Get("/{entity}?filter=locationType eq 'STATE' and parentId eq '{payload}'")]
        Task<ApiResponse<V>> State<V>(string entity, string payload);
        [Get("/{entity}?filter=locationType eq 'STATE' and header_Name eq '{payload}'")]
        Task<ApiResponse<V>> StateByCountry<V>(string entity, string payload);
        [Get("/{entity}")]
        Task<ApiResponse<V>> Dropdown<V>(string entity);
        [Get("/{entity}/{command}")]
        Task<ApiResponse<V>> GetRecord<V>(string entity, string command);
        [Post("/{entity}/handle")]
        Task<ApiResponse<V>> Approval<C, V>(string entity, [Body] C createPayload);


        [Post("/AuditTrail/log/")]
        Task<ApiResponse<V>> CreateAuditTrail<P, V>([Body] P processPayload);
        [Post("/DepositProduct/approve/")]
        Task<ApiResponse<V>> ApproveDepositProduct<P, V>([Body] P processPayload);

        [Post("/ApprovalWorkflows/create/")]
        Task<ApiResponse<V>> CreateApprovalWorkflow<P, V>([Body] P processPayload);
        [Post("/ApprovalWorkflows/update/")]
        Task<ApiResponse<V>> UpdateApprovalWorkflow<P, V>([Body] P processPayload);
        [Post("/ApprovalWorkflows/delete/")]
        Task<ApiResponse<V>> DeleteApprovalWorkflow<P, V>([Body] P processPayload);
        [Post("/ApprovalGroup/create/")]
        Task<ApiResponse<V>> CreateApprovalGroup<P, V>([Body] P processPayload);
        [Post("/ApprovalGroup/update/")]
        Task<ApiResponse<V>> UpdateApprovalGroup<P, V>([Body] P processPayload);
        [Post("/ApprovalGroup/view/")]
        Task<ApiResponse<V>> ViewApprovalGroup<P, V>([Body] P processPayload);
        [Post("/ApprovalGroup/createOrUpdateGroupMember/")]
        Task<ApiResponse<V>> CreateOrUpdateGroupMember<P, V>([Body] P processPayload);
        [Post("/ApprovalGroup/delete/")]
        Task<ApiResponse<V>> DeleteApprovalGroupMember<P, V>([Body] P processPayload);

        [Get("/{command}/{productId}")]
        Task<ApiResponse<V>> GetProduct<V>(string command, string productId);
        [Get("/ApprovalGroup/GetApprovalGroupById/{id}")]
        Task<ApiResponse<V>> GetApprovalGroupById<V>(string command, string id);

        [Post("/Approval/getByEntityId/")]
        Task<ApiResponse<V>> GetApprovalByEntityId<P, V>([Body] P processPayload);

        [Post("/ApprovalWorkflows/getApprovalWorkflowById/")]
        Task<ApiResponse<V>> GetApprovalWorkflowById<P, V>([Body] P processPayload);
        [Post("/Approval/process/")]
        Task<ApiResponse<V>> ProcessApproval<P, V>([Body] P processPayload);

        [Post("/ApprovalNotifications/create/")]
        Task<ApiResponse<V>> CreateApprovalNotifications<P, V>([Body] P processPayload);

        [Get("/DepartmentMasterView")]
        Task<ApiResponse<V>> GetDepartmentMasterView<V>();
        [Get("/ApprovalGroupMasterView")]
        Task<ApiResponse<V>> GetApprovalGroupMasterView<V>();
        [Get("/{entity}?filter=MembershipId eq '{payload}'")]
        Task<ApiResponse<V>> GetMemberProfileValue<V>(string entity, string payload);
        [Post("/LoanApplicationGuarantor/{MembershipId}/verify")]
        Task<ApiResponse<V>> VerifyGuarantor<V, P>(string MembershipId, [Body] P payload);
        [Get("/LoanApplicationGuarantor/{key}/details")]
        Task<ApiResponse<V>> GetLoanApplication<V>(string key);
        [Post("/{entity}/{command}/")]
        Task<ApiResponse<V>> PostCommand<P, V>(string entity, string command, [Body] P processPayload);

        [Get("/odata/{entity}?filter={filterParameter} eq '{payload}'")]
        Task<ApiResponse<V>> GetOdataRecord<V>(string entity, string filterParameter, string payload);

        #region Select only column that you need  ======================================================================

        [Get("/{entity}?filter={filterValue} eq '{payload}' &$select={columns}")]
        Task<ApiResponse<V>> GetCustomValues<V>(string entity, string filterValue, string payload, string columns);

        [Get("/{entity}?$filter={filterValue} eq {payload} & $select={columns}")]
        Task<ApiResponse<V>> GetCustomValuesWithBoolean<V>(string entity, string filterValue, string payload, string columns);

        [Get("/{entity}?$filter={filterValue} eq '{payload}' and {filter2} eq {filter2Value} & $select={columns}")]
        Task<ApiResponse<V>> Get2CustomValuesWithBoolean<V>(string entity, string filterValue, string payload,string filter2, string filter2Value, string columns);

        [Get("/{entity}?filter={filterValue} eq '{payload}' and {filter2} eq '{filter2Value}' &$select={columns}")]
        Task<ApiResponse<V>> GetCustom2FiltersValues<V>(string entity, string filterValue, string payload, string filter2, string filter2Value, string columns);
        
        [Get("/{entity}?&$select={columns}")]
        Task<ApiResponse<V>> GetCustomValuesEntityOnly<V>(string entity, string columns);

        [Get("/{entity}")]
        Task<ApiResponse<V>> GetCustomValuesEntityAllFields<V>(string entity);

        [Get("/{entity}?$filter={filterValue} eq '{payload}'")]
        Task<ApiResponse<V>> GetCustomViewEntityWithFilterAndAllFields<V>(string entity, string filterValue, string payload);

        #endregion


        #region Aggregation Endpoints ===================================================================================

        [Get("/odata/{entity}?$Apply=filter({accountType} eq '{accountType_Value}' and {approvalstatus} eq '{approvalstatusValue}')/aggregate({fieldToSum} with sum as TotalAmount)")]
        Task<ApiResponse<V>> GetSummation<V>(string entity, string accountType, string accountType_Value, string fieldToSum, string approvalstatus, string approvalstatusValue);

        [Get("/odata/{entity}?$Apply=filter({approvalstatus} eq '{approvalstatusValue}')/aggregate({fieldToSum} with sum as TotalAmount)")]
        Task<ApiResponse<V>> GetAllSummation<V>(string entity, string fieldToSum, string approvalstatus, string approvalstatusValue);

        //For Total Rows

        [Get("/odata/{entity}?$filter={FieldType} eq '{FieldTypeValue}' and {approvalstatus} eq '{approvalstatusValue}' & $select = {fieldToSelect} & count=true")]
        Task<object> GetTotalRowsByType(string entity, string FieldType, string FieldTypeValue, string fieldToSelect, string approvalstatus, string approvalstatusValue);

        [Get("/odata/{entity}?$filter={approvalstatus} eq '{approvalstatusValue}' & $select = {fieldToSelect} & count=true")]
        Task<object> GetTotalRowsByApprovalStatus(string entity, string fieldToSelect, string approvalstatus, string approvalstatusValue);

        #endregion
        [Get("/{entity}/{payload}")]
        Task<ApiResponse<V>> GetApprovalRecord<V>(string entity, string payload);
        [Get("/LoanProduct/{ProductId}/{CustomerId}/customerloanproductstatus")]
        Task<ApiResponse<V>> CustomerLoanEligibility<V>(string ProductId, string CustomerId);
        [Get("/{entity}?$filter={filterValue} eq true")]
        Task<ApiResponse<V>> GetCustomViewEntityWithFilterAndAllFields<V>(string entity, string filterValue);
    }
}
