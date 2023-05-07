namespace Users.Repositories
{
	public class MessageRepository : IMessageRepository
	{
		private IRoomRepository roomRepository;

		private Dictionary<int, MessageModel> messageDictionary = new()
		{
			{ 1, new MessageModel {userId = 1, selectedUserId = 2, content = "שלום זאת ריקי", date = "01/12/2022 09:24:20" , room = new Room {myRoomName="1-2" , selectedRoomName="2-1"}}},
			{ 2, new MessageModel {userId = 1, selectedUserId = 2, content = "מה נשמע?", date = "02/12/2022 19:44:24", room = new Room {myRoomName="1-2" , selectedRoomName="2-1"}}},
			{ 3, new MessageModel {userId = 2, selectedUserId = 1, content = "שלום זה דניאל", date = "03/12/2022 20:44:24", room = new Room {myRoomName="2-1" , selectedRoomName="1-2"}}},
			{ 4, new MessageModel {userId = 3, selectedUserId = 4, content = "בלה בלה בלה", date = "04/12/2022 22:44:24", room = new Room {myRoomName="3-4" , selectedRoomName="4-3"}}},
		};

		public MessageRepository(IRoomRepository roomRepository)
		{
			this.roomRepository = roomRepository;
		}

		public IEnumerable<MessageModel> GetAllMessages() => messageDictionary.Values.ToList();

		public Dictionary<int, MessageModel> GetAllMessagesWithKeys() => messageDictionary;

		public void AddMessageToDB(MessageModel message) => messageDictionary.Add(messageDictionary.LastOrDefault().Key + 1, message);

		public void DeleteInvitationMessageFromDB(int myMessageKey, int otherMessageKey)
		{
			messageDictionary.Remove(myMessageKey);
			messageDictionary.Remove(otherMessageKey);
		}
	}
}