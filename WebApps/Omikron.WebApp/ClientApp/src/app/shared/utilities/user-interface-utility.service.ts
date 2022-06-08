import { Injectable, Inject } from '@angular/core';
import { DOCUMENT } from '@angular/common';
import { Observable, Subject } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class UserInterfaceUtilityService {
    public roleInteractiveManagementObservable: Observable<boolean>;
    private roleInteractiveManagementSubject: Subject<boolean> = new Subject();
    
    constructor(@Inject(DOCUMENT) private readonly document: Document) {
        this.roleInteractiveManagementObservable = this.roleInteractiveManagementSubject.asObservable();
     }

    addBodyClass(className: string): void {
        this.document.body.classList.add(className);
    }

    removeBodyClass(className: string): void {
        this.document.body.classList.remove(className);
    }

    toggleRoleInteractiveManagement(enable: boolean): void {
        this.roleInteractiveManagementSubject.next(enable);
    }
}