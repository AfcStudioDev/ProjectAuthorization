import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-dummy',
  standalone: true,
  imports: [],
  templateUrl: '../../../../outer/dummy.html',
  styleUrl: './dummy.css',
})
export class DummyComponent {  
  constructor(private router: Router){

  }

  OnSignupButtonClick() {
  this.router.navigate(["/signup"]);
}
}
