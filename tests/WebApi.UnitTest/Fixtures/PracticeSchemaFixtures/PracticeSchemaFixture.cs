using System.Collections.Generic;
using Denex.Application.Wrappers;
using Denex.Domain.Entities;

namespace WebApi.UnitTest.Fixtures.PracticeSchemaFixtures
{
    public class PracticeSchemaFixture
    {
        public static ServiceResponse<List<PracticeSchema>> GetAll(){
            List<PracticeSchema> schemas = new List<PracticeSchema>(){
                new PracticeSchema(){
                    Id="507f1f77bcf86cd799439012",
                },
                new PracticeSchema(){
                    Id="507f1f77bcf86cd799439013"
                },
            };
            return new ServiceResponse<List<PracticeSchema>>(schemas);
        }
    }
}