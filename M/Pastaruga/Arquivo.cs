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
}