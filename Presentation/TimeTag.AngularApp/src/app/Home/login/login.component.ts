import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ActivatedRoute, Route, Router } from '@angular/router';
import { tap } from 'rxjs';
import { Result } from 'src/app/Models/EntityResultModel';
import { AccountService } from 'src/app/Services/httpService/account.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  constructor(private accountService: AccountService, private activatedRoute: ActivatedRoute, private route: Router) { }
  returnUrl = "/panel";
  info: string | null = null;
  infoColor: string | null = null;
  loading: boolean = false;
  ngOnInit(): void {
    this.activatedRoute.queryParams.subscribe(c => {
      this.returnUrl = c["returnUrl"] || "/panel";
    })

  }

  login(form: NgForm) {
    if (form.invalid) {
      this.info = "Form validation error";
      this.infoColor = "danger";
      return;
    }
    this.loading = true;
    this.accountService.login(form.value.email, form.value.password).subscribe({
      next: response => {
        if (response.result == Result.Success) {
          this.route.navigate([this.returnUrl]);
        } else {
          this.info = response.resultMessage;
          this.infoColor = "danger";
          this.loading = false;
          window.scroll({
            top: 0,
            left: 0,
            behavior: 'smooth'
          });
        }
      },
      error: err => {
        this.info = err;
        this.infoColor = "danger";
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
