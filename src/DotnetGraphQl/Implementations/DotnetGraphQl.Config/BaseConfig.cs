using System;
using System.Runtime.InteropServices;

namespace DotnetGraphQl.Config
{
    public static class BaseConfig
    {
        private const string DataFolder = "Data";
        private const string ConfigFolder = "DefaultConfigs";
        
        public static readonly string GetAllDataFilename = $"{DataFolder}/GetAll.json";
        public static readonly string DefaultConfigFilename = $"{ConfigFolder}/DefaultConfig.json";
        public static readonly string DefaultConfigDockerFilename = $"{ConfigFolder}/DefaultConfig.Docker.json";
        
        public static bool InContainer => 
            Environment.GetEnvironmentVariable("RUNNING_IN_CONTAINER") == "true";
    }
}