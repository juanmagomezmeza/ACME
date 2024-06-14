using ACME.SchoolManagement.Core.Application.Entities;
using ACME.SchoolManagement.Core.Application.Use_cases.RegisterStudent;
using AutoMapper;

namespace ACME.SchoolManagement.Core.Application.Mappings
{
    public class RegisterStudentMappings : Profile
    {
        public RegisterStudentMappings()
        {
            CreateMap<RegisterStudentCommand, Student>()
                .ForMember(r => r.Name, m => m.MapFrom(d => d.Name))
                .ForMember(r => r.Age, m => m.MapFrom(d => d.Age))
                .ForMember(r => r.StudentID, m => m.MapFrom(_ => Guid.NewGuid()));
        }
    }
}
