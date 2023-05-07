using Azure.Messaging.WebPubSub;
using Users.Repositories;

namespace Users.Services;

public class UserService : IUserService
{
	private IUserRepository userRepository;

	public UserService(IUserRepository userRepository)
	{
		this.userRepository = userRepository;
	}

	public bool VerifyRegistration(User user)
	{
		if (IsUsernameExistsInDB(user) && IsUsernameValid(user))
		{
			user.Id = userRepository.GetUsers().Count + 1;
			userRepository.AddUserToDB(user);
			return true;
		}
		return false;
	}

	public User VerifyLogin(User user)
	{
		var DBUser = userRepository.GetUserByName(user.Username);

		if ((DBUser?.Username == user.Username) && (DBUser?.Password == user.Password))
		{
			DBUser.IsLoggedIn = true;
			return DBUser;
		}
		return user;
	}

	public User? GetUserById(int id) => userRepository.GetUserById(id);

	public IEnumerable<User> GetAllUsersOrderedByLoginStatus(User user)
	{
		VerifyTokensInRegisteredUsers();

		return userRepository.GetUsers()
							 .OrderByDescending(x => x.IsLoggedIn)
							 .Where(x => x.Id != user.Id);
	}

	public User ResetUserStatus(User user)
	{
		var updatedUser = userRepository.GetUserById(user.Id);
		updatedUser!.IsLoggedIn = false;
		return updatedUser;
	}

	public User AddTokenToUser(User user)
	{
		var updatedUser = userRepository.GetUserById(user.Id);
		updatedUser!.Token = GenerateToken(updatedUser);
		return updatedUser;
	}

	public WebPubSubServiceClient GetClient()
	{
		var connectionstring = "Endpoint=https://rikidan-games.webpubsub.azure.com;AccessKey=VwYMrAb7YkzK8sRt+mYp3AtSHs1axhRhOpHQfnJWWms=;Version=1.0;";
		var hubName = "Hub";
		var serviceClient = new WebPubSubServiceClient(connectionstring, hubName);

		return serviceClient;
	}

	private string GenerateToken(User user)
	{
		var serviceClient = GetClient();
		return serviceClient.GetClientAccessUri(TimeSpan.FromHours(72), $"{user.Id}").OriginalString;
	}

	private void VerifyTokensInRegisteredUsers()
	{
		foreach (var user in userRepository.GetUsers())
		{
			if (user.Token == "") AddTokenToUser(user);
		}
	}

	private bool IsUsernameExistsInDB(User user)
	{
		var DBUser = userRepository.GetUserByName(user.Username);
		if (DBUser == null) return true;
		return false;
	}

	private bool IsUsernameValid(User user)
	{
		if (!string.IsNullOrEmpty(user.Username) && !string.IsNullOrEmpty(user.Password))
			return true;

		return false;
	}
}