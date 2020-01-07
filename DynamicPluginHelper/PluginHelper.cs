using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;

namespace DynamicPluginHelper
{
    public class PluginHelper<TIJobAction> where TIJobAction : class
    {
        public TIJobAction GetJobAction(string pluginPath)
        {
            Assembly pluginAssembly = LoadPlugin(pluginPath);
            TIJobAction command = CreateJobActions(pluginAssembly).First();
            return command;
        }

        public IEnumerable<TIJobAction> GetJobActions(string[] pluginPaths)
        {
            IEnumerable<TIJobAction> commands = pluginPaths.SelectMany(pluginPath =>
            {
                Assembly pluginAssembly = LoadPlugin(pluginPath);
                return CreateJobActions(pluginAssembly);
            }).ToList();
            return commands;
        }

        private Assembly LoadPlugin(string relativePath)
        {
            string pluginLocation;
            if (System.IO.Path.IsPathRooted(relativePath))
            {
                pluginLocation = relativePath.Replace('\\', Path.DirectorySeparatorChar);
            }
            else
            {
#if DEBUG
                string root = Path.GetFullPath(Path.Combine(
                        Path.GetDirectoryName(
                            Path.GetDirectoryName(
                                Path.GetDirectoryName(
                                    Path.GetDirectoryName(
                                        Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory)))))));

                pluginLocation = Path.GetFullPath(Path.Combine(root, relativePath.Replace('\\', Path.DirectorySeparatorChar)));
#else
                pluginLocation = Path.GetFullPath(relativePath.Replace('\\', Path.DirectorySeparatorChar));
#endif
            }

            Console.WriteLine($"Loading commands from: {pluginLocation}");
            PluginLoadContext loadContext = new PluginLoadContext(pluginLocation);
            return loadContext.LoadFromAssemblyName(new AssemblyName(Path.GetFileNameWithoutExtension(pluginLocation)));
        }

        private IEnumerable<TIJobAction> CreateJobActions(Assembly assembly)
        {
            int count = 0;

            foreach (Type type in assembly.GetTypes())
            {
                bool myval = false;
                if (typeof(TIJobAction).IsAssignableFrom(type) || myval)
                {
                    TIJobAction result = Activator.CreateInstance(type) as TIJobAction;
                    if (result != null)
                    {
                        count++;
                        yield return result;
                    }
                }
            }

            if (count == 0)
            {
                string availableTypes = string.Join(",", assembly.GetTypes().Select(t => t.FullName));
                throw new ApplicationException(
                    $"Can't find any type which implements Interface in {assembly} from {assembly.Location}.\n" +
                    $"Available types: {availableTypes}");
            }
        }
    }
}
