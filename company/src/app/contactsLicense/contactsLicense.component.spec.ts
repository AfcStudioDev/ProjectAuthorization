import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ContactsLicenseComponent } from './contactsLicense.component';

describe('ContactsLicenseComponent', () => {
  let component: ContactsLicenseComponent;
  let fixture: ComponentFixture<ContactsLicenseComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ContactsLicenseComponent]
    })
      .compileComponents();

    fixture = TestBed.createComponent(ContactsLicenseComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  // it('should create', () => {
  //   expect(component).toBeTruthy();
  // });
});
