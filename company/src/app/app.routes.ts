import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { SignUpComponent } from './signup/singup.component';
import { LoginComponent } from './login/login.component';
import { LicenseComponent } from './license/license.component';

export const routes: Routes = [
  {
  path:'', redirectTo: '/signup', pathMatch: 'full'
  },
  {
    path:'login', component: LoginComponent
  },
  {
    path:'signup', component: SignUpComponent
  },
  {
    path:'home', component: HomeComponent
  },
  {
    path:'license', component: LicenseComponent
  }
];