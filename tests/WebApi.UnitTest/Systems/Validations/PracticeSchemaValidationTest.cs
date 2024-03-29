using System;
using System.Collections.Generic;
using System.Linq;
using Denex.Application.Dto;
using Denex.Application.Features.Commands.PracticeSchemas.PracticeSchemaInsert;
using Denex.Application.Features.Commands.PracticeSchemas.PracticeSchemaLessonInsert;
using Denex.Application.Features.Commands.PracticeSchemas.PracticeSchemaUpdate;
using Xunit;

namespace WebApi.UnitTest.Systems.Validations
{
    public class PracticeSchemaValidationTest
    {
        [Theory]
        [InlineData("507f1f77bcf86cd799439014","schema1",null,null,"lesson1",1,"subject1")]
        [InlineData("507f1f77bcf86cd799439015","schema2",null,null,"lesson1",5,"subject1","subject2","subject3")]
        [InlineData("507f1f77bcf86cd799439016","schema3",120,null,"lesson2",7,"subject1")]
        [InlineData("507f1f77bcf86cd799439017","schema4",120,4,"lesson3",10,"subject1")]
        public void PracticeSchemaInsertCommandValidator_ValidObjectPassed_ReturnsValid(string schemaId,string schemaName,int? schemaNetCalculationRate,int? schemaDuration,string lessonName,int lessonQuestionCount,params string[] subjects)
        {
            // Arrange

            PracticeSchemaInsertValidation Validator = new PracticeSchemaInsertValidation();
            
            var lessons = new List<LessonSchemaInsertDto>()
            {
                new LessonSchemaInsertDto()
                {
                    Name = lessonName,
                    QuestionCount = lessonQuestionCount,
                    Subjects = subjects != null ? subjects.ToList(): null!
                }
            };
            var insertModel = new PracticeSchemaInsertCommand(schemaId,lessons,DateTime.Now,schemaNetCalculationRate,schemaDuration);

            // Act

            bool validationResult = Validator.Validate(insertModel).IsValid;

            // Assert   

            Assert.True(validationResult);
        }

        [Theory]
        [InlineData("507f1f77bcf86cd799439014","schema1",null,null)]
        [InlineData("507f1f77bcf86cd799439016","schema3",120,null)]
        [InlineData("507f1f77bcf86cd799439017","schema4",120,4)]
        public void PracticeSchemaUpdateCommandValidator_ValidObjectPassed_ReturnsValid(string schemaId,string schemaName,int? schemaNetCalculationRate,int? schemaDuration)
        {
            // Arrange

            PracticeSchemaUpdateValidaton Validator = new PracticeSchemaUpdateValidaton();
            
   
            var updateModel = new PracticeSchemaUpdateCommand(schemaId,schemaName,schemaNetCalculationRate,schemaDuration,DateTime.Now);

            // Act

            bool validationResult = Validator.Validate(updateModel).IsValid;

            // Assert   

            Assert.True(validationResult);
        }
           
        [Theory]
        [InlineData("507f1f77bcf86cd799439014","lesson1",1,"subject1")]
        [InlineData("507f1f77bcf86cd799439015","lesson1",5,"subject1","subject2","subject3")]
        [InlineData("507f1f77bcf86cd799439016","lesson2",7,"subject1")]
        [InlineData("507f1f77bcf86cd799439017","lesson3",10,"subject1")]
        public void PracticeSchemaLessonInsertValidator_ValidObjectPassed_ReturnsValid(string schemaId,string lessonName,int lessonQuestionCount,params string[] subjects)
        {
            // Arrange

            PracticeSchemaLessonInsertValidation Validator = new PracticeSchemaLessonInsertValidation();
            
            var insertModel = new PracticeSchemaLessonInsertCommand(schemaId,lessonName,lessonQuestionCount,subjects?.ToList());

            // Act

            bool validationResult = Validator.Validate(insertModel).IsValid;

            // Assert   

            Assert.True(validationResult);
        }
    }
}