namespace PiPhotoBoot.Commands;

using System.Diagnostics;
using System.Text;

public abstract class BaseCommand
{
    protected abstract string Command { get; set; }
    
    protected Task<string> ExecuteCommandAsync()
    {
        return Task<string>.Run(() =>
        {
            var processStartInfo = new ProcessStartInfo
            {
                FileName = OsEnvironment.CommandLineFileName,
                Arguments = OsEnvironment.CreateCommandExecution(this.Command),
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            var output = new StringBuilder("");
            using (var process = new Process())
            {
                process.StartInfo = processStartInfo;
                process.OutputDataReceived += (sender, args) => output.AppendLine(args.Data);
                process.ErrorDataReceived += (sender, args) => output.AppendLine(args.Data);

                process.Start();
                process.BeginOutputReadLine();
                process.BeginErrorReadLine();
                process.WaitForExit();
            }
            
            return output.ToString();
        });
    }
}