import { ComponentFixture, TestBed } from '@angular/core/testing';

import { VaultEditPropertyComponent } from './vault-edit-property.component';

describe('VaultEditPropertyComponent', () => {
  let component: VaultEditPropertyComponent;
  let fixture: ComponentFixture<VaultEditPropertyComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ VaultEditPropertyComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(VaultEditPropertyComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
