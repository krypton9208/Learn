using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentValidation;
using FluentValidation.Attributes;

namespace Learn.Models
{
    [Validator(typeof(EmployeeValidator))]
    public class Employee
    {
        public virtual int Id { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
    }

    public class EmployeeValidator : AbstractValidator<Employee>
    {
        public EmployeeValidator()
        {
            RuleFor(emp => emp.FirstName).NotEmpty().WithMessage("This field can't be blank").Length(3, 250).WithMessage("Plese specify a first name.");
            RuleFor(emp => emp.LastName).NotEmpty().WithMessage("This field can't be blank").Length(3, 250).WithMessage("Plese specify a last name.");
        }
    }
}