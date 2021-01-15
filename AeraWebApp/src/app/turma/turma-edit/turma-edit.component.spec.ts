import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TurmaEditComponent } from './turma-edit.component';

describe('TurmaEditComponent', () => {
  let component: TurmaEditComponent;
  let fixture: ComponentFixture<TurmaEditComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TurmaEditComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TurmaEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
