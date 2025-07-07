import { ComponentFixture, TestBed } from '@angular/core/testing';

import { Assign } from './assign';
import { bugService } from '../../core/Services/bugService';
import { devBugService } from '../../core/Services/devBugService';
import { ActivatedRoute } from '@angular/router';
import { of } from 'rxjs/internal/observable/of';

describe('Assign', () => {
  let component: Assign;
  let fixture: ComponentFixture<Assign>;
  let bugServiceMock:any;
  let devBugServiceMock:any;
  let ActivatedRouteMock:any;

  beforeEach(async () => {
      ActivatedRouteMock = { queryParams: { subscribe: jasmine.createSpy() } };

      devBugServiceMock={
        getBugStatusAPI:jasmine.createSpy().and.returnValue(of({  })),
        getAvailableDevsAPI:jasmine.createSpy().and.returnValue(of({  })),
      }
    await TestBed.configureTestingModule({
      imports: [Assign],
       providers:[
        {provide: bugService ,useValue:bugServiceMock},
        {provide:devBugService,useValue:devBugServiceMock},
        {provide:ActivatedRoute,useValue:ActivatedRouteMock}
      ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(Assign);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
