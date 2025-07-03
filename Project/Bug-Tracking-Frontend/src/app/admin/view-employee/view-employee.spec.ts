import { ComponentFixture, TestBed, fakeAsync, tick } from '@angular/core/testing';
import { of, throwError } from 'rxjs';
import { ActivatedRoute, Router } from '@angular/router';

import { ViewEmployee } from './view-employee';
import { empManageService } from '../../core/Services/empManageService';
import { devBugService } from '../../core/Services/devBugService';
import { EmployeeModel } from '../../core/Models/EmployeeModel';
import { BugStatusModel } from '../../core/Models/BugStatusModel';
import { BugCommentModel } from '../../core/Models/BugCommentModel';

describe('ViewEmployee', () => {
  let component: ViewEmployee;
  let fixture: ComponentFixture<ViewEmployee>;
  let empManageServiceMock: any;
  let devBugServiceMock: any;
  let routerMock: any;
  let activatedRouteMock: any;

  const empId = 42;
  const empDetails: EmployeeModel = { employeeId: empId, name: 'John', role: 'dev', email: 'john@x.com' };
  const bugs: BugStatusModel[] = [{ bugId: 1, title: 'Bug1', status: 'Open' } as any];
  const comments: BugCommentModel[] = [{ commentId: 1, comment: 'Test', bugId: 1, employeeId: empId } as any];

  beforeEach(async () => {
    empManageServiceMock = {
      getEmployeeDetailsAPI: jasmine.createSpy().and.returnValue(of({ body: empDetails })),
      getBugsReportedByAPI: jasmine.createSpy().and.returnValue(of({ body: { $values: bugs } })),
      getLatestCommentsAPI: jasmine.createSpy().and.returnValue(of({ body: { $values: comments } })),
      deleteEmployeeAPI: jasmine.createSpy().and.returnValue(of({ success: true }))
    };
    devBugServiceMock = {
      getMyBugsByIdAPI: jasmine.createSpy().and.returnValue(of({ body: { $values: bugs } }))
    };
    routerMock = { navigate: jasmine.createSpy('navigate') };
    activatedRouteMock = { queryParams: of({ id: empId }) };

    await TestBed.configureTestingModule({
      imports: [ViewEmployee],
      providers: [
        { provide: empManageService, useValue: empManageServiceMock },
        { provide: devBugService, useValue: devBugServiceMock },
        { provide: Router, useValue: routerMock },
        { provide: ActivatedRoute, useValue: activatedRouteMock }
      ]
    }).compileComponents();

    fixture = TestBed.createComponent(ViewEmployee);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should fetch employee details, bugs, comments, and assigned bugs on init', () => {
    expect(empManageServiceMock.getEmployeeDetailsAPI).toHaveBeenCalledWith(empId);
    expect(empManageServiceMock.getBugsReportedByAPI).toHaveBeenCalledWith(empId);
    expect(empManageServiceMock.getLatestCommentsAPI).toHaveBeenCalledWith(empId);
    expect(devBugServiceMock.getMyBugsByIdAPI).toHaveBeenCalledWith(empId);
    expect(component.empDetails).toEqual(empDetails);
    expect(component.mybugs).toEqual(bugs);
    expect(component.latestComments).toEqual(comments);
    expect(component.bugsAssignedToMe).toEqual(bugs);
  });

  it('should call deleteEmployee and navigate on confirmDelete', fakeAsync(() => {
    spyOn(window, 'confirm').and.returnValue(true);
    spyOn(window, 'alert');
    component.confirmDelete(empId);
    expect(empManageServiceMock.deleteEmployeeAPI).toHaveBeenCalledWith(empId);
    tick();
    expect(window.alert).toHaveBeenCalledWith('employee Deleted Successfully');
    expect(routerMock.navigate).toHaveBeenCalledWith(['/admin/manage/employees']);
  }));

  it('should not call deleteEmployee if confirm is cancelled', () => {
    spyOn(window, 'confirm').and.returnValue(false);
    spyOn(component, 'deleteEmployee');
    component.confirmDelete(empId);
    expect(component.deleteEmployee).not.toHaveBeenCalled();
  });

  it('should handle error on deleteEmployee', fakeAsync(() => {
    empManageServiceMock.deleteEmployeeAPI.and.returnValue(throwError(() => new Error('fail')));
    spyOn(window, 'alert');
    component.deleteEmployee(empId);
    tick();
    expect(window.alert).toHaveBeenCalledWith('Error while deleting employee');
    expect(routerMock.navigate).toHaveBeenCalledWith(['/admin/manage/employees']);
  }));
});
