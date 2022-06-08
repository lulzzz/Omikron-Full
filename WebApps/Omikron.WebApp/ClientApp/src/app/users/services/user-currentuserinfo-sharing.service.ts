import { Injectable } from '@angular/core';
import { Observable, BehaviorSubject } from 'rxjs';
import { UserBase } from 'src/app/shared';

@Injectable({ providedIn: 'root' })
export class CurrentUserInfoSharingService {
    private userInfo = new BehaviorSubject<UserBase>({} as UserBase);

    constructor() { }

    updateUserInfo(newUserInfo: UserBase) {
        this.userInfo.next(newUserInfo);
    }

    getUserInfo() : Observable<any> {
        return this.userInfo.asObservable();
    }

    clearUserInfo() {
        this.userInfo.next({} as UserBase);
    }
}