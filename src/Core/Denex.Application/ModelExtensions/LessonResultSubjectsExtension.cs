using Denex.Application.Exceptions;
using Denex.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Denex.Domain.EntityExtensions
{
    public static class LessonResultSubjectsExtension
    {
        // If there is an error, return it. Else return null

        public static string? Compare(this LessonResult lessonResult, LessonSchema lessonSchema)
        {
            if (lessonResult.Subjects == null)
                return null;
            else if (lessonSchema != null)
            {
                bool idCompare = lessonSchema.Id == lessonResult.LessonId;
                if (idCompare)
                {
                    foreach (var subject in lessonResult.Subjects)
                    {
                        bool subjectContain = lessonSchema.Subjects.Contains(subject);
                        if (!subjectContain)
                        {
                            throw new SubjectNotFoundException( $"Subject ({subject}) not found in subjects of lesson schema !");
                        }
                    }
                }
                else throw new LessonSchemaNotFound("Lesson not equal to schema !");
            }
            else throw new LessonSchemaNotFound();
            return null;
        }
    }
}
