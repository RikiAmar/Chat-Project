import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { GameModel } from 'src/app/models/gameModel';
import { MessageModel } from 'src/app/models/messageModel';
import { Room } from 'src/app/models/room.model';
import User from 'src/app/models/user.model';

const GameController = "http://localhost:5228/Game/";

@Injectable()

export class GameService {

  constructor(private http: HttpClient) {

  }

  getGrid(room: Room): Observable<string[]> {
    return this.http.post<string[]>(GameController + 'getGridConstantly', room) ;
  }
// שיניתי שיחזיר האם עדכן או לא, אם לא עדכן סימן שנלחץ על כפתור שנלחץ בעבר
  updateGrid(gameModel: GameModel): Observable<boolean>{
    return this.http.post<boolean>(GameController + 'updateGrid', gameModel);
  }

  checkIfTwoUsersConnected(room: Room): Observable<boolean> {
    return this.http.post<boolean>(GameController + 'checkIfTwoUsersConnected', room);
  }
//add
  getCurrentPlayers(room: Room): Observable<number[]> {
    return this.http.post<number[]>(GameController + 'getCurrentPlayers', room);
  }
  removeRoomFromDb(room: Room) {
    return this.http.post(GameController + 'removeRoomFromTicTacToeDb', room)
  }

  updatesWhoseTurnItIsNow(gameModel: GameModel): Observable<GameModel> {
    return this.http.post<GameModel>(GameController + 'updatesWhoseTurnItIsNow', gameModel)
  }

  sendTurnFromDB(): Observable<number> {
    return this.http.get<number>(GameController + 'sendTurnFromDB');
  }

  getCounterFromDB(): Observable<number> {
    return this.http.get<number>(GameController + 'getCounterToEndGame');
  }

  calculateWinner(room: Room) : Observable<boolean>{
    return this.http.post<boolean>(GameController + 'calculateWinner', room);
  }

  updateLoser(room: Room) {
    return this.http.post(GameController + 'updateLoser', room);
  }

  getIdOfLoser() : Observable<number> {
    return this.http.get<number>(GameController + 'getIdOfLoser');
  }

  updateGameEndedStatus() {
    return this.http.get(GameController +'updateGameEndedStatus');
  }

  getGameEndedStatus() : Observable<boolean> {
    return this.http.get<boolean>(GameController +'getGameEndedStatus');
  }

  removeGameFromDB(room: Room) {
    return this.http.post(GameController + 'removeGameFromDB', room);
  }

  getGameStartedStatus() : Observable<boolean> {
    return this.http.get<boolean>(GameController +'getGameStartedStatus');
  }
}