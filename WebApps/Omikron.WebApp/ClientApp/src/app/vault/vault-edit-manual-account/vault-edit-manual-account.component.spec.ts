import { ComponentFixture, TestBed } from '@angular/core/testing';

import { VaultEditManualAccountComponent } from './vault-edit-manual-account.component';

describe('VaultEditManualAccountComponent', () => {
  let component: VaultEditManualAccountComponent;
  let fixture: ComponentFixture<VaultEditManualAccountComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ VaultEditManualAccountComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(VaultEditManualAccountComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
