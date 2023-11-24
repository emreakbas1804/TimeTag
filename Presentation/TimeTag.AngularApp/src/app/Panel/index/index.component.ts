import { Component, OnInit } from '@angular/core';
import { CompanyService } from 'src/app/Services/company.service';

@Component({
  selector: 'app-index',
  templateUrl: './index.component.html',
  styleUrls: ['./index.component.css']
})
export class IndexComponent implements OnInit {

  constructor(private companyService : CompanyService) { }

  ngOnInit(): void {
    this.companyService.getCompanies().subscribe({
      next : response=> {console.log("response",response)},
      error : err=> {console.log("error",err)}
    })
  }

}
