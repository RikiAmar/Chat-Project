import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import {HttpClientModule} from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './components/login/login.component';
import { SignUpComponent } from './components/sign-up/sign-up.component';
import { NavbarComponent } from './components/navbar/navbar.component';
import UserService from 'services/user.service';
import { MessageService } from 'services/message.service';
import { ChatComponent } from './components/chat/chat.component';
import { RoomService } from 'services/room.service';
import { UserListComponent } from './components/user-list/user-list.component';
import { LobbyComponent } from './components/lobby/lobby.component';
import {NgbModule} from '@ng-bootstrap/ng-bootstrap';
import { GameComponent } from './components/game/game.component';
import { GridComponent } from './components/grid/grid.component';
import { GameService } from 'services/game.service';



@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    SignUpComponent,
    NavbarComponent,
    ChatComponent,
    UserListComponent,
    LobbyComponent,
    GameComponent,
    GridComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    NgbModule

  ],
  providers: [UserService , MessageService, RoomService, GameService],
  bootstrap: [AppComponent]
})
export class AppModule { }
