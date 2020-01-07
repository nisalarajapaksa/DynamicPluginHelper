using DynamicPluginHelper;
using SamplePluginInterfaces;
using System;
using System.Collections.Generic;

namespace SampleProgram
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Dynamicaly resolve Plugins by Interfaces");

            // Single Plugin DLL example function
            SampleCodeForRunPlugin();

            // Multiple Plugin DLL example function
            SampleCodeRunMultiplePlugin();

        }

        // Single Plugin DLL example
        private static void SampleCodeForRunPlugin() {
            var libLocation = @"H:\\GitProjects\\DynamicPluginHelper\\SamplePlugin1\\bin\\Debug\\netcoreapp3.1\\SamplePlugin1.dll";
            var pluginHelperBase = new PluginHelper<IPluginSample1>();
            IPluginSample1 command = pluginHelperBase.GetJobAction(libLocation);
            if (command != null)
            {
                command.SampleFunction();
            }
        }

        // Multiple Plugin DLL example
        private static void SampleCodeRunMultiplePlugin()
        {
            var libLocations = new[] { 
                "H:\\GitProjects\\DynamicPluginHelper\\SamplePlugin2\\bin\\Debug\\netcoreapp3.1\\SamplePlugin2.dll", 
                "SamplePlugin3\\bin\\Debug\\netcoreapp3.1\\SamplePlugin3.dll" 
            };
            var pluginHelperBase = new PluginHelper<IPluginSample2>();
            IEnumerable<IPluginSample2> commands = pluginHelperBase.GetJobActions(libLocations);
            if (commands != null)
            {
                foreach (var command in commands)
                {
                    command.SampleFunction();
                }
            }
        }
    }
}
