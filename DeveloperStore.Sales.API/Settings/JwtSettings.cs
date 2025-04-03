namespace DeveloperStore.Sales.API.Settings
{
    public class JwtSettings
    {
        public string Key { get; set; } = string.Empty;
        public int ExpireHours { get; set; } = 2;
        public string Issuer { get; set; } = string.Empty;
        public string Audience { get; set; } = string.Empty;
    }
}
