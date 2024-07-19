// Ignore Spelling: Validator Vadlidatoins

using FluentValidation;
using SchoolProject.Core.Features.Students.Commands.Models;
using SchoolProject.Service.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Core.Features.Students.Commands.Vadlidatoins
{
    public class UpdateStudentValidator : AbstractValidator<UpdateStudentCommand>
    {
        #region Fields
        private readonly IStudentService _studentService;

        public UpdateStudentValidator(IStudentService studentService)
        {
            _studentService = studentService;
        }
        #endregion

        #region Constructors
        public UpdateStudentValidator()
        {
            ApplyValidationRules();
            ApplyCustomValidationRules();
        }
        #endregion

        #region Actions
        public void ApplyValidationRules()
        {
            RuleFor(s => s.Name)
                .NotEmpty().WithMessage("{PropertyName} Shouldn't be empty")
                .NotNull().WithMessage("{PropertyName} Shouldn't be null")
                .MaximumLength(100).WithMessage("{PropertyName} Max Length is 50");


             RuleFor(s => s.Address)
                .NotEmpty().WithMessage("{PropertyName} Shouldn't be empty")
                .NotNull().WithMessage("{PropertyName} Shouldn't be null")
                .MaximumLength(100).WithMessage("{PropertyName} Max Length is 50");
        }


        public async void ApplyCustomValidationRules()
        {
            RuleFor(s => s.Name)
                .MustAsync(async (Model , Key, CancellationToken) =>
                // Why "!" because if it name was found then it's true
                           !await _studentService.IsNameExistsExcludeSelf(Key , Model.StudID))
                .WithMessage("This Name is already Exists");
        } 
        #endregion

    }
}
