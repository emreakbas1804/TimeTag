import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AddCompanyComponent } from './add-company/add-company.component';
import { HomeModule } from '../Home/home.module';
import { IndexComponent } from './index/index.component';
import { SharedModule } from '../Shared/shared.module';
import { FormsModule } from '@angular/forms';
import { MyCompaniesComponent } from './my-companies/my-companies.component';
import {MatTableModule} from '@angular/material/table';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MAT_DATE_LOCALE } from '@angular/material/core';
import { RouterModule } from '@angular/router';
import { EditCompanyComponent } from './edit-company/edit-company.component';
import { MatSnackBarModule } from '@angular/material/snack-bar';
@NgModule({
  providers :[
    { provide: MAT_DATE_LOCALE, useValue: 'tr-TR' },
  ],
  declarations: [
    AddCompanyComponent,
    IndexComponent,
    MyCompaniesComponent,
    EditCompanyComponent
  ],
  imports: [
    CommonModule,
    SharedModule,
    FormsModule,
    RouterModule,
    MatTableModule,
    MatPaginatorModule,
    MatSnackBarModule,
  ],
 
})
export class PanelModule { }
