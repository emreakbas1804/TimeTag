import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeModule } from './Home/home.module';
import { SharedModule } from './Shared/shared.module';
import { PanelModule } from './Panel/panel.module';
import {HttpClientModule} from '@angular/common/http';



@NgModule({
  declarations: [
    AppComponent,       
  ],
  imports: [
    BrowserModule,  
    HttpClientModule,
    HomeModule,
    SharedModule,
    PanelModule,
    AppRoutingModule,
    
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
