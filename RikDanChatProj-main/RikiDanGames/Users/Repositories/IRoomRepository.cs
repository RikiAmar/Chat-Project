namespace Users.Repositories
{
	public interface IRoomRepository
	{
		IEnumerable<Room> GetRooms();

		Room? GetRoomById(int Userid, int SelectedId);

		void AddRoomInDB(Room room);
	}
}