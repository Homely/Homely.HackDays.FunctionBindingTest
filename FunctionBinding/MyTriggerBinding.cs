using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs.Host.Bindings;
using Microsoft.Azure.WebJobs.Host.Listeners;
using Microsoft.Azure.WebJobs.Host.Protocols;
using Microsoft.Azure.WebJobs.Host.Triggers;

namespace FunctionBinding
{
    public class MyTriggerBinding : ITriggerBinding
    {
        private readonly MyCustomTriggerAttribute _attribute;

        public MyTriggerBinding(MyCustomTriggerAttribute attribute)
        {
            _attribute = attribute;
        }

        public Task<ITriggerData> BindAsync(object value, ValueBindingContext context)
        {
            var bindingData = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase)
            {
                {"data", value}
            };

            return Task.FromResult<ITriggerData>(new TriggerData(bindingData));
        }

        public Task<IListener> CreateListenerAsync(ListenerFactoryContext context)
        {
            return Task.FromResult<IListener>(new MyListener(context.Executor, _attribute));
        }

        public ParameterDescriptor ToParameterDescriptor()
        {
            return new ParameterDescriptor() { Name = "My Trigger" };
        }

        public Type TriggerValueType => typeof(string);
        public IReadOnlyDictionary<string, Type> BindingDataContract => new Dictionary<string, Type>(StringComparer.OrdinalIgnoreCase)
        {
            {"data", typeof(string)}
        };


    }
}