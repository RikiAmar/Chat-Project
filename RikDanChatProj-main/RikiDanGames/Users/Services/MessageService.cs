using Azure.Core;
using Users.Repositories;

namespace Users.Services;

public class MessageService : IMessageService
{
	private readonly IMessageRepository messageRepository;
	private readonly IUserService userService;

	public MessageService(IMessageRepository messageRepository, IUserService userService)
	{
		this.messageRepository = messageRepository;
		this.userService = userService;
	}

	public IEnumerable<MessageModel> GetMessagesBasedOnRoom(MessageModel messageModel)
	{
		var orderedList = messageRepository.GetAllMessages().Where(msgInDB =>
		(msgInDB.room.myRoomName == messageModel.room.myRoomName) || (msgInDB.room.myRoomName == messageModel.room.selectedRoomName));

		return orderedList;
	}

	public MessageModel SendMessageToDBAndUser(MessageModel message)
	{
		var updatedMessage = UpdateDateInMessageModel(message);
		messageRepository.AddMessageToDB(updatedMessage);
		SendMessageToUser(updatedMessage);
		return updatedMessage;
	}

	private MessageModel UpdateDateInMessageModel(MessageModel message)
	{
		var updatedMessage = message;
		message.date = DateTime.Now.ToString();
		return updatedMessage;
	}

	private void SendMessageToUser(MessageModel message)
	{
		var client = userService.GetClient();
		client.SendToUser($"{message.selectedUserId}", RequestContent.Create(new { msg = message }), ContentType.ApplicationJson);
	}

	public void DeleteInvitationMessageFromDB(Room room)
	{
		var invitationMessage = "Click on the picture to enter the game.The invitation will expire in 60 seconds";
		var allMessages = messageRepository.GetAllMessagesWithKeys();
		var myMessage = allMessages.FirstOrDefault(x => x.Value.userId == room.myId && x.Value.content == invitationMessage);
		var otherMessage = allMessages.FirstOrDefault(x => x.Value.selectedUserId == room.selectedId && x.Value.content == invitationMessage);
		messageRepository.DeleteInvitationMessageFromDB(myMessage.Key, otherMessage.Key);
	}
}