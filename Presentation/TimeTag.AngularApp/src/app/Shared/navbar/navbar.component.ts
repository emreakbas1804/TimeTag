import { Component, OnInit } from '@angular/core';
declare var $: any;
@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {

  constructor() { }

  ngOnInit(): void {
    $( "#toggle-button" ).click(function() {
      $( "#mobil" ).toggle( "slow", function() {
        // Animation complete.
      });
    });
  }

}
