using FluentValidation;
using ToDoList.Application.DTO;

namespace ToDoList.Application.Validators;

internal class CreateToDoListValidator : AbstractValidator<CreateToDoListDTO>
{
    public CreateToDoListValidator()
    {
        RuleFor(createDTO => createDTO.UserId).NotEmpty()
                                              .WithMessage("UserId is required");

        RuleFor(createDTO => createDTO.Name).NotEmpty()
                                            .MinimumLength(1)
                                            .MaximumLength(255)
                                            .WithMessage("Name is required. Length - [1, 255].");
                             
    }
}
