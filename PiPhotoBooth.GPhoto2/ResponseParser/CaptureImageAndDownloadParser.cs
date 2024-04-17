namespace PiPhotoBoot.ResponseParser;

using ResultMonad;

public class CaptureImageAndDownloadParser
{
    private const string ErrorIndicator = "*** Fehler";

    public ResultWithError<string> Execute(string response)
    {
        if (response.Contains(ErrorIndicator))
        {
            return ResultWithError.Fail(response);
        }

        return ResultWithError.Ok<string>();
    }
}