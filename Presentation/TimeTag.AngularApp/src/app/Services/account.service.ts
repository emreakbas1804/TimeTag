import { Injectable } from '@angular/core';
import { BehaviorSubject, catchError, tap } from 'rxjs';
import { UserModel } from '../Models/UserModel';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { EntityResultModel, Result } from '../Models/EntityResultModel';

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  constructor(private http: HttpClient) { }
  private readonly apiUrl = environment.apiUrl;
  user = new BehaviorSubject<UserModel | null>(null);



  Login(email: string, password: string) {
    return this.http.post<EntityResultModel>(this.apiUrl + "account/login", {
      Email: email,
      Password: password
    }).pipe(
      tap(response => {
        
      })
    )
  }


  private HandleUser(jwtToken: string) {
    const user = new UserModel(jwtToken);
    this.user.next(user);
    localStorage.setItem("accessToken", jwtToken);
  }

  AutoLogin() {
    if (localStorage.getItem("accessToken") == null) {
      return
    }
    var accessToken = localStorage.getItem("user") || "";
    const user = new UserModel(accessToken);
    this.user.next(user)
  }
}

