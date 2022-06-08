import { Injectable } from '@angular/core';
import { DeviceDetectorService } from 'ngx-device-detector';
import { UserInterfaceUtilityService } from './user-interface-utility.service';

@Injectable({ providedIn: 'root' })
export class DeviceService {
    constructor(
        private deviceDetectorService: DeviceDetectorService,
        private userInterfaceUtilityService: UserInterfaceUtilityService) { }

    initialize(): void {
        this.userInterfaceUtilityService.addBodyClass(this.getBrowser());

        if (this.deviceDetectorService.isDesktop()) {
            this.userInterfaceUtilityService.addBodyClass('desktop');
        }
        else if (this.deviceDetectorService.isMobile()){
            this.userInterfaceUtilityService.addBodyClass('mobile');
        }
        else if (this.deviceDetectorService.isTablet()){
            this.userInterfaceUtilityService.addBodyClass('tablet');
        }
    }

    getBrowser(): string {
        return this.deviceDetectorService.browser.toLowerCase();
    }
}