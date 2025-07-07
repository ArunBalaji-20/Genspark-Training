import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ChatList } from './chat-list';
import { bugService } from '../../core/Services/bugService';
import { of } from 'rxjs/internal/observable/of';

describe('ChatList', () => {
  let component: ChatList;
  let fixture: ComponentFixture<ChatList>;
  let bugserviceMock:any;

  beforeEach(async () => {
    bugserviceMock={
      getAllBugStatusAPI:jasmine.createSpy().and.returnValue(of({  }))
    }
    await TestBed.configureTestingModule({
      imports: [ChatList],
      providers:[{provide:bugService ,useValue:bugserviceMock}]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ChatList);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
