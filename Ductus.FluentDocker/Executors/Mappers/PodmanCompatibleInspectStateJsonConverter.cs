using System;
using System.Collections.Generic;
using Ductus.FluentDocker.Model.Containers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Ductus.FluentDocker.Executors.Mappers
{
  public class PodmanCompatibleInspectStateJsonConverter : JsonConverter<ContainerState>
  {
    public override void WriteJson(JsonWriter writer, ContainerState value, JsonSerializer serializer) => throw new NotImplementedException();

    public override ContainerState ReadJson(JsonReader reader, Type objectType, ContainerState existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
      var item = JObject.Load(reader);

      if (string.IsNullOrWhiteSpace(item["Health"]["Status"].Value<string>()))
      {
        // Multiple possible replacements here...but sadly nothing matches up exactly.
        if(!HealthStateFromPodmanContainerState.TryGetValue(item["Status"].Value<string>(), out var currentState))
        {
          currentState = "Unhealthy";
        }
        
        item["Health"]["Status"].Replace(JValue.CreateString(currentState));
      }

      return item.ToObject<ContainerState>();
    }

    private Dictionary<string, string> HealthStateFromPodmanContainerState = new Dictionary<string, string>(){
    { "unknown", "Unknown" },
    { "running", "Healthy" }};
  }
}
