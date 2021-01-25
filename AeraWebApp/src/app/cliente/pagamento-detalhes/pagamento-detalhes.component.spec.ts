import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PagamentoDetalhesComponent } from './pagamento-detalhes.component';

describe('PagamentoDetalhesComponent', () => {
  let component: PagamentoDetalhesComponent;
  let fixture: ComponentFixture<PagamentoDetalhesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PagamentoDetalhesComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PagamentoDetalhesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
