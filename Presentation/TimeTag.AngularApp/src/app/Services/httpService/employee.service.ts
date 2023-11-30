import { HttpClient, HttpErrorResponse, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { EntityResultModel } from '../../Models/EntityResultModel';
import { environment } from 'src/environments/environment';
import { catchError, throwError } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class EmployeeService {

  constructor(private http: HttpClient) { }
  private readonly apiUrl = environment.apiUrl;
  getEmployeesCount(companyId: number) {
    const params = new HttpParams().set('companyId', companyId);
    return this.http.get<EntityResultModel>(this.apiUrl + "/employee/getEmployeesCompanyCount", { params }).pipe(
      catchError(this.handleError),
    )
  }
  handleError(err: HttpErrorResponse) {
    let message = "Beklenmedik bir hata oluştu";
    return throwError(() => message);
  }
}
