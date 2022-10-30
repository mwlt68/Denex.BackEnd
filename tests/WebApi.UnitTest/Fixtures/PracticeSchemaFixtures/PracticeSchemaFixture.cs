using System.Collections.Generic;
using Denex.Domain.Entities;

namespace WebApi.UnitTest.Fixtures.PracticeSchemaFixtures
{
    public static class PracticeSchemaFixture
    {
        public static List<PracticeSchema> GetAll(){
            List<PracticeSchema> schemas = new List<PracticeSchema>(){
                new PracticeSchema(){
                    Id="507f1f77bcf86cd799439012",
                },
                new PracticeSchema(){
                    Id="507f1f77bcf86cd799439013"
                },
            };
            return schemas;
        }

        public static PracticeSchema Get(string schemaName,int? schemaNetCalculationRate,int? schemaDuration) => new PracticeSchema(){
                Name = schemaName,
                Duration = schemaDuration,
                NetCalculationRate = schemaDuration
        };
        public static PracticeSchema Get(string schemaId,string schemaName,int? schemaNetCalculationRate,int? schemaDuration) => new PracticeSchema(){
                Name = schemaName,
                Duration = schemaDuration,
                NetCalculationRate = schemaDuration,
                Id = schemaId
        };
        public static PracticeSchema Get(string id) => new PracticeSchema(){
            Id = id,
            Name = "Test"
        };
    }
}