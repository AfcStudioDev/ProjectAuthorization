import { Component } from '@angular/core';
import { RouterOutlet, RouterLink, Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
})

export class AppComponent {
  constructor(private router: Router){}
  title = "MAIN";

  OnMainButtonClick(){
    this.router.navigate([""]);
  } 

  OnLoginButtonClick(){
    this.router.navigate(["/login"]);
  } 

  OnSignUpButtonClick(){
    this.router.navigate(["/signup"]);
  }
  
  OnProfileButtonClick(){
    this.router.navigate(["/home"]);
  }
  
  OnLicenseButtonClick(){
    this.router.navigate(["/license"]);
  }
}