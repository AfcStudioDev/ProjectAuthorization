import { Component } from '@angular/core';
import { RouterOutlet, Router } from '@angular/router';
import { FormsModule, Validators } from '@angular/forms';
import { RegistrationRequest } from '../../requests/AuthorizationRequest/RegistrationRequest';
import { AuthorizationService } from '../http/authorization.service';

@Component({
  selector: 'app-signup',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './signup.component.html',
  styleUrl: './signup.component.css',
  providers: [AuthorizationService]
})
export class SignUpComponent {
  constructor(private router: Router, private authorizationService: AuthorizationService) { 
    this.CheckAuthToken();
  }
  emailRegex: RegExp = new RegExp("^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$");
  //passwordRegex: RegExp = new RegExp("^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*#?&])[A-Za-z\d@$!%*#?&]{8,}$");
  login: string = "john@gmail.com";
  password: string = "";
  passwordRepeated: string = "";
  prompt: string = "";

  registration: RegistrationRequest = new RegistrationRequest;

  OnSignUpButtonClick() {
    if (!this.CheckLogin()) {
      this.prompt = "Некорректная почта";
    }
    else if (!this.CheckPassword()) {
      this.prompt = "Недопустимый пароль";
    }
    else if (!this.CheckPasswordsMatch()) {
      this.prompt = "Пароли не совпадают";
    } else {

      this.authorizationService.Registration(this.registration).subscribe({
        next: (response) => {
          this.router.navigate(["/home"]);
        },
        error: (err) => {
          alert("Не удалось создать аккаунт");
          console.log(err);
        }
      });
    }
  }

  CheckLogin(): boolean {
    return this.emailRegex.test(this.registration.email);
  }
  CheckPassword(): boolean {
    return this.registration.password.length >= 8;
  }
  CheckPasswordsMatch(): boolean {
    return this.registration.password == this.passwordRepeated;
  }

  OnLoginButtonClick() {
    this.router.navigate(["/login"]);
  }
  CheckAuthToken(){
    let token:string = localStorage.getItem("token") || "";
    if( token != "" )
    {
      this.router.navigate(['/home']);
    }
  }
  
}
