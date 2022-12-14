using System;
using Ductus.FluentDocker.Model.Containers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Ductus.FluentDocker.Executors.Mappers
{
  public class PodmanCompatibleInspectContainerConfigJsonConverter : JsonConverter<ContainerConfig>
  {
    public override void WriteJson(JsonWriter writer, ContainerConfig value, JsonSerializer serializer) => throw new NotImplementedException();

    public override ContainerConfig ReadJson(JsonReader reader, Type objectType, ContainerConfig existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
      var item = JObject.Load(reader);

      if (item["Entrypoint"].Type == JTokenType.String)
      {
        item["Entrypoint"].Replace(JArray.FromObject(new string[] { item["Entrypoint"].Value<string>() }));
      }

      return item.ToObject<ContainerConfig>();
    }
  }
}
