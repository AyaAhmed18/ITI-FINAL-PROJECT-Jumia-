import { Component, OnInit } from '@angular/core';
import { CategoryserviceService } from '../../Services/categoryservice.service';
import { Router } from '@angular/router';
import { TranslateService } from '@ngx-translate/core';
import { DomSanitizer } from '@angular/platform-browser';

@Component({
  selector: 'app-category-slider',
  standalone: true,
  imports: [],
  templateUrl: './category-slider.component.html',
  styleUrl: './category-slider.component.css'
})
export class CategorySliderComponent implements OnInit {
  allCategories : any[]=[];




  SelectedCategoryId : number=0;
  isArabic: boolean = localStorage.getItem('isArabic') === 'true';
  loggedInUsername!:string
constructor(private _categoryService :CategoryserviceService

  ,private _router : Router,
  private  translate: TranslateService,

private _sanitizer:DomSanitizer,

)
{

}
ngOnInit(): void {
  
this.GetCategories();
}

  GetCategories()
    {
      this._categoryService.getAllCategory()
      .subscribe({ next: (data) => {
        this.allCategories = data;
        console.log("allCategories")
        console.log(data)
      }
      });
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

    isArabicLanguage(): boolean {
      return this.translate.currentLang === 'ar';
    }

    
    GetProductsByCatId(categoryId: number):void
{
  this._router.navigateByUrl(`/GetCategory/${categoryId}`);
}
}
