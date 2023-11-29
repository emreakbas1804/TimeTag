import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { config, firstValueFrom } from 'rxjs';
import { Result } from 'src/app/Models/EntityResultModel';
import { CompanyService } from 'src/app/Services/company.service';
import { MatSnackBar } from '@angular/material/snack-bar';
@Component({
  selector: 'app-edit-company',
  templateUrl: './edit-company.component.html',
  styleUrls: ['./edit-company.component.css']
})
export class EditCompanyComponent implements OnInit {
  companyId: any;
  loading = false;
  info: string = "";
  infoColor: string = "danger";
  company: any = {
    title: "",
    address: "",
    description: "",
    webSite: "",
    image: "",
  }
  constructor(private router: Router, private companyService: CompanyService, private snackBar: MatSnackBar) { }

  async ngOnInit(): Promise<void> {
    this.companyId = this.router.url.split("/")[3];
    await this.getCompany()
 
  }

  async getCompany() {
    var response = await firstValueFrom(this.companyService.getCompany(this.companyId));
  
    if (response.result == Result.Success) {
      this.company.title = response.resultObject?.title;
      this.company.address = response.resultObject?.address;
      this.company.description = response.resultObject?.description;
      this.company.webSite = response.resultObject?.webSite;
      this.company.image = response.resultObject?.imageUrl;
      
    }
    else if (response.result == Result.Error) {
      
      this.snackBar.open(response.resultMessage, "", {
        verticalPosition: 'top',
        horizontalPosition: 'end',
        duration: 3000,
        panelClass: ['snackBar-error']
      });      
    }
  }

  update(form: NgForm) {
    if (form.invalid) {
      this.info = "Form validation error";
      this.infoColor = "danger";
      return;
    }
    this.loading = true;
    this.companyService.addCompany(form.value.title, form.value.address, form.value.description, form.value.webSite, form.value.licanceKey).subscribe({
      next: response => {
        if (response.result == Result.Success) {
          this.infoColor = "success";
          this.info = "Created new company"
        }
        this.info = response.resultMessage;
        this.loading = false;
        window.scroll({
          top: 0,
          left: 0,
          behavior: 'smooth'
        });
      },
      error: err => {
        this.info = err;
        this.loading = false;
        window.scroll({
          top: 0,
          left: 0,
          behavior: 'smooth'
        });
      }
    });
  }

}
