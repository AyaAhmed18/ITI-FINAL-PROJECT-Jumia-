
import { Component, OnChanges, OnInit, SimpleChanges } from '@angular/core';
import { Router, RouterLink, RouterOutlet } from '@angular/router';
import { ApiLoginService } from '../../Services/api-login.service';
import { ApiProductsService } from '../../Services/api-products.service';
import { SearchResultsService } from '../../Services/search-results.service';
import { FormsModule } from '@angular/forms';
import { TranslateModule, TranslateService } from '@ngx-translate/core';






@Component({
  selector: 'app-navigiation-bar',
  standalone: true,
  imports: [RouterLink, RouterOutlet, FormsModule, TranslateModule],
  templateUrl: './navigiation-bar.component.html',
  styleUrl: './navigiation-bar.component.css'
})
export class NavigiationBarComponent implements OnInit {

  loggedInUsername: string;
  


  //load page and check if logged or not
  IsUserLogged: boolean = false
  isArabic: boolean = localStorage.getItem('isArabic') === 'true';
 // isArabic1: string =  localStorage.getItem('isArabic')!;
  constructor(private _apiLoginService: ApiLoginService
    , private router: Router,
    private _ApiProductsService: ApiProductsService,
    private _searchResultsService: SearchResultsService, private translate: TranslateService) {
    this.loggedInUsername = this._apiLoginService.getLoggedInUsername();
    if (this._apiLoginService.IsLoggedIn()) {
     
    }
    else {
      this._apiLoginService.logout();
    }
  }

  ngOnInit() {
    this._apiLoginService.gettName2().subscribe((stat) => {
      this.loggedInUsername = stat
    })
   

    this.translate.onLangChange.subscribe((Event)=>{
      this.isArabic = Event.lang === 'ar'
      
    })

    this._apiLoginService.getLoggedStatus().subscribe((stat) => {
      this.IsUserLogged = stat
    })
   
  }

  SignInNav() {
    this.IsUserLogged = this._apiLoginService.IsLoggedIn();
  }
  SignOutNav() {
    this._apiLoginService.logout();
    this.IsUserLogged = this._apiLoginService.IsLoggedIn();
    this.translate.get('navigation.loggedInUsername').subscribe((translation: string) => {
      this.loggedInUsername = translation;
    });
  }

  searchTerm: string = '';


  searchProducts() {
    setTimeout(() => {
    if (this.searchTerm.trim() !== '') {
      this._ApiProductsService.SearchByNameOrDesc(this.searchTerm).subscribe(
        (searchResults) => {
          this._searchResultsService.setSearchResults([searchResults]);
         
          console.log("searchResults");
          console.log(searchResults);
        },
        (error) => {
          console.error('Error occurred while searching:', error);
        }
      );
    }}, 1000);
    this.router.navigate(['/Product'])
  }



  changeLanguage(lang: string) {
    if (lang == 'en') {
      localStorage.setItem('lang', 'en')
    }
    else {
      localStorage.setItem('lang', 'ar')
    }
       
    window.location.reload();

  }











}