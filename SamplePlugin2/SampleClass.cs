using SamplePluginInterfaces;
using System;

namespace SamplePlugin2
{
    public class SampleClass : IPluginSample2
    {
        public void SampleFunction()
        {
            Console.WriteLine("This is SamplePlugin2 - Uses IPluginSample2");
        }
    }
}
