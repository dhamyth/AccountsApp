import { HttpClient } from '@angular/common/http';
import { Component, inject, OnInit } from '@angular/core';
import { AccountBalances } from '../_models/account-balances';
import { ToastrService } from 'ngx-toastr';
import { AccountBalancesService } from '../_services/account-balances.service';

@Component({
  selector: 'app-current-balance',
  standalone: true,
  imports: [],
  templateUrl: './current-balance.component.html',
  styleUrl: './current-balance.component.css'
})
export class CurrentBalanceComponent implements OnInit {
  private toastr = inject(ToastrService);

  data: AccountBalances | undefined;
  accountBalanceService = inject(AccountBalancesService);

  ngOnInit() {
    this.getAccountBalanceData();
  }

  getAccountBalanceData() {
    this.accountBalanceService.getAccountBalances().subscribe({
      next: (response) => {
        this.data = response;
      },
      error: (error) => this.toastr.error(error.error)
    });
  }
}
