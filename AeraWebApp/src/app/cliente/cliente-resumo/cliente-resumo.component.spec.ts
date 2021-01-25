import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ClienteResumoComponent } from './cliente-resumo.component';

describe('ClienteResumoComponent', () => {
  let component: ClienteResumoComponent;
  let fixture: ComponentFixture<ClienteResumoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ClienteResumoComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ClienteResumoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
