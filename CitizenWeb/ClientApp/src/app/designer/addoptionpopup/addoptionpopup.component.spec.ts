import { async, ComponentFixture, TestBed } from '@angular/core/testing';

//import { HistorypopupComponent } from './historypopup.component';
import { AddoptionpopupComponent} from './addoptionpopup.component'

describe('HistorypopupComponent', () => {
    let component: AddoptionpopupComponent;
    let fixture: ComponentFixture<AddoptionpopupComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
        declarations: [AddoptionpopupComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
      fixture = TestBed.createComponent(AddoptionpopupComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
