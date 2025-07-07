import { ComponentFixture, TestBed } from '@angular/core/testing';

import { Manage } from './manage';
import { empManageService } from '../../core/Services/empManageService';
import { of } from 'rxjs/internal/observable/of';

describe('Manage', () => {
  let component: Manage;
  let fixture: ComponentFixture<Manage>;
  let empManageServiceMock:any;

  beforeEach(async () => {
    empManageServiceMock={
           getAllEmployeesAPI:jasmine.createSpy().and.returnValue(of({  })),
           getAvailableEmpAPI:jasmine.createSpy().and.returnValue(of({  })),
        }
    await TestBed.configureTestingModule({
      imports: [Manage],
       providers:[  
                      {provide: empManageService ,useValue:empManageServiceMock}
            ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(Manage);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
