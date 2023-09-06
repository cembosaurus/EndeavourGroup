import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AppComponent } from './app.component';
import { ProductsAlbumComponent } from './API_Services/Inventory/CatalogueProduct/catalogue-products-album/catalogue-products-album.component';

const routes: Routes = [
  {
    path: '',
    component: ProductsAlbumComponent,
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
