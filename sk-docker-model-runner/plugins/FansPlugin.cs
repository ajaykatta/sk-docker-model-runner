using Microsoft.SemanticKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text.Json.Serialization;
using System.Linq;
using System.Threading.Tasks;

namespace sk_docker_model_runner.plugins;

public class FansPlugin
{
    // Mock data for the fans
    private readonly List<FanModel> fans = new()
   {
      new FanModel { Id = 1, Name = "Ceiling Fan", IsOn = false },
      new FanModel { Id = 2, Name = "Desk Fan", IsOn = true },
      new FanModel { Id = 3, Name = "Exhaust Fan", IsOn = false }
   };

    [KernelFunction("get_fans")]
    [Description("Gets a list of fans and their current state")]
    public async Task<List<FanModel>> GetFansAsync()
    {
        return fans;
    }

    [KernelFunction("change_state")]
    [Description("Changes the state of the fan")]
    public async Task<FanModel?> ChangeStateAsync(int id, bool isOn)
    {
        var fan = fans.FirstOrDefault(fan => fan.Id == id);

        if (fan == null)
        {
            return null;
        }

        // Update the fan with the new state
        fan.IsOn = isOn;

        return fan;
    }
}

public class FanModel
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("is_on")]
    public bool? IsOn { get; set; }
}
