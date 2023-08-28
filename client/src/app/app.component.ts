import { Component, OnInit } from '@angular/core';
import { BasketService } from './basket/basket.service';
import { AccountService } from './account/account.service';
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  title = 'skinet';
  

  constructor(
    private basketService: BasketService,
    private accountService: AccountService) { }
  
  
  ngOnInit(): void {
    this.loadBasket();
    this.loadCurrentUser();
  }

  loadCurrentUser() {
    const token = localStorage.getItem('token');
    // inserting initial value to ReplaySubject
    this.accountService.loadCurrentUser(token)
      .subscribe({
        next: () => console.log("loaded user"),
        error: err => console.log(err)
      })
    
  }

  loadBasket() {
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
