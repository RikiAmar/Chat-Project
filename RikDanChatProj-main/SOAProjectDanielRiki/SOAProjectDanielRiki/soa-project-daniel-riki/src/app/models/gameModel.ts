import { Room } from "./room.model";

export class GameModel {
    userId: number =0; //מסמל מי לחץ על הכפתור איקס או עיגול
    whoStartedGame = 0;
    whoseTurn = 0;
    currentCell: number = 0;
    room = new Room();
}