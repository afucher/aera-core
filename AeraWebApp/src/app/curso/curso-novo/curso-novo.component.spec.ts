import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CursoNovoComponent } from './curso-novo.component';

describe('CursoNovoComponent', () => {
  let component: CursoNovoComponent;
  let fixture: ComponentFixture<CursoNovoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CursoNovoComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CursoNovoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
