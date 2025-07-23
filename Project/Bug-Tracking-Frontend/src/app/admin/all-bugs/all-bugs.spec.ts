import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AllBugs } from './all-bugs';
import { BugStatusModel } from '../../core/Models/BugStatusModel';
import { of, throwError } from 'rxjs';
import { devBugService } from '../../core/Services/devBugService';

describe('MyBugs', () => {
  let component: AllBugs;
  let fixture: ComponentFixture<AllBugs>;
  let devBugServiceMock: any;
  
  const bugs: BugStatusModel[] = [{ bugId: 1, title: 'Bug1', status: 'Open' } as any];
  

  beforeEach(async () => {
     devBugServiceMock = {
          getMyBugsAPI: jasmine.createSpy().and.returnValue(of({ body: { $values: bugs } }))
        };
    await TestBed.configureTestingModule({
      imports: [AllBugs],
      providers: [
        { provide: devBugService, useValue: devBugServiceMock }
      ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AllBugs);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});


