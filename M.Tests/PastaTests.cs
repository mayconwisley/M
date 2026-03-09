using M.Pastaruga;
using Xunit;

namespace M.Tests;

public sealed class PastaTests : IDisposable
{
    private readonly string _root;

    public PastaTests()
    {
        _root = Path.Combine(Path.GetTempPath(), "MTests", Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(_root);
    }

    [Fact]
    public void Criar_ShouldCreateDirectory_WhenInputIsValid()
    {
        var result = Pasta.Criar(_root, "NovaPasta");

        Assert.True(result);
        Assert.True(Directory.Exists(Path.Combine(_root, "NovaPasta")));
    }

    [Fact]
    public void Mover_ShouldMoveDirectoryToDestination()
    {
        var origem = Path.Combine(_root, "Origem");
        var destino = Path.Combine(_root, "Destino");

        Directory.CreateDirectory(origem);
        Directory.CreateDirectory(destino);

        var result = Pasta.Mover(origem, destino);

        Assert.True(result);
        Assert.False(Directory.Exists(origem));
        Assert.True(Directory.Exists(Path.Combine(destino, "Origem")));
    }

    [Fact]
    public void Deletar_ShouldDeleteDirectoryRecursively()
    {
        var alvo = Path.Combine(_root, "Alvo");
        var sub = Path.Combine(alvo, "Sub");

        Directory.CreateDirectory(sub);
        File.WriteAllText(Path.Combine(sub, "arquivo.txt"), "ok");

        var result = Pasta.Deletar(alvo);

        Assert.True(result);
        Assert.False(Directory.Exists(alvo));
    }

    public void Dispose()
    {
        if (Directory.Exists(_root))
            Directory.Delete(_root, true);
    }
}
