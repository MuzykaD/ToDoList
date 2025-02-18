using FluentValidation;
using ToDoList.Application.DTO;

namespace ToDoList.Application.Validators;

public class ShareToDoListValidator : AbstractValidator<ShareToDoListDTO>
{
    public ShareToDoListValidator()
    {
        RuleFor(shareDTO => shareDTO.UserId).NotEmpty()
                                            .WithMessage("UserId is required"); ;

        RuleFor(shareDTO => shareDTO.SharedUserId).NotEmpty()
                                                  .NotEqual(shareDTO => shareDTO.UserId)
                                                  .WithMessage("SharedUserId is required");

        RuleFor(shareDTO => shareDTO.SharedUserId).NotEqual(shareDTO => shareDTO.UserId)
                                                  .WithMessage("SharedUserId can`t be equal to UserId");

        RuleFor(shareDTO => shareDTO.ToDoListId).NotEmpty()
                                                .WithMessage("ToDoListId is required"); ;
    }
}
