import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { LayoutpopupComponent } from './layoutpopup.component';

describe('LayoutpopupComponent', () => {
  let component: LayoutpopupComponent;
  let fixture: ComponentFixture<LayoutpopupComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ LayoutpopupComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(LayoutpopupComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
