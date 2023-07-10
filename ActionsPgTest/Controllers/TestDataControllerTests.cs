using ActionsPg;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ActionsPg.Controllers;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using System.Net.Http.Json;

namespace TestProject1.Controllers
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class TestDataControllerTests : TestBase
    {
        private TestDataController testDataController;

        [TestInitialize]
        public override async Task InitializeAsync()
        {
            var context = fixture.CreateContext();

            this.testDataController = new TestDataController(context);

            await base.InitializeAsync();
        }

        [TestMethod]
        public async Task GetShouldReturnTestData()
        {
            var response = await this.testDataController.GetAsync();
            var result = (OkObjectResult)response.Result;
            var values = result.Value as IEnumerable<TestData>;

            Assert.IsTrue(values.Count() == 2);
            Assert.IsTrue(values.Any(v => v.Name == "TestData1"));
        }

    }
}
