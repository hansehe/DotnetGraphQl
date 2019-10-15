using System.Linq;
using System.Threading.Tasks;
using DotnetGraphQl.Abstractions;
using DotnetGraphQl.Contracts;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using TestUtilities.DotnetGraphQl;
using Xunit;

namespace Test.DotnetGraphQl.Tests
{
    public class PersonalInfoHandlerTests
    {
        [Fact]
        public async Task GetPersonalInfos()
        {
            var serviceProvider = TestTools.GetServiceProvider();
            var personalInfoHandler = serviceProvider.GetService<IPersonalInfoHandler>();

            var @params = new PersonalInfoSearchParams();
            var personalInfos = await personalInfoHandler.GetPersonalInfos(@params);
            personalInfos.Count().Should().BeGreaterThan(0);
        }
    }
}
