using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

namespace DotnetGraphQl.Config
{
    public static class ConfigBuilderExtensions
    {
        public static IConfiguration GetConfiguration(Dictionary<string, string> overrideConfig = null)
        {
            var configuration = GetConfigurationBuilder(overrideConfig)
                .Build();
            return configuration;
        }
        
        public static IConfigurationBuilder GetConfigurationBuilder(Dictionary<string, string> overrideConfig = null)
        {
            return new ConfigurationBuilder()
                .AddConfiguration(overrideConfig);
        }
        
        public static IConfiguration GetDefaultConfiguration(Dictionary<string, string> overrideConfig = null)
        {
            return new ConfigurationBuilder()
                .AddJsonFile(BaseConfig.DefaultConfigFilename)
                .AddJsonFileIfTrue(BaseConfig.DefaultConfigDockerFilename, () => BaseConfig.InContainer)
                .AddEnvironmentVariables()
                .AddInMemoryIfTrue(overrideConfig, () => overrideConfig != null)
                .Build();
        }
        
        public static IConfigurationBuilder AddConfiguration(
            this IConfigurationBuilder configurationBuilder, 
            Dictionary<string, string> overrideConfig = null)
        {
            var defaultConfiguration = GetDefaultConfiguration(overrideConfig);

            return configurationBuilder
                .AddConfiguration(defaultConfiguration);
        }

        private static IConfigurationBuilder AddJsonFileIfTrue(this IConfigurationBuilder configurationBuilder, string filename,
            Func<bool> func)
        {
            if (func())
            {
                configurationBuilder.AddJsonFile(filename);
            }

            return configurationBuilder;
        }
        
        private static IConfigurationBuilder AddInMemoryIfTrue(this IConfigurationBuilder configurationBuilder, Dictionary<string, string> overrideConfig,
            Func<bool> func)
        {
            if (func())
            {
                configurationBuilder.AddInMemoryCollection(overrideConfig);
            }

            return configurationBuilder;
        }
    }
}