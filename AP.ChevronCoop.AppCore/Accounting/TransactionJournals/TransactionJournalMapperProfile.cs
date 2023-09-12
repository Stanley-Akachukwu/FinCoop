using AP.ChevronCoop.AppDomain.Accounting.TransactionJournals;
using AP.ChevronCoop.Entities.Accounting.TransactionJournals;
using AutoMapper;

namespace AP.ChevronCoop.AppCore.Accounting.TransactionJournals
{
    public class TransactionJournalMapperProfile : Profile
    {

        public TransactionJournalMapperProfile()
        {

            CreateMap<TransactionJournal, TransactionJournalViewModel>().ReverseMap();
            CreateMap<TransactionJournal, CreateTransactionJournalCommand>().ReverseMap();
            CreateMap<TransactionJournal, UpdateTransactionJournalCommand>().ReverseMap();
            CreateMap<TransactionJournal, TransactionJournalMasterView>().ReverseMap();
            CreateMap<TransactionJournalViewModel, TransactionJournalMasterView>().ReverseMap();
            CreateMap<CreateTransactionJournalCommand, TransactionJournalMasterView>().ReverseMap();
            CreateMap<UpdateTransactionJournalCommand, TransactionJournalMasterView>().ReverseMap();




        }
    }


}