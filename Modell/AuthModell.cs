namespace AngularApi.Modell
{
    public class AuthModell
    {
        public string? Massage { get; set; }
        public bool IsAuth { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public List<string>? Role { get; set; }
        public string? Token { get; set;}
        public DateTime expiration { get; set; }
    }
}
