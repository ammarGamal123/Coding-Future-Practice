﻿using MediatR;
using SchoolProject.Core.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Core.Features.Students.Commands.Models
{
    public class UpdateStudentCommand : IRequest<Response<string>>
    {
        public int StudID { get; set; }     

        public string Name { get;set; }

        public string Address { get; set; }

        public string? Phone { get; set; }

        public int DepartmentID { get; set; }
    }
}
