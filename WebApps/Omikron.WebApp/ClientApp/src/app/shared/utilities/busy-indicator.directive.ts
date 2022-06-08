import { Directive, ElementRef, Input, OnChanges, Renderer2 }  from "@angular/core";


@Directive({
    selector: '[busyIndicator]'
})
export class BusyIndicatorDirective implements OnChanges {
    @Input('busyIndicator') isBusy: boolean;
    @Input() overlay = false;

    private busyClassName = 'loading-spinner';
    private overlayClassName = 'overlay';

    constructor(
        private element: ElementRef,
        private renderer: Renderer2) {
    }

    ngOnChanges(changes: Object): void {
        this.setIsBusy();
    }

    private setIsBusy(): void {
        this.isBusy ? this.renderer.addClass(this.element.nativeElement, this.busyClassName) : this.renderer.removeClass(this.element.nativeElement, this.busyClassName);
        if (this.overlay)
            this.isBusy ? this.renderer.addClass(this.element.nativeElement, this.overlayClassName) : this.renderer.removeClass(this.element.nativeElement, this.overlayClassName);
    }
}