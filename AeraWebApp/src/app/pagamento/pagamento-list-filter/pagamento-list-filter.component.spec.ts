import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PagamentoListFilterComponent } from './pagamento-list-filter.component';

describe('PagamentoListFilterComponent', () => {
  let component: PagamentoListFilterComponent;
  let fixture: ComponentFixture<PagamentoListFilterComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PagamentoListFilterComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PagamentoListFilterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
