namespace M.Pastaruga;

public sealed class Arquivo
{
    public static bool Copiar(string pathFileOrigin, string pathFileDestination, bool isCut = false)
    {
        if (string.IsNullOrEmpty(pathFileOrigin))
            return false;
        if (string.IsNullOrEmpty(pathFileDestination))
            return false;

        try
        {
            if (!Directory.Exists(pathFileOrigin) || !Directory.Exists(pathFileDestination))
                return false;

            foreach (var file in Directory.GetFiles(pathFileOrigin))
            {
                var destinationFile = Path.Combine(pathFileDestination, Path.GetFileName(file));

                if (isCut)
                    File.Move(file, destinationFile);
                else
                    File.Copy(file, destinationFile, true);
            }
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

    public static bool Criar(string directoryPath, string fileName)
    {
        if (string.IsNullOrWhiteSpace(directoryPath))
            return false;
        if (string.IsNullOrWhiteSpace(fileName))
            return false;

        foreach (var c in Path.GetInvalidFileNameChars())
            if (fileName.Contains(c.ToString()))
                return false;

        try
        {
            if (!Directory.Exists(directoryPath))
                return false;

            var filePath = Path.Combine(directoryPath, fileName);
            if (File.Exists(filePath))
                return true;

            using var stream = File.Create(filePath);
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

    public static bool Mover(string filePath, string destinationDirectory)
    {
        if (string.IsNullOrWhiteSpace(filePath))
            return false;
        if (string.IsNullOrWhiteSpace(destinationDirectory))
            return false;

        try
        {
            if (!File.Exists(filePath) || !Directory.Exists(destinationDirectory))
                return false;

            var destinationFile = Path.Combine(destinationDirectory, Path.GetFileName(filePath));
            if (File.Exists(destinationFile))
                return false;

            File.Move(filePath, destinationFile);
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

    public static bool Deletar(string filePath)
    {
        if (string.IsNullOrWhiteSpace(filePath))
            return false;

        try
        {
            if (!File.Exists(filePath))
                return false;

            File.Delete(filePath);
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

    public static bool CopiarArquivo(string filePath, string destinationDirectory, bool isCut = false)
    {
        if (string.IsNullOrWhiteSpace(filePath))
            return false;
        if (string.IsNullOrWhiteSpace(destinationDirectory))
            return false;

        try
        {
            if (!File.Exists(filePath) || !Directory.Exists(destinationDirectory))
                return false;

            var destinationFile = Path.Combine(destinationDirectory, Path.GetFileName(filePath));

            if (isCut)
                File.Move(filePath, destinationFile, true);
            else
                File.Copy(filePath, destinationFile, true);

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
