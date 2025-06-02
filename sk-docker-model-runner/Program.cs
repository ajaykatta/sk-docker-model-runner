using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using sk_docker_model_runner.plugins;

Console.WriteLine("Welcome to the Semantic Kernel Docker Model Runner!");
var builder = Kernel.CreateBuilder();

builder.AddOpenAIChatCompletion(modelId: "ai/gemma3", endpoint: new Uri("http://localhost:12434/engines/v1"), apiKey: "not-required");

builder.Services.AddLogging(services => services.AddConsole().SetMinimumLevel(LogLevel.Warning));

var kernel = builder.Build();

var chatCompletionService = kernel.Services.GetRequiredService<IChatCompletionService>();

kernel.Plugins.AddFromType<LightsPlugin>("Lights");
kernel.Plugins.AddFromType<FansPlugin>("Fans");

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

    // Get the response from the AI
    var result = await chatCompletionService.GetChatMessageContentAsync(
        history,
        executionSettings: openAIPromptExecutionSettings,
        kernel: kernel);

    // Print the results
    Console.WriteLine("Assistant > " + result);

    // Add the message from the agent to the chat history
    history.AddMessage(result.Role, result.Content ?? string.Empty);
} while (userInput is not null);