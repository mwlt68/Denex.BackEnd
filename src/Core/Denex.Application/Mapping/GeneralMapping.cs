using AutoMapper;
using Denex.Application.Features.Commands.UserInsert;
using Denex.Domain.Entities;

namespace Denex.Application.Mapping
{
    internal class GeneralMapping:Profile
    {
        public GeneralMapping()
        {
            CreateMap<UserInsertCommand, User>();
        }
    }
}
