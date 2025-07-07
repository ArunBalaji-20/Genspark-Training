import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AllBugsAssign } from './all-bugs-assign';
import { bugService } from '../../core/Services/bugService';
import { of } from 'rxjs/internal/observable/of';

describe('AllBugsAssign', () => {
  let component: AllBugsAssign;
  let fixture: ComponentFixture<AllBugsAssign>;
  let bugServiceMock:any;

  beforeEach(async () => {
    bugServiceMock={
      getAllBugsInSubmittedState:jasmine.createSpy().and.returnValue(of({  }))
    }
    await TestBed.configureTestingModule({
      imports: [AllBugsAssign],
       providers:[
              {provide: bugService ,useValue:bugServiceMock}
       ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AllBugsAssign);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
