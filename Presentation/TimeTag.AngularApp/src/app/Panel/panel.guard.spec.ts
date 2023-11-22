import { TestBed } from '@angular/core/testing';

import { PanelGuard } from './panel.guard';

describe('PanelGuard', () => {
  let guard: PanelGuard;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    guard = TestBed.inject(PanelGuard);
  });

  it('should be created', () => {
    expect(guard).toBeTruthy();
  });
});
