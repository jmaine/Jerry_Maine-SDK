using Jerry.Maine.SDK;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System.Threading.Tasks;
using Xunit;
using System.Linq;

namespace TestProject1
{
    public class UnitTest1
    {
        private const string KEY = "KihVBhou568jWF4uyFhk";

        public UnitTest1()
        {
            
            
        }

        [Fact]
        public async Task Test()
        {
            LotR service = GetSystemUnderTest();
            var books = await service.Books();
            Assert.NotNull(books);
            Assert.True(books.Docs.All(x => !string.IsNullOrEmpty(x.Id)));
            Assert.True(books.Docs.All(x => !string.IsNullOrEmpty(x.Name)));

        }

        private static LotR GetSystemUnderTest()
        {
            var mock = new Mock<IApiKey>();
            mock.SetupGet(x => x.Key).Returns(KEY);

            var services = new Microsoft.Extensions.DependencyInjection.ServiceCollection();
            services.AddHttpClient();
            services.AddSingleton<LotR>();
            services.AddSingleton(mock.Object);
            var provider = services.BuildServiceProvider();
            var service = provider.GetService<LotR>()!;
            return service;
        }
    }
}