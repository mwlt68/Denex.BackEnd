using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using Denex.Application.Features.Queries.PracticeSchemaList;
using Denex.Application.Interfaces.Repository;
using Denex.Application.Wrappers;
using Denex.Domain.Entities;
using Moq;
using WebApi.UnitTest.Fixtures.PracticeSchemaFixtures;
using Xunit;
using static Denex.Application.Features.Queries.PracticeSchemaList.PracticeSchemaListQuery;

namespace WebApi.UnitTest.Systems.Queries
{
    public class PracticeSchemaQueriesTest
    {

        [Fact]
        public async void PracticeSchemaListQuery_ReturnsAllItems()
        {
            // Arrange

            var repository = new Mock<IPracticeSchemaRepository>();
            var expected = PracticeSchemaFixture.GetAll();

            repository.Setup(repo=> repo.GetListAsync(It.IsAny<Expression<Func<PracticeSchema, bool>>>()))
                .ReturnsAsync(PracticeSchemaFixture.GetAll());
            
            var handler = new PracticeSchemaListQueryHandler(repository.Object);
            

            // Act

            var getAllResult = await handler.Handle(new PracticeSchemaListQuery(),It.IsAny<CancellationToken>());

            // Assert


            Assert.IsType<ServiceResponse<List<PracticeSchema>>>(getAllResult);

            Assert.NotNull(getAllResult?.Value);
            Assert.Null(getAllResult?.Message);
            Assert.True(getAllResult?.Success);
            var idList = getAllResult!.Value!.Select(x=>x.Id);
            foreach (var item in expected)
            {
                Assert.Contains(item.Id,idList);
            }
        }
        
    }
}