using Denex.Application.Dto;
using Denex.Application.Exceptions;
using Denex.Application.Interfaces.Service;
using Denex.Application.Repository;
using Denex.Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Denex.Application.Features.Queries.UserAuthentication
{
    public class UserAuthenticationQuery: IRequest<ServiceResponse<UserAuthenticationDto>>
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public class UserAuthenticationQueryHandler : IRequestHandler<UserAuthenticationQuery, ServiceResponse<UserAuthenticationDto>>
        {
            private readonly IUserRepository userRepoistory;
            private readonly IJwtService jwtService;
            public UserAuthenticationQueryHandler(IJwtService jwtService,IUserRepository userRepository)
            {
                this.jwtService = jwtService;
                this.userRepoistory = userRepository;
            }
            public async Task<ServiceResponse<UserAuthenticationDto>> Handle(UserAuthenticationQuery request, CancellationToken cancellationToken)
            {
                var  user = await userRepoistory.GetAsync(x => x.Username == request.Username && x.Password == request.Password);
                if (user != null)
                {
                    var token = jwtService.CreateToken(user.Id);
                    var authenticateDto = new UserAuthenticationDto()
                    {
                        Id = user.Id,
                        Username = user.Username,
                        Token = token
                    };
                    return new ServiceResponse<UserAuthenticationDto>(authenticateDto);
                }
                else throw new UserNotFoundException("Kullanıcı adı yada parola hatalı !");
            }
        }
    }
}
