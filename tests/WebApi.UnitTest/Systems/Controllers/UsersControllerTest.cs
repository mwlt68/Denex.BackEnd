using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Denex.Application.Dto;
using Denex.Application.Features.Commands.Users.UserInsert;
using Denex.Application.Features.Queries.UserAuthentication;
using Denex.Application.Wrappers;
using Denex.WebApi.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebApi.UnitTest.Fixtures.UserFixtures;
using Xunit;

namespace WebApi.UnitTest.Systems.Controllers
{
    public class UsersControllerTest
    {

        [Fact]
        public async Task Insert_ValidObjectPassed_ReturnsCreatedItemAsync()
        {
            // Arrage

            var mediator = new Mock<IMediator>();
            var expected = UserAuthenticationDtoFixture.GetTestModel;

            mediator.Setup(mdtr => mdtr.Send(It.IsAny<UserInsertCommand>(),It.IsAny<CancellationToken>()))
                .ReturnsAsync(UserAuthenticationDtoFixture.GetTestModel);

            UsersController usersController = new UsersController(mediator.Object);

            // Act

            var insertResult = await usersController.InsertAsync( new UserInsertCommand());

            //Assert

            Assert.IsType<ObjectResult>(insertResult.Result);
            
            var result = (insertResult.Result as ObjectResult);
            Assert.Equal(result.StatusCode,(int)HttpStatusCode.Created);
            Assert.IsType<ServiceResponse<UserAuthenticationDto>>(result?.Value);

            ServiceResponse<UserAuthenticationDto>? serviceResponse = (result?.Value as ServiceResponse<UserAuthenticationDto>);
            var userDto = serviceResponse?.Value;

            Assert.Equal(expected?.Value?.Id,userDto?.Id);
            Assert.Equal(expected?.Value?.Token,userDto?.Token);
            Assert.Equal(expected?.Value?.Username,userDto?.Username);
        }

        [Fact]
        public async Task Authentication_ValidObjectPassed_ReturnedResponseHasTokenItem()
        {
            // Arrange

            var mediator = new Mock<IMediator>();
            var expected = UserAuthenticationDtoFixture.GetTestModel;

            mediator.Setup(mdtr=> mdtr.Send(It.IsAny<UserAuthenticationQuery>(),It.IsAny<CancellationToken>()))
                .ReturnsAsync(UserAuthenticationDtoFixture.GetTestModel);
            
            UsersController usersController = new UsersController(mediator.Object);

            // Act

            var actionResult = await usersController.AuthenticationAsync(It.IsAny<UserAuthenticationQuery>());

            //Assert

            Assert.IsType<OkObjectResult>(actionResult.Result);
            
            var result = (actionResult.Result as OkObjectResult);
            Assert.IsType<ServiceResponse<UserAuthenticationDto>>(result?.Value);

            ServiceResponse<UserAuthenticationDto>? serviceResponse = (result?.Value as ServiceResponse<UserAuthenticationDto>);
            var userDto = serviceResponse?.Value;

            Assert.Equal(expected?.Value?.Id,userDto?.Id);
            Assert.Equal(expected?.Value?.Token,userDto?.Token);
            Assert.Equal(expected?.Value?.Username,userDto?.Username);

        }

    }
}