namespace Users.Repositories
{
	public interface IGameRepository
	{
		List<int> GetExistingIds(Room room);

		Dictionary<string, List<int>> GetwaitingUserList();

		void AddNewWaitingUserList(Room room);

		void AddToExistingKey(Room room);

		KeyValuePair<string, List<int>> GetRoomByKey(string key);

		void DeleteRoomByKey(string key);

		KeyValuePair<string, List<GameModel>> GetTicTacToeGame(Room room);

		void CreateGameDB(string key);

		string GetKeyFromRoomsForGames(Room room);

		List<GameModel> GetFullGrid(string key);

		List<GameModel> GetTicTacToeValue(Room room);

		void DeleteGameByKey(string myRoomName);
	}
}