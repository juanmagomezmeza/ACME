using ACME.SchoolManagement.Core.Application.Use_cases.RegisterStudent;
using ACME.SchoolManagement.Core.Domain.Entities;
using AutoMapper;

namespace ACME.SchoolManagement.Core.Application.Mappings
{
    public class RegisterStudentMappings : Profile
    {
        public RegisterStudentMappings()
        {
            CreateMap<RegisterStudentCommand, Student>()
                .ForMember(r => r.Name, m => m.MapFrom(d => d.Name))
                .ForMember(r => r.Age, m => m.MapFrom(d => d.Age));
        }
    }
}
