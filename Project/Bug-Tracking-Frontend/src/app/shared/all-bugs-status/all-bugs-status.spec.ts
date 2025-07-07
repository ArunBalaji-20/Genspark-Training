import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AllBugsStatus } from './all-bugs-status';
import { bugService } from '../../core/Services/bugService';
import { of } from 'rxjs/internal/observable/of';

describe('AllBugsStatus', () => {
  let component: AllBugsStatus;
  let fixture: ComponentFixture<AllBugsStatus>;
  let bugServiceMock:any;

  beforeEach(async () => {
    bugServiceMock={
          getAllBugStatusAPI:jasmine.createSpy().and.returnValue(of({  })),
          getAllBugsAssignedListAPI:jasmine.createSpy().and.returnValue(of({  }))
        }
    await TestBed.configureTestingModule({
      imports: [AllBugsStatus],
      providers:[
            {provide: bugService ,useValue:bugServiceMock}
          ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AllBugsStatus);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
