import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TesteDownloadComponent } from './teste-download.component';

describe('TesteDownloadComponent', () => {
  let component: TesteDownloadComponent;
  let fixture: ComponentFixture<TesteDownloadComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TesteDownloadComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TesteDownloadComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
