import { Component } from '@angular/core';
import { RouterOutlet, Router } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { HomeComponent } from "../../home/home.component";

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [FormsModule, HomeComponent],
  templateUrl: './userManaged.component.html',
  styleUrl: './userManaged.component.css'
})
export class UserManagedComponent {  
  constructor(private router: Router){}
  login:string = "john@gmail.com";
  password:string = "";

  OnLoginButtonClick(){
    this.router.navigate(["/home"]);
  }
  OnSignUpButtonClick(){
    this.router.navigate(["/signup"]);
  }
}
