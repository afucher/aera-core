import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DownloadAtestadoComponent } from './download-atestado.component';

describe('TesteDownloadComponent', () => {
  let component: DownloadAtestadoComponent;
  let fixture: ComponentFixture<DownloadAtestadoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DownloadAtestadoComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DownloadAtestadoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
