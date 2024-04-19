namespace GPhoto2.ResponseParser;

internal sealed class AutoDetectParser
{
    public List<string> Execute(string response)
    {
        var models = new List<string>();
        string[] lines = response.Split(Environment.NewLine);

        foreach (string line in lines)
        {
            if (line.Trim().Length == 0 || line.Contains("Model") || line.Contains("---"))
            {
                continue;
            }

            var parts = line.Split(new[] { "   " }, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length > 1)
            {
                models.Add(parts[0]);
            }
        }

        return models;
    }
}