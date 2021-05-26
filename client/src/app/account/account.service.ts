import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { BehaviorSubject } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { User } from '../models/User';

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  baseUrl = environment.apiUrl;
  private loginURL = this.baseUrl+'account/login';
  private registerURL = this.baseUrl+'account/register';
  private checkEmailURL = this.baseUrl+'account/emailexists?email=';
  private tokenItem = 'token';

  private currentUserSource = new BehaviorSubject<User>(null);
  currentUser$ = this.currentUserSource.asObservable();

  constructor(private http: HttpClient, private router: Router) { }

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
}
