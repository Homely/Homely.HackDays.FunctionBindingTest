using System;
using Microsoft.Azure.WebJobs.Description;


namespace FunctionBinding
{
    [AttributeUsage(AttributeTargets.Parameter)]
    [Binding]
    public class MyCustomTriggerAttribute : Attribute
    {
        public int Interval { get; set; } = 10;
    }
}
