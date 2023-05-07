namespace Users.Repositories
{
	public class RoomRepository : IRoomRepository
	{
		private Dictionary<int, Room> rooms = new()
		{
			{ 1, new Room { myId= 1, selectedId=2, myRoomName="1-2", selectedRoomName="2-1" }},
			{ 2, new Room { myId= 2, selectedId=2, myRoomName="2-1", selectedRoomName="1-2" }},
			{3, new Room { myId= 3, selectedId=4, myRoomName="3-4", selectedRoomName="4-3" }},
			{ 4, new Room { myId= 4, selectedId=3, myRoomName="4-3", selectedRoomName="3-4" }}
		};

		public IEnumerable<Room> GetRooms() => rooms.Values.ToList();

		public Room? GetRoomById(int Userid, int SelectedId) => rooms.Values.FirstOrDefault(room => room.myId == Userid && room.selectedId == SelectedId);

		public void AddRoomInDB(Room room) => rooms.Add(rooms.Count + 1, room);
	}
}