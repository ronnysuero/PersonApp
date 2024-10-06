using FluentValidation.TestHelper;
using PersonApi.Application.Commands;
using PersonApi.Application.Validators;

namespace PersonApi.Test.Application.Validators;

public class UpdatePersonCommandValidatorTests
{
    private readonly UpdatePersonCommandValidator _validator = new();

    [Fact]
    public void Should_Have_Error_When_Id_Is_Less_Than_Or_Equal_To_Zero()
    {
        var command = new UpdatePersonCommand { Id = 0 };
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Id);
    }

    [Fact]
    public void Should_Have_Error_When_FirstName_Is_Empty()
    {
        var command = new UpdatePersonCommand { FirstName = string.Empty };
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.FirstName);
    }

    [Fact]
    public void Should_Have_Error_When_LastName_Is_Empty()
    {
        var command = new UpdatePersonCommand { LastName = string.Empty };
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.LastName);
    }

    [Fact]
    public void Should_Have_Error_When_DateOfBirth_Is_In_The_Future()
    {
        var command = new UpdatePersonCommand { DateOfBirth = DateTime.Now.AddDays(1) };
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.DateOfBirth);
    }

    [Fact]
    public void Should_Have_Error_When_Email_Is_Invalid()
    {
        var command = new UpdatePersonCommand { Email = "invalid-email" };
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Email);
    }

    [Fact]
    public void Should_Not_Have_Error_When_Command_Is_Valid()
    {
        var command = new UpdatePersonCommand
        {
            Id = 1,
            FirstName = "John",
            LastName = "Doe",
            DateOfBirth = DateTime.Now.AddYears(-30),
            Email = "john.doe@example.com"
        };
        var result = _validator.TestValidate(command);
        result.ShouldNotHaveAnyValidationErrors();
    }
}
