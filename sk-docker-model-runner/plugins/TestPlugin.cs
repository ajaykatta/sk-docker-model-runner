using Microsoft.SemanticKernel;

namespace sk_docker_model_runner.plugins;

public class TestPlugin
{
    [KernelFunction("echo")]
    public string Echo(string input)
    {
        return input;
    }
}
