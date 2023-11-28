import { Component, OnInit } from '@angular/core';
import { Form, NgForm } from '@angular/forms';
import { Result } from 'src/app/Models/EntityResultModel';
import { CompanyService } from 'src/app/Services/company.service';

@Component({
  selector: 'app-add-company',
  templateUrl: './add-company.component.html',
  styleUrls: ['./add-company.component.css']
})
export class AddCompanyComponent implements OnInit {

  constructor(private companyService: CompanyService) { }
  loading = false;
  info: string = "";
  infoColor: string = "danger";
  ngOnInit(): void {
  }
  login(form: NgForm) {
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
