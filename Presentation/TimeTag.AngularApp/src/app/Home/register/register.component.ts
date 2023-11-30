import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Result } from 'src/app/Models/EntityResultModel';
import { RegisterModel } from 'src/app/Models/RegisterModel';
import { AccountService } from 'src/app/Services/httpService/account.service';
declare var $: any;
@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  constructor(private accountService: AccountService) { }
  info: string | null = null;
  infoColor: string | null = null;
  loading: boolean = false;
  registerModel: RegisterModel = {
    Name: '',
    Surname: '',
    Email: '',
    Password: "",
    Phone: '',
  }
  ngOnInit(): void {
   
  }


  register(form: NgForm) {
    if (form.invalid) {
      this.info = "Form validation error";
      this.infoColor = "danger";
      window.scroll({
        top: 0,
        left: 0,
        behavior: 'smooth'
      });
      return;
    }
    this.loading = true;
    const user = {
      Name: this.registerModel.Name,
      Surname: this.registerModel.Surname,
      Email: this.registerModel.Email,
      Password: this.registerModel.Password,
      Phone: this.registerModel.Phone
    }

    this.accountService.register(user).subscribe({
      error: err => {
        this.info = err;
        this.infoColor = "danger";
        this.loading = false;
        window.scroll({
          top: 0,
          left: 0,
          behavior: 'smooth'
        });
      },
      next: respone => {
        this.loading = false;
        if (respone.result == Result.Success) {
          this.info = "Success";
          this.infoColor = "success";                   
          return;
        }
        this.info = respone.resultMessage;
        this.infoColor = "danger";        
      }
    })

  }



}
