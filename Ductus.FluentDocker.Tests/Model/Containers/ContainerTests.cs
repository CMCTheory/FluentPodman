using Ductus.FluentDocker.Model.Common;
using Ductus.FluentDocker.Extensions;
using Ductus.FluentDocker.Model.Containers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Ductus.FluentDocker.Executors.Mappers;

namespace Ductus.FluentDocker.Tests.Model.Containers
{
  [TestClass]
  public class ContainerTests
  {
    [TestMethod]
    public void TestWithNoCreated()
    {
      var data = ((TemplateString)"Model/Containers/inspect_no_create.json").FromFile();
      var obj = JsonConvert.DeserializeObject<Container>(data,
        new PodmanCompatibleInspectContainerConfigJsonConverter(),
        new PodmanCompatibleInspectStateJsonConverter());

      Assert.AreEqual(obj.Created, default);
    }

    [TestMethod]
    public void TestWithNoCreatedPodman()
    {
      var data = ((TemplateString)"Model/Containers/inspect_no_create_podman.json").FromFile();
      var obj = JsonConvert.DeserializeObject<Container>(data,
        new PodmanCompatibleInspectContainerConfigJsonConverter(),
        new PodmanCompatibleInspectStateJsonConverter());

      Assert.AreEqual(obj.Created, default);
    }
  }
}
