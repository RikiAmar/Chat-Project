namespace Users.Repositories
{
	public interface IMessageRepository
	{
		IEnumerable<MessageModel> GetAllMessages();

		void AddMessageToDB(MessageModel message);

		void DeleteInvitationMessageFromDB(int myMessageKey, int otherMessageKey);

		Dictionary<int, MessageModel> GetAllMessagesWithKeys();
	}
}