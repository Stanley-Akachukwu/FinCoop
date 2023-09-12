using AP.ChevronCoop.Entities.Accounting.TransactionJournals;
using AP.ChevronCoop.Entities.Documents.DocumentTypes;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AP.ChevronCoop.Entities.Accounting.TransactionDocuments
{
    public class TransactionDocument : BaseEntity<string>
    {

        public TransactionDocument()
        {
            Id = NUlid.Ulid.NewUlid().ToString();
        }


        public string DocumentNo { get; set; }

        public string TransactionJournalId { get; set; }

        public virtual TransactionJournal TransactionJournal { get; set; }

        public string DocumentTypeId { get; set; }
        
        public virtual DocumentType DocumentType { get; set; }


        public string Name { get; set; }


        public byte[] Document { get; set; }
        public string DocumentUrl { get; set; }

        public override string DisplayCaption
        {
            get
            {
                return "";
            }
        }

        public override string DropdownCaption
        {
            get
            {
                return "";
            }
        }

        public override string ShortCaption
        {
            get
            {
                return "";
            }
        }

    }
}