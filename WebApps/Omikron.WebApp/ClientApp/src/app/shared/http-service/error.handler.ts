import { HttpErrorResponse } from "@angular/common/http";
import { ErrorHandler, Injectable, Injector } from "@angular/core";
import { Router } from "@angular/router";
import { LogService } from '../logging/log.service';

@Injectable({ providedIn: "root" })
export class AppErrorHandler implements ErrorHandler {
    constructor(private readonly injector: Injector) { }

    handleError(error: any) {
        const logService = this.injector.get(LogService);
        const router = this.injector.get(Router);
        if (error instanceof HttpErrorResponse) {
            let properties = error.headers;
            properties["url"] = router.url;
            logService.fatal({
                exception: error,
                message: "Fatal error occurred.",
                properties: error.headers
            });
        } else {
            logService.fatal({exception: error});
        }
    }
}