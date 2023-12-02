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

  addEmployee(companyId : any, departmentId : any, nameSurname : any, title : any, phone : any, address :any, email : any, birthDay : any, photo : any){
    const formData: FormData = new FormData();
    formData.append("companyId", companyId);
    formData.append("departmentId", departmentId);
    formData.append("nameSurname", nameSurname);
    formData.append("title", title);
    formData.append("phone", phone);
    formData.append("address", address);
    formData.append("email", email);
    formData.append("birthDay", birthDay);
    formData.append("photo", photo);
    console.log(formData);
    return this.http.post<EntityResultModel>(this.apiUrl + "/employee/addEmployee", formData).pipe(
      catchError(this.handleError)
    );
  }
  handleError(err: HttpErrorResponse) {
    let message = "Beklenmedik bir hata oluştu";
    return throwError(() => message);
  }
}
