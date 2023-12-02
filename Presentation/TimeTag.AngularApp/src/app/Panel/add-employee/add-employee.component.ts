import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { firstValueFrom } from 'rxjs';
import { Result } from 'src/app/Models/EntityResultModel';
import { SnackBarService } from 'src/app/Services/customService/snack-bar.service';
import { CompanyService } from 'src/app/Services/httpService/company.service';
import { EmployeeService } from 'src/app/Services/httpService/employee.service';
declare var $: any;
@Component({
  selector: 'app-add-employee',
  templateUrl: './add-employee.component.html',
  styleUrls: ['./add-employee.component.css'],
  encapsulation: ViewEncapsulation.None
})
export class AddEmployeeComponent implements OnInit {
  loading = false;
  selectedFile: File | null = null;
  previewUrl: any = null;
  allowedTypes = ["image/jpeg", "image/jpg", "image/webp", "image/png"];
  departments: any[] = [];
  selectedDepartment: any = 0;
  constructor(private snackBarService: SnackBarService, private employeeService: EmployeeService, private companyService: CompanyService, private router: Router) { }

  async ngOnInit(): Promise<void> {
    await this.getDepartments();
    $("#selectDepartment").select2();
    if (this.departments.length == 0) {
      this.snackBarService.warning("You must add a departman for add employee");
      this.router.navigate(['/panel/add-department']);
    }

    $('#selectDepartment').on('change', async (event: any) => {
      if ($("#selectDepartment").val() != 0) {
        this.selectedDepartment = $("#selectDepartment").val();
      }
    });
  }
  ngOnDestroy() {    
    $('#selectDepartment').select2('destroy');
  }
  addEmployee(form: NgForm) {
    if (form.invalid || this.selectedDepartment == 0) {
      this.snackBarService.error("Form validation error");
      return;
    }
    this.loading = true;
    var companyId = this.companyService.getCurrentCompany();
    this.employeeService.addEmployee(companyId, this.selectedDepartment, form.value.fullName, form.value.title, form.value.phone, form.value.address, form.value.email, form.value.birthDay, this.selectedFile).subscribe({
      next: response => {
        this.loading = false;
        if (response.result == Result.Success) {
          this.snackBarService.success("Added employee");
          form.reset();
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



}
