namespace M.Pastaruga;

public class Pasta
{
    public static bool Criar(string path, string nameDirectory)
    {
        if (string.IsNullOrEmpty(path))
            return false;
        if (string.IsNullOrEmpty(nameDirectory))
            return false;

        // Validar nome de diretório em busca de caracteres inválidos
        foreach (var c in Path.GetInvalidFileNameChars())
            if (nameDirectory.Contains(c.ToString()))
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
        catch (UnauthorizedAccessException uaEx)
        {
            throw;
        }
        catch (Exception ex)
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
        catch (UnauthorizedAccessException auEx)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw;
        }
    }
}