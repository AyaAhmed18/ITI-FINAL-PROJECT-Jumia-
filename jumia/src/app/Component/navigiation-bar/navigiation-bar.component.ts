import { Component, OnInit } from '@angular/core';
import { Router, RouterLink, RouterOutlet } from '@angular/router';
import { ApiProductsService } from '../../Services/api-products.service';
import { FormsModule } from '@angular/forms';
import { SearchResultsService } from '../../Services/search-results.service';
import { ApiLoginService } from '../../Services/api-login.service';


@Component({
  selector: 'app-navigiation-bar',
  standalone: true,
  imports: [RouterLink,RouterOutlet,FormsModule,],
  templateUrl: './navigiation-bar.component.html',
  styleUrl: './navigiation-bar.component.css'
})
export class NavigiationBarComponent implements OnInit {
  searchTerm: string = '';
  loggedInUsername: string="";

  //load page and check if logged or not
  IsUserLogged:boolean=false
  ngOnInit() {
   // this.IsUserLogged= this._apiLoginService.IsLoggedIn();
  // this.loggedInUsername = this._apiLoginService.getLoggedInUsername();
    this._apiLoginService.getLoggedStatus().subscribe((stat)=>{
    this.IsUserLogged=stat
   })
   this._apiLoginService.gettName2().subscribe((stat)=>{
    this.loggedInUsername=stat
   })
   }
  constructor(private _ApiProductsService :ApiProductsService, private _searchResultsService: SearchResultsService ,private _apiLoginService : ApiLoginService , private router: Router) {



    this.loggedInUsername = this._apiLoginService.getLoggedInUsername();
    if(this._apiLoginService.IsLoggedIn()){
      
    }
    else{
      this._apiLoginService.logout();
    }
    
  }
 
  SignInNav(){

    this.IsUserLogged= this._apiLoginService.IsLoggedIn();
  }
  SignOutNav(){
    this._apiLoginService.logout();
    this.IsUserLogged= this._apiLoginService.IsLoggedIn();
  }




  
 
  searchProducts() {
    if (this.searchTerm.trim() !== '') {
      this._ApiProductsService.SearchByNameOrDesc(this.searchTerm).subscribe(
        (searchResults) => {
          this._searchResultsService.setSearchResults([searchResults]);
          console.log(searchResults);
        },
        (error) => {
          console.error('Error occurred while searching:', error);
        }
      );
    }

  }

}
