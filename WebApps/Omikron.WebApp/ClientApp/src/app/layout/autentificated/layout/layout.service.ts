import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';
import { StickyHeaderContent } from './layout.model';

@Injectable({
    providedIn: 'root'
})
export class LayoutService {
    private changeBackgroundSource = new Subject<boolean>();
    private stickyHeaderContentSource = new Subject<StickyHeaderContent>();

    backgroundChanged$ = this.changeBackgroundSource.asObservable();
    headerContent$ = this.stickyHeaderContentSource.asObservable();

    changeBackground(transparent: boolean) {
        this.changeBackgroundSource.next(transparent);
    }

    sendHeaderContent(stickyHeaderContent: StickyHeaderContent) {
        this.stickyHeaderContentSource.next(stickyHeaderContent);
    }
}
