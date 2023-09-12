using AP.ChevronCoop.Commons;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace AP.ChevronCoop.AppDomain.Documents.DocumentTypes
{
    public partial class CreateDocumentTypeCommand : CreateCommand, IRequest<CommandResult<DocumentTypeViewModel>>
    {

        [MaxLength(256)]
        [Required]
        public string Name { get; set; }

        [Required]
        public bool SystemFlag { get; set; }

        
    }







}
