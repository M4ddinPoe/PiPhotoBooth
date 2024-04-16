namespace PiPhotoBoot;

using System.Runtime.InteropServices;

public static class OsEnvironment
{
    public static string CommandLineFileName =>
        RuntimeInformation.IsOSPlatform(OSPlatform.Windows) 
            ? "cmd.exe" 
            : "/bin/bash";

    public static string CreateCommandExecution(string command)
    {
        return RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
            ? $"/c {command}"
            : $"-c \"{command}\"";
    }
}