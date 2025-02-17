using FluentValidation;
using ToDoList.Application.DTO;

namespace ToDoList.Application.Validators;

internal class DeleteToDoListValidator : AbstractValidator<DeleteToDoListDTO>
{
    public DeleteToDoListValidator()
    {
        RuleFor(deleteDTO => deleteDTO.ToDoListId).NotEmpty()
                                                  .WithMessage("ToDoListId is required");

        RuleFor(deleteDTO => deleteDTO.UserId).NotEmpty()
                                              .WithMessage("UserId is required");
    }
}
