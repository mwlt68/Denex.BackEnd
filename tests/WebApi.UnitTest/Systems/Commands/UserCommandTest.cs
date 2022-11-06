using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Denex.Application.Dto;
using Denex.Application.Features.Commands.Users.UserInsert;
using Denex.Application.Interfaces.Service;
using Denex.Application.Repository;
using Denex.Application.Wrappers;
using Denex.Domain.Entities;
using Moq;
using WebApi.UnitTest.Fixtures.UserFixtures;
using Xunit;

namespace WebApi.UnitTest.Systems.Commands
{
    public class UserCommandTest
    {
        private readonly Mock<IMapper> mapper;
        private readonly Mock<IJwtService> jwtService;

        public UserCommandTest()
        {
            mapper = new Mock<IMapper>();
            
            jwtService = new Mock<IJwtService>();
            jwtService.Setup(x=> x.CreateToken(It.IsAny<string>()))
                .Returns(UserFixture.Token);
        }


        [Theory]
        [InlineData("Mevlut")]
        [InlineData("Gur")]
        public async Task UserInsertCommand_ValidObjectPassed_ReturnsAuthenticationItemAsync(string username)
        {
            // Arrange 

            var expectedUser = UserFixture.GetUser(username);
            var query = new UserInsertCommand(){Username=username};

            Mock<IUserRepository> userRepository = new Mock<IUserRepository>();
            userRepository.Setup(x=> x.AddAsync(It.IsAny<User>()))
                .ReturnsAsync(()=>{
                    
                    var expected = UserFixture.GetUser(username);
                    return new User(){
                        Id = expected.Id,
                        CreatedAt = expected.CreatedAt,
                        Username = username,
                    };
                });

            var userAuthHanler = new UserInsertCommand.UserInsertCommandHandler(mapper.Object,jwtService.Object,userRepository.Object);

            // Act

            var authResult = await userAuthHanler.Handle(query,It.IsAny<CancellationToken>());
            // Assert

            Assert.IsType<ServiceResponse<UserAuthenticationDto>>(authResult);
            Assert.Null(authResult.Message);
            Assert.True(authResult.Success);
            Assert.NotNull(authResult.Value);
            Assert.Equal(authResult.Value!.Id,expectedUser.Id);
            Assert.Equal(authResult.Value.Token,UserFixture.Token);
            Assert.Equal(authResult.Value.Username,expectedUser.Username);
        }
    }
}