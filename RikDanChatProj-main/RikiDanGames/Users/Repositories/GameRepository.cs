namespace Users.Repositories;

public class GameRepository : IGameRepository
{
	private Dictionary<string, List<GameModel>> ticTacToeDb = new()
	{
		//{ "1-2", new List<GameModel>{new Room {myId=0, selectedId=0 },
		//                      new GameModel {currentCell = 1, userId= 0},
		//                      new GameModel {currentCell = 2, userId= 0},
		//                      new GameModel {currentCell = 3, userId= 0},
		//                      new GameModel {currentCell = 4, userId= 0},
		//                      new GameModel {currentCell = 5, userId= 0},
		//                      new GameModel {currentCell = 6, userId= 0},
		//                      new GameModel {currentCell = 1, userId= 0},
		//                      new GameModel {currentCell = 1, userId= 0},
		//                      new GameModel {currentCell = 1, userId= 0}}}
	};

	private Dictionary<string, List<int>> roomsForGames = new()
	{
	};

	public Dictionary<string, List<int>> GetwaitingUserList() => roomsForGames;

	public void AddNewWaitingUserList(Room room) => roomsForGames.Add(room.myRoomName, new List<int> { room.myId });

	public void AddToExistingKey(Room room) => roomsForGames.FirstOrDefault(x => x.Key == room.selectedRoomName).Value.Add(room.myId);

	public List<int> GetExistingIds(Room room) => roomsForGames.FirstOrDefault(x => x.Key == room.myRoomName || x.Key == room.selectedRoomName).Value.ToList();

	public KeyValuePair<string, List<int>> GetRoomByKey(string key) => roomsForGames.FirstOrDefault(x => x.Key == key);

	public void DeleteRoomByKey(string key) => roomsForGames.Remove(key);

	public void DeleteGameByKey(string key) => ticTacToeDb.Remove(key);

	public KeyValuePair<string, List<GameModel>> GetTicTacToeGame(Room room) => ticTacToeDb.FirstOrDefault(x => x.Key == room.myRoomName || x.Key == room.selectedRoomName);

	public List<GameModel> GetTicTacToeValue(Room room) => ticTacToeDb.FirstOrDefault(x => x.Key == room.myRoomName || x.Key == room.selectedRoomName).Value;

	public void CreateGameDB(string key)
	{
		var newListGameModel = new List<GameModel>();
		for (int cell = 0; cell < 9; cell++)
		{
			var currentCell = new GameModel { currentCell = cell, userId = 0 };
			newListGameModel.Add(currentCell);
		}
		ticTacToeDb.Add(key, newListGameModel);
	}

	public string GetKeyFromRoomsForGames(Room room) => roomsForGames.FirstOrDefault(x => x.Key == room.myRoomName || x.Key == room.selectedRoomName).Key;

	public List<GameModel> GetFullGrid(string key) => ticTacToeDb.FirstOrDefault(x => x.Key == key).Value;
}