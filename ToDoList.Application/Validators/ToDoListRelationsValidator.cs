using FluentValidation;
using ToDoList.Application.DTO;

namespace ToDoList.Application.Validators;

public class ToDoListRelationsValidator : AbstractValidator<ToDoListRelationsDTO>
{
    public ToDoListRelationsValidator()
    {

        RuleFor(relationsDTO => relationsDTO.UserId).NotEmpty()
                                                    .WithMessage("UserId is required");

        RuleFor(relationsDTO => relationsDTO.ToDoListId).NotEmpty()
                                                        .WithMessage("ToDoListId is required");
    }
}
