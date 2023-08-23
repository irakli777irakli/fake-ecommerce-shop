import { Component, OnInit } from '@angular/core';
import { IProduct } from 'src/app/shared/models/product';
import { ShopService } from '../shop.service';
import { ActivatedRoute } from '@angular/router';
import { BreadcrumbService } from 'xng-breadcrumb';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.scss']
})
export class ProductDetailsComponent implements OnInit {
  product: IProduct

  constructor(
    private shopService: ShopService,
    private activeRoute: ActivatedRoute,
    private bcService: BreadcrumbService) { 
      this.bcService.set('@productDetails','');
  }

  ngOnInit(): void {
    this.loadProducts();
  }

  loadProducts() {
    this.shopService
    .getProduct(+this.activeRoute.snapshot.paramMap.get('id'))
      .subscribe({
        next: response => {
          this.product = response;
          // replaces id to name shop-routing aliased
          this.bcService.set('@productDetails',this.product.name);
        },
        error: err => console.log(err)
      })
  }

}
