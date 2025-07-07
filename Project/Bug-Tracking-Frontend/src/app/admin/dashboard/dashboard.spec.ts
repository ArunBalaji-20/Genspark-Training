import { ComponentFixture, TestBed } from '@angular/core/testing';

import { Dashboard } from './dashboard';
import { empManageService } from '../../core/Services/empManageService';
import { bugService } from '../../core/Services/bugService';
import { of } from 'rxjs/internal/observable/of';

describe('Dashboard', () => {
  let component: Dashboard;
  let fixture: ComponentFixture<Dashboard>;
  let empManageServiceMock:any;
  let bugServiceMock:any;

  beforeEach(async () => {
    empManageServiceMock={
       getAllEmployeesAPI:jasmine.createSpy().and.returnValue(of({  })),
       getAvailableEmpAPI:jasmine.createSpy().and.returnValue(of({  })),
    }
    bugServiceMock={
          getAllBugStatusAPI:jasmine.createSpy().and.returnValue(of({  })),
        }
    await TestBed.configureTestingModule({
      imports: [Dashboard],
      providers:[  {provide: bugService ,useValue:bugServiceMock},
                {provide: empManageService ,useValue:empManageServiceMock}
      ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(Dashboard);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
