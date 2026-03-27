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
    public void Criar_ShouldReturnFalse_WhenPathIsInvalid()
    {
        Assert.False(Pasta.Criar("", "NovaPasta"));
        Assert.False(Pasta.Criar(" ", "NovaPasta"));
    }

    [Fact]
    public void Criar_ShouldReturnFalse_WhenNameIsEmptyOrWhitespace()
    {
        Assert.False(Pasta.Criar(_root, ""));
        Assert.False(Pasta.Criar(_root, " "));
    }

    [Fact]
    public void Criar_ShouldReturnFalse_WhenNameHasInvalidChars()
    {
        var name = "Nova\"Pasta";

        Assert.False(Pasta.Criar(_root, name));
    }

    [Fact]
    public void Criar_ShouldReturnFalse_WhenPathDoesNotExist()
    {
        var missing = Path.Combine(_root, "Missing");

        Assert.False(Pasta.Criar(missing, "NovaPasta"));
    }

    [Fact]
    public void Criar_ShouldReturnTrue_WhenDirectoryAlreadyExists()
    {
        var existing = Path.Combine(_root, "Existente");
        Directory.CreateDirectory(existing);

        Assert.True(Pasta.Criar(_root, "Existente"));
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
    public void Mover_ShouldReturnFalse_WhenPathsAreInvalid()
    {
        Assert.False(Pasta.Mover("", _root));
        Assert.False(Pasta.Mover(" ", _root));
        Assert.False(Pasta.Mover(_root, ""));
        Assert.False(Pasta.Mover(_root, " "));
    }

    [Fact]
    public void Mover_ShouldReturnFalse_WhenOriginOrDestinationDoesNotExist()
    {
        var origem = Path.Combine(_root, "Origem");
        var destino = Path.Combine(_root, "Destino");

        Directory.CreateDirectory(destino);

        Assert.False(Pasta.Mover(origem, destino));

        Directory.CreateDirectory(origem);
        Directory.Delete(destino, true);

        Assert.False(Pasta.Mover(origem, destino));
    }

    [Fact]
    public void Mover_ShouldReturnFalse_WhenFinalDestinationAlreadyExists()
    {
        var origem = Path.Combine(_root, "Origem");
        var destino = Path.Combine(_root, "Destino");

        Directory.CreateDirectory(origem);
        Directory.CreateDirectory(destino);
        Directory.CreateDirectory(Path.Combine(destino, "Origem"));

        Assert.False(Pasta.Mover(origem, destino));
        Assert.True(Directory.Exists(origem));
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

    [Fact]
    public void Deletar_ShouldReturnFalse_WhenPathIsInvalidOrMissing()
    {
        Assert.False(Pasta.Deletar(""));
        Assert.False(Pasta.Deletar(" "));
        Assert.False(Pasta.Deletar(Path.Combine(_root, "Inexistente")));
    }

    [Fact]
    public void DeletarSubPasta_ShouldDeleteOnlySubdirectories()
    {
        var alvo = Path.Combine(_root, "Alvo");
        var sub = Path.Combine(alvo, "Sub");

        Directory.CreateDirectory(sub);
        File.WriteAllText(Path.Combine(alvo, "raiz.txt"), "ok");
        File.WriteAllText(Path.Combine(sub, "arquivo.txt"), "ok");

        var result = Pasta.DeletarSubPasta(alvo);

        Assert.True(result);
        Assert.True(Directory.Exists(alvo));
        Assert.False(Directory.Exists(sub));
        Assert.True(File.Exists(Path.Combine(alvo, "raiz.txt")));
    }

    [Fact]
    public void DeletarSubPasta_ShouldReturnFalse_WhenPathIsInvalid()
    {
        Assert.False(Pasta.DeletarSubPasta(""));
        Assert.False(Pasta.DeletarSubPasta(" "));
    }

    [Fact]
    public void DeletarSubPasta_ShouldThrow_WhenPathDoesNotExist()
    {
        var missing = Path.Combine(_root, "Inexistente");

        Assert.Throws<DirectoryNotFoundException>(() => Pasta.DeletarSubPasta(missing));
    }

    public void Dispose()
    {
        if (Directory.Exists(_root))
            Directory.Delete(_root, true);
    }
}
