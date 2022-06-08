import { Component, OnInit, ChangeDetectionStrategy, ChangeDetectorRef } from "@angular/core";
import { AuthService } from 'src/app/shared/auth.service';
import { UsersApiService } from 'src/app/users/users-api.service';
import { UserBase, ApiResponse, Constants } from 'src/app/shared';
import { FileUploaderOptions, FileUploader } from 'ng2-file-upload';
import { SwalComponent, SweetAlert2LoaderService } from '@sweetalert2/ngx-sweetalert2';
import { TenantResolverService } from 'src/app/shared/tenant-resolver.service';
import { Router } from '@angular/router';
import { SecurityUserPersistenceService } from '../security-user-persistence.service';

@Component({
    selector: "security-onboarding",
    templateUrl: "security-onboarding.component.html",
    changeDetection: ChangeDetectionStrategy.OnPush

})
export class SecurityOnboardingComponent implements OnInit {
    isBusy: boolean;
    user: UserBase = <UserBase>{};
    errors: string[] = [];

    private uploaderOptions: FileUploaderOptions = {
        autoUpload: true,
        isHTML5: true,
        removeAfterUpload: true,
        maxFileSize: 3097152,
        allowedMimeType: ['image/png', 'image/jpg', 'image/jpeg'],
    };
    public uploader: FileUploader;

    constructor(
        private readonly cd: ChangeDetectorRef,
        private readonly authService: AuthService,
        private readonly tenantResolver: TenantResolverService,
        private readonly router: Router,
        private readonly securityUserPersistenceService: SecurityUserPersistenceService,
        private readonly sweetAlert2Loader: SweetAlert2LoaderService,
        private readonly userApiService: UsersApiService) { }

    ngOnInit() {
        this.user = this.authService.getUserProfile();
        this.initFileUploader();
    }

    save(): void {
        this.setBusy(true);
        this.userApiService.updateOnboarding(this.user).subscribe(() => {
            this.authService.updateClaims(this.user);
            this.securityUserPersistenceService.update(this.user);
            this.setBusy(false);
            this.router.navigate(["/home"]);
        }, (response) => {
            this.errors = response.error.errors;
            this.setBusy(false);
        });
    }

    private initFileUploader(): void {
        if (this.uploader) {
            this.uploader.destroy();
            this.uploader = null;
        }

        this.uploaderOptions.headers = [{
            name: 'Authorization',
            value: this.authService.authorizationHeader()
        },
        {
            name: Constants.TenantHeaderKey,
            value: this.tenantResolver.getTenantIdentity()
        }];

        this.uploader = new FileUploader(this.uploaderOptions);

        this.uploader.onBeforeUploadItem = fileItem => {
            fileItem.withCredentials = false;
            fileItem.url = `${this.userApiService.baseUrl}/profile-image`;
            fileItem.method = "PUT";
            this.setBusy(true);
        };

        this.uploader.onAfterAddingFile = () =>
            this.uploader.queue.splice(0, this.uploader.queue.length - 1);

        this.uploader.onSuccessItem = this.onSuccessAddPhoto;
        this.uploader.onErrorItem = () => (this.isBusy = false);
        this.uploader.onWhenAddingFileFailed = this.onWhenAddingFileFailed;
    }

    private onWhenAddingFileFailed = async (fileItem: any) => {
        if (fileItem.size > this.uploaderOptions.maxFileSize) {
            const swalAlert = await this.sweetAlert2Loader.swal;
            swalAlert.fire({
                text: `The image size exceeded the maximum size. The image '${fileItem.name}' is bigger than 3Mb`,
                titleText: 'File size exceeded the maximum size.',
                buttonsStyling: false,
                customClass: {
                    confirmButton: 'btn btn-primary',
                }
            });
        }
    };

    private onSuccessAddPhoto = (uploader: any, response: string) => {
        const data = JSON.parse(response) as ApiResponse<any>;
        this.user.profilePhoto = data.records;
        this.setBusy(false);
    };

    private setBusy(isBusy: boolean) {
        this.isBusy = isBusy;
        this.cd.markForCheck();
    }
}