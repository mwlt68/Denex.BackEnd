﻿using AutoMapper;
using Denex.Application.Dto;
using Denex.Application.Features.Commands.PracticeResults.PracticeResultInsert;
using Denex.Application.Features.Commands.PracticeResults.PracticeResultUpdate;
using Denex.Application.Features.Commands.PracticeSchemas.PracticeSchemaInsert;
using Denex.Application.Features.Commands.PracticeSchemas.PracticeSchemaLessonInsert;
using Denex.Application.Features.Commands.PracticeSchemas.PracticeSchemaUpdate;
using Denex.Application.Features.Commands.Users.UserInsert;
using Denex.Domain.Entities;

namespace Denex.Application.Mapping
{
    internal class GeneralMapping:Profile
    {
        public GeneralMapping()
        {
            CreateMap<UserInsertCommand, User>();
            CreateMap<LessonSchemaInsertDto, LessonSchema>();
            CreateMap<LessonSchemaUpdateDto, LessonSchema>();
            CreateMap<PracticeSchemaInsertCommand, PracticeSchema>().ForMember(dest=> dest.Lessons, opt=> opt.MapFrom(x=> x.Lessons));
            CreateMap<PracticeSchemaUpdateCommand, PracticeSchema>();
            CreateMap<PracticeResultUpdateCommand, PracticeResult>();
            CreateMap<PracticeSchemaLessonInsertCommand, LessonSchema>();
            CreateMap<PracticeResultInsertCommand, PracticeResult>();
            CreateMap<PracticeResult, PracticeResultDetailDto>();
        }
    }
}
