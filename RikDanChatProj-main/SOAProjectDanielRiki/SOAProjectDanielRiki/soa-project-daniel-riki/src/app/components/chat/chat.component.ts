import { Component, EventEmitter, Input, OnChanges, OnInit, Output, SimpleChanges } from '@angular/core';
import { interval, Observable, timer } from 'rxjs';
import { webSocket } from 'rxjs/webSocket';
import { GameService } from 'services/game.service';
import { MessageService } from 'services/message.service';
import UserService from 'services/user.service';
import { MessageModel } from 'src/app/models/messageModel';
import { Room } from 'src/app/models/room.model';
import User from 'src/app/models/user.model';

@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.css']
})
export class ChatComponent implements OnChanges, OnInit {

  messages: MessageModel[] = [];
  messageModelForArray = new MessageModel();
  messageModelToSend = new MessageModel();
  messageModelToRecieve = new MessageModel();
  selectedUsername: string = "";
  isInvitationVisible: boolean = false;
  invitationMessage = "Click on the picture to enter the game.The invitation will expire in 60 seconds";
  sumbitedUserId = 0;
  timeLeftToInvitation = 10;
  intervallTimer = interval(1000);
  subscription: any;
  isGameVisible = false;
  isAGameBeingPlayed = false;

  @Output() makeGameVisible = new EventEmitter();
  @Output() updateSumbitedUserId = new EventEmitter();
  @Output() updateRoomForGame = new EventEmitter();

  @Input() chatRoom = new Room();
  @Input() user = new User();

  constructor(private userService: UserService, private messageService: MessageService, private gameService: GameService) {

  }
  ngOnInit(): void {
    this.connect().subscribe();
  }


  ngOnChanges(changes: SimpleChanges): void {
    if (this.chatRoom.selectedId !== changes.chatRoom.previousValue) {
      this.getFriendNameFromDB();
    }
  }

  observableTimer() {
    this.subscription = this.intervallTimer.subscribe(() => {
      if (this.timeLeftToInvitation > 0) {
        this.timeLeftToInvitation--;
      }
      else {
        this.timeLeftToInvitation = 10;
        this.isInvitationVisible = false;
        this.filterMessageArray();
        this.unsubscribeTimer();
      }
    })
  }
  unsubscribeTimer() {
    this.subscription.unsubscribe();
  }

  getFriendNameFromDB() {
    this.userService.getById(this.chatRoom.selectedId).subscribe(user => {
      this.selectedUsername = user.username;
      this.setMessageModelForArray();
      this.checkIfAGameIsBeingPlayed();
    })
  }

  setMessageModelForArray() {
    this.messageModelForArray.userId = this.user.id;
    this.messageModelForArray.selectedUserId = this.chatRoom.selectedId;
    this.messageModelForArray.room.myRoomName = `${this.user.id}-${this.chatRoom.selectedId}`;
    this.messageModelForArray.room.selectedRoomName = `${this.chatRoom.selectedId}-${this.user.id}`;
    this.filterMessageArray();
  }

  setMessageModelToSend() {
    this.messageModelToSend.userId = this.user.id;
    this.messageModelToSend.selectedUserId = this.chatRoom.selectedId;
    this.messageModelToSend.room.myRoomName = `${this.user.id}-${this.chatRoom.selectedId}`;
    this.messageModelToSend.room.selectedRoomName = `${this.chatRoom.selectedId}-${this.user.id}`;

  }

  filterMessageArray() {
    this.messageService.getfilteredMessageArray(this.messageModelForArray).subscribe(messageArray => {
      this.messages = messageArray;
    })
  }

  sendMessage(message: string) {
    this.setMessageModelToSend();
    this.messageModelToSend.content = message;
    this.messageService.sendMsg(this.messageModelToSend).subscribe(messageModel => {
      if (message === this.invitationMessage) {
        this.isInvitationVisible = true;
        this.sumbitedUserId = this.chatRoom.myId;
        this.observableTimer();
        this.updateSumbitedUserId.emit(this.sumbitedUserId);
        this.updateRoomForGame.emit(this.chatRoom);
      }
      this.filterMessageArray();
      this.messages.push(messageModel);
    });
  }

  checkIfAGameIsBeingPlayed(){
    this.gameService.getGameStartedStatus().subscribe(hasGameStarted => {
      this.isAGameBeingPlayed = hasGameStarted;
    })
  }

  connect(): Observable<any> {
    var observable = new Observable<any>((subscriber) => {

      var subject = webSocket(this.user.token);

      subject.subscribe(msg => {
        var message = Object.values(msg as MessageModel)[0] as MessageModel;
        if (message.content === this.invitationMessage) {
          this.isInvitationVisible = true;
          this.observableTimer();
          this.updateRoomForGame.emit(this.chatRoom);
        }
        this.filterMessageArray();
        this.messages.push(message);
      });
    })
    return observable
  }

  showGame() {
    this.isGameVisible = true;
    this.isInvitationVisible = false;
    this.filterMessageArray();
    this.makeGameVisible.emit(this.isGameVisible);
  }
}
