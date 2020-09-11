using CRUD.DTO;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUD.Validators
{
    public class UnitValidator : AbstractValidator<Unit>
    {
       public UnitValidator()
        {
            RuleFor(prop => prop.Name).NotEmpty().WithMessage("Пустое назавние")
                                      .Length(1, 450).WithMessage("Недопустимое количество символов");
        }
    }
}
