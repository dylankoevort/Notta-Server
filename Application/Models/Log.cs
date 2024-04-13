namespace Models;

public class Log
{
    public int LogId { get; set; }
    public int UserId { get; set; }
    public int LogTypeId { get; set; }
    public string LogContent { get; set; }
    public string LogResult { get; set; }
    public DateTime LogDate { get; set; }
}