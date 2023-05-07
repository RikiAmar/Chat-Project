import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { webSocket } from 'rxjs/webSocket';
import UserService from 'services/user.service';
import { MessageModel } from 'src/app/models/messageModel';
import User from 'src/app/models/user.model';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  newUser = new User();
  hasFailedToLogin = false;

  constructor(private userService: UserService, private router: Router) {

  }

  Login() {
    this.UpdateNewUserFromDB();
  }

  UpdateNewUserFromDB(){
    this.userService.login(this.newUser).subscribe(updatedUser => {
      this.newUser = updatedUser as User;
      this.ValidateLogin();
    })
  }

  ValidateLogin(){
    if (this.newUser.id === 0){
      this.hasFailedToLogin = true;
    }
    else{
      this.transferUserToLobby();
    }
  }


  transferUserToLobby(){
    this.router.navigate([`/lobby/${this.newUser.id}`]);
  }
}
