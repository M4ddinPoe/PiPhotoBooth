namespace PiPhotoBooth.Services;

public static class OSEnvironment
{
    private static Lazy<string> LazyDataDirectory = new(() =>
    {
        if (System.OperatingSystem.IsWindows())
        {
            var fullPath = Path.Combine("%Appdata%", "PiPhotoBooth");

            if (!Directory.Exists(fullPath))
            {
                Directory.CreateDirectory(fullPath);
            }

            return Environment.ExpandEnvironmentVariables("%Appdata%");
        }

        return Environment.CurrentDirectory;
    });

    public static string DataDirectory => LazyDataDirectory.Value;
}