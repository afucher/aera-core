import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PagamentoListComponent } from './pagamento-list.component';

describe('PagamentoListComponent', () => {
  let component: PagamentoListComponent;
  let fixture: ComponentFixture<PagamentoListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PagamentoListComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PagamentoListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
