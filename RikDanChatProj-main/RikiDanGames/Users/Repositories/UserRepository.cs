namespace Users.Repositories
{
	public class UserRepository : IUserRepository
	{
		private Dictionary<int, User> usersDictionary = new()
		{
			{ 1, new User { Username = "riki", Password = "1234", Id = 1 }},
			{ 2, new User { Username = "daniel", Password = "1234", Id = 2 }},
			{ 3, new User { Username = "moshe", Password = "1234", Id = 3} },
			{ 4, new User { Username = "Rose", Password = "1234", Id = 4} }
		};

		public UserRepository()
		{
		}

		public List<User> GetUsers() => usersDictionary.Values.ToList();

		public User? GetUserById(int id) => usersDictionary.Values.FirstOrDefault(x => x.Id == id);

		public User? GetUserByName(string username) => usersDictionary.Values.FirstOrDefault(x => x.Username == username);

		public void AddUserToDB(User user) => usersDictionary.Add(usersDictionary.Count + 1, user);
	}
}