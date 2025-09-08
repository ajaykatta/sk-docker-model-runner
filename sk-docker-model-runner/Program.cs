using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using sk_docker_model_runner.plugins;
using System.Net.Http;

Console.WriteLine("Welcome to the Semantic Kernel Docker Model Runner!");
var builder = Kernel.CreateBuilder();

//builder.AddOpenAIChatCompletion(modelId: "ai/gemma3n", endpoint: new Uri("http://localhost:12434/engines/v1"), apiKey: "not-required");

builder.AddOpenAIChatCompletion(modelId: "llama3.1", 
    endpoint: new Uri("http://localhost:11434/v1"),
    apiKey: "ollama");

builder.Services.AddLogging(services => services.AddConsole().SetMinimumLevel(LogLevel.Warning));

var kernel = builder.Build();

var chatCompletionService = kernel.Services.GetRequiredService<IChatCompletionService>();

//kernel.Plugins.AddFromType<LightsPlugin>("Lights");
//kernel.Plugins.AddFromType<FansPlugin>("Fans");

OpenAIPromptExecutionSettings openAIPromptExecutionSettings = new()
{
    FunctionChoiceBehavior = FunctionChoiceBehavior.Auto()
};

var history = new ChatHistory();

// Initiate a back-and-forth chat
string? userInput;
do
{
    // Collect user input
    Console.Write("User > ");
    userInput = Console.ReadLine();

    // Add user input
    history.AddUserMessage(userInput);
    ChatMessageContent result;

    try
    {
        // Get the response from the AI
        result = await chatCompletionService.GetChatMessageContentAsync(
            history,
            executionSettings: openAIPromptExecutionSettings,
            kernel: kernel);
    }
    catch (HttpRequestException ex) when (ex.Message.Contains("429"))
    {
        Console.WriteLine("Assistant > Too many requests (HTTP 429). Please wait and try again later.");
        continue;
    }
    catch (TaskCanceledException ex)
    {
        Console.WriteLine($"Assistant > The request timed out. Please try again. {ex.Message}");
        continue;
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Assistant > An error occurred: {ex.Message}");
        continue;
    }

    // Print the results
    Console.WriteLine("Assistant > " + result);

    // Add the message from the agent to the chat history
    history.AddMessage(result.Role, result.Content ?? string.Empty);
} while (userInput is not null);