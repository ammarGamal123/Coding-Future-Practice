using SchoolProject.Core.Features.Students.Queries.Responses;
using SchoolProject.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Core.Mapping.StudentMapper
{
    public partial class StudentProfile
    {
        public void GetStudentByIDMapping()
        {

            CreateMap<Student, GetStudentByIDResponse>()
                    .ForMember(dest => dest.DepartmentName,
                               opt => opt.MapFrom(src => src.Department.Name))
                    .ReverseMap();
        }
    }
}
