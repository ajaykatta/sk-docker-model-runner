using ModelContextProtocol.Server;
using System.ComponentModel;

namespace sk_docker_model_runner.tools;

[McpServerToolType]
public static class EchoTool
{
    [McpServerTool, Description("Echoes the message back to the client.")]
    public static string Echo(string message) => $"Hello, {message}";

    [McpServerTool, Description("Echoes in reverse the message sent by the client.")]
    public static string ReverseEcho(string message) => new([.. message.Reverse()]);
}