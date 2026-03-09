using M.Pastaruga;
using Xunit;

namespace M.Tests;

public sealed class ArquivoTests : IDisposable
{
    private readonly string _root;

    public ArquivoTests()
    {
        _root = Path.Combine(Path.GetTempPath(), "MTests", Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(_root);
    }

    [Fact]
    public void Criar_ShouldCreateFile_WhenInputIsValid()
    {
        var result = Arquivo.Criar(_root, "arquivo.txt");

        Assert.True(result);
        Assert.True(File.Exists(Path.Combine(_root, "arquivo.txt")));
    }

    [Fact]
    public void Mover_ShouldMoveFileToDestinationDirectory()
    {
        var origem = Path.Combine(_root, "origem.txt");
        var destino = Path.Combine(_root, "destino");

        File.WriteAllText(origem, "conteudo");
        Directory.CreateDirectory(destino);

        var result = Arquivo.Mover(origem, destino);

        Assert.True(result);
        Assert.False(File.Exists(origem));
        Assert.True(File.Exists(Path.Combine(destino, "origem.txt")));
    }

    [Fact]
    public void CopiarArquivo_ShouldCopyFile()
    {
        var origem = Path.Combine(_root, "origem.txt");
        var destino = Path.Combine(_root, "destino");

        File.WriteAllText(origem, "conteudo");
        Directory.CreateDirectory(destino);

        var result = Arquivo.CopiarArquivo(origem, destino);

        Assert.True(result);
        Assert.True(File.Exists(origem));
        Assert.True(File.Exists(Path.Combine(destino, "origem.txt")));
    }

    [Fact]
    public void CopiarArquivo_WithCut_ShouldMoveFile()
    {
        var origem = Path.Combine(_root, "origem.txt");
        var destino = Path.Combine(_root, "destino");

        File.WriteAllText(origem, "conteudo");
        Directory.CreateDirectory(destino);

        var result = Arquivo.CopiarArquivo(origem, destino, true);

        Assert.True(result);
        Assert.False(File.Exists(origem));
        Assert.True(File.Exists(Path.Combine(destino, "origem.txt")));
    }

    [Fact]
    public void Deletar_ShouldDeleteExistingFile()
    {
        var file = Path.Combine(_root, "arquivo.txt");
        File.WriteAllText(file, "conteudo");

        var result = Arquivo.Deletar(file);

        Assert.True(result);
        Assert.False(File.Exists(file));
    }

    [Fact]
    public void Copiar_ShouldCopyAllFilesFromDirectory()
    {
        var origem = Path.Combine(_root, "origem");
        var destino = Path.Combine(_root, "destino");

        Directory.CreateDirectory(origem);
        Directory.CreateDirectory(destino);

        File.WriteAllText(Path.Combine(origem, "a.txt"), "a");
        File.WriteAllText(Path.Combine(origem, "b.txt"), "b");

        var result = Arquivo.Copiar(origem, destino);

        Assert.True(result);
        Assert.True(File.Exists(Path.Combine(destino, "a.txt")));
        Assert.True(File.Exists(Path.Combine(destino, "b.txt")));
    }

    public void Dispose()
    {
        if (Directory.Exists(_root))
            Directory.Delete(_root, true);
    }
}
