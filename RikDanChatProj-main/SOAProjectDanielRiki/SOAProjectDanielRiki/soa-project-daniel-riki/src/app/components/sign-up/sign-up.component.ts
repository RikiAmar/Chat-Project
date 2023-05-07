import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import UserService from 'services/user.service';
import User from 'src/app/models/user.model';

@Component({
  selector: 'app-sign-up',
  templateUrl: './sign-up.component.html',
  styleUrls: ['./sign-up.component.css']
})

export class SignUpComponent {
  user = new User();
  nullErrorMessage = '';
  duplicateErrorMessage = '';

  constructor(private userService: UserService, private router: Router) {
    
  }

  registerUser(){
    this.TryToAddUserToDB();
  }

  TryToAddUserToDB(){
    this.userService.register(this.user).subscribe(hasVerificationWorked => {
      if (hasVerificationWorked === false && (!this.user.username || !this.user.password)){
        this.duplicateErrorMessage="";
        this.nullErrorMessage = "Username and/or password is empty. Please fill them out, and try again.";
      }
      else if(hasVerificationWorked === false && (this.user.username.length > 0)){
       this.nullErrorMessage="";
        this.duplicateErrorMessage = "The desired username is already taken. Please try again with a different username.";
      }
      else{
        this.TransferUserToLoginPage();
      }
    })
  }

  
  TransferUserToLoginPage(){
    this.router.navigate(['/login']);
  }
















}
