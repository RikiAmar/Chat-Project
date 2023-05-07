using Users.Repositories;

namespace Users.Services;

public class GameService : IGameService
{
	private readonly IGameRepository gameRepository;
	private int turnNowInTheGame;
	private int counterToEndGame;
	private int idLoser = 0;
	private bool hasGameEnded;
	private bool hasGameStarted = false;

	public GameService(IGameRepository gameRepository)
	{
		this.gameRepository = gameRepository;
	}

	public List<int> GetCurrentWaitingUserList(Room room)
	{
		var myRoom = gameRepository.GetRoomByKey(room.myRoomName);
		var otherRoom = gameRepository.GetRoomByKey(room.selectedRoomName);
		if (VerifyIfRoomExistsInWaitingList(otherRoom))
		{
			if (otherRoom.Value.Count != 2 && otherRoom.Value.FirstOrDefault() != room.myId)
				gameRepository.AddToExistingKey(room);

			return otherRoom.Value!;
		}
		else if (VerifyIfRoomExistsInWaitingList(myRoom))
		{
			if (myRoom.Value.Count != 2 && myRoom.Value == null)
				gameRepository.AddToExistingKey(room);

			return myRoom.Value!;
		}
		else if (!VerifyIfRoomExistsInWaitingList(otherRoom) && !VerifyIfRoomExistsInWaitingList(myRoom))
		{
			gameRepository.AddNewWaitingUserList(room);
			return gameRepository.GetExistingIds(room);
		}
		return new List<int>();
	}

	public void RemoveRoomFromDb(Room room)
	{
		var otherRoom = gameRepository.GetRoomByKey(room.selectedRoomName);
		var myRoom = gameRepository.GetRoomByKey(room.myRoomName);
		if (VerifyIfRoomExistsInWaitingList(otherRoom))
		{
			RemoveGame(room.selectedRoomName);
			gameRepository.DeleteRoomByKey(otherRoom.Key);
			hasGameStarted = false;
		}

		if (VerifyIfRoomExistsInWaitingList(myRoom))
		{
			RemoveGame(room.myRoomName);
			gameRepository.DeleteRoomByKey(myRoom.Key);
			hasGameStarted = false;
		}
	}

	private void RemoveGame(string myRoomName) => gameRepository.DeleteGameByKey(myRoomName);

	private bool VerifyIfRoomExistsInWaitingList(KeyValuePair<string, List<int>> room)
	{
		if (room.Key != null)
			return true;

		return false;
	}

	public bool AreUsersConnected(Room room)
	{
		var myRoom = gameRepository.GetRoomByKey(room.myRoomName);
		var otherRoom = gameRepository.GetRoomByKey(room.selectedRoomName);

		if ((VerifyIfRoomExistsInWaitingList(otherRoom) && otherRoom.Value.Count == 2) ||
			VerifyIfRoomExistsInWaitingList(myRoom) && myRoom.Value.Count == 2)
			return true;

		return false;
	}

	public List<string> GetGridByRoom(Room room)
	{
		if (hasGameStarted == false)
			hasGameStarted = true;

		var game = gameRepository.GetTicTacToeGame(room);
		var playerList = gameRepository.GetExistingIds(room);
		if (!VerifyIfRoomExistsInGameDB(game))
		{
			var key = GetKeyFromRoom(room);
			if (key != null && playerList.Count == 2)
			{
				gameRepository.CreateGameDB(key);
				var listGameModels = gameRepository.GetFullGrid(key);
				var stringListForUI = ConvertGameModelListToString(listGameModels, key);
				return stringListForUI;
			}
		}
		else
		{
			var stringListForUI = ConvertGameModelListToString(game.Value, game.Key);
			return stringListForUI;
		}
		return new List<string>();
	}

	private List<string> ConvertGameModelListToString(List<GameModel> fullList, string key)
	{
		var xId = Convert.ToInt32(key[0].ToString());
		var oId = Convert.ToInt32(key[2].ToString());
		var stringList = new List<string>();

		foreach (var gm in fullList)
		{
			if (gm.userId == xId)
				stringList.Add("X");
			else if (gm.userId == oId)
				stringList.Add("O");
			else
				stringList.Add("");
		}
		return stringList;
	}

	private string GetKeyFromRoom(Room room) => gameRepository.GetKeyFromRoomsForGames(room);

	private bool VerifyIfRoomExistsInGameDB(KeyValuePair<string, List<GameModel>> game)
	{
		if (game.Key != null)
			return true;

		return false;
	}

	//עובד טוב
	public bool HasTheGridBeenUpdated(GameModel gameModel)
	{
		//נשיג את הגייםמודל מהדאטה של המשחק
		var userIdInCurrentCell = gameRepository.GetTicTacToeValue(gameModel.room).First(x => x.currentCell == gameModel.currentCell);

		//נבדוק האם התא נתפס - אם יש שם ת.ז == 0 סימן שהתא עוד לא נתפס ולכן אפשר לעדכן את הדאטה
		switch (userIdInCurrentCell.userId)
		{
			case 0:
				//ונעדכן את הדאטה בת.ז החדשה
				userIdInCurrentCell.userId = gameModel.room.myId;
				turnNowInTheGame = gameModel.room.selectedId;
				counterToEndGame++;
				return true;

			default:
				return false;
		}
	}

	public int GetIdOfCurrentTurn() => turnNowInTheGame;

	public int GetCounterToEndGame() => counterToEndGame;

	public bool IsThereAWinner(Room room)
	{
		var game = gameRepository.GetTicTacToeValue(room);
		var gameDetailsFirstUser = game.FindAll(x => x.userId == room.myId)
									   .Select(x => x.currentCell).ToList();

		if (CalculateWinner(gameDetailsFirstUser))
		{
			turnNowInTheGame = 0;
			counterToEndGame = 0;
			return true;
		}

		return false;
	}

	private bool CalculateWinner(List<int> gameDetailsFirstUser)
	{
		if (gameDetailsFirstUser.Contains(0) && gameDetailsFirstUser.Contains(1) && gameDetailsFirstUser.Contains(2) ||
			gameDetailsFirstUser.Contains(3) && gameDetailsFirstUser.Contains(4) && gameDetailsFirstUser.Contains(5) ||
			gameDetailsFirstUser.Contains(6) && gameDetailsFirstUser.Contains(7) && gameDetailsFirstUser.Contains(8) ||
			gameDetailsFirstUser.Contains(0) && gameDetailsFirstUser.Contains(3) && gameDetailsFirstUser.Contains(6) ||
			gameDetailsFirstUser.Contains(1) && gameDetailsFirstUser.Contains(4) && gameDetailsFirstUser.Contains(7) ||
			gameDetailsFirstUser.Contains(2) && gameDetailsFirstUser.Contains(5) && gameDetailsFirstUser.Contains(8) ||
			gameDetailsFirstUser.Contains(0) && gameDetailsFirstUser.Contains(4) && gameDetailsFirstUser.Contains(8) ||
			gameDetailsFirstUser.Contains(2) && gameDetailsFirstUser.Contains(4) && gameDetailsFirstUser.Contains(6))
			return true;

		return false;
	}

	public void UpdateLoser(Room room) => idLoser = room.selectedId;

	public int GetIdOfLoser() => idLoser;

	public void GameEnded() => hasGameEnded = true;

	public bool GetGameEndedStatus() => hasGameEnded;

	public bool GetGameStartedStatusFromDB() => hasGameStarted;
}