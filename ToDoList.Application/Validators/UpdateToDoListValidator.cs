using FluentValidation;
using ToDoList.Application.DTO;

namespace ToDoList.Application.Validators;

internal class UpdateToDoListValidator : AbstractValidator<UpdateToDoListDTO>
{
    public UpdateToDoListValidator()
    {
        RuleFor(updateDTO => updateDTO.Id).NotEmpty()
                                          .WithMessage("Id is required");


        RuleFor(updateDTO => updateDTO.UserId).NotEmpty()
                                              .WithMessage("UserId is required");

        RuleFor(updateDTO => updateDTO.Name).NotEmpty()
                                            .MinimumLength(1)
                                            .MaximumLength(255)
                                            .WithMessage("Name is required. Length - [1, 255].");
    }
}
