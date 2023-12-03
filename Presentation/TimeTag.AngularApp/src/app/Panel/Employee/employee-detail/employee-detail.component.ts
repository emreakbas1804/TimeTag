import { DatePipe } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { firstValueFrom } from 'rxjs';
import { Result } from 'src/app/Models/EntityResultModel';
import { SnackBarService } from 'src/app/Services/customService/snack-bar.service';
import { CompanyService } from 'src/app/Services/httpService/company.service';
import { EmployeeService } from 'src/app/Services/httpService/employee.service';
import { environment } from 'src/environments/environment';
declare var $: any;
@Component({
  selector: 'app-employee-detail',
  templateUrl: './employee-detail.component.html',
  styleUrls: ['./employee-detail.component.css']
})
export class EmployeeDetailComponent implements OnInit {

  loading = false;
  selectedFile: File | null = null;
  previewUrl: any = null;
  allowedTypes = ["image/jpeg", "image/jpg", "image/webp", "image/png"];
  departments: any[] = [];
  selectedDepartment: any = 0;
  employeeId: any = null;
  employee: any = {
    nameSurname: "",
    title: "",
    phone: "",
    address: "",
    email: "",
    birthDay: "",
    photo: "",
  }
  cdnUrl = environment.cdnUrl;
  constructor(private snackBarService: SnackBarService, private employeeService: EmployeeService, private companyService: CompanyService, private router: Router, private datePipe: DatePipe) { }


  async ngOnInit(): Promise<void> {
    
    this.employeeId = this.router.url.split("/")[3];
    await this.getEmployee();
    await this.getDepartments();
    $("#selectDepartment").select2();
          
    $('#selectDepartment').on('change', async (event: any) => {
      if ($("#selectDepartment").val() != 0) {
        this.selectedDepartment = $("#selectDepartment").val();
      }
    });

  }


  updateEmployee(form: NgForm) {
    if (form.invalid || this.selectedDepartment == 0) {
      this.snackBarService.error("Form validation error");
      return;
    }
    this.loading = true;
    var companyId = this.companyService.getCurrentCompany();
    this.employeeService.updateEmployee(this.selectedDepartment,this.employeeId,form.value.fullName,form.value.title,form.value.phone,form.value.address, form.value.email,true,form.value.birthDay,this.selectedFile).subscribe({
      next: response => {
        this.loading = false;
        if (response.result == Result.Success) {
          this.snackBarService.success("Updated Employee");          
          this.selectedFile = null;
        }
        else {
          this.loading = false;
          this.snackBarService.error(response.resultMessage);
        }
      },
      error: err => {
        this.snackBarService.error("UnKnow Error. Please try again later.");
        this.loading = false;
      }
    })
  }
  async getEmployee() {
    var response = await firstValueFrom(this.employeeService.getEmployee(this.employeeId));
    if (response.result == Result.Success) {
      this.employee.nameSurname = response.resultObject?.nameSurname;
      this.employee.title = response.resultObject?.title;
      this.employee.phone = response.resultObject?.phone;
      this.employee.address = response.resultObject?.address;
      this.employee.email = response.resultObject?.email;
      this.employee.birthDay = this.formatDate(response.resultObject?.birthDay);
      this.selectedDepartment = response.resultObject?.departmentId;
      this.employee.photo = response.resultObject?.imageUrl


    } else if (response.result == Result.Error) {
      this.snackBarService.error(response.resultMessage);
      this.router.navigate(["/panel/my-employees"]);
    }
  }

  async getDepartments() {
    var companyId = this.companyService.getCurrentCompany();
    const response = await firstValueFrom(this.companyService.getDepartments(companyId));
    if (response.result == Result.Success) {

      this.departments = response.resultObject.map((item: { id: any, name: any }) => ({
        name: item.name,
        id: item.id,
      }
      ));
    }
    if (this.departments.length == 0) {
      this.snackBarService.warning("You must add a departman for add employee");
      this.router.navigate(['/panel/add-department']);
    }
  }

  onFileSelected(event: any): void {
    const file = event.target.files[0];

    if (!this.allowedTypes.includes(file.type)) {
      this.snackBarService.error("File type is invalid");
      return
    }
    const maxSize = 10 * 1024 * 1024; // 10MB
    if (file.size > maxSize) {
      this.snackBarService.error("File size con not bigger than 10 MB");
      return
    }
    if (file) {
      this.selectedFile = file;
      const reader = new FileReader();

      reader.onload = () => {
        this.previewUrl = reader.result;
      };

      reader.readAsDataURL(this.selectedFile as Blob);
    }
  }

  formatDate(apiDate: string): string {
    const date = new Date(apiDate);
    const year = date.getFullYear();
    const month = (date.getMonth() + 1).toString().padStart(2, '0');
    const day = date.getDate().toString().padStart(2, '0');
    return `${year}-${month}-${day}`;
  }

  addBankAccount(form : NgForm){

  }

}
