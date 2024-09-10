import { Component } from '@angular/core';
import { RouterOutlet, Router } from '@angular/router';

@Component({
  selector: 'app-license',
  standalone: true,
  imports: [],
  templateUrl: './license.component.html',
  styleUrl: './license.component.css'
})
export class LicenseComponent {
  constructor(private router: Router){}

  login:string = "";
  password:string = "";
  passwordRepeated:string="";
  prompt:string="";
  OnSignUpButtonClick(){
    if(this.password == this.passwordRepeated)
    {
      this.router.navigate(["/home"]);
    }
    else
    {
      this.prompt = "Пароли не совпадают";
    }
  }
}

