import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { MessageModel } from 'src/app/models/messageModel';
import { Room } from 'src/app/models/room.model';
import User from 'src/app/models/user.model';

const RoomsController = "http://localhost:5228/Rooms/";

@Injectable()

export class RoomService {

constructor(private http: HttpClient) {

}

 
getRoom(room: Room) : Observable<Room>{
  return this.http.post<Room>(RoomsController+'getRoom',room);
}

sendMsg(messageModel: MessageModel) : Observable<MessageModel>{
  return this.http.post<MessageModel>(RoomsController+'Sendmsg',messageModel);
}

getfilteredMessageArray(messageModel: MessageModel) : Observable<MessageModel[]>{
  return this.http.post<MessageModel[]>(RoomsController+'filteredArray', messageModel);
}

}