import { Injectable, resolveForwardRef } from '@angular/core';
import { BehaviorSubject, catchError, tap, throwError } from 'rxjs';
import { UserModel } from '../Models/UserModel';
import { HttpClient, HttpErrorResponse, HttpHeaders, HttpParams, HttpResponse } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { EntityResultModel, Result } from '../Models/EntityResultModel';
import { JsonPipe } from '@angular/common';
import { RegisterModel } from '../Models/RegisterModel';

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  constructor(private http: HttpClient) { }
  private readonly apiUrl = environment.apiUrl;
  user = new BehaviorSubject<UserModel | null>(null);



  login(email: string, password: string) {
    var body = new HttpParams();
    body = body.set("email", email);
    body = body.set("password", password);

    return this.http.post<EntityResultModel>(this.apiUrl + "account/login", body).pipe(

      tap(response => {
        if (response.result == Result.Success) {
          this.handleUser(response.resultObject.token);
        }
      }),

      catchError(this.handleError)
    );
  }

  register(user: RegisterModel) {
    var body = new HttpParams();
    body = body.set("Name", user.Name);
    body = body.set("Surname", user.Surname);
    body = body.set("Email", user.Email);
    body = body.set("Password", user.Password);
    body = body.set("Phone", user.Phone)
    
    return this.http.post<EntityResultModel>(this.apiUrl + "account/register", body).pipe(
      catchError(this.handleError)
    );
  }

  autoLogin() {
    if (localStorage.getItem("accessToken") == null) {
      return
    }
    var accessToken = localStorage.getItem("user") || "";
    const user = new UserModel(accessToken);
    this.user.next(user)
  }

  handleError(err: HttpErrorResponse) {
    let message = "Beklenmedik bir hata oluştu";
    return throwError(() => message);
  }

  logOut(){
    localStorage.removeItem('accessToken');
  }

  private handleUser(jwtToken: string) {
    const user = new UserModel(jwtToken);
    this.user.next(user);
    localStorage.setItem("accessToken", jwtToken);
  }
}




