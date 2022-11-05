using System;
using System.Threading;
using AutoMapper;
using Denex.Application.Features.Commands.PracticeSchemas.PracticeSchemaInsert;
using Denex.Application.Features.Commands.PracticeSchemas.PracticeSchemaUpdate;
using Denex.Application.Interfaces.Repository;
using Denex.Application.Wrappers;
using Denex.Domain.Entities;
using Moq;
using WebApi.UnitTest.Fixtures.PracticeSchemaFixtures;
using Xunit;
using static Denex.Application.Features.Commands.PracticeSchemas.PracticeSchemaInsert.PracticeSchemaInsertCommand;
using static Denex.Application.Features.Commands.PracticeSchemas.PracticeSchemaUpdate.PracticeSchemaUpdateCommand;

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

        [Theory]
        [InlineData("507f1f77bcf86cd799439014","schema",120,null)]
        [InlineData("507f1f77bcf86cd799439014","schema",120,null)]
        [InlineData("507f1f77bcf86cd799439014","schema",120,4)]
        public async void PracticeSchemaUpdateCommand_ValidObjectPassed_ReturnsResponseHasItem(string schemaId,string schemaName,int? schemaNetCalculationRate,int? schemaDuration)
        {
            // Arrange
            
            var expected = PracticeSchemaFixture.Get(schemaId,schemaName,schemaNetCalculationRate,schemaDuration);
            var query = new PracticeSchemaUpdateCommand(schemaId,schemaName,schemaNetCalculationRate,schemaDuration,DateTime.Now);

            var repository = new Mock<IPracticeSchemaRepository>();
            repository.Setup(x => x.UpdateAsync(It.IsAny<PracticeSchema>()))
                .ReturnsAsync((PracticeSchema schema) => PracticeSchemaFixture.Get(schema.Id,schema.Name,schema.NetCalculationRate,schema.Duration));

            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<PracticeSchemaUpdateCommand, PracticeSchema>();
            });
            var customAutoMapper = config.CreateMapper();

            
            // This setup is for the schema to be updated.
            repository.Setup(x => x.GetByIdAsync(It.IsAny<string>()))
                .ReturnsAsync((string id) => PracticeSchemaFixture.Get(id,"Test Name",3,150));
            
            var handler = new PracticeSchemaUpdateCommandHandler(customAutoMapper,repository.Object);

            // Act

            var updateResult =await handler.Handle(query,It.IsAny<CancellationToken>());
            
            
            // Assert

            Assert.IsType<ServiceResponse<PracticeSchema>>(updateResult);
            
            Assert.NotNull(updateResult?.Value);
            Assert.Null(updateResult!.Message);
            Assert.True(updateResult.Success);

            var schemaModel = updateResult.Value!;
            Assert.Equal(schemaModel.Name,expected.Name);
            Assert.Equal(schemaModel.Duration,expected.Duration);
            Assert.Equal(schemaModel.NetCalculationRate,expected.NetCalculationRate);
        }
    }
}