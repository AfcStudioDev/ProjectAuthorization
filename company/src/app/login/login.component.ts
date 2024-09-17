import { Component } from '@angular/core';
import { RouterOutlet, Router } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { HomeComponent } from "../home/home.component";
import { LoginRequest } from '../../requests/AuthorizationRequest/LoginRequest';
import { AuthorizationService } from '../http/authorization.service';
import { DownloadDistrService } from '../http/downloadDistr.service';
import { request } from 'http';
import { GetFilesUploadRequest } from '../../requests/Download/GetFilesUploadRequest';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [FormsModule, HomeComponent],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css',
  providers: [AuthorizationService]
})
export class LoginComponent {  
  constructor(private router: Router, private authorizationService : AuthorizationService, private downloadDistrService : DownloadDistrService){
    this.CheckAuthToken();
  }
  
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

  OnDownLoad1Click(){
    let request = new GetFilesUploadRequest(0);
    this.downloadDistrService.GetDistr(request).subscribe({
      next: (response) => { 

      },
      error: (err) => {
        alert("Ссылка на скачивание временно не доступна");
        console.log(err)
      }
    });
  }

  OnDownLoad2Click(){
    let request = new GetFilesUploadRequest(1);
    this.downloadDistrService.GetDistr(request).subscribe({
      next: (response) => { 

      },
      error: (err) => {
        alert("Ссылка на скачивание временно не доступна");
        console.log(err)
      }
    });
  }

  CheckAuthToken(){
    let token:string = localStorage.getItem("token") || "";
    if( token != "" )
    {
      this.router.navigate(['/home']);
    }
  }
}
