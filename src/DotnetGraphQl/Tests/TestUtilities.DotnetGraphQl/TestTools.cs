using System;
using DotnetGraphQl;
using DotnetGraphQl.Config;
using Microsoft.Extensions.DependencyInjection;

namespace TestUtilities.DotnetGraphQl
{
    public static class TestTools
    {
        public static IServiceCollection GetServiceCollection()
        {
            var serviceCollection = new ServiceCollection();
            var configuration = ConfigBuilderExtensions.GetConfiguration();
            return serviceCollection
                .RegisterService(configuration);
        }
        
        public static IServiceProvider GetServiceProvider()
        {
            return GetServiceCollection().BuildServiceProvider();
        }
    }
}