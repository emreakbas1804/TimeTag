import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AddCompanyComponent } from './add-company/add-company.component';
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
import { AddDepartmentComponent } from './add-department/add-department.component';
import { MatInputModule } from '@angular/material/input';
import {NgxMaterialTimepickerModule} from 'ngx-material-timepicker';
import { MyDepartmentsComponent } from './my-departments/my-departments.component';
import { EditDepartmentComponent } from './edit-department/edit-department.component';


@NgModule({
  providers :[
    { provide: MAT_DATE_LOCALE, useValue: 'tr-TR' },
  ],
  declarations: [
    AddCompanyComponent,
    IndexComponent,
    MyCompaniesComponent,
    EditCompanyComponent,
    AddDepartmentComponent,
    MyDepartmentsComponent,
    EditDepartmentComponent
  ],
  imports: [
    CommonModule,
    SharedModule,
    NgxMaterialTimepickerModule.setOpts("format" , "24h"),    
    FormsModule,
    RouterModule,
    MatTableModule,
    MatPaginatorModule,
    MatSnackBarModule,
  ],
 
})
export class PanelModule { }
