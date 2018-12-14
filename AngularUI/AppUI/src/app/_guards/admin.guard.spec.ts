import { TestBed, async, inject } from '@angular/core/testing';

import { Guards\adminGuard } from './-guards\admin.guard';

describe('Guards\adminGuard', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [Guards\adminGuard]
    });
  });

  it('should ...', inject([Guards\adminGuard], (guard: Guards\adminGuard) => {
    expect(guard).toBeTruthy();
  }));
});
