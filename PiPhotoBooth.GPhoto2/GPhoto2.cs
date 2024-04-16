namespace PiPhotoBoot;

using Commands;

public class GPhoto2
{
    private readonly AutoDetect autoDetect = new();
    private readonly CaptureImageAndDownload captureImageAndDownload = new (); 
    
    public List<string> AutoDetect()
    {
        return this.autoDetect.Execute();
    }

    public void CaptureImageAndDownload(string filename)
    {
        this.captureImageAndDownload.Execute(filename);
    }
}