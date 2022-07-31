using Denex.Application.Exceptions;
using Denex.Domain.Entities;
using Denex.Domain.Exceptions;

namespace Denex.Domain.EntityExtensions
{
    public static class LessonResultExtension
    {
        // If there is an error, return it. Else return null
        // subjects set distinct of subject
        public static string? Compare(this List<LessonResult> LessonResults, List<LessonSchema> lessonSchemas)
        {
            if (lessonSchemas != null && lessonSchemas.Count > 0)
            {
                foreach (var lessonSchema in lessonSchemas)
                {
                    var lessonResult = LessonResults.FirstOrDefault(x => x.LessonId == lessonSchema.Id);
                    if (lessonResult != null)
                    {
                        int resultQuestionCount = lessonResult.TrueCount + lessonResult.FalseCount;
                        if (resultQuestionCount <= lessonSchema.QuestionCount)
                        {
                            string? lessonCompareRes = lessonResult.Compare(lessonSchema);
                            if (lessonCompareRes != null)
                            {
                                return lessonCompareRes;
                            }
                            else
                            {
                                lessonResult.Subjects = lessonResult.Subjects?.Distinct().ToList() ?? null;
                            }
                        }
                        else throw new LessonResultQuestionCountBadRequestException($"Expected number of questions {lessonSchema.QuestionCount}, but received {resultQuestionCount}"); 
                    }
                    else
                    {
                        throw new LessonResultNotFoundException($"Lesson not found in schema ({lessonSchema.Name}) !");
                    }
                }
                return null;
            }
            else
            {
                throw new LessonSchemaEmptyException();
            }

        }
    }
}
