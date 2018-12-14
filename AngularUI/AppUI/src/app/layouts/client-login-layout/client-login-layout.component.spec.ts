import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ClientLoginLayoutComponent } from './client-login-layout.component';

describe('ClientLoginLayoutComponent', () => {
  let component: ClientLoginLayoutComponent;
  let fixture: ComponentFixture<ClientLoginLayoutComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ClientLoginLayoutComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ClientLoginLayoutComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
