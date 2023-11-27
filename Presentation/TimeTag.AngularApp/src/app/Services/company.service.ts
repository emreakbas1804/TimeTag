import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { EntityResultModel } from '../Models/EntityResultModel';
import { Observable, catchError, throwError } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CompanyService {

  constructor(private http: HttpClient) { }
  private readonly apiUrl = environment.apiUrl;

  getCompanies(){
    return this.http.get<EntityResultModel>(this.apiUrl + "company/getCompanies").pipe(
      catchError(this.handleError),
      
    );
  }

  setCurrentCompany(id: number){
    localStorage.setItem("currentCompanyId",id?.toString());
  }
  getCurrentCompany(){
    return localStorage.getItem("currentCompanyId");
  }
  handleError(err: HttpErrorResponse) {
    let message = "Beklenmedik bir hata oluştu";
    return throwError(() => message);
  }


}
