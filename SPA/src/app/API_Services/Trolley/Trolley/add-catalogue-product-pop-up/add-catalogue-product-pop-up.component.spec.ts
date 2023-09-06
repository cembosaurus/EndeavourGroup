import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddCatalogueProductPopUpComponent } from './add-catalogue-product-pop-up.component';

describe('AddCatalogueProductPopUpComponent', () => {
  let component: AddCatalogueProductPopUpComponent;
  let fixture: ComponentFixture<AddCatalogueProductPopUpComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [AddCatalogueProductPopUpComponent]
    });
    fixture = TestBed.createComponent(AddCatalogueProductPopUpComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
