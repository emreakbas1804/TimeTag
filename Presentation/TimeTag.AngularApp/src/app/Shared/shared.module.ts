import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NavbarComponent } from './navbar/navbar.component';
import { RouterModule } from '@angular/router';
import { PanelSidebarComponent } from './panel-sidebar/panel-sidebar.component';



@NgModule({
  declarations: [
    NavbarComponent,
    PanelSidebarComponent
  ],
  imports: [
    CommonModule,
    RouterModule
  ],
  exports: [
    NavbarComponent,
    PanelSidebarComponent
  ]
})
export class SharedModule { }
