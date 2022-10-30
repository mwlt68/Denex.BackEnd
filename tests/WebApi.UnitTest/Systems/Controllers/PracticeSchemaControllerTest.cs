using System.Collections.Generic;
using System.Threading;
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
    public class PracticeSchemaControllerTest
    {
        [Fact]
        public async void GetAll_ReturnsOkResponseHasAllItems()
        {
            // Arrange

            var mediator = new Mock<IMediator>();
            var expected = PracticeSchemaFixture.GetAll();

            mediator.Setup(mdtr=> mdtr.Send(It.IsAny<PracticeSchemaListQuery>(),It.IsAny<CancellationToken>()))
                .ReturnsAsync(expected);
            
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
            Assert.Equal(expected.Value!,serviceResponse!.Value!);
        }
    }
}