import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { HomeComponent } from "../home/home.component";
import { LoginRequest } from '../../requests/AuthorizationRequest/LoginRequest';
import { AuthorizationService } from '../http/authorization.service';
import { DownloadDistrService } from '../http/downloadDistr.service';
import { DownloadModule } from '../shared/download.module';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [FormsModule, HomeComponent],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css',
  providers: [AuthorizationService, DownloadDistrService, DownloadModule]
})
export class LoginComponent {
  constructor(private router: Router, private authorizationService: AuthorizationService, protected download: DownloadModule) {
    this.CheckAuthToken();
  }

  login: LoginRequest = new LoginRequest;

  OnLoginButtonClick() {
    this.authorizationService.Login(this.login).subscribe({
      next: (response) => {
        console.log(response);
        localStorage.setItem('token', response.token);
        this.router.navigate(["/home"]);
      },
      error: (err) => {
        alert("Не удалось авторизоваться: " + err.error);
        console.log(err)
      }
    });

  }
  OnSignUpButtonClick() {
    this.router.navigate(["/signup"]);
  }

  CheckAuthToken() {
    let token: string = localStorage.getItem("token") || "";
    if (token != "") {
      this.router.navigate(['/home']);
    }
  }
}
