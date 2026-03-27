namespace M.Pastaruga;

public class Pasta
{
    public static bool Criar(string path, string nameDirectory)
    {
        if (string.IsNullOrEmpty(path))
            return false;
        if (string.IsNullOrWhiteSpace(nameDirectory))
            return false;

        if (nameDirectory.Contains('"'))
            return false;
        if (nameDirectory.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0)
            return false;

        try
        {
            if (!Directory.Exists(path))
                return false;

            var newDirectory = Path.Combine(path, nameDirectory);
            if (Directory.Exists(newDirectory))
                return true;

            Directory.CreateDirectory(newDirectory);
            return true;
        }
        catch (UnauthorizedAccessException)
        {
            throw;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public static bool Mover(string pathOrigin, string pathDestination)
    {
        if (string.IsNullOrEmpty(pathOrigin))
            return false;
        if (string.IsNullOrEmpty(pathDestination))
            return false;

        try
        {
            if (!Directory.Exists(pathOrigin) || !Directory.Exists(pathDestination))
                return false;

            var pathFinalDestination = Path.Combine(pathDestination, Path.GetFileName(pathOrigin));
            if (Directory.Exists(pathFinalDestination))
                return false;

            Directory.Move(pathOrigin, pathFinalDestination);
            return true;
        }
        catch (UnauthorizedAccessException)
        {
            throw;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public static bool Deletar(string path)
    {
        if (string.IsNullOrWhiteSpace(path))
            return false;

        try
        {
            if (!Directory.Exists(path))
                return false;

            Directory.Delete(path, true);
            return true;
        }
        catch (UnauthorizedAccessException)
        {
            throw;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public static bool DeletarSubPasta(string path)
    {
        if (string.IsNullOrWhiteSpace(path))
            return false;

        try
        {
            var foders = Directory.GetDirectories(path);

            foreach (var item in foders)
                Directory.Delete(item, true);

            return true;
        }
        catch (UnauthorizedAccessException)
        {
            throw;
        }
        catch (Exception)
        {
            throw;
        }
    }
}
