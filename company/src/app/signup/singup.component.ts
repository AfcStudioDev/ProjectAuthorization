import { Component } from '@angular/core';
import { RouterOutlet, Router } from '@angular/router';
import { FormsModule, Validators } from '@angular/forms';

@Component({
  selector: 'app-signup',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './signup.component.html',
  styleUrl: './signup.component.css'
})
export class SignUpComponent {
  constructor(private router: Router) { }
  emailRegex: RegExp = new RegExp("^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$");
  passwordRegex: RegExp = new RegExp("^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*#?&])[A-Za-z\d@$!%*#?&]{8,}$");
  login: string = "john@gmail.com";
  password: string = "";
  passwordRepeated: string = "";
  prompt: string = "";

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
      this.router.navigate(["/home"]);
    }
  }

  CheckLogin(): boolean {
    return this.emailRegex.test(this.login);
  }
  CheckPassword(): boolean {
    return this.passwordRegex.test(this.password);
  }
  CheckPasswordsMatch(): boolean {
    return this.password == this.passwordRepeated;
  }

  OnLoginButtonClick() {
    this.router.navigate(["/login"]);
  }
}
