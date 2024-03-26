import { Component } from '@angular/core';
import { FilterComponent } from "../filter/filter.component";

@Component({
    selector: 'app-product',
    standalone: true,
    templateUrl: './product.component.html',
    styleUrl: './product.component.css',
    imports: [FilterComponent]
})
export class ProductComponent {

}
