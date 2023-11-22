import { Component, NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { IndexComponent } from './Home/index/index.component';
import { RegisterComponent } from './Home/register/register.component';
import { LoginComponent } from './Home/login/login.component';
import { IndexComponent as PanelIndexComponent } from './Panel/index/index.component';
import { PanelGuard } from './Panel/panel.guard';
import { AddCompanyComponent } from './Panel/add-company/add-company.component';

const routes: Routes = [
  
  { path: "", component: IndexComponent },
  { path: "register", component: RegisterComponent },
  { path: "login", component: LoginComponent },
  { path: "panel", component: PanelIndexComponent ,canActivate: [PanelGuard] },
  



];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
