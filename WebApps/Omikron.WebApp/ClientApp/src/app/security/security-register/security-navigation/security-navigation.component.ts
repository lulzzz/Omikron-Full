import { Component, Input, OnInit } from '@angular/core';

import { Registration } from '../../models/registration-steps.enum';

@Component({
    selector: "app-security-navigation",
    templateUrl:"./security-navigation.component.html",
    styleUrls: ["security-navigation.component.scss"]
})
export class SecurityNavigationComponent implements OnInit {
    @Input() registration: Registration;
    constructor() {}

    setupStep(step: number): void{
        if(this.registration != undefined)
            this.registration.activeStep=step;
    }

    ngOnInit(){
        if(this.registration == undefined)
        {
            this.registration = new Registration();
            this.registration.activeStep = 5;
        }
    }
}
