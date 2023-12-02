import { Component, NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { IndexComponent } from './Home/index/index.component';
import { RegisterComponent } from './Home/register/register.component';
import { LoginComponent } from './Home/login/login.component';
import { IndexComponent as PanelIndexComponent } from './Panel/index/index.component';
import { PanelGuard } from './Panel/panel.guard';
import { AddCompanyComponent } from './Panel/add-company/add-company.component';
import { MyCompaniesComponent } from './Panel/my-companies/my-companies.component';
import { EditCompanyComponent } from './Panel/edit-company/edit-company.component';
import { AddDepartmentComponent } from './Panel/add-department/add-department.component';
import { MyDepartmentsComponent } from './Panel/my-departments/my-departments.component';
import { EditDepartmentComponent } from './Panel/edit-department/edit-department.component';
import { AddEmployeeComponent } from './Panel/add-employee/add-employee.component';

const routes: Routes = [

  { path: "", component: IndexComponent },
  { path: "register", component: RegisterComponent },
  { path: "login", component: LoginComponent },
  { path: "panel", component: PanelIndexComponent, canActivate: [PanelGuard] },
  { path: "panel/add-company", component: AddCompanyComponent, canActivate: [PanelGuard] },
  { path: "panel/my-companies", component: MyCompaniesComponent, canActivate: [PanelGuard] },
  { path: "panel/edit-company/:Id:", component: EditCompanyComponent, canActivate: [PanelGuard] },
  { path: "panel/add-department", component: AddDepartmentComponent, canActivate: [PanelGuard] },
  { path: "panel/my-departments", component: MyDepartmentsComponent, canActivate: [PanelGuard] },
  { path: "panel/edit-department/:Id:", component: EditDepartmentComponent, canActivate: [PanelGuard] },
  { path: "panel/add-employee", component: AddEmployeeComponent, canActivate: [PanelGuard] },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
