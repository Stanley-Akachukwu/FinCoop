using AP.ChevronCoop.AppDomain.Security.MemberProfiles.MemberBulkUploads;
using AP.ChevronCoop.Entities.Security.MemberProfiles.MemberBulkUploads;
using AutoMapper;

namespace AP.ChevronCoop.AppCore.Security.MemberProfiles.MemberBulkUploads
{

    public class MemberDateUploadMappingProfile : Profile
    {
        public MemberDateUploadMappingProfile()
        {
            CreateMap<MemberBulkUploadTemp, MemberDataUpload>().ReverseMap();

        }

    }
}
