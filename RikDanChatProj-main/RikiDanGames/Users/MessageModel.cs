namespace Users
{
	public class MessageModel
	{
		public MessageModel()
		{
			date = DateTime.Now.ToString();
		}

		public string content { get; set; } = string.Empty;
		public int selectedUserId { get; set; } = 0;
		public int userId { get; set; } = 0;
		public string date { get; set; } = "";
		public Room room { get; set; }
	}
}