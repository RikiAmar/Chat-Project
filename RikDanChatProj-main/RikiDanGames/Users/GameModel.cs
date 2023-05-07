namespace Users;

public class GameModel
{
	public int userId { get; set; }///מסמל מי לחץ על הכפתור איקס או עיגול
	public int whoStartedGame { get; set; } = 0;
	public int whoseTurn { get; set; } = 0;
	public int currentCell { get; set; }
	public Room room { get; set; }
}