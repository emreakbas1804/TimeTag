import { Component, OnInit } from '@angular/core';
import { AccountService } from './Services/httpService/account.service';
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'TimeTag.AngularApp';

  constructor(private accountService: AccountService, public translateService: TranslateService) {
    translateService.addLangs(["en", "tr"])
  }


  ngOnInit(): void {
    this.accountService.autoLogin();
  }
}
