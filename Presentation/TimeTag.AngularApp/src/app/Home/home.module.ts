import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { IndexComponent } from './index/index.component';
import { RouterModule } from '@angular/router';
import { RegisterComponent } from './register/register.component';
import { LoginComponent } from './login/login.component';
import { HttpLoaderFactory, SharedModule } from '../Shared/shared.module';
import { FormsModule } from '@angular/forms';
import { TranslateLoader, TranslateModule } from '@ngx-translate/core';
import { HttpClient } from '@angular/common/http';
<<<<<<< HEAD
=======
import { ForgotPasswordComponent } from './forgot-password/forgot-password.component';
>>>>>>> 33172811856cd780c1ce01dd3e21954380600f2c



@NgModule({
  declarations: [
    IndexComponent,
    RegisterComponent,
    LoginComponent,
    ForgotPasswordComponent
  ],
  imports: [
    CommonModule,
    RouterModule,
    SharedModule,
    FormsModule,
    TranslateModule
  ]
})
export class HomeModule { }
