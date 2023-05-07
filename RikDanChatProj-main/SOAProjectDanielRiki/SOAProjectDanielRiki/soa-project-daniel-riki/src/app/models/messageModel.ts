import { Room } from "./room.model";

export class MessageModel {
    content: string = "";
    userId = 0;
    selectedUserId = 0;
    date = "";
    room = new Room();

    constructor() {

    }
}
