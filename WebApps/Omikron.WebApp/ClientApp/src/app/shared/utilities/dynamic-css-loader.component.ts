import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { TenantResolverService } from '../tenant-resolver.service';

@Component({
    selector: 'dynamic-css-loader',
    template: '',
    encapsulation: ViewEncapsulation.None
})
export class DynamicCssLoaderComponent implements OnInit {
    constructor(private readonly tenantResolver: TenantResolverService) { }

    ngOnInit() {
        const tenant = this.tenantResolver.getTenant();
        const style = document.createElement('style');
        style.innerHTML = tenant.cssStyleContent;
        document.body.appendChild(style);
    }
}
