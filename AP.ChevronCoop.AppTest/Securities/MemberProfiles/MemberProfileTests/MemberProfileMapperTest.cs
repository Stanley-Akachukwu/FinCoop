using System.Runtime.Serialization;
using AP.ChevronCoop.AppCore.Security.MemberProfiles.MemberProfiles;
using AP.ChevronCoop.AppDomain.Security.MemberProfiles.MemberProfiles;
using AP.ChevronCoop.Entities.Security.MemberProfiles.MemberProfiles;
using AutoMapper;

namespace AP.ChevronCoop.AppTest.Securities.MemberProfiles.MemberProfileTests;

public class MemberProfileMapperTest
{
  private readonly IConfigurationProvider _configuration;
  private readonly IMapper _mapper;

  public MemberProfileMapperTest()
  {
    _configuration = new MapperConfiguration(config => 
      config.AddProfile<MemberProfileMapperProfile>());

    _mapper = _configuration.CreateMapper();
  }

  // [Fact]
  // public void ShouldHaveValidConfiguration()
  // {
  //   _configuration.AssertConfigurationIsValid();
  // }
  //
  // [Theory]
  // [InlineData(typeof(MemberProfile), typeof(MemberProfileViewModel))]
  // [InlineData(typeof(MemberProfile), typeof(MemberProfileMasterView))]
  // public void ShouldSupportMappingFromSourceToDestination(Type source, Type destination)
  // {
  //   var instance = GetInstanceOf(source);
  //
  //   _mapper.Map(instance, source, destination);
  // }

  private object GetInstanceOf(Type type)
  {
    if (type.GetConstructor(Type.EmptyTypes) != null)
      return Activator.CreateInstance(type)!;

    // Type without parameterless constructor
    return FormatterServices.GetUninitializedObject(type);
  }
}