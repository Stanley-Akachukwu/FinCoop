using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AP.ChevronCoop.Entities.Documents.OfficeSheets
{
    public partial class OfficeSheetMasterView
    {

        public long? RowNumber { get; set; } 
        public string Id { get; set; } 
        public string DocumentNo { get; set; } 
        public string DocumentTypeId { get; set; } 
        public string Name { get; set; } 
        public byte[] DocumentData { get; set; } 
        public string? MimeType { get; set; } 
        public string? FilePath { get; set; } 
        public string? Description { get; set; } 
        public bool IsActive { get; set; } 
        public string? CreatedByUserId { get; set; } 
        public DateTimeOffset? DateCreated { get; set; } 
        public string? UpdatedByUserId { get; set; } 
        public DateTimeOffset? DateUpdated { get; set; } 
        public string? DeletedByUserId { get; set; } 
        public bool IsDeleted { get; set; } 
        public DateTimeOffset? DateDeleted { get; set; } 
        public Guid RowVersion { get; set; } 
        public string? FullText { get; set; } 
        public string? Tags { get; set; } 
        public string? Caption { get; set; } 
        public string? DocumentTypeId_Name { get; set; } 
        public bool? DocumentTypeId_SystemFlag { get; set; } 
        public bool? DocumentTypeId_IsActive { get; set; } 
        public string? DocumentTypeId_CreatedByUserId { get; set; } 
        public string? DocumentTypeId_UpdatedByUserId { get; set; } 
        public string? DocumentTypeId_DeletedByUserId { get; set; } 
        public bool? DocumentTypeId_IsDeleted { get; set; } 
        public string? DocumentTypeId_Tags { get; set; } 
        public string? DocumentTypeId_Caption { get; set; } 
    }
}