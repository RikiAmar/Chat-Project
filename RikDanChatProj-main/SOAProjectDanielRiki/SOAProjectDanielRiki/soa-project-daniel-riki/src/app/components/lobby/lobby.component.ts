import { ThisReceiver } from '@angular/compiler';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs';
import { webSocket } from 'rxjs/webSocket';
import { MessageService } from 'services/message.service';
import UserService from 'services/user.service';
import { MessageModel } from 'src/app/models/messageModel';
import { Room } from 'src/app/models/room.model';
import User from 'src/app/models/user.model';

@Component({
  selector: 'app-lobby',
  templateUrl: './lobby.component.html',
  styleUrls: ['./lobby.component.css']
})
export class LobbyComponent implements OnInit {

  user = new User();
  room = new Room();
  messageModelToChat = new MessageModel();
  isGameVisible = false;
  @Input() userId = 0;


  constructor(private userService: UserService, private messageService: MessageService, private router: ActivatedRoute) {
  }

  ngOnInit(): void {
    this.setMyUserId();
    this.getUserById();

  }

  setMyUserId() {
    this.router.params.subscribe(params => {
      this.user.id = params['id'];
    })
  }
  getUserById() {
    this.userService.getById(this.user.id).subscribe(user => {
      this.user = user;
      this.updateUserWithToken();
    });
  }

  getRoomFromUserList(room: Room) {
    this.room = room;
  }


  updateUserWithToken() {
    this.userService.getToken(this.user).subscribe(updatedUser => {
      this.user = updatedUser;
      //this.connect().subscribe();
    })
  }

  getGame(isGameVisible: boolean) {
    this.isGameVisible = isGameVisible;
  }

  sendSubmitedUserToGame(userId: number) {
    this.userId = userId;
  }

  

}
