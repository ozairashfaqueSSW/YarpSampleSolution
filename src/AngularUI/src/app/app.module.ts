import { NgModule, PLATFORM_ID } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { DataService } from '../services/data.service';
import { HttpClientModule } from '@angular/common/http';
import { isPlatformBrowser } from '@angular/common';
import { DataViewModalComponent } from './components/data-view-modal/data-view-modal.component';

@NgModule({
  declarations: [
    AppComponent,
    DataViewModalComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule
  ],
  providers: [
    {
      provide: 'baseUrl',
      useFactory: (platformId: Object) => {
        if (isPlatformBrowser(platformId)) {
          return window.location.origin;
        }
        return '';
      },
      deps: [PLATFORM_ID]
    },
  DataService
],
  bootstrap: [AppComponent]
})
export class AppModule { }
