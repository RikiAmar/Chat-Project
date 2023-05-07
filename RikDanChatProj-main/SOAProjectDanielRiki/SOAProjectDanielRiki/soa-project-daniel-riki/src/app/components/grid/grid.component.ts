import { Component, Input, OnInit } from '@angular/core';
import { interval } from 'rxjs';
import { GameService } from 'services/game.service';
import { GameModel } from 'src/app/models/gameModel';
import { Room } from 'src/app/models/room.model';

@Component({
  selector: 'app-grid',
  templateUrl: './grid.component.html',
  styleUrls: ['./grid.component.css']
})
export class GridComponent implements OnInit {

  grid: string[] = [];
  usersListForGame: number[] = [];
  gameModel: GameModel = new GameModel();
  isWaitingForUser = true;
  isGameVisible = true;
  @Input() userId = 0;
  @Input() room = new Room();
  delay = (ms: number | undefined) => new Promise(res => setTimeout(res, ms));
  isMyTurn = false;
  counter = 0;
  winningMessage = "";
  losingMessage = ""
  tieingMessage = "";
  hasGameEnded = false;


  constructor(private gameService: GameService) {
  }
  ngOnInit(): void {
    if (this.userId !== 0) {
      this.isMyTurn = true;
    }
    this.gameService.getCurrentPlayers(this.room).subscribe(usersId => {
      this.usersListForGame = usersId;
      this.CheckRoomParticipants();
    });
  }

  async CheckRoomParticipants() {
    interval(1000).subscribe(x => {
      this.gameService.checkIfTwoUsersConnected(this.room).subscribe(async areThereTwoUsers => {
        if (areThereTwoUsers) {
          this.isWaitingForUser = false;
          this.getGridConstantly();
        }
        else {
          this.isWaitingForUser = true;
        }
        await this.delay(60000);
        this.gameService.checkIfTwoUsersConnected(this.room).subscribe(areThereTwoUsers => {
          if (!areThereTwoUsers) {
            this.isWaitingForUser = true;
            window.location.reload();
          }
        });
      });
    })
  }

  getGridConstantly() {
    this.gameService.getGrid(this.room).subscribe(gridFromDB => {
      this.grid = gridFromDB;
      if (this.counter >= 5){
        this.calculateWinner();
        this.findLoser();
        this.checkIfGameEnded();
      }
      if (this.counter === 9 && this.hasGameEnded === false){
        this.tieingMessage = 'A tie!'
        this.updateGameEndedStatusInDB();
      }
      this.SendTurnFromDb();
      this.updateCounterFromDb();
    });
  }
  calculateWinner() {
    this.gameService.calculateWinner(this.room).subscribe(isThereaWinner => {
      if (isThereaWinner){
        this.winningMessage = 'Congrats! You are our winner!'
        this.updateLoserInDB();
        this.updateGameEndedStatusInDB();
        this.isMyTurn = false;
      }
    })
  }

  updateGameEndedStatusInDB() {
    this.gameService.updateGameEndedStatus().subscribe()
  }

  findLoser() {
    this.gameService.getIdOfLoser().subscribe(loserId => {
      if (loserId === this.room.myId){
        this.losingMessage = 'You have lost.';
        this.isMyTurn = false;
        this.updateGameEndedStatusInDB();
      }
    })
  }

  updateLoserInDB() {
    this.gameService.updateLoser(this.room).subscribe();
  }

  updateCounterFromDb() {
    this.gameService.getCounterFromDB().subscribe(counterFromDB => {
      this.counter = counterFromDB;
    })
  }

  SendTurnFromDb(){
    this.gameService.sendTurnFromDB().subscribe(turnFromDB => {
      if (turnFromDB === this.room.myId){
        this.isMyTurn = true;
      }
    })
  }

  closeGame() {
    this.gameService.removeRoomFromDb(this.room).subscribe(_ => {
      window.location.reload();
    });
  }

  updateGridToDB(currentCell: number) {

    this.setGameModelForDB(currentCell);
      if (this.isMyTurn === true) {
        // נעדכן את הגריד בתא שלחצתי
        this.gameService.updateGrid(this.gameModel).subscribe(isUpdated => {
          if (isUpdated) {
            //נעדכן את הבראוזר שעכשיו לא תורי
            this.isMyTurn = false;
          }
        });
      }
  }

  setGameModelForDB(currentCell: number) {
    this.gameModel.currentCell = currentCell;
    this.gameModel.whoStartedGame = this.userId;
    this.gameModel.whoseTurn = this.room.myId;
    this.gameModel.room = this.room;
  }

  checkIfGameEnded() {
    this.gameService.getGameEndedStatus().subscribe(ifGameEnded => {
      this.hasGameEnded = ifGameEnded;
    })
  }
}