import { Component, OnInit } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
declare var $: any;
@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {

  constructor(public translateService: TranslateService) { }

  ngOnInit(): void {
    $( "#toggle-button" ).click(function() {
      $( "#mobil" ).toggle( "slow", function() {
        // Animation complete.
      });
    });
  }



}
