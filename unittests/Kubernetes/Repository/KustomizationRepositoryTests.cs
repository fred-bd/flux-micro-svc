using k8s.Models;
using Kubernetes.Repository;
using Kubernetes.Repository.Interfaces;
using KubernetesCRDModelGen.Models.kustomize.toolkit.fluxcd.io;
using Moq;

namespace unittests.Kubernetes.Repository;

public class FluxRepositoryTests
{

  private readonly Mock<IClientWrapper> _clientWrapperMoc;
  private readonly FluxRepository _fluxRepository;


  public FluxRepositoryTests()
  {
    _clientWrapperMoc = new Mock<IClientWrapper>();
    _fluxRepository = new FluxRepository(_clientWrapperMoc.Object);
  }

  [Fact]
  public async Task Should_return_kustomization_list_if_exists_in_namespace()
  {
    // Arrange
    var result = new V1KustomizationList
    {
      Items =
      [
        new() { Metadata = new V1ObjectMeta { Name = "test1-ks" } },
        new() { Metadata = new V1ObjectMeta { Name = "test2-ks" } }
      ]
    };

    _clientWrapperMoc.Setup(
      x => x.GetListAsync(It.Is<string>(k => k == "test"))
    ).ReturnsAsync(result);

    // Act
    var testing = await _fluxRepository.GetListAsync("test");

    // Assert
    Assert.NotNull(testing);
    Assert.Equal(2, testing.Items!.Count);
  }

  [Theory]
  [InlineData("test1")]
  [InlineData("test2")]
  [InlineData("test3")]
  public async Task Should_return_empty_list_if_not_exists_in_namespace(string ns)
  {
    // Arrange
    var result = new V1KustomizationList
    {
      Items =
      [
        new() { Metadata = new V1ObjectMeta { Name = "test1-ks" } },
        new() { Metadata = new V1ObjectMeta { Name = "test2-ks" } }
      ]
    };

    _clientWrapperMoc.Setup(
      x => x.GetListAsync(It.Is<string>(k => k == "test"))
    ).ReturnsAsync(result);


    _clientWrapperMoc.Setup(
      x => x.GetListAsync(It.Is<string>(k => k != "test"))
    ).ReturnsAsync(new V1KustomizationList());

    // Act
    var testing = await _fluxRepository.GetListAsync(ns);

    // Assert
    Assert.NotNull(testing);
    Assert.Null(testing.Items);
  }
}
