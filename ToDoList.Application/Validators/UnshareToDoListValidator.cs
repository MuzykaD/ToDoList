using FluentValidation;
using ToDoList.Application.DTO;

namespace ToDoList.Application.Validators;

internal class UnshareToDoListValidator : AbstractValidator<UnshareToDoListDTO>
{
    public UnshareToDoListValidator()
    {
        RuleFor(unshareDTO => unshareDTO.UserId).NotEmpty()
                                                .WithMessage("UserId is required");

        RuleFor(unshareDTO => unshareDTO.SharedUserId).NotEmpty()
                                                      .WithMessage("SharedUserId is required");

        RuleFor(unshareDTO => unshareDTO.ToDoListId).NotEmpty()
                                                    .WithMessage("ToDoListId is required");
    }
}
