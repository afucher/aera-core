import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ClienteNovoComponent } from './cliente-novo.component';

describe('ClienteNovoComponent', () => {
  let component: ClienteNovoComponent;
  let fixture: ComponentFixture<ClienteNovoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ClienteNovoComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ClienteNovoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
