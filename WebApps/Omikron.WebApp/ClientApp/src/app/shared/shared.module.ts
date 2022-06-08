import { CommonModule } from '@angular/common';
import { ErrorHandler, NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import {
    NgbButtonsModule,
    NgbModalModule,
    NgbModule,
    NgbPaginationModule,
    NgbTooltipModule,
} from '@ng-bootstrap/ng-bootstrap';
import { SweetAlert2Module } from '@sweetalert2/ngx-sweetalert2';
import { OAuthModule } from 'angular-oauth2-oidc';
import { RecaptchaModule } from 'ng-recaptcha';
import { FileUploadModule } from 'ng2-file-upload';
import { TagInputModule } from 'ngx-chips';
import { CookieService } from 'ngx-cookie-service';
import { NgxDaterangepickerMd } from 'ngx-daterangepicker-material';
import { NgxJsonViewerModule } from 'ngx-json-viewer';
import { MomentModule } from 'ngx-moment';
import { ToastrModule } from 'ngx-toastr';
import { UiSwitchModule } from 'ngx-ui-switch';

import { FallbackImageDirective, ImageComponent } from '.';
import { AuthService } from './auth.service';
import { FormApiService } from './forms/form-api.service';
import { ValidationErrorMessageComponent } from './forms/validation/validation-error-message.component';
import { AppErrorHandler } from './http-service/error.handler';
import { AppInsightsService } from './logging/implementations/app-insights.service';
import { BlobAccessTokenPipe } from './pipes/blob-access-token.pipe';
import { TenantResolverService } from './tenant-resolver.service';
import { BusyIndicatorDirective } from './utilities/busy-indicator.directive';
import { DynamicCssLoaderComponent } from './utilities/dynamic-css-loader.component';
import { SentenceCaseTextPipe } from './utilities/sentence-case-text.pipe';

@NgModule({
    imports: [
        CommonModule,
        RouterModule,
        FormsModule,
        ToastrModule.forRoot(),
        NgbModule,
        NgbButtonsModule,
        NgbPaginationModule,
        NgbModalModule,
        NgbTooltipModule,
        NgxDaterangepickerMd.forRoot(),

        OAuthModule.forRoot(),
        UiSwitchModule,
        FileUploadModule,
        RecaptchaModule,
        TagInputModule,
        NgxJsonViewerModule,
        MomentModule,
        FormsModule,
        ReactiveFormsModule
    ],
    declarations: [
        FallbackImageDirective,
        ImageComponent,
        BusyIndicatorDirective,
        ValidationErrorMessageComponent,
        DynamicCssLoaderComponent,
        SentenceCaseTextPipe,
        BlobAccessTokenPipe,

    ],
    exports: [
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
        NgbModule,
        NgbPaginationModule,
        NgbButtonsModule,
        NgbModalModule,
        NgxDaterangepickerMd,
        FileUploadModule,
        SweetAlert2Module,
        UiSwitchModule,
        OAuthModule,
        BusyIndicatorDirective,
        FallbackImageDirective,
        ImageComponent,
        ValidationErrorMessageComponent,
        DynamicCssLoaderComponent,
        RecaptchaModule,
        TagInputModule,
        SentenceCaseTextPipe,
        NgxJsonViewerModule,
        MomentModule,
        BlobAccessTokenPipe
    ],
    providers: [
        AppInsightsService,
        { provide: ErrorHandler, useClass: AppErrorHandler },
        TenantResolverService,
        AuthService,
        CookieService,
        FormApiService
    ]
})
export class SharedModule { }
