import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NotificationMessageComponent } from './notification-message/notification-message.component';
import { MaterialCustomModule } from '../material-custom/material-custom.module';
import { AlertNotificationService } from './services/alert-notification.service';
import { OverlayService } from './services/overlay.service';

@NgModule({
  declarations: [NotificationMessageComponent],
  imports: [
    CommonModule,
    MaterialCustomModule
  ],
  providers: [AlertNotificationService, OverlayService],
})
export class OverlayModule { }
