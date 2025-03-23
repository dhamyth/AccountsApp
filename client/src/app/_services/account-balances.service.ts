import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { AccountBalances } from '../_models/account-balances';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AccountBalancesService {
  private http = inject(HttpClient);
  baseUrl = environment.apiUrl;

  getAccountBalances(){
    return this.http.get<AccountBalances>(this.baseUrl + 'AccountBalance/balance');
  }
}
