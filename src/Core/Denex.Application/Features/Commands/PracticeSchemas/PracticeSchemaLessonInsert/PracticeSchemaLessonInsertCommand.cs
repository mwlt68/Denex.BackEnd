﻿using AutoMapper;
using Denex.Application.Interfaces.Repository;
using Denex.Application.Wrappers;
using Denex.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Denex.Application.Features.Commands.PracticeSchemas.PracticeSchemaLessonInsert
{
    public class PracticeSchemaLessonInsertCommand: IRequest<ServiceResponse<LessonSchema>>
    {
        public string PracticeSchemaId { get; set; }
        public string Name { get; set; }
        public int QuestionCount { get; set; }
        public List<String> Subjects { get; set; }

        public class PracticeSchemaLessonInsertCommandHandler : IRequestHandler<PracticeSchemaLessonInsertCommand, ServiceResponse<LessonSchema>>
        {
            private readonly IMapper mapper;
            private readonly IPracticeSchemaRepository practiceSchemaRep;
            public PracticeSchemaLessonInsertCommandHandler(IMapper mapper, IPracticeSchemaRepository practiceSchemaRep)
            {
                this.mapper = mapper;
                this.practiceSchemaRep = practiceSchemaRep;
            }
            public async Task<ServiceResponse<LessonSchema>> Handle(PracticeSchemaLessonInsertCommand request, CancellationToken cancellationToken)
            {
                var practiceSchema = await practiceSchemaRep.GetByIdAsync(request.PracticeSchemaId);
                if (practiceSchema != null)
                {
                    var lessonSchema = mapper.Map<LessonSchema>(request);
                    if (practiceSchema.Lessons == null)
                    {
                        practiceSchema.Lessons = new List<LessonSchema>();
                    }
                    practiceSchema.Lessons.Add(lessonSchema);
                    var practiceSchemaUpdated = await practiceSchemaRep.UpdateAsync(practiceSchema);
                    if (practiceSchemaUpdated != null)
                    {
                        var lesson = practiceSchemaUpdated.Lessons.FirstOrDefault(l => l.Id == lessonSchema.Id);
                        return new ServiceResponse<LessonSchema>(lesson);
                    }
                    return new ServiceResponse<LessonSchema>(false, "Hata meydana geldi !");
                }
                return new ServiceResponse<LessonSchema>(false, "Deneme tipi bulunamadı !");
            }
        }
    }
}
