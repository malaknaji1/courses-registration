using AutoMapper;
using courses_registration.DTO;
using courses_registration.Models;

namespace courses_registration.Helpers
{
    public class MappingProfiles:Profile
    {
        public MappingProfiles()
        {
            CreateMap<Course,CourseDTO>();
            CreateMap<CourseDTO, Course>();
            CreateMap<Student, StudentDTO>();
            CreateMap<StudentDTO, Student>();
            CreateMap<PrerequisiteDTO,Prerequisite>();
            CreateMap<Prerequisite, PrerequisiteDTO>();
            CreateMap<Enrollment, EnrollmentDTO>();
            CreateMap<EnrollmentDTO, Enrollment>();
            CreateMap<User, UserDTO>();
            CreateMap<UserDTO, User>();
            CreateMap<Lookup, LookupDTO>();
            CreateMap<LookupDTO, Lookup>();
        }
    }
}
