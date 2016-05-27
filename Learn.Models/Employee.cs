using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentValidation;
using FluentValidation.Attributes;
using System.Web.Mvc;

namespace Learn.Models
{
    [Validator(typeof(EmployeeValidator))]
    public class Employee
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Solution { get; set; }
        public virtual string Category { get; set; }
    }

    public class EmployeeValidator : AbstractValidator<Employee>
    {
        public EmployeeValidator()
        {
            RuleFor(emp => emp.Name).NotEmpty().WithMessage("This field can't be blank").Length(3, 250).WithMessage("Plese specify a first name.");
            RuleFor(emp => emp.Solution).NotEmpty().WithMessage("This field can't be blank").Length(1, 1000000000).WithMessage("Plese specify a last name.");
            RuleFor(emp => emp.Category).NotEmpty().WithMessage("This field can't be blank").Length(3, 250).WithMessage("Plese specify a category.");
        }
    }

    public class EmployeeViewModel
    {
        public ICollection<Employee> Model { get; set; }
        public ICollection<SelectListItem> Categories { get; set; }
        public EmployeeViewModel()
        {
            Model = new HashSet<Employee>();
            Categories = new HashSet<SelectListItem>();
        }
    }
}