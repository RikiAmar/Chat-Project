using Users.Repositories;

namespace Users.Services;

public class RoomService : IRoomService
{
	private readonly IRoomRepository roomRepository;

	public RoomService(IRoomRepository roomRepository)
	{
		this.roomRepository = roomRepository;
	}

	public Room GetRoomId(Room room)
	{
		Room? roomFromDB = roomRepository.GetRoomById(room.myId, room.selectedId);
		if (!IsRoomFoundInDB(room))
		{
			var updatedroom = CreateRoomInDB(room);
			return updatedroom;
		}
		return roomFromDB;
	}

	public Room CreateRoomInDB(Room room)
	{
		var myRoomName = $"{room.myId}-{room.selectedId}";
		var selectedRoomName = $"{room.selectedId}-{room.myId}";
		room.myRoomName = myRoomName;
		room.selectedRoomName = selectedRoomName;

		roomRepository.AddRoomInDB(room);

		var updatedRoom = roomRepository.GetRoomById(room.myId, room.selectedId);
		return updatedRoom!;
	}

	private bool IsRoomFoundInDB(Room room)
	{
		var roomFromDB = roomRepository.GetRoomById(room.myId, room.selectedId);
		if (roomFromDB != null)
			return true;

		return false;
	}
}