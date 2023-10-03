namespace DotnetLabVar17.Models;

public class Config
{
    public Config(string? logPath)
    {
        this._logPath = logPath;
      
    }

    private readonly string? _logPath;
    public Exception? Exception { get; set; }
    public string LogPath => new string(_logPath);
}