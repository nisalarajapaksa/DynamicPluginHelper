using SamplePluginInterfaces;
using System;

namespace SamplePlugin3
{
    public class SampleClass : IPluginSample2
    {
        public void SampleFunction()
        {
            Console.WriteLine("This is SamplePlugin3 - Uses IPluginSample2");
        }
    }
}
