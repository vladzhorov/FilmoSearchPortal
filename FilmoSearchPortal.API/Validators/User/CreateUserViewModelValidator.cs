﻿using FilmoSearchPortal.API.ViewModels.User;
using FluentValidation;

namespace FilmoSearchPortal.API.Validators.User
{
    public class CreateUserViewModelValidator : AbstractValidator<CreateUserViewModel>
    {
        public CreateUserViewModelValidator()
        {
            RuleFor(user => user.Username).NotEmpty().MaximumLength(50);

        }
    }
}
