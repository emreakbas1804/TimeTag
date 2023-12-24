import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NavbarComponent } from './navbar/navbar.component';
import { RouterModule } from '@angular/router';
import { PanelSidebarComponent } from './panel-sidebar/panel-sidebar.component';
import { PanelNavbarComponent } from './panel-navbar/panel-navbar.component';
import { TranslateLoader, TranslateModule } from '@ngx-translate/core';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';



@NgModule({
  declarations: [
    NavbarComponent,
    PanelSidebarComponent,
    PanelNavbarComponent
  ],
  imports: [
    CommonModule,
    RouterModule,
    HttpClientModule,
<<<<<<< HEAD
    TranslateModule.forRoot({
      defaultLanguage : "en",
=======
    TranslateModule.forRoot({     
>>>>>>> 33172811856cd780c1ce01dd3e21954380600f2c
      loader : {
        provide: TranslateLoader,
        useFactory : HttpLoaderFactory,
        deps : [HttpClient]
      }
    })
  ],
  exports: [
    NavbarComponent,
    PanelSidebarComponent,
    PanelNavbarComponent
  ]
})
export class SharedModule { }

export function HttpLoaderFactory(httpclient: HttpClient){
  return new TranslateHttpLoader(httpclient);
}

