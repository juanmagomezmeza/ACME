using ACME.SchoolManagement.Core.Domain.Entities;
using ACME.SchoolManagement.Core.Domain.Models;
using AutoMapper;

namespace ACME.SchoolManagement.Core.Application.Mappings
{
    public class ListOfCoursesAndStudentsByDateMappings : Profile
    {
        public ListOfCoursesAndStudentsByDateMappings()
        {
            CreateMap<Enrollment, EnrollmentModel>()
                .ForMember(r => r.EndDate, m => m.MapFrom(d => d.Course.EndDate))
                .ForMember(r => r.StartDate, m => m.MapFrom(d => d.Course.StartDate))
                .ForMember(r => r.RegistrationFee, m => m.MapFrom(d => d.Course.RegistrationFee))
                .ForMember(r => r.CourseName, m => m.MapFrom(d => d.Course.Name))
                .ForMember(r => r.CourseID, m => m.MapFrom(d => d.Course.CourseID))
                .ForMember(r => r.StudentID, m => m.MapFrom(d => d.Student.StudentID))
                .ForMember(r => r.StudentName, m => m.MapFrom(d => d.Student.Name))
                .ForMember(r => r.Age, m => m.MapFrom(d => d.Student.Age));
        }
    }
}
