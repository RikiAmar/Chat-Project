namespace Users
{
	public class User
	{
		public int Id { get; set; }
		public string Username { get; set; } = "";
		public string Password { get; set; } = "";
		public bool IsLoggedIn { get; set; } = false;
		public string Token { get; set; } = string.Empty;
	}
}