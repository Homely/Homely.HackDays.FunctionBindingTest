using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs.Host.Executors;
using Microsoft.Azure.WebJobs.Host.Listeners;

namespace FunctionBinding
{
    public class MyListener : IListener
    {
        private readonly Timer _timer;

        private readonly MyCustomTriggerAttribute _attribute;

        public MyListener(ITriggeredFunctionExecutor executor, MyCustomTriggerAttribute attribute)
        {
            _attribute = attribute;

            _timer = new Timer(s =>
                {
                    // Set trigger value here...
                    executor.TryExecuteAsync(new TriggeredFunctionData { TriggerValue = DateTime.UtcNow.ToString("o") },
                        CancellationToken.None);
                },
            null, TimeSpan.FromSeconds(300), TimeSpan.FromSeconds(_attribute.Interval));
        }

        public void Dispose()
        {
            _timer.Dispose();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer.Change(TimeSpan.FromSeconds(0), TimeSpan.FromSeconds(_attribute.Interval));
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer.Change(TimeSpan.FromSeconds(300), TimeSpan.FromSeconds(_attribute.Interval));
            return Task.CompletedTask;
        }

        public void Cancel()
        {
            _timer.Change(TimeSpan.FromSeconds(300), TimeSpan.FromSeconds(_attribute.Interval));
        }
    }
}