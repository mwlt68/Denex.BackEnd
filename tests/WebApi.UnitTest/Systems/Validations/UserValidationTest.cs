using Denex.Application.Features.Commands.Users.UserInsert;
using Xunit;

namespace WebApi.UnitTest.Systems.Validations
{
    public class UserValidationTest
    {
        private UserInsertCommandValidator Validator {get;}
        public UserValidationTest()
        {
          Validator = new UserInsertCommandValidator();
        }

        [Theory]
        [InlineData("","")]
        [InlineData("username","password")]
        [InlineData("usr","Password1")]
        [InlineData("username","Password")]
        public void UserInsertCommandValidator_InvalidObjectPassed_ReturnsInvalid(string username,string password)
        {
          var insertModel = new UserInsertCommand(){Username= username,Password = password};
          bool validationResult = Validator.Validate(insertModel).IsValid;
          Assert.False(validationResult);
        }

        [Theory]
        [InlineData("username","Password1")]
        public void UserInsertCommandValidator_ValidObjectPassed_ReturnsValid(string username,string password)
        {
          var insertModel = new UserInsertCommand(){Username= username,Password = password};
          bool validationResult = Validator.Validate(insertModel).IsValid;
          Assert.True(validationResult);
        }
    }
}