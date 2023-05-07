using Microsoft.AspNetCore.Mvc;
using Users.Services;

namespace Users.Controllers
{
	[Route("[controller]")]
	[ApiController]
	public class RoomsController : ControllerBase
	{
		private IRoomService roomService;

		public RoomsController(IRoomService roomService)
		{
			this.roomService = roomService;
		}

		[HttpPost("getRoom")]
		public ActionResult<Room> GetRoom(Room room)
		{
			var updatedRoom = roomService.GetRoomId(room);
			return Ok(updatedRoom);
		}
	}
}