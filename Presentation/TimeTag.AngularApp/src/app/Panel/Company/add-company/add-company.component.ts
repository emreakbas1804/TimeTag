import { Component, OnInit } from '@angular/core';
import { Form, NgForm } from '@angular/forms';
import { Result } from 'src/app/Models/EntityResultModel';
import { SnackBarService } from 'src/app/Services/customService/snack-bar.service';
import { CompanyService } from 'src/app/Services/httpService/company.service';

@Component({
  selector: 'app-add-company',
  templateUrl: './add-company.component.html',
  styleUrls: ['./add-company.component.css']
})
export class AddCompanyComponent implements OnInit {

  constructor(private companyService: CompanyService, private snackBarService: SnackBarService) { }
  loading = false;
  selectedFile: File | null = null;
  previewUrl: any = null;
  allowedTypes = ["image/jpeg", "image/jpg", "image/webp", "image/png"]
  ngOnInit(): void {
  }
  addCompany(form: NgForm) {
    if (form.invalid) {
      this.snackBarService.error("Form validation error");
      return;
    }
    this.loading = true;
    this.companyService.addCompany(form.value.title, form.value.address, form.value.description, form.value.webSite, form.value.licanceKey, this.selectedFile).subscribe({
      next: response => {
        if (response.result == Result.Success) {
          this.snackBarService.success("Company created");
          form.reset();
        }else this.snackBarService.error(response.resultMessage);
                
        this.loading = false;       
      },
      error: err => {
        this.loading = false;
        this.snackBarService.error("An unexpected error occurred")
      }
    });
  }

  onFileSelected(event: any): void {
    const file = event.target.files[0];
    
    if (!this.allowedTypes.includes(file.type)) {
      this.snackBarService.error("File type is invalid");
      return
    }
    const maxSize = 10 * 1024 * 1024; // 10MB
    if(file.size > maxSize){
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
