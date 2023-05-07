using Microsoft.AspNetCore.Mvc;
using Users.Services;

namespace Users.Controllers
{
	[Route("[controller]")]
	[ApiController]
	public class MessagesController : ControllerBase
	{
		private IMessageService messageService;

		public MessagesController(IMessageService messageService)
		{
			this.messageService = messageService;
		}

		[HttpPost("Sendmsg")]
		public ActionResult<MessageModel> SendMessageToUser(MessageModel message)
		{
			var updatedMessage = messageService.SendMessageToDBAndUser(message);
			return Ok(updatedMessage);
		}

		[HttpPost("filteredArray")]
		public ActionResult<IEnumerable<MessageModel>> FilterMessageArray(MessageModel messageModel)
		{
			var filteredMessageList = messageService.GetMessagesBasedOnRoom(messageModel);
			return Ok(filteredMessageList);
		}

		[HttpPost("deleteInvitationMessage")]
		public IActionResult DeleteInvitationMessage(Room room)
		{
			messageService.DeleteInvitationMessageFromDB(room);
			return Ok();
		}
	}
}