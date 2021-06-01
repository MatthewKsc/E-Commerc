import { Component, Input, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { AccountService } from 'src/app/account/account.service';
import { Address } from 'src/app/models/address';

@Component({
  selector: 'app-checkout-address',
  templateUrl: './checkout-address.component.html',
  styleUrls: ['./checkout-address.component.scss']
})
export class CheckoutAddressComponent implements OnInit {

  @Input() checkoutForm: FormGroup;

  constructor(private accountService: AccountService, private toaster: ToastrService) { }

  ngOnInit(): void {
  }

  saveUserAddress(){
    this.accountService.updateUserAddress(this.checkoutForm.get('addressForm').value).subscribe((address: Address)=>{
      this.toaster.success('Address saved');
      this.checkoutForm.get('addressForm').reset(address);
    }, error=>{
      console.log(error);
    });
  }

}
