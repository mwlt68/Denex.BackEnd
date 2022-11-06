using Denex.Application.Dto;
using Denex.Domain.Entities;

namespace WebApi.UnitTest.Fixtures.UserFixtures
{
    public class UserFixture
    {
        public static User GetUser(string username) => new User(){
            Id="507f1f77bcf86cd799439011",
            Username = username,
            CreatedAt = System.DateTime.Now
        };

        public static string Token => "MyToken";
    }
}