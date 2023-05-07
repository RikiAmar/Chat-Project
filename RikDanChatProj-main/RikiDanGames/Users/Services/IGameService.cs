namespace Users.Services;

public interface IGameService
{
	void RemoveRoomFromDb(Room room);
	List<int> GetCurrentWaitingUserList(Room room);
	bool AreUsersConnected(Room room);
	List<string> GetGridByRoom(Room room);
	bool HasTheGridBeenUpdated(GameModel gameModel);
	int GetIdOfCurrentTurn();
	int GetCounterToEndGame();
	bool IsThereAWinner(Room room);
	void UpdateLoser(Room room);
	int GetIdOfLoser();
	void GameEnded();
	bool GetGameEndedStatus();
	bool GetGameStartedStatusFromDB();
}