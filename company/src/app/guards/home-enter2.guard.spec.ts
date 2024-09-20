import { TestBed } from '@angular/core/testing';
import { CanActivateFn } from '@angular/router';

import { homeEnter2Guard } from './home-enter2.guard';

describe('homeEnter2Guard', () => {
  const executeGuard: CanActivateFn = (...guardParameters) => 
      TestBed.runInInjectionContext(() => homeEnter2Guard(...guardParameters));

  beforeEach(() => {
    TestBed.configureTestingModule({});
  });

  it('should be created', () => {
    expect(executeGuard).toBeTruthy();
  });
});
