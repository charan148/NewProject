import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SecurityLookUpComponent } from './security-look-up.component';

describe('SecurityLookUpComponent', () => {
  let component: SecurityLookUpComponent;
  let fixture: ComponentFixture<SecurityLookUpComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SecurityLookUpComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SecurityLookUpComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
