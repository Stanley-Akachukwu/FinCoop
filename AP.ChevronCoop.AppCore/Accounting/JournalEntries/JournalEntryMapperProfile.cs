using AP.ChevronCoop.AppDomain.Accounting.JournalEntries;
using AP.ChevronCoop.Entities.Accounting.JournalEntries;
using AutoMapper;

namespace AP.ChevronCoop.AppCore.Accounting.JournalEntries
{
    public class JournalEntryMapperProfile : Profile
    {
        public JournalEntryMapperProfile()
        {
            CreateMap<JournalEntry, JournalEntryViewModel>().ReverseMap();
            CreateMap<JournalEntry, CreateJournalEntryCommand>().ReverseMap();
            CreateMap<JournalEntry, UpdateJournalEntryCommand>().ReverseMap();
            CreateMap<JournalEntry, JournalEntryMasterView>().ReverseMap();
            CreateMap<JournalEntryViewModel, JournalEntryMasterView>().ReverseMap();
            CreateMap<CreateJournalEntryCommand, JournalEntryMasterView>().ReverseMap();
            CreateMap<UpdateJournalEntryCommand, JournalEntryMasterView>().ReverseMap();
        }
    }
}
