import { Component, OnInit } from '@angular/core';
import { firstValueFrom } from 'rxjs';
import { Result } from 'src/app/Models/EntityResultModel';
import { CompanyService } from 'src/app/Services/company.service';
declare var $: any;
@Component({
  selector: 'app-index',
  templateUrl: './index.component.html',
  styleUrls: ['./index.component.css']
})
export class IndexComponent implements OnInit {


  constructor(private companyService: CompanyService) { }
  companies: any[] = [];
  selectedCompany: any = 0;
  async ngOnInit(): Promise<void> {

    
    await this.getCompanies();

    $("#selectCompany").select2();
    if (this.companyService.getCurrentCompany() == null) {
      this.selectedCompany = this.companies[0]?.id;
      if (this.selectedCompany != undefined && this.selectedCompany != 0) {
        this.companyService.setCurrentCompany(this.selectedCompany);
      }
    } else {
      this.selectedCompany = parseInt(this.companyService.getCurrentCompany()!);
    }
    if (this.selectedCompany != 0 && this.selectedCompany != undefined) {
      $('#selectCompany option[value="0"]').prop("disabled", true);
    }
    $('#selectCompany').on('change', (event: any) => {
      if ($("#selectCompany").val() != 0) {
        this.companyService.setCurrentCompany($("#selectCompany").val());
      }
    });


  }



  async getCompanies() {
    try {
      const response = await firstValueFrom(this.companyService.getCompanies());
      if (response.result == Result.Success) {
        this.companies = response.resultObject.map((item: { title: any; id: number; }) => ({ title: item.title, id: item.id }));
      }
    } catch (error) {
      console.error("Error fetching companies:", error);
    }

  }

  async getDepartments() {
    
  }





}
