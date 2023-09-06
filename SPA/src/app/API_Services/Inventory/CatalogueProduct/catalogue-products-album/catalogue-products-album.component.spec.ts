import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProductsAlbumComponent } from './catalogue-products-album.component';

describe('ProductsAlbumComponent', () => {
  let component: ProductsAlbumComponent;
  let fixture: ComponentFixture<ProductsAlbumComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ProductsAlbumComponent]
    });
    fixture = TestBed.createComponent(ProductsAlbumComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
