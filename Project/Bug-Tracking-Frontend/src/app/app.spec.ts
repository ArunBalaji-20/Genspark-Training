import { TestBed } from '@angular/core/testing';
import { App } from './app';
import { NotificationService } from './core/Services/notificationService';
import { authService } from './core/Services/authService';
import { of } from 'rxjs/internal/observable/of';


describe('App', () => {
  let NotificationServiceMock:any;
  let authServiceMock:any;
  beforeEach(async () => {

     authServiceMock={
               authAPI : jasmine.createSpy().and.returnValue(of({  })),
               isLoggedIn: jasmine.createSpy().and.returnValue(true),
               role: jasmine.createSpy().and.returnValue('admin'),
               logoutAPI: jasmine.createSpy().and.returnValue(of({ status: 200 })),
               logout: jasmine.createSpy('logout')
            }
        NotificationServiceMock = {
          notifications$: of([]),
          startConnection: jasmine.createSpy()
        };


    await TestBed.configureTestingModule({
      imports: [App],
      providers:[{provide:NotificationService,useValue:NotificationServiceMock},
        {provide:authService,useValue:authServiceMock}
      ]
    }).compileComponents();
  });

  it('should create the app', () => {
    const fixture = TestBed.createComponent(App);
    const app = fixture.componentInstance;
    expect(app).toBeTruthy();
  });

  it('should render title', () => {
    const fixture = TestBed.createComponent(App);
    fixture.detectChanges();
    const compiled = fixture.nativeElement as HTMLElement;
    expect(compiled.querySelector('h1')?.textContent).toContain('hello ,BugTracking System');
  });
});
