import { ComponentFixture, TestBed } from '@angular/core/testing';

import { Login } from './login';
import { authService } from '../../core/Services/authService';
import { of } from 'rxjs/internal/observable/of';
import { LoginModel } from '../../core/Models/LoginModel';

describe('Login', () => {
  let component: Login;
  let fixture: ComponentFixture<Login>;
  let authServicemock:any;

  const loginmodel: LoginModel[] = [{ email: 'test@gmail.com', password: 'test123'} as any];

  beforeEach(async () => {
    authServicemock = {
              loginAPI: jasmine.createSpy().and.returnValue(of({ body: { $values: loginmodel } }))
            };
    await TestBed.configureTestingModule({
      imports: [Login],
       providers: [
              { provide: authService, useValue: authServicemock }
            ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(Login);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
