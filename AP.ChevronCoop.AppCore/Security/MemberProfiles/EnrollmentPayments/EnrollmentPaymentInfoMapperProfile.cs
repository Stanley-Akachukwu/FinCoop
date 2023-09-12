using AP.ChevronCoop.AppDomain.Security.MemberProfiles.EnrollmentPayments;
using AP.ChevronCoop.Entities.Security.MemberProfiles.EnrollmentPayments;
using AutoMapper;

namespace AP.ChevronCoop.AppCore.Security.MemberProfiles.EnrollmentPayments
{
    public class EnrollmentPaymentInfoMapperProfile : Profile
    {

        public EnrollmentPaymentInfoMapperProfile()
        {

            CreateMap<EnrollmentPaymentInfo, EnrollmentPaymentInfoViewModel>().ReverseMap();
            CreateMap<EnrollmentPaymentInfo, CreateEnrollmentPaymentInfoCommand>().ReverseMap();
            CreateMap<EnrollmentPaymentInfo, UpdateEnrollmentPaymentInfoCommand>().ReverseMap();
            CreateMap<EnrollmentPaymentInfo, EnrollmentPaymentInfoMasterView>().ReverseMap();
            CreateMap<EnrollmentPaymentInfoViewModel, EnrollmentPaymentInfoMasterView>().ReverseMap();
            CreateMap<CreateEnrollmentPaymentInfoCommand, EnrollmentPaymentInfoMasterView>().ReverseMap();
            CreateMap<UpdateEnrollmentPaymentInfoCommand, EnrollmentPaymentInfoMasterView>().ReverseMap();




        }
    }

}
