import { Component, OnInit } from '@angular/core';

import { ControlOverlayRef } from '../models';
import { alertAnimations, AlertAnimationState, AlertData } from '../models/alert-notification';

@Component({
	templateUrl: './notification-message.component.html',
	animations: [alertAnimations.fadeToast]
})
export class NotificationMessageComponent implements OnInit {
	data: AlertData;
	animationState: AlertAnimationState = 'default';
	private intervalId: any;
	constructor(public dialogRef: ControlOverlayRef) {}
	ngOnInit(): void {
		this.data = this.dialogRef.data;
		this.intervalId = setTimeout(() => (this.animationState = 'closing'), 3000);
	}
	ngOnDestroy(): void {
		clearTimeout(this.intervalId);
	}
	close(): void {
		this.dialogRef.close();
	}
	onFadeFinished(event: any): void {
		const { toState } = event;
		const isFadeOut = (toState as AlertAnimationState) === 'closing';
		const itFinished = this.animationState === 'closing';
		if (isFadeOut && itFinished) {
			this.close();
		}
	}
}

