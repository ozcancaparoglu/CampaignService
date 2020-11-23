namespace CampaignService.Logging
{
    public interface ILoggerManager
    {
        void LogDebug(string message);
        void LogError(string message);
        void LogInfo(string message);
        string MessageFormatter(string message);
    }
}