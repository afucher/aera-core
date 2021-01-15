import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TurmaNovaComponent } from './turma-nova.component';

describe('TurmaNovaComponent', () => {
  let component: TurmaNovaComponent;
  let fixture: ComponentFixture<TurmaNovaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TurmaNovaComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TurmaNovaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
