﻿namespace AP.ChevronCoop.Entities.Accounting.TransactionDocuments;

public class TransactionDocumentMasterView
{
    public long? RowNumber { get; set; }
    public string Id { get; set; }
    public string DocumentNo { get; set; }
    public string TransactionJournalId { get; set; }
    public string DocumentTypeId { get; set; }
    public string Name { get; set; }
    public byte[] Document { get; set; }
    public string? DocumentUrl { get; set; }
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
    public string? TransactionJournalId_TransactionNo { get; set; }
    public string? TransactionJournalId_Title { get; set; }
    public string? TransactionJournalId_TransactionType { get; set; }
    public string? TransactionJournalId_DocumentRef { get; set; }
    public string? TransactionJournalId_DocumentRefId { get; set; }
    public string? TransactionJournalId_PostingRef { get; set; }
    public string? TransactionJournalId_PostingRefId { get; set; }
    public string? TransactionJournalId_EntityRef { get; set; }
    public string? TransactionJournalId_EntityRefId { get; set; }
    public DateTime? TransactionJournalId_TransactionDate { get; set; }
    public bool? TransactionJournalId_IsPosted { get; set; }
    public string? TransactionJournalId_PostedByUserId { get; set; }
    public DateTime? TransactionJournalId_DatePosted { get; set; }
    public bool? TransactionJournalId_IsReversed { get; set; }
    public string? TransactionJournalId_ReversedByUserId { get; set; }
    public DateTime? TransactionJournalId_ReversalDate { get; set; }
    public string? TransactionJournalId_ApprovalId { get; set; }
    public DateTime? TransactionJournalId_ApprovalDate { get; set; }
    public string? TransactionJournalId_Memo { get; set; }
    public bool? TransactionJournalId_IsActive { get; set; }
    public string? TransactionJournalId_CreatedByUserId { get; set; }
    public string? TransactionJournalId_UpdatedByUserId { get; set; }
    public string? TransactionJournalId_DeletedByUserId { get; set; }
    public bool? TransactionJournalId_IsDeleted { get; set; }
    public string? TransactionJournalId_Tags { get; set; }
    public string? TransactionJournalId_Caption { get; set; }
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