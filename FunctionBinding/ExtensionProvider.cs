using Microsoft.Azure.WebJobs.Host.Config;
using Microsoft.Azure.WebJobs.Host.Triggers;

namespace FunctionBinding
{
    public class ExtensionProvider : IExtensionConfigProvider
    {
        private readonly ITriggerBindingProvider _provider = new MyTriggerBindingProvider();

        public void Initialize(ExtensionConfigContext context)
        {
            context.AddBindingRule<MyCustomTriggerAttribute>().BindToTrigger<string>(_provider);

            context.AddBindingRule<MyCustomBindingAttribute>().BindToCollector(attr => new MyAsyncCollector(attr));
        }
    }
}