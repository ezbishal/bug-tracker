namespace Server.Authentication;

public class ApiKeyModel
{
    public string Id { get; set; }
    public string Key { get; set; }
    public string UserId { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime ExpiryDate { get; set; }
}