import { ComponentFixture, TestBed } from '@angular/core/testing';

import { Navbar } from './navbar';
import { authService } from '../../core/Services/authService';
import { NotificationService } from '../../core/Services/notificationService';
import { of } from 'rxjs/internal/observable/of';

describe('Navbar', () => {
  let component: Navbar;
  let fixture: ComponentFixture<Navbar>;

  let authservicemock:any;
  let notificationMock:any;
  // let isLoggedIn:Boolean=false;

  beforeEach(async () => {
    authservicemock={
           authAPI : jasmine.createSpy().and.returnValue(of({  })),
           isLoggedIn: jasmine.createSpy().and.returnValue(true),
           role: jasmine.createSpy().and.returnValue('admin'),
           logoutAPI: jasmine.createSpy().and.returnValue(of({ status: 200 })),
           logout: jasmine.createSpy('logout')
        }
    notificationMock = {
      notifications$: of([]),
      updateNotifications: jasmine.createSpy('updateNotifications')
    };
    await TestBed.configureTestingModule({
      imports: [Navbar],
      providers:[{ provide: authService , useValue: authservicemock },
                  { provide: NotificationService , useValue: notificationMock }
      ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(Navbar);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
