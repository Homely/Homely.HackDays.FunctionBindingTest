using System;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;

namespace FunctionBinding
{
    public class Function
    {
        [FunctionName("sample-trigger-function")]
        public static Task Run([MyCustomTrigger(Interval = 2)] string message, 
                               [MyCustomBinding(Color = ConsoleColor.Yellow)] IAsyncCollector<string> collector)
        {
            return collector.AddAsync(message);
        }
    }
}
