using Denex.Application.Dto;
using Denex.Application.Wrappers;

namespace WebApi.UnitTest.Fixtures.UserFixtures
{
    public static class UserAuthenticationDtoFixture
    {
        public static ServiceResponse<UserAuthenticationDto> GetTestModel =>
            new ServiceResponse<UserAuthenticationDto>(
                new UserAuthenticationDto()
                {
                    Id = "507f1f77bcf86cd799439011",
                    Token = "MyToken",
                    Username = "Mevlut"
        });
        
    }
}