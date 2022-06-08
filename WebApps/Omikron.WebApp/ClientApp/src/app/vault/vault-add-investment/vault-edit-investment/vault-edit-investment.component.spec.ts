import { ComponentFixture, TestBed } from '@angular/core/testing';

import { VaultEditInvestmentComponent } from './vault-edit-investment.component';

describe('VaultEditInvestmentComponent', () => {
  let component: VaultEditInvestmentComponent;
  let fixture: ComponentFixture<VaultEditInvestmentComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ VaultEditInvestmentComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(VaultEditInvestmentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
