import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RolesdetailsComponent } from './rolesdetails.component';

describe('RolesdetailsComponent', () => {
  let component: RolesdetailsComponent;
  let fixture: ComponentFixture<RolesdetailsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RolesdetailsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RolesdetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
