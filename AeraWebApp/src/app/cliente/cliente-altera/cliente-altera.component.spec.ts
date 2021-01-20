import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ClienteAlteraComponent } from './cliente-altera.component';

describe('ClienteAlteraComponent', () => {
  let component: ClienteAlteraComponent;
  let fixture: ComponentFixture<ClienteAlteraComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ClienteAlteraComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ClienteAlteraComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
