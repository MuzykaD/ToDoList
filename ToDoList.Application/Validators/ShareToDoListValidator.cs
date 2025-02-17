using FluentValidation;
using ToDoList.Application.DTO;

namespace ToDoList.Application.Validators;

internal class ShareToDoListValidator : AbstractValidator<ShareToDoListDTO>
{
    public ShareToDoListValidator()
    {
        RuleFor(shareDTO => shareDTO.UserId).NotEmpty()
                                            .WithMessage("UserId is required"); ;

        RuleFor(shareDTO => shareDTO.SharedUserId).NotEmpty()
                                                  .WithMessage("SharedUserId is required"); ;

        RuleFor(shareDTO => shareDTO.ToDoListId).NotEmpty()
                                                .WithMessage("ToDoListId is required"); ;
    }
}
