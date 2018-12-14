using FunctionBinding;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Hosting;

[assembly: WebJobsStartup(typeof(Startup), "Extensions registration")]
namespace FunctionBinding
{
    public class Startup : IWebJobsStartup
    {
        public void Configure(IWebJobsBuilder builder)
        {

            // Registering an extension
            builder.AddExtension<ExtensionProvider>();
        }
    }
}