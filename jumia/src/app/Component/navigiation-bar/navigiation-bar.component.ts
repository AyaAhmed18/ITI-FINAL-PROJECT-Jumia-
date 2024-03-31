import { Component, OnInit } from '@angular/core';
import { RouterLink, RouterOutlet } from '@angular/router';
import { ApiProductsService } from '../../Services/api-products.service';
import { FormsModule } from '@angular/forms';
import { SearchResultsService } from '../../Services/search-results.service';


@Component({
  selector: 'app-navigiation-bar',
  standalone: true,
  imports: [RouterLink,RouterOutlet,FormsModule,],
  templateUrl: './navigiation-bar.component.html',
  styleUrl: './navigiation-bar.component.css'
})
export class NavigiationBarComponent implements OnInit {
  searchTerm: string = '';

  constructor(private _ApiProductsService :ApiProductsService, private _searchResultsService: SearchResultsService) {}
  ngOnInit(): void {
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
