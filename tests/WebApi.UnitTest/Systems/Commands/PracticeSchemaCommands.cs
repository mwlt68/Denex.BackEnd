using System;
using System.Threading;
using AutoMapper;
using Denex.Application.Features.Commands.PracticeSchemas.PracticeSchemaInsert;
using Denex.Application.Interfaces.Repository;
using Denex.Application.Wrappers;
using Denex.Domain.Entities;
using Moq;
using WebApi.UnitTest.Fixtures.PracticeSchemaFixtures;
using Xunit;
using static Denex.Application.Features.Commands.PracticeSchemas.PracticeSchemaInsert.PracticeSchemaInsertCommand;

namespace WebApi.UnitTest.Systems.Commands
{
    public class PracticeSchemaCommands
    {
        private readonly Mock<IMapper> mapper;
        public PracticeSchemaCommands()
        {
            mapper = new Mock<IMapper>();
        }

        [Theory]
        [InlineData("Schema",null,null)]
        [InlineData("Schema",120,null)]
        [InlineData("Schema",120,4)]
        public async void PracticeSchemaInsertCommand_ValidObjectPassed_ReturnsCreatedItemAsync(string schemaName,int? schemaNetCalculationRate,int? schemaDuration)
        {
            //Arrange

            var expected = PracticeSchemaFixture.Get(schemaName,schemaNetCalculationRate,schemaDuration);
            var query = new PracticeSchemaInsertCommand(schemaName,DateTime.MaxValue,schemaNetCalculationRate,schemaDuration);

            var repository = new Mock<IPracticeSchemaRepository>();
            repository.Setup(x => x.AddAsync(It.IsAny<PracticeSchema>()))
                .ReturnsAsync(() => PracticeSchemaFixture.Get(schemaName,schemaNetCalculationRate,schemaDuration));
            
            var handler = new PracticeSchemaInsertCommandHandler(mapper.Object,repository.Object);

            // Act

            var insertResult =await handler.Handle(query,It.IsAny<CancellationToken>());
            
            
            // Assert

            Assert.IsType<ServiceResponse<PracticeSchema>>(insertResult);
            
            Assert.NotNull(insertResult?.Value);
            Assert.Null(insertResult!.Message);
            Assert.True(insertResult.Success);

            var schemaModel = insertResult.Value!;
            Assert.Equal(schemaModel.Name,expected.Name);
            Assert.Equal(schemaModel.Duration,expected.Duration);
            Assert.Equal(schemaModel.NetCalculationRate,expected.NetCalculationRate);

        }
    }
}