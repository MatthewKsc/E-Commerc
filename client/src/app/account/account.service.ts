import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Address } from '../models/address';
import { BehaviorSubject, of, ReplaySubject } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { User } from '../models/User';

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  baseUrl = environment.apiUrl;
  private accountURL = this.baseUrl+'account';
  private loginURL = this.accountURL+'/login';
  private registerURL = this.accountURL+'/register';
  private checkEmailURL = this.accountURL+'/emailexists?email=';
  private addressURL = this.accountURL+'/address';
  private tokenItem = 'token';
  private tokenPrefix= 'Bearer ';

  private currentUserSource = new ReplaySubject<User>(1);
  currentUser$ = this.currentUserSource.asObservable();

  constructor(private http: HttpClient, private router: Router) { }

  loadCurrentUser(token: string){

    if(token === null){
      this.currentUserSource.next(null);
      return of(null);
    }

    let headers = new HttpHeaders();
    headers = headers.set('Authorization', this.tokenPrefix+token);

    return this.http.get(this.accountURL, {headers}).pipe(
      map((user:User) => {
        if(user){
          localStorage.setItem(this.tokenItem, user.token);
          this.currentUserSource.next(user);
        }
      })
    );
  }

  login(values: any){
    return this.http.post(this.loginURL, values).pipe(
      map((user:User) =>{
        if(user){
          localStorage.setItem(this.tokenItem, user.token);
          this.currentUserSource.next(user);
        }
      })
    );
  }

  register(values: any){
    return this.http.post(this.registerURL, values).pipe(
      map((user: User)=>{
        if(user){
          localStorage.setItem(this.tokenItem, user.token);
          this.currentUserSource.next(user);
        }
      })
    );
  }

  logout(){
    localStorage.removeItem(this.tokenItem)
    this.currentUserSource.next(null);
    this.router.navigateByUrl('/');
  }

  checkEmailExists(email: string){
    return this.http.get(this.checkEmailURL+email);
  }

  getUserAddress(){
    return this.http.get(this.baseUrl+'account/address');
  }

  updateUserAddress(address: Address){
    return this.http.put(this.addressURL, address);
  }
}
