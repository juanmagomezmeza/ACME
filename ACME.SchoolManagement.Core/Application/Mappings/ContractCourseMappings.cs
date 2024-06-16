using ACME.SchoolManagement.Core.Application.Use_cases.ContractCourse;
using ACME.SchoolManagement.Core.Entities;
using AutoMapper;

namespace ACME.SchoolManagement.Core.Application.Mappings
{
    public class ContractCourseMappings : Profile
    {
        public ContractCourseMappings()
        {
            CreateMap<ContractCourseCommand, Enrollment>()
                .ForMember(r => r.CourseID, m => m.MapFrom(d => d.CourseID))
                .ForMember(r => r.StudentID, m => m.MapFrom(d => d.StudentID));
        }
    }
}
