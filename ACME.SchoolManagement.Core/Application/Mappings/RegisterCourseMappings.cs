﻿using ACME.SchoolManagement.Core.Application.Entities;
using ACME.SchoolManagement.Core.Application.Use_cases.RegisterCourse;
using AutoMapper;

namespace ACME.SchoolManagement.Core.Application.Mappings
{
    public class RegisterCourseMappings : Profile
    {
        public RegisterCourseMappings()
        {
            CreateMap<RegisterCourseCommand, Course>()
                .ForMember(r => r.EndDate, m => m.MapFrom(d => d.EndDate))
                .ForMember(r => r.StartDate, m => m.MapFrom(d => d.StartDate))
                .ForMember(r => r.RegistrationFee, m => m.MapFrom(d => d.RegistrationFee))
                .ForMember(r => r.Name, m => m.MapFrom(d => d.Name))
                .ForMember(r => r.CourseID, m => m.MapFrom(_ => Guid.NewGuid()));
        }
    }
}
