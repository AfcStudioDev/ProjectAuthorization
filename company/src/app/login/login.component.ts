import { Component } from '@angular/core';
import { RouterOutlet, Router } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { HomeComponent } from "../home/home.component";
import { LoginRequest } from '../../requests/AuthorizationRequest/LoginRequest';
import { AuthorizationService } from '../http/authorization.service';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [FormsModule, HomeComponent],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css',
  providers: [AuthorizationService]
})
export class LoginComponent {  
  constructor(private router: Router, private authorizationService : AuthorizationService){}
  
  login: LoginRequest  = new LoginRequest;

  OnLoginButtonClick(){
    this.authorizationService.Login(this.login).subscribe({
      next: (response) => {
        console.log(response);
        localStorage.setItem('token', response.token);
        this.router.navigate(["/home"]);
      },
      error: (err) => {
        alert("не удалось авторизоваться!");
        console.log(err)
      }
    });

  }
  OnSignUpButtonClick(){
    this.router.navigate(["/signup"]);
  }
}
