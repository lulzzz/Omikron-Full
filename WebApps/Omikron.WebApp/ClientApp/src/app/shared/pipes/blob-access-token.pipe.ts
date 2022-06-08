import { Pipe, PipeTransform } from '@angular/core';
import { AuthService } from '../auth.service';

@Pipe({
    name: 'blobAccessToken'
})
export class BlobAccessTokenPipe implements PipeTransform {
    constructor(private readonly authService: AuthService) { }

    transform(url: any): string {
        return `${url}${this.authService.getBlobAccessToken()}`
    }
}
