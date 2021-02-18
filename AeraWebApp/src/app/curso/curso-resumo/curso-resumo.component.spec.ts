import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CursoResumoComponent } from './curso-resumo.component';

describe('CursoResumoComponent', () => {
  let component: CursoResumoComponent;
  let fixture: ComponentFixture<CursoResumoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CursoResumoComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CursoResumoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
