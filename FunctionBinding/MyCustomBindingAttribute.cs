using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Description;

namespace FunctionBinding
{
    [AttributeUsage(AttributeTargets.Parameter)]
    [Binding]
    public class MyCustomBindingAttribute : Attribute
    {
        public ConsoleColor Color { get; set; } = ConsoleColor.Cyan;
    }

    public class MyAsyncCollector : IAsyncCollector<string>
    {
        private readonly MyCustomBindingAttribute _attribute;

        public MyAsyncCollector(MyCustomBindingAttribute attribute)
        {
            _attribute = attribute;
        }

        public Task AddAsync(string item, CancellationToken cancellationToken = new CancellationToken())
        {
            var color = Console.ForegroundColor;
            Console.ForegroundColor = _attribute.Color;
            Console.WriteLine(item);
            Console.ForegroundColor = color;

            return Task.CompletedTask;
        }

        public Task FlushAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            return Task.CompletedTask;
        }
    }
}