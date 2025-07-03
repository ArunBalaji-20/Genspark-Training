import { ComponentFixture, TestBed, fakeAsync, tick } from '@angular/core/testing';
import { of, throwError } from 'rxjs';
import { ActivatedRoute } from '@angular/router';
import { FormBuilder } from '@angular/forms';
import { Managebugs } from './managebugs';
import { devBugService } from '../../core/Services/devBugService';
import { BugStatusModel } from '../../core/Models/BugStatusModel';

describe('Managebugs', () => {
  let component: Managebugs;
  let fixture: ComponentFixture<Managebugs>;
  let devBugServiceMock: any;
  let activatedRouteMock: any;
  let bug: BugStatusModel;

  beforeEach(async () => {
    bug = { bugId: 1, status: 'InProgress', screenshot: 'uploads/shot.png' } as any;
    devBugServiceMock = {
      getBugStatusAPI: jasmine.createSpy().and.returnValue(of({ body: bug })),
      patchBugStatusResolveAPI: jasmine.createSpy().and.returnValue(of({})),
      patchBugStatusInProgressAPI: jasmine.createSpy().and.returnValue(of({}))
    };
    activatedRouteMock = { queryParams: of({ id: 1 }) };

    await TestBed.configureTestingModule({
      imports: [Managebugs],
      providers: [
        { provide: devBugService, useValue: devBugServiceMock },
        { provide: ActivatedRoute, useValue: activatedRouteMock },
        FormBuilder
      ]
    }).compileComponents();

    fixture = TestBed.createComponent(Managebugs);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should load bug details and patch form on init', () => {
    expect(devBugServiceMock.getBugStatusAPI).toHaveBeenCalledWith(1);
    expect(component.bug).toEqual(bug);
    expect(component.bugUpdateForm.value.Status).toBe('InProgress');
  });

  it('should return correct screenshot url', () => {
    component.bug = { screenshot: 'uploads/shot.png' } as any;
    expect(component.getScreenshotUrl()).toContain('http://localhost:5258/screenshots/shot.png');
  });

  it('should call patchBugStatusResolveAPI and set statusMessage on Resolved', fakeAsync(() => {
    component.bugId = 1;
    component.bugUpdateForm.patchValue({ Status: 'Resolved' });
    component.handleStatusUpdate();
    expect(devBugServiceMock.patchBugStatusResolveAPI).toHaveBeenCalledWith(1);
    tick();
    expect(component.statusMessage).toBe('Status updated successfully!');
  }));

  it('should call patchBugStatusInProgressAPI and set statusMessage on InProgress', fakeAsync(() => {
    component.bugId = 1;
    component.bugUpdateForm.patchValue({ Status: 'InProgress' });
    component.handleStatusUpdate();
    expect(devBugServiceMock.patchBugStatusInProgressAPI).toHaveBeenCalledWith(1);
    tick();
    expect(component.statusMessage).toBe('Status updated successfully!');
  }));

  it('should set errorMessage if patchBugStatusResolveAPI fails', fakeAsync(() => {
    devBugServiceMock.patchBugStatusResolveAPI.and.returnValue(throwError(() => ({ error: { detail: 'fail' } })));
    component.bugId = 1;
    component.bugUpdateForm.patchValue({ Status: 'Resolved' });
    component.handleStatusUpdate();
    tick();
    expect(component.errorMessage).toBe('fail');
  }));

  it('should set errorMessage if patchBugStatusInProgressAPI fails', fakeAsync(() => {
    devBugServiceMock.patchBugStatusInProgressAPI.and.returnValue(throwError(() => ({ error: { detail: 'fail2' } })));
    component.bugId = 1;
    component.bugUpdateForm.patchValue({ Status: 'InProgress' });
    component.handleStatusUpdate();
    tick();
    expect(component.errorMessage).toBe('fail2');
  }));
});
