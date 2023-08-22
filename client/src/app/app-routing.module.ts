import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './home/home.component';


const routes: Routes = [
  {path: '',component: HomeComponent},
  // lazy loading, shop module and it's registered routes
  {path: 'shop',loadChildren: () =>
   import('./shop/shop.module')
   .then(mod => mod.ShopModule)
  },
  {path: '**',redirectTo: '',pathMatch:'full'},

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
