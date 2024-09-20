import { TestBed } from '@angular/core/testing';
import { CanActivateFn } from '@angular/router';

import { homeEnter1Guard } from './home-enter1.guard';

describe('homeEnter1Guard', () => {
  const executeGuard: CanActivateFn = (...guardParameters) => 
      TestBed.runInInjectionContext(() => homeEnter1Guard(...guardParameters));

  beforeEach(() => {
    TestBed.configureTestingModule({});
  });

  it('should be created', () => {
    expect(executeGuard).toBeTruthy();
  });
});
