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
    public void Criar_ShouldReturnFalse_WhenPathOrNameIsInvalid()
    {
        Assert.False(Arquivo.Criar("", "arquivo.txt"));
        Assert.False(Arquivo.Criar(" ", "arquivo.txt"));
        Assert.False(Arquivo.Criar(_root, ""));
        Assert.False(Arquivo.Criar(_root, " "));
    }

    [Fact]
    public void Criar_ShouldReturnFalse_WhenDirectoryDoesNotExist()
    {
        var missing = Path.Combine(_root, "Missing");

        Assert.False(Arquivo.Criar(missing, "arquivo.txt"));
    }

    [Fact]
    public void Criar_ShouldReturnTrue_WhenFileAlreadyExists()
    {
        var file = Path.Combine(_root, "arquivo.txt");
        File.WriteAllText(file, "conteudo");

        Assert.True(Arquivo.Criar(_root, "arquivo.txt"));
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
    public void Mover_ShouldReturnFalse_WhenPathIsInvalid()
    {
        Assert.False(Arquivo.Mover("", _root));
        Assert.False(Arquivo.Mover(" ", _root));
        Assert.False(Arquivo.Mover(Path.Combine(_root, "origem.txt"), ""));
        Assert.False(Arquivo.Mover(Path.Combine(_root, "origem.txt"), " "));
    }

    [Fact]
    public void Mover_ShouldReturnFalse_WhenOriginOrDestinationDoesNotExist()
    {
        var origem = Path.Combine(_root, "origem.txt");
        var destino = Path.Combine(_root, "destino");

        Directory.CreateDirectory(destino);

        Assert.False(Arquivo.Mover(origem, destino));

        File.WriteAllText(origem, "conteudo");
        Directory.Delete(destino, true);

        Assert.False(Arquivo.Mover(origem, destino));
    }

    [Fact]
    public void Mover_ShouldReturnFalse_WhenDestinationFileAlreadyExists()
    {
        var origem = Path.Combine(_root, "origem.txt");
        var destino = Path.Combine(_root, "destino");

        File.WriteAllText(origem, "conteudo");
        Directory.CreateDirectory(destino);
        File.WriteAllText(Path.Combine(destino, "origem.txt"), "x");

        Assert.False(Arquivo.Mover(origem, destino));
        Assert.True(File.Exists(origem));
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
    public void CopiarArquivo_ShouldOverwriteDestinationWhenExists()
    {
        var origem = Path.Combine(_root, "origem.txt");
        var destino = Path.Combine(_root, "destino");

        File.WriteAllText(origem, "novo");
        Directory.CreateDirectory(destino);
        File.WriteAllText(Path.Combine(destino, "origem.txt"), "antigo");

        var result = Arquivo.CopiarArquivo(origem, destino);

        Assert.True(result);
        Assert.Equal("novo", File.ReadAllText(Path.Combine(destino, "origem.txt")));
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
    public void CopiarArquivo_ShouldReturnFalse_WhenInputIsInvalid()
    {
        Assert.False(Arquivo.CopiarArquivo("", _root));
        Assert.False(Arquivo.CopiarArquivo(" ", _root));
        Assert.False(Arquivo.CopiarArquivo(Path.Combine(_root, "origem.txt"), ""));
        Assert.False(Arquivo.CopiarArquivo(Path.Combine(_root, "origem.txt"), " "));
    }

    [Fact]
    public void CopiarArquivo_ShouldReturnFalse_WhenOriginOrDestinationDoesNotExist()
    {
        var origem = Path.Combine(_root, "origem.txt");
        var destino = Path.Combine(_root, "destino");

        Directory.CreateDirectory(destino);

        Assert.False(Arquivo.CopiarArquivo(origem, destino));

        File.WriteAllText(origem, "conteudo");
        Directory.Delete(destino, true);

        Assert.False(Arquivo.CopiarArquivo(origem, destino));
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
    public void Deletar_ShouldReturnFalse_WhenPathIsInvalidOrMissing()
    {
        Assert.False(Arquivo.Deletar(""));
        Assert.False(Arquivo.Deletar(" "));
        Assert.False(Arquivo.Deletar(Path.Combine(_root, "inexistente.txt")));
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

    [Fact]
    public void Copiar_WithCut_ShouldMoveFilesFromDirectory()
    {
        var origem = Path.Combine(_root, "origem");
        var destino = Path.Combine(_root, "destino");

        Directory.CreateDirectory(origem);
        Directory.CreateDirectory(destino);

        var fileA = Path.Combine(origem, "a.txt");
        var fileB = Path.Combine(origem, "b.txt");
        File.WriteAllText(fileA, "a");
        File.WriteAllText(fileB, "b");

        var result = Arquivo.Copiar(origem, destino, true);

        Assert.True(result);
        Assert.False(File.Exists(fileA));
        Assert.False(File.Exists(fileB));
        Assert.True(File.Exists(Path.Combine(destino, "a.txt")));
        Assert.True(File.Exists(Path.Combine(destino, "b.txt")));
    }

    [Fact]
    public void Copiar_ShouldReturnFalse_WhenOriginOrDestinationDoesNotExist()
    {
        var origem = Path.Combine(_root, "origem");
        var destino = Path.Combine(_root, "destino");

        Directory.CreateDirectory(destino);

        Assert.False(Arquivo.Copiar(origem, destino));

        Directory.CreateDirectory(origem);
        Directory.Delete(destino, true);

        Assert.False(Arquivo.Copiar(origem, destino));
    }

    public void Dispose()
    {
        if (Directory.Exists(_root))
            Directory.Delete(_root, true);
    }
}
