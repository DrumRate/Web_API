using CRUD.DTO;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUD.Validators
{
    public class TankValidator : AbstractValidator<Tank>
    {
        public TankValidator()
        {
            RuleFor(prop => prop.Name).NotEmpty().WithMessage("Пустое название")
                                  .Length(1, 450).WithMessage("Недопустимое количество символов");
            RuleFor(prop => prop.Volume).LessThan(prop => prop.MaxVolume);
        }
    }
}
