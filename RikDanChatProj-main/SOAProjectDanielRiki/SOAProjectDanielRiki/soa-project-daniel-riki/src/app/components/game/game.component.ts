import { Component, Input, OnChanges, SimpleChanges } from '@angular/core';
import { Room } from 'src/app/models/room.model';

@Component({
  selector: 'app-game',
  templateUrl: './game.component.html',
  styleUrls: ['./game.component.css']
})
export class GameComponent {
@Input() room = new Room();
@Input() userId = 0;

constructor(){


}


}
