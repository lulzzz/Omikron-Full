import { Injectable } from '@angular/core';
import { CookieService } from 'ngx-cookie-service';
import { UserBase } from '../shared';

@Injectable()
export class SecurityUserPersistenceService {
    constructor(private cookieService: CookieService) { }

    save(user: UserBase): void {
        const payload = JSON.stringify(user);
        this.cookieService.set(`${user.id}-profile`, payload);
    }

    update(user: UserBase): void {
        const exists = this.getUsers().some(u => u.id === user.id);
        if (exists) {
            this.save(user);
        }
    }

    getUsers(): UserBase[] {
        const users = [];
        const cookies = this.cookieService.getAll();
        for (var key in cookies) {
            const payload = cookies[key];
            const user = JSON.parse(payload);
            users.push(user);
        }
        return users;
    }

    remove(user: UserBase): void {
        this.cookieService.delete(`${user.id}-profile`);
    }
}
