import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NavbarComponent } from './navbar/navbar.component';
import { RouterModule } from '@angular/router';
import { PanelSidebarComponent } from './panel-sidebar/panel-sidebar.component';
import { PanelNavbarComponent } from './panel-navbar/panel-navbar.component';



@NgModule({
  declarations: [
    NavbarComponent,
    PanelSidebarComponent,
    PanelNavbarComponent
  ],
  imports: [
    CommonModule,
    RouterModule
  ],
  exports: [
    NavbarComponent,
    PanelSidebarComponent,
    PanelNavbarComponent
  ]
})
export class SharedModule { }
