import { Component, OnInit } from '@angular/core';
import { BasketService } from './basket/basket.service';
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  title = 'skinet';
  

  constructor(private basketService: BasketService) { }
  
  
  ngOnInit(): void {
    // fishing out basket id from local storage
    const basketId = localStorage.getItem('basket_id');
    if(basketId) {
      this.basketService.getBasket(basketId)
        .subscribe({
          next: () => {
            console.log("init")
          },
          error: err => console.log(err)
        })
    }
  }

  
}
