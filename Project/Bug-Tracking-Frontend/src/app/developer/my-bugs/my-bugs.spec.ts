import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MyBugs } from './my-bugs';
import { BugStatusModel } from '../../core/Models/BugStatusModel';
import { of, throwError } from 'rxjs';
import { devBugService } from '../../core/Services/devBugService';

describe('MyBugs', () => {
  let component: MyBugs;
  let fixture: ComponentFixture<MyBugs>;
  let devBugServiceMock: any;
  
  const bugs: BugStatusModel[] = [{ bugId: 1, title: 'Bug1', status: 'Open' } as any];
  

  beforeEach(async () => {
     devBugServiceMock = {
          getMyBugsAPI: jasmine.createSpy().and.returnValue(of({ body: { $values: bugs } }))
        };
    await TestBed.configureTestingModule({
      imports: [MyBugs],
      providers: [
        { provide: devBugService, useValue: devBugServiceMock }
      ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(MyBugs);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});


