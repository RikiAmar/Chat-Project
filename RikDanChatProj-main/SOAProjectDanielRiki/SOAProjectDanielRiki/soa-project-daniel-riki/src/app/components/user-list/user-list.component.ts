import { Component, EventEmitter, HostListener, Input, OnChanges, OnInit, Output, SimpleChanges } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { interval, Observable } from 'rxjs';
import { webSocket } from 'rxjs/webSocket';
import { MessageService } from 'services/message.service';
import { RoomService } from 'services/room.service';
import UserService from 'services/user.service';
import { MessageModel } from 'src/app/models/messageModel';
import { Room } from 'src/app/models/room.model';
import User from 'src/app/models/user.model';

@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.css']
})
export class UserListComponent {

  userList: User[] = [];
  
  @Input() user = new User();
  userSelected = new User();
  room = new Room();
  @Output() transerUserSelectedToLobby = new EventEmitter<User>();
  @Output() transerRoomToLobby = new EventEmitter<Room>();

  constructor(private userService: UserService, private messageService: MessageService, private router: ActivatedRoute, private roomService: RoomService) {
    interval(1000).subscribe(x => {
      this.getUsersByLoggedIn();      
    })
    }


  getUsersByLoggedIn() {
    this.userService.getAllUsersByLoggedIn(this.user).subscribe(updatedUser => {
      this.userList = updatedUser;      
    })
  }
 
  clickToShowChat(id: number) {
    this.userService.getById(id).subscribe(user => {
      this.userSelected = user;
      this.setRoomParams();
      this.getRoomFromDB();
    })
  }

  setRoomParams() {
    this.room.myId = this.user.id;
    this.room.selectedId = this.userSelected.id;
  }

  getRoomFromDB() {
    this.roomService.getRoom(this.room).subscribe(room => {
      this.transerRoomToLobby.emit(room);
    })
  }

  @HostListener('window:popstate', ['$event'])
  onPopState() {
    this.resetUserState();
  }
  @HostListener('window:beforeunload', ['$event'])
   onWindowClose() {
    this.resetUserState();
  }

  resetUserState() {
    this.userService.resetStatus(this.user).subscribe();
  }

}
