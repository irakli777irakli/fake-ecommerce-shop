import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, ReplaySubject, of } from 'rxjs';
import { environment } from 'src/environments/environment';
import { IUser } from '../shared/models/user';
import { map } from 'rxjs/operators';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  baseUrl = environment.apiUrl;
  // old value emit for auth guard
  private currentUserSource = new ReplaySubject<IUser>(1);
  currentUser$ = this.currentUserSource.asObservable();


  constructor(
    private http: HttpClient,
    private router: Router) { }

  
  



  loadCurrentUser(token: string) {
    if(token === null) {
      this.currentUserSource.next(null);
      return of(null);
    }
    // fishout token from localstorage and load user from api 
    let headers = new HttpHeaders();
    headers = headers.set('Authorization',`Bearer ${token}`);

    return this.http.get(`${this.baseUrl}account/`,{headers})
      .pipe(
        map((user: IUser) => {
          if(user) {
            localStorage.setItem('token',user.token);
            this.currentUserSource.next(user);
          }
        })
      )
  }

  login(value: any) {
    return this.http.post(`${this.baseUrl}account/login`,value)
      .pipe(
        map((user:IUser) => {
          if(user ) {
            localStorage.setItem('token',user.token);
            this.currentUserSource.next(user);
          }
        })
      )
  }

  register(values: any) {
    return this.http.post(`${this.baseUrl}account/register`,values)
      .pipe(
        map((user: IUser) => {
          localStorage.setItem('token',user.token);
          this.currentUserSource.next(user);

        })
      )
  }


  logout() {
    localStorage.removeItem('token');
    this.currentUserSource.next(null);
    this.router.navigateByUrl('/');
  }

  checkEmailExists(email: string) {
    return this.http.get(`${this.baseUrl}account/emailexists?email=${email}`);
  }



}
