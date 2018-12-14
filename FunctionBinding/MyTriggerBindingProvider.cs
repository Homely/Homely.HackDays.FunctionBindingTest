using System;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs.Host.Triggers;

namespace FunctionBinding
{
    public class MyTriggerBindingProvider : ITriggerBindingProvider
    {
        public Task<ITriggerBinding> TryCreateAsync(TriggerBindingProviderContext context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            var parameter = context.Parameter;
            var attribute = parameter.GetCustomAttribute<MyCustomTriggerAttribute>(false);
            if (attribute == null)
            {
                return Task.FromResult<ITriggerBinding>(null);
            }

            return Task.FromResult((ITriggerBinding)new MyTriggerBinding(attribute));
        }
    }
}