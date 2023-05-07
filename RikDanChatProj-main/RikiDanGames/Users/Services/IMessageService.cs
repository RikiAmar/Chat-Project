namespace Users.Services;

public interface IMessageService
{
	MessageModel SendMessageToDBAndUser(MessageModel message);

	IEnumerable<MessageModel> GetMessagesBasedOnRoom(MessageModel messageModel);

	void DeleteInvitationMessageFromDB(Room room);
}