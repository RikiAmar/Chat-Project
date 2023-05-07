using Azure.Messaging.WebPubSub;

namespace Users.Services
{
	public interface IUserService
	{
		bool VerifyRegistration(User user);

		User VerifyLogin(User user);

		User? GetUserById(int id);

		IEnumerable<User> GetAllUsersOrderedByLoginStatus(User user);

		User ResetUserStatus(User user);

		User AddTokenToUser(User user);

		WebPubSubServiceClient GetClient();
	}
}