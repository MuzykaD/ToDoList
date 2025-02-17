using FluentValidation;
using ToDoList.Application.DTO;

namespace ToDoList.Application.Validators;

public class ToDoListPaginationValidator : AbstractValidator<ToDoListPaginationDTO>
{
    public ToDoListPaginationValidator()
    {
        RuleFor(paginationDTO => paginationDTO.UserId).NotEmpty()
                                                      .WithMessage("UserId is required");

        RuleFor(paginationDTO => paginationDTO.CurrentPage).GreaterThanOrEqualTo(1)
                                                           .WithMessage("CurrentPage value must be >= 1");

        RuleFor(paginationDTO => paginationDTO.PageSize).GreaterThanOrEqualTo(1)
                                                        .WithMessage("PageSize value must be >= 1");;
    }
}
