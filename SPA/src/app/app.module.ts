import { ErrorInterceptorProvider } from './_services/error.interceptor';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { MatDialogModule } from '@angular/material/dialog';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';

import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { ProductsAlbumComponent } from './API_Services/Inventory/CatalogueProduct/catalogue-product-album/catalogue-product-album.component';
import { HttpHeaderInterceptor } from './_interceptors/http.header.interceptor';
import { HttpImagePipe } from './_pipes/http.image.pipe';
import { AddCatalogueProductPopUpComponent } from './API_Services/Trolley/Trolley/add-catalogue-product-pop-up/add-catalogue-product-pop-up.component';
// import {enableProdMode} from '@angular/core';


// enableProdMode();
@NgModule({
  declarations: [
    AppComponent,
    ProductsAlbumComponent,
    HttpImagePipe,
    AddCatalogueProductPopUpComponent
  ],
  //entryComponents: [AddCatalogueProductPopUpComponent],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule,
    MatDialogModule,
    BrowserAnimationsModule
  ],
  providers: [
    ErrorInterceptorProvider,
    { provide: HTTP_INTERCEPTORS, useClass: HttpHeaderInterceptor, multi: true },
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }