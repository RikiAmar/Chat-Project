using Microsoft.AspNetCore.Mvc;
using Users.Services;

namespace Users.Controllers
{
	[Route("[controller]")]
	[ApiController]
	public class UsersController : ControllerBase
	{
		private IUserService userService;

		public UsersController(IUserService userService)
		{
			this.userService = userService;
		}

		[HttpGet("{id}")]
		public ActionResult<User> GetUserById(int id) => Ok(userService.GetUserById(id));

		[HttpPost("SendToken")]
		public ActionResult<User> SendTokenToUser(User user)
		{
			var updatedUser = userService.AddTokenToUser(user);
			return Ok(updatedUser);
		}

		[HttpPost("UsersByLoggedIn")]
		public ActionResult<IEnumerable<User>> GetAllUsersOrderedByLoginStatus(User user) => Ok(userService.GetAllUsersOrderedByLoginStatus(user));

		[HttpPut("reset")]
		public ActionResult<User> ResetUserStatus(User user)
		{
			userService.ResetUserStatus(user);
			return Ok();
		}

		[HttpPost("register")]
		public ActionResult<bool> Register(User user)
		{
			var isUserRegistered = userService.VerifyRegistration(user);
			return Ok(isUserRegistered);
		}

		[HttpPost("login")]
		public ActionResult<User> Login(User user)
		{
			var updatedUser = userService.VerifyLogin(user);
			if (updatedUser != null)
				return Ok(updatedUser);

			return Ok(user);
		}
	}
}