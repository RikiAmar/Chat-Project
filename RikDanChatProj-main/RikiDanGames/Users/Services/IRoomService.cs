namespace Users.Services;

public interface IRoomService
{
	Room GetRoomId(Room room);

	Room CreateRoomInDB(Room room);
}