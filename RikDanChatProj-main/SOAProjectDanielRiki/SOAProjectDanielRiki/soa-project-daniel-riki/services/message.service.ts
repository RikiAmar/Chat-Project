import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { MessageModel } from 'src/app/models/messageModel';
import { Room } from 'src/app/models/room.model';
import User from 'src/app/models/user.model';

const MessagesController = "http://localhost:5228/Messages/";

@Injectable()

export class MessageService {

constructor(private http: HttpClient) {

}

sendMsg(messageModel: MessageModel) : Observable<MessageModel>{
  return this.http.post<MessageModel>(MessagesController+'Sendmsg',messageModel);
}

getfilteredMessageArray(messageModel: MessageModel) : Observable<MessageModel[]>{
  return this.http.post<MessageModel[]>(MessagesController+'filteredArray', messageModel);
}

createRoom(meesageModel: MessageModel) : Observable<User>{
  return this.http.post<User>(MessagesController+'createRoom', meesageModel);
}

handleInvitationMessage(room : Room) {
  return this.http.post(MessagesController+'deleteInvitationMessage' ,room);
}
}
