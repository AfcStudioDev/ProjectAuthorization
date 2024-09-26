import { NgModule } from '@angular/core';
import { Router } from '@angular/router';

@NgModule({
  imports: [],
})
export class LicenseComponent {
  constructor(private router: Router) { }

  login: string = "";
  password: string = "";
  passwordRepeated: string = "";
  prompt: string = "";
  OnSignUpButtonClick() {
    if (this.password == this.passwordRepeated) {
      this.router.navigate(["/home"]);
    }
    else {
      this.prompt = "Пароли не совпадают";
    }
  }
}

