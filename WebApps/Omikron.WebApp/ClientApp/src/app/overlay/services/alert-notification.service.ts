import { Overlay } from '@angular/cdk/overlay';
import { Injectable } from '@angular/core';
import { AlertConfig, AlertData, AlertType, DefaultAlertConfig } from 'src/app/overlay/models/alert-notification';
import { NotificationMessageComponent } from 'src/app/overlay/notification-message/notification-message.component';

import { OverlayService } from './overlay.service';

​
@Injectable()
export class AlertNotificationService {
	constructor(
		private overlay: Overlay,
		private overlayService: OverlayService
	) {}
​
	showSuccess(
		options: {
			title?: string;
			text?: string;
			alertConfig?: AlertConfig;
		} = {}
	): void {
		const config = new AlertData({
			title: options.title || 'Success',
			text: options.text || 'Data has been successfully saved.',
			type: AlertType.success
		});
		const dialogRef = this.overlayService.open<AlertData>(NotificationMessageComponent, {
			positionStrategy: this.getPositionStrategy(),
			data: config,
			backdropClass: ''
		});
	}
​
	showWarning(
		options: {
			title?: string;
			text?: string;
			alertConfig?: AlertConfig;
		} = {}
	): void {
		const config = new AlertData({
			title: options.title || 'Error',
			text: options.text || 'An error occurred during the save operation. Please try again or contact an administrator.',
			type: AlertType.warning
		});
		const dialogRef = this.overlayService.open<AlertData>(NotificationMessageComponent, {
			positionStrategy: this.getPositionStrategy(),
			data: config,
			backdropClass: ''
		});
	}
​
	showInfo(
		options: {
			title?: string;
			text?: string;
			alertConfig?: AlertConfig;
		} = {}
	): void {
		const config = new AlertData({
			title: options.title || 'Info',
			text: options.text || 'Data has been successfully saved.',
			type: AlertType.info
		});
		const dialogRef = this.overlayService.open<AlertData>(NotificationMessageComponent, {
			positionStrategy: this.getPositionStrategy(),
			data: config,
			backdropClass: ''
		});
	}
​
	private getPositionStrategy() {
		return this.overlay
			.position()
			.global()
			.top(DefaultAlertConfig.position.top + 'px')
			.right(DefaultAlertConfig.position.right + 'px');
	}
}
