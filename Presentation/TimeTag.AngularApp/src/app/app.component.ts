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
<<<<<<< HEAD
    translateService.addLangs(["en", "tr"])
=======
    translateService.addLangs(["en", "tr"]);
    var selectedLang = localStorage.getItem("langCode");
    
    
    translateService.use(selectedLang ?? "en");
>>>>>>> 33172811856cd780c1ce01dd3e21954380600f2c
  }


  ngOnInit(): void {
    this.accountService.autoLogin();
  }
}
