using M.Infrastructure.File;
using Xunit;

namespace M.Tests;

public sealed class FileRepositoryTests : IDisposable
{
    private readonly string _root;
    private readonly FileRepository _sut = new();

    public FileRepositoryTests()
    {
        _root = Path.Combine(Path.GetTempPath(), "MTests", Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(_root);
    }

    // ── Create ─────────────────────────────────────────────────────────────

    [Fact]
    public void Create_ShouldCreateFile_WhenInputIsValid()
    {
        var result = _sut.Create(_root, "arquivo.txt");

        Assert.True(result);
        Assert.True(File.Exists(Path.Combine(_root, "arquivo.txt")));
    }

    [Fact]
    public void Create_ShouldReturnFalse_WhenDirectoryDoesNotExist()
    {
        Assert.False(_sut.Create(Path.Combine(_root, "Missing"), "arquivo.txt"));
    }

    [Fact]
    public void Create_ShouldReturnTrue_WhenFileAlreadyExists()
    {
        File.WriteAllText(Path.Combine(_root, "arquivo.txt"), "conteudo");

        Assert.True(_sut.Create(_root, "arquivo.txt"));
    }

    // ── Move ───────────────────────────────────────────────────────────────

    [Fact]
    public void Move_ShouldMoveFileToDestinationDirectory()
    {
        var origin = Path.Combine(_root, "origem.txt");
        var dest = Path.Combine(_root, "destino");

        File.WriteAllText(origin, "conteudo");
        Directory.CreateDirectory(dest);

        Assert.True(_sut.Move(origin, dest));
        Assert.False(File.Exists(origin));
        Assert.True(File.Exists(Path.Combine(dest, "origem.txt")));
    }

    [Fact]
    public void Move_ShouldReturnFalse_WhenOriginOrDestinationDoesNotExist()
    {
        var origin = Path.Combine(_root, "origem.txt");
        var dest = Path.Combine(_root, "destino");

        Directory.CreateDirectory(dest);
        Assert.False(_sut.Move(origin, dest));

        File.WriteAllText(origin, "conteudo");
        Directory.Delete(dest, true);
        Assert.False(_sut.Move(origin, dest));
    }

    [Fact]
    public void Move_ShouldReturnFalse_WhenDestinationFileAlreadyExists()
    {
        var origin = Path.Combine(_root, "origem.txt");
        var dest = Path.Combine(_root, "destino");

        File.WriteAllText(origin, "conteudo");
        Directory.CreateDirectory(dest);
        File.WriteAllText(Path.Combine(dest, "origem.txt"), "kx");

        Assert.False(_sut.Move(origin, dest));
        Assert.True(File.Exists(origin));
    }

    // ── Delete ─────────────────────────────────────────────────────────────

    [Fact]
    public void Delete_ShouldDeleteExistingFile()
    {
        var file = Path.Combine(_root, "arquivo.txt");
        File.WriteAllText(file, "conteudo");

        Assert.True(_sut.Delete(file));
        Assert.False(File.Exists(file));
    }

    [Fact]
    public void Delete_ShouldReturnFalse_WhenFileDoesNotExist()
    {
        Assert.False(_sut.Delete(Path.Combine(_root, "inexistente.txt")));
    }

    // ── Copy ───────────────────────────────────────────────────────────────

    [Fact]
    public void Copy_ShouldCopyFile()
    {
        var origin = Path.Combine(_root, "origem.txt");
        var dest = Path.Combine(_root, "destino");

        File.WriteAllText(origin, "conteudo");
        Directory.CreateDirectory(dest);

        Assert.True(_sut.Copy(origin, dest));
        Assert.True(File.Exists(origin));
        Assert.True(File.Exists(Path.Combine(dest, "origem.txt")));
    }

    [Fact]
    public void Copy_ShouldOverwriteDestinationWhenExists()
    {
        var origin = Path.Combine(_root, "origem.txt");
        var dest = Path.Combine(_root, "destino");

        File.WriteAllText(origin, "novo");
        Directory.CreateDirectory(dest);
        File.WriteAllText(Path.Combine(dest, "origem.txt"), "antigo");

        Assert.True(_sut.Copy(origin, dest));
        Assert.Equal("novo", File.ReadAllText(Path.Combine(dest, "origem.txt")));
    }

    [Fact]
    public void Copy_WithCut_ShouldMoveFile()
    {
        var origin = Path.Combine(_root, "origem.txt");
        var dest = Path.Combine(_root, "destino");

        File.WriteAllText(origin, "conteudo");
        Directory.CreateDirectory(dest);

        Assert.True(_sut.Copy(origin, dest, cut: true));
        Assert.False(File.Exists(origin));
        Assert.True(File.Exists(Path.Combine(dest, "origem.txt")));
    }

    [Fact]
    public void Copy_ShouldReturnFalse_WhenOriginOrDestinationDoesNotExist()
    {
        var origin = Path.Combine(_root, "origem.txt");
        var dest = Path.Combine(_root, "destino");

        Directory.CreateDirectory(dest);
        Assert.False(_sut.Copy(origin, dest));

        File.WriteAllText(origin, "conteudo");
        Directory.Delete(dest, true);
        Assert.False(_sut.Copy(origin, dest));
    }

    public void Dispose()
    {
        if (Directory.Exists(_root))
            Directory.Delete(_root, true);
    }
}
