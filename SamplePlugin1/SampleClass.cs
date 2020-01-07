using SamplePluginInterfaces;
using System;

namespace SamplePlugin1
{
    public class SampleClass : IPluginSample1
    {
        public void SampleFunction()
        {
            Console.WriteLine("This is SamplePlugin1 - Uses IPluginSample1");
        }
    }
}
