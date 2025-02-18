using FluentValidation;
using ToDoList.Application.DTO;

namespace ToDoList.Application.Validators;

public class UnshareToDoListValidator : AbstractValidator<UnshareToDoListDTO>
{
    public UnshareToDoListValidator()
    {
        RuleFor(unshareDTO => unshareDTO.UserId).NotEmpty()
                                                .WithMessage("UserId is required");

        RuleFor(unshareDTO => unshareDTO.SharedUserId).NotEmpty()
                                                      .WithMessage("SharedUserId is required");

        RuleFor(unshareDTO => unshareDTO.SharedUserId).NotEqual(shareDTO => shareDTO.UserId)
                                                      .WithMessage("SharedUserId can`t be equal to UserId");

        RuleFor(unshareDTO => unshareDTO.ToDoListId).NotEmpty()
                                                    .WithMessage("ToDoListId is required");
    }
}
