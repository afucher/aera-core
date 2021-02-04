import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DownloadListaComponent } from './download-lista.component';

describe('TesteDownloadComponent', () => {
  let component: DownloadListaComponent;
  let fixture: ComponentFixture<DownloadListaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DownloadListaComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DownloadListaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
