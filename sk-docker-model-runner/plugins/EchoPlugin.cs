using Microsoft.SemanticKernel;
using System.ComponentModel;

namespace sk_docker_model_runner.plugins;

public class EchoPlugin
{
    public EchoPlugin()
    {
    }

    [KernelFunction("echo")]
    [Description("Echoes the input string")]
    public string Echo(string input)
    {
        return input;
    }    
}
