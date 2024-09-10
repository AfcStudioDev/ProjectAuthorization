import { ApplicationConfig, provideZoneChangeDetection } from '@angular/core';
import { provideRouter, Routes } from '@angular/router';

import { routes } from './app.routes';
import { provideClientHydration } from '@angular/platform-browser';
import { HomeComponent } from './home/home.component';
import { AppComponent } from './app.component';
import { SignUpComponent } from './signup/singup.component';

const appRoutes: Routes =[
  { path: "", component: AppComponent},
  { path: "home", component: HomeComponent},
  { path: "signup", component: SignUpComponent }
];

export const appConfig: ApplicationConfig = {
  providers: [provideZoneChangeDetection({ eventCoalescing: true }), provideRouter(routes), provideClientHydration()]
};
