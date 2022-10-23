using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Denex.Application.Dto;
using Denex.Application.Exceptions;
using Denex.Application.Features.Queries.UserAuthentication;
using Denex.Application.Interfaces.Service;
using Denex.Application.Repository;
using Denex.Application.Wrappers;
using Denex.Domain.Entities;
using Moq;
using WebApi.UnitTest.Fixtures.UserFixtures;
using Xunit;

namespace WebApi.UnitTest.Systems.Queries
{
    public class UserQueriesTest
    {

        private readonly Mock<IMapper> mapper;
        private readonly Mock<IJwtService> jwtService;

        public UserQueriesTest()
        {
            mapper = new Mock<IMapper>();

            jwtService = new Mock<IJwtService>();
            jwtService.Setup(x => x.CreateToken(It.IsAny<string>()))
                .Returns(UserFixture.Token);
        }


        [Theory]
        [InlineData("Mevlut")]
        [InlineData("Gur")]
        public async Task UserAuthenticationQuery_ValidObjectPassed_ReturnsAuthenticationItemAsync(string username)
        {
            // Arrange 

            var expectedUser = UserFixture.GetUser(username);
            var query = new UserAuthenticationQuery() { Username = username };

            Mock<IUserRepository> userRepository = new Mock<IUserRepository>();
            userRepository.Setup(x => x.GetAsync(It.IsAny<Expression<System.Func<Denex.Domain.Entities.User, bool>>>()))
                .ReturnsAsync(() =>
                {
                    return new User()
                    {
                        Id = expectedUser.Id,
                        CreatedAt = expectedUser.CreatedAt,
                        Username = username,
                    };
                });

            var userAuthHanler = new UserAuthenticationQuery.UserAuthenticationQueryHandler(jwtService.Object, userRepository.Object);

            // Act

            var authResult = await userAuthHanler.Handle(query, It.IsAny<CancellationToken>());
            // Assert

            Assert.IsType<ServiceResponse<UserAuthenticationDto>>(authResult);
            Assert.Null(authResult.Message);
            Assert.True(authResult.Success);
            Assert.NotNull(authResult.Value);
            Assert.Equal(authResult.Value!.Id, expectedUser.Id);
            Assert.Equal(authResult.Value.Token, UserFixture.Token);
            Assert.Equal(authResult.Value.Username, expectedUser.Username);
        }

        [Fact]
        public async Task UserAuthenticationQuery_InvalidUserPassed_ThrownUserNotFoundExceptionAsync()
        {
            // Arrange 


            Mock<IUserRepository> userRepository = new Mock<IUserRepository>();
            userRepository.Setup(x => x.GetAsync(It.IsAny<Expression<System.Func<Denex.Domain.Entities.User, bool>>>()))
                .ReturnsAsync(() => null);

            var userAuthHanler = new UserAuthenticationQuery.UserAuthenticationQueryHandler(jwtService.Object, userRepository.Object);

            // Act

            Func<Task> testCode = async () =>
            {
                await userAuthHanler.Handle(It.IsAny<UserAuthenticationQuery>(), It.IsAny<CancellationToken>());
            };

            // Assert

            await Assert.ThrowsAnyAsync<UserNotFoundException>(testCode);

        }
    }
}