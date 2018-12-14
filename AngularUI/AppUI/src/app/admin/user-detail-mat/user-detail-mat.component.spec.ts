import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { UserDetailMatComponent } from './user-detail-mat.component';

describe('UserDetailMatComponent', () => {
  let component: UserDetailMatComponent;
  let fixture: ComponentFixture<UserDetailMatComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ UserDetailMatComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(UserDetailMatComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
