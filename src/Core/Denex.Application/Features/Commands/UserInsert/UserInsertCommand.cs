using AutoMapper;
using Denex.Application.Dto;
using Denex.Application.Interfaces.Service;
using Denex.Application.Repository;
using Denex.Application.Wrappers;
using Denex.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Denex.Application.Features.Commands.UserInsert
{
    public class UserInsertCommand :IRequest<ServiceResponse<UserAuthenticationDto>>
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public class UserInsertCommandHandler : IRequestHandler<UserInsertCommand, ServiceResponse<UserAuthenticationDto>>
        {
            private readonly IUserRepository userRepository;
            private readonly IMapper mapper;
            private readonly IJwtService jwtService;
            public UserInsertCommandHandler(IMapper mapper, IJwtService jwtService, IUserRepository userRepository)
            {
                this.mapper = mapper;
                this.jwtService = jwtService;
                this.userRepository = userRepository;
            }

            public async Task<ServiceResponse<UserAuthenticationDto>> Handle(UserInsertCommand request, CancellationToken cancellationToken)
            {
                // TODO is username unique control add
                var user = mapper.Map<User>(request);
                var addedUser = await userRepository.AddAsync(user);
                if (addedUser != null)
                {
                    var token = jwtService.CreateToken(addedUser.Id);
                    var authenticateDto = new UserAuthenticationDto()
                    {
                        Id = addedUser.Id,
                        Username = addedUser.Username,
                        Token = token
                    };
                    return new ServiceResponse<UserAuthenticationDto>(authenticateDto);
                }
                else return new ServiceResponse<UserAuthenticationDto>(false, "Kullanıcı ekleme sırasında hata meydana geldi !");
            }
        }

    }
}
