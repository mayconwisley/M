using M.Infrastructure.Folder;
using Xunit;

namespace M.Tests;

public sealed class FolderRepositoryTests : IDisposable
{
    private readonly string _root;
    private readonly FolderRepository _sut = new();

    public FolderRepositoryTests()
    {
        _root = Path.Combine(Path.GetTempPath(), "MTests", Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(_root);
    }

    // ── Create ─────────────────────────────────────────────────────────────

    [Fact]
    public void Create_ShouldCreateDirectory_WhenInputIsValid()
    {
        var result = _sut.Create(_root, "NovaPasta");

        Assert.True(result);
        Assert.True(Directory.Exists(Path.Combine(_root, "NovaPasta")));
    }

    [Fact]
    public void Create_ShouldReturnFalse_WhenPathDoesNotExist()
    {
        Assert.False(_sut.Create(Path.Combine(_root, "Missing"), "NovaPasta"));
    }

    [Fact]
    public void Create_ShouldReturnTrue_WhenDirectoryAlreadyExists()
    {
        Directory.CreateDirectory(Path.Combine(_root, "Existente"));

        Assert.True(_sut.Create(_root, "Existente"));
    }

    // ── Move ───────────────────────────────────────────────────────────────

    [Fact]
    public void Move_ShouldMoveDirectoryToDestination()
    {
        var origin = Path.Combine(_root, "Origem");
        var dest   = Path.Combine(_root, "Destino");

        Directory.CreateDirectory(origin);
        Directory.CreateDirectory(dest);

        var result = _sut.Move(origin, dest);

        Assert.True(result);
        Assert.False(Directory.Exists(origin));
        Assert.True(Directory.Exists(Path.Combine(dest, "Origem")));
    }

    [Fact]
    public void Move_ShouldReturnFalse_WhenOriginOrDestinationDoesNotExist()
    {
        var origin = Path.Combine(_root, "Origem");
        var dest   = Path.Combine(_root, "Destino");

        Directory.CreateDirectory(dest);
        Assert.False(_sut.Move(origin, dest));

        Directory.CreateDirectory(origin);
        Directory.Delete(dest, true);
        Assert.False(_sut.Move(origin, dest));
    }

    [Fact]
    public void Move_ShouldReturnFalse_WhenFinalDestinationAlreadyExists()
    {
        var origin = Path.Combine(_root, "Origem");
        var dest   = Path.Combine(_root, "Destino");

        Directory.CreateDirectory(origin);
        Directory.CreateDirectory(dest);
        Directory.CreateDirectory(Path.Combine(dest, "Origem"));

        Assert.False(_sut.Move(origin, dest));
        Assert.True(Directory.Exists(origin));
    }

    // ── Delete ─────────────────────────────────────────────────────────────

    [Fact]
    public void Delete_ShouldDeleteDirectoryRecursively()
    {
        var target = Path.Combine(_root, "Alvo");
        var sub    = Path.Combine(target, "Sub");

        Directory.CreateDirectory(sub);
        File.WriteAllText(Path.Combine(sub, "arquivo.txt"), "ok");

        Assert.True(_sut.Delete(target));
        Assert.False(Directory.Exists(target));
    }

    [Fact]
    public void Delete_ShouldReturnFalse_WhenPathDoesNotExist()
    {
        Assert.False(_sut.Delete(Path.Combine(_root, "Inexistente")));
    }

    // ── DeleteSubFolders ───────────────────────────────────────────────────

    [Fact]
    public void DeleteSubFolders_ShouldDeleteOnlySubdirectories()
    {
        var target = Path.Combine(_root, "Alvo");
        var sub    = Path.Combine(target, "Sub");

        Directory.CreateDirectory(sub);
        File.WriteAllText(Path.Combine(target, "raiz.txt"), "ok");
        File.WriteAllText(Path.Combine(sub, "arquivo.txt"), "ok");

        Assert.True(_sut.DeleteSubFolders(target));
        Assert.True(Directory.Exists(target));
        Assert.False(Directory.Exists(sub));
        Assert.True(File.Exists(Path.Combine(target, "raiz.txt")));
    }

    [Fact]
    public void DeleteSubFolders_ShouldThrow_WhenPathDoesNotExist()
    {
        Assert.Throws<DirectoryNotFoundException>(
            () => _sut.DeleteSubFolders(Path.Combine(_root, "Inexistente")));
    }

    // ── DeleteFiles ────────────────────────────────────────────────────────

    [Fact]
    public void DeleteFiles_ShouldDeleteOnlyFilesInDirectory()
    {
        var target = Path.Combine(_root, "Alvo");
        var sub    = Path.Combine(target, "Sub");

        Directory.CreateDirectory(sub);
        File.WriteAllText(Path.Combine(target, "a.txt"), "a");
        File.WriteAllText(Path.Combine(target, "b.txt"), "b");
        File.WriteAllText(Path.Combine(sub, "c.txt"), "c");

        Assert.True(_sut.DeleteFiles(target));
        Assert.True(Directory.Exists(target));
        Assert.True(Directory.Exists(sub));
        Assert.False(File.Exists(Path.Combine(target, "a.txt")));
        Assert.False(File.Exists(Path.Combine(target, "b.txt")));
        Assert.True(File.Exists(Path.Combine(sub, "c.txt")));
    }

    [Fact]
    public void DeleteFiles_ShouldReturnTrue_WhenDirectoryIsEmpty()
    {
        var target = Path.Combine(_root, "Vazio");
        Directory.CreateDirectory(target);

        Assert.True(_sut.DeleteFiles(target));
    }

    [Fact]
    public void DeleteFiles_ShouldThrow_WhenPathDoesNotExist()
    {
        Assert.Throws<DirectoryNotFoundException>(
            () => _sut.DeleteFiles(Path.Combine(_root, "Inexistente")));
    }

    // ── CopyFiles ──────────────────────────────────────────────────────────

    [Fact]
    public void CopyFiles_ShouldCopyAllFilesFromDirectory()
    {
        var origin = Path.Combine(_root, "origem");
        var dest   = Path.Combine(_root, "destino");

        Directory.CreateDirectory(origin);
        Directory.CreateDirectory(dest);
        File.WriteAllText(Path.Combine(origin, "a.txt"), "a");
        File.WriteAllText(Path.Combine(origin, "b.txt"), "b");

        Assert.True(_sut.CopyFiles(origin, dest));
        Assert.True(File.Exists(Path.Combine(dest, "a.txt")));
        Assert.True(File.Exists(Path.Combine(dest, "b.txt")));
    }

    [Fact]
    public void CopyFiles_WithCut_ShouldMoveFilesFromDirectory()
    {
        var origin = Path.Combine(_root, "origem");
        var dest   = Path.Combine(_root, "destino");

        Directory.CreateDirectory(origin);
        Directory.CreateDirectory(dest);
        File.WriteAllText(Path.Combine(origin, "a.txt"), "a");
        File.WriteAllText(Path.Combine(origin, "b.txt"), "b");

        Assert.True(_sut.CopyFiles(origin, dest, cut: true));
        Assert.False(File.Exists(Path.Combine(origin, "a.txt")));
        Assert.True(File.Exists(Path.Combine(dest, "a.txt")));
    }

    [Fact]
    public void CopyFiles_ShouldReturnFalse_WhenOriginOrDestinationDoesNotExist()
    {
        var origin = Path.Combine(_root, "origem");
        var dest   = Path.Combine(_root, "destino");

        Directory.CreateDirectory(dest);
        Assert.False(_sut.CopyFiles(origin, dest));

        Directory.CreateDirectory(origin);
        Directory.Delete(dest, true);
        Assert.False(_sut.CopyFiles(origin, dest));
    }

    public void Dispose()
    {
        if (Directory.Exists(_root))
            Directory.Delete(_root, true);
    }
}
