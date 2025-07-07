import { ComponentFixture, TestBed } from '@angular/core/testing';

import { Report } from './report';
import { bugService } from '../../core/Services/bugService';
import { of } from 'rxjs/internal/observable/of';
import { BugReportModel } from '../../core/Models/BugReportModel';

describe('Report', () => {
  let component: Report;
  let fixture: ComponentFixture<Report>;
  let bugServiceMock:any;

  let reportmodel:BugReportModel[]=[{ BugName: "",
         Description: "",
         Screenshot:  "",
         CvssScore: 0,} as any];

  beforeEach(async () => {
    bugServiceMock={
       submitBugReportAPI : jasmine.createSpy().and.returnValue(of({ body: { $values: reportmodel } }))
    }
    await TestBed.configureTestingModule({
      imports: [Report],
      providers:[{ provide: bugService , useValue: bugServiceMock }]
    })
    .compileComponents();

    fixture = TestBed.createComponent(Report);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
