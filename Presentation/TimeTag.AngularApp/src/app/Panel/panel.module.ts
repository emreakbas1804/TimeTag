import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AddCompanyComponent } from './add-company/add-company.component';
import { HomeModule } from '../Home/home.module';
import { IndexComponent } from './index/index.component';
import { SharedModule } from '../Shared/shared.module';



@NgModule({
  declarations: [
    AddCompanyComponent,
    IndexComponent
  ],
  imports: [
    CommonModule,
    SharedModule
  ],
 
})
export class PanelModule { }
