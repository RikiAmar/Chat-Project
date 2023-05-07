namespace Users.Repositories
{
	public interface IUserRepository
	{
		List<User> GetUsers();

		User? GetUserById(int id);

		User? GetUserByName(string username);

		void AddUserToDB(User user);
	}
}