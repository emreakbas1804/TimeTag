import { Component, OnInit } from '@angular/core';
import { AccountService } from './Services/httpService/account.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'TimeTag.AngularApp';

  constructor(private accountService: AccountService) {

  }

  ngOnInit(): void {
    this.accountService.autoLogin();
  }
}
