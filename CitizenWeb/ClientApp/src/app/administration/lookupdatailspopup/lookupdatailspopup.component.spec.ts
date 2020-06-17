import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { LookupdatailspopupComponent } from './lookupdatailspopup.component';

describe('LookupdatailspopupComponent', () => {
  let component: LookupdatailspopupComponent;
  let fixture: ComponentFixture<LookupdatailspopupComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ LookupdatailspopupComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(LookupdatailspopupComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
