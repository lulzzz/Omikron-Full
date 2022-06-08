import { ComponentFixture, TestBed } from '@angular/core/testing';

import { VaultEditVehicleComponent } from './vault-edit-vehicle.component';

describe('VaultEditVehicleComponent', () => {
  let component: VaultEditVehicleComponent;
  let fixture: ComponentFixture<VaultEditVehicleComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ VaultEditVehicleComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(VaultEditVehicleComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
