import { HttpClient, HttpErrorResponse, HttpHeaders, HttpParams } from '@angular/common/http';
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

  getCompanies() {
    return this.http.get<EntityResultModel>(this.apiUrl + "/company/getCompanies").pipe(
      catchError(this.handleError),
    );
  }
  getDepartmentsCount(companyId: number) {
    const params = new HttpParams().set('companyId', companyId);

    return this.http.get<EntityResultModel>(this.apiUrl + '/company/getDepartmentsCount', { params }).pipe(
      catchError(this.handleError)
    );
  }



  addCompany(title: any, address: any, description: any, webSite: any, licanceKey: any) {
    const params = {
      title,
      address,
      description,
      webSite,
      serialNumber: licanceKey
    };
  
    const body = new HttpParams({ fromObject: params });
     
  
    return this.http.post<EntityResultModel>(this.apiUrl + "/company/addCompany", body).pipe(
      catchError(this.handleError)
    );
  }

  getCompany(companyId : number){
    const params = new HttpParams().set('companyId', companyId);
    return this.http.get<EntityResultModel>(this.apiUrl + "/company/getCompany", { params }).pipe(
      catchError(this.handleError),
    )
  }
  


  setCurrentCompany(id: number) {
    localStorage.setItem("currentCompanyId", id?.toString());
  }
  getCurrentCompany() {
    return localStorage.getItem("currentCompanyId");
  }
  handleError(err: HttpErrorResponse) {
    let message = "Beklenmedik bir hata oluştu";
    return throwError(() => message);
  }


}
