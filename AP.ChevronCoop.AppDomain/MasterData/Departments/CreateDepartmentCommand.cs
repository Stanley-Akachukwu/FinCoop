using AP.ChevronCoop.Commons;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace AP.ChevronCoop.AppDomain.MasterData.Departments
{
    public partial class CreateDepartmentCommand : CreateCommand, IRequest<CommandResult<DepartmentViewModel>>
    {

        [MaxLength(256)]
        [Required]
        public string Name { get; set; }

       
    }







}
