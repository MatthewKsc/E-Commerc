import { Injectable } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';

@Injectable({
  providedIn: 'root'
})
export class BusyService {
  bustRquestCount=0;

  constructor(private spinnerService: NgxSpinnerService) { }

  busy(){
    this.bustRquestCount++;
    this.spinnerService.show(undefined, {
      type: 'timer',
      bdColor: 'rgba(255,255,255,0.7)',
      color: '#333333'
    });
  }

  idle(){
    this.bustRquestCount--;
    if(this.bustRquestCount<=0){
      this.bustRquestCount = 0;
      this.spinnerService.hide()
    }
  }
}
