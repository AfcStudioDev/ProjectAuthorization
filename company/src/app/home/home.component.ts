import { Input, Component } from '@angular/core';
import { RouterOutlet, Router } from '@angular/router';
import { FormsModule, Validators } from '@angular/forms';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent{
  constructor(private router: Router){}
  login: string = "tg";

  getLogin(){
    return this.login;
  }
  OnLoginButtonClick(){
    this.router.navigate(["/login"]);
  }
}
