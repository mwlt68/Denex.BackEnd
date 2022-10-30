using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Denex.Application.Features.Commands.PracticeSchemas.PracticeSchemaDelete;
using Denex.Application.Features.Commands.PracticeSchemas.PracticeSchemaInsert;
using Denex.Application.Features.Commands.PracticeSchemas.PracticeSchemaUpdate;
using Denex.Application.Features.Queries.PracticeSchemaList;
using Denex.Application.Wrappers;
using Denex.Domain.Entities;
using Denex.WebApi.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebApi.UnitTest.Fixtures.PracticeSchemaFixtures;
using Xunit;

namespace WebApi.UnitTest.Systems.Controllers
{
    public class PracticeSchemasControllerTest
    {
        [Fact]
        public async void GetAll_ReturnsOkResponseHasAllItems()
        {
            // Arrange

            var mediator = new Mock<IMediator>();
            var expected = PracticeSchemaFixture.GetAll();

            mediator.Setup(mdtr=> mdtr.Send(It.IsAny<PracticeSchemaListQuery>(),It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ServiceResponse<List<PracticeSchema>>(PracticeSchemaFixture.GetAll()));
            
            var practiceSchemaController = new PracticeSchemasController(mediator.Object);
            

            // Act

            var getAllResult = await practiceSchemaController.AllAsync();

            // Assert

            Assert.IsType<OkObjectResult>(getAllResult.Result);
            var result = getAllResult.Result as OkObjectResult;

            Assert.IsType<ServiceResponse<List<PracticeSchema>>>(result?.Value);

            var serviceResponse = result?.Value as ServiceResponse<List<PracticeSchema>>;

            Assert.NotNull(serviceResponse?.Value);
            Assert.Null(serviceResponse?.Message);
            Assert.True(serviceResponse?.Success);
            var idList = serviceResponse!.Value!.Select(x=>x.Id);
            foreach (var item in expected)
            {
                Assert.Contains(item.Id,idList);
            }
        }

        [Theory]
        [InlineData("Schema",null,null)]
        [InlineData("Schema",120,null)]
        [InlineData("Schema",120,4)]
        public async void Insert_ValidObjectPassed_ReturnsResponseHasCreatedItemAsync(string schemaName,int? schemaNetCalculationRate,int? schemaDuration)
        {
            //Arrange

            var expected = PracticeSchemaFixture.Get(schemaName,schemaNetCalculationRate,schemaDuration);
            
            var mediator = new Mock<IMediator>();
            mediator.Setup(mdtr=> mdtr.Send(It.IsAny<PracticeSchemaInsertCommand>(),It.IsAny<CancellationToken>()))
                .ReturnsAsync((PracticeSchemaInsertCommand insertCommand,CancellationToken token) =>{
                    var schema = PracticeSchemaFixture.Get(insertCommand.Name,insertCommand.NetCalculationRate,insertCommand.Duration);
                    return new ServiceResponse<PracticeSchema>(schema);
                });
            
            var controller = new PracticeSchemasController(mediator.Object);
            var insertCommand = new PracticeSchemaInsertCommand(schemaName,DateTime.MaxValue,schemaNetCalculationRate,schemaDuration);
            
            // Act
            
            var insertResult =await controller.InsertAsync(insertCommand);
            
            // Assert

            Assert.IsType<CreatedResult>(insertResult.Result);

            var result = insertResult.Result as CreatedResult;

            Assert.IsType<ServiceResponse<PracticeSchema>>(result?.Value);
            
            var serviceResponse = result?.Value as ServiceResponse<PracticeSchema>;
            Assert.NotNull(serviceResponse?.Value);
            Assert.Null(serviceResponse!.Message);
            Assert.True(serviceResponse.Success);

            var schemaModel = serviceResponse.Value!;
            Assert.Equal(schemaModel.Name,expected.Name);
            Assert.Equal(schemaModel.Duration,expected.Duration);
            Assert.Equal(schemaModel.NetCalculationRate,expected.NetCalculationRate);

        }

        [Theory]
        [InlineData("507f1f77bcf86cd799439014","schema",null,null)]
        [InlineData("507f1f77bcf86cd799439014","schema",120,null)]
        [InlineData("507f1f77bcf86cd799439014","schema",120,4)]
        public async void Update_ValidObjectPassed_ReturnsReposneHasItem(string schemaId,string schemaName,int? schemaNetCalculationRate,int? schemaDuration)
        {
            // Arrange

            var mediator = new Mock<IMediator>();
            var expected = PracticeSchemaFixture.Get(schemaId,schemaName,schemaNetCalculationRate,schemaDuration);
            mediator.Setup(mdtr=> mdtr.Send(It.IsAny<PracticeSchemaUpdateCommand>(),It.IsAny<CancellationToken>()))
                .ReturnsAsync((PracticeSchemaUpdateCommand updateCommand,CancellationToken token) =>{
                    var schema = PracticeSchemaFixture.Get(updateCommand.Id,updateCommand.Name,updateCommand.NetCalculationRate,updateCommand.Duration);
                    return new ServiceResponse<PracticeSchema>(schema);
                });

            var controller = new PracticeSchemasController(mediator.Object);
            var updateCommand =  new PracticeSchemaUpdateCommand(schemaId,schemaName,schemaNetCalculationRate,schemaDuration,DateTime.MaxValue);
            
            // Act
            
            var updateResult = await controller.UpdateAsync(updateCommand);

            // Assert

            Assert.IsType<OkObjectResult>(updateResult.Result);

            var result = updateResult.Result as OkObjectResult;

            Assert.IsType<ServiceResponse<PracticeSchema>>(result?.Value);
            
            var serviceResponse = result?.Value as ServiceResponse<PracticeSchema>;

            Assert.NotNull(serviceResponse?.Value);
            Assert.Null(serviceResponse!.Message);
            Assert.True(serviceResponse.Success);

            var schemaModel = serviceResponse.Value!;
            Assert.Equal(schemaModel.Name,expected.Name);
            Assert.Equal(schemaModel.Duration,expected.Duration);
            Assert.Equal(schemaModel.NetCalculationRate,expected.NetCalculationRate);
        }

        [Theory]
        [InlineData("507f1f77bcf86cd799439014")]
        public async void Delete_ValidObjectPassed_ReturnsReposneHasItem(string schemaId)
        {
            // Arrange

            var expected = PracticeSchemaFixture.Get(schemaId);

            var mediator = new Mock<IMediator>();
            mediator.Setup(mdtr=> mdtr.Send(It.IsAny<PracticeSchemaDeleteCommand>(),It.IsAny<CancellationToken>()))
                .ReturnsAsync((PracticeSchemaDeleteCommand deleteCommand,CancellationToken token) =>{
                    var schema = PracticeSchemaFixture.Get(deleteCommand.Id);
                    return new ServiceResponse<PracticeSchema>(schema);
                });

            var controller = new PracticeSchemasController(mediator.Object);
            
            // Act
            
            var deleteResult = await controller.DeleteAsync(schemaId);

            // Assert

            Assert.IsType<OkObjectResult>(deleteResult.Result);

            var result = deleteResult.Result as OkObjectResult;

            Assert.IsType<ServiceResponse<PracticeSchema>>(result?.Value);
            
            var serviceResponse = result?.Value as ServiceResponse<PracticeSchema>;

            Assert.NotNull(serviceResponse?.Value);
            Assert.Null(serviceResponse!.Message);
            Assert.True(serviceResponse.Success);
            Assert.Equal(serviceResponse.Value!.Id,expected.Id);
        }

    }
}