import { ComponentFixture, TestBed } from '@angular/core/testing';

import { VaultEditPersonalItemComponent } from './vault-edit-personal-item.component';

describe('VaultEditPersonalItemComponent', () => {
  let component: VaultEditPersonalItemComponent;
  let fixture: ComponentFixture<VaultEditPersonalItemComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ VaultEditPersonalItemComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(VaultEditPersonalItemComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
