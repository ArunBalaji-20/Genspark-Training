import { ComponentFixture, TestBed } from '@angular/core/testing';

import { Chat } from './chat';
import { bugCommentService } from '../../core/Services/bugCommentService';
import { authService } from '../../core/Services/authService';
import { ActivatedRoute } from '@angular/router';
import { of } from 'rxjs/internal/observable/of';

describe('Chat', () => {
  let component: Chat;
  let fixture: ComponentFixture<Chat>;
  let bugCommentServiceMock:any;
  let authserviceMock:any;
  let ActivatedRouteMock:any;

  beforeEach(async () => {
    bugCommentServiceMock = {
      getAllCommentsAPI:jasmine.createSpy().and.returnValue(of({  }))
    };
    authserviceMock = {};
    ActivatedRouteMock = { queryParams: { subscribe: jasmine.createSpy() } };

    await TestBed.configureTestingModule({
      imports: [Chat],
      providers:[
        {provide:bugCommentService ,useValue:bugCommentServiceMock},
        {provide:authService,useValue:authserviceMock},
        {provide:ActivatedRoute,useValue:ActivatedRouteMock}
      ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(Chat);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
