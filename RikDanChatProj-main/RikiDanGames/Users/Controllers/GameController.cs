using Microsoft.AspNetCore.Mvc;
using Users.Services;

namespace Users.Controllers
{
	[Route("[controller]")]
	[ApiController]
	public class GameController : ControllerBase
	{
		private IGameService gameService;

		public GameController(IGameService gameService)
		{
			this.gameService = gameService;
		}

		[HttpPost("getCurrentPlayers")]
		public ActionResult<List<int>> GetCurrentPlayers(Room room)
		{
			var list = gameService.GetCurrentWaitingUserList(room);
			return Ok(list);
		}

		[HttpPost("removeRoomFromTicTacToeDb")]
		public IActionResult RemoveRoomFromTicTacToeDb(Room room)
		{
			gameService.RemoveRoomFromDb(room);
			return Ok();
		}

		[HttpPost("checkIfTwoUsersConnected")]
		public ActionResult<bool> CheckIfTwoUsersConnected(Room room) => Ok(gameService.AreUsersConnected(room));

		[HttpPost("getGridConstantly")]
		public ActionResult<List<string>> GetGridConstantly(Room room) => Ok(gameService.GetGridByRoom(room));

		[HttpPost("updateGrid")]
		public ActionResult<bool> UpdateGrid(GameModel gameModel) => Ok(gameService.HasTheGridBeenUpdated(gameModel));

		[HttpGet("sendTurnFromDB")]
		public ActionResult<int> SendTurnFromDB() => Ok(gameService.GetIdOfCurrentTurn());

		[HttpGet("getCounterToEndGame")]
		public ActionResult<int> GetCounterToEndGame() => Ok(gameService.GetCounterToEndGame());

		[HttpPost("calculateWinner")]
		public ActionResult<int> CalculateWinner(Room room) => Ok(gameService.IsThereAWinner(room));

		[HttpPost("updateLoser")]
		public IActionResult UpdateLoser(Room room)
		{
			gameService.UpdateLoser(room);
			return Ok();
		}

		[HttpGet("getIdOfLoser")]
		public ActionResult<int> GetIdOfLoser() => Ok(gameService.GetIdOfLoser());

		[HttpGet("updateGameEndedStatus")]
		public IActionResult UpdateGameEndedStatus()
		{
			gameService.GameEnded();
			return Ok();
		}

		[HttpGet("getGameEndedStatus")]
		public ActionResult<bool> GetGameEndedStatus() => Ok(gameService.GetGameEndedStatus());

		[HttpGet("getGameStartedStatus")]
		public ActionResult<bool> GetGameStartedStatus()
		{
			return Ok(gameService.GetGameStartedStatusFromDB());
		}
	}
}