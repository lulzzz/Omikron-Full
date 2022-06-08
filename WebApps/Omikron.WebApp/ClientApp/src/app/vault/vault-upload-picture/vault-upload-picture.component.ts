import { Component, ElementRef, EventEmitter, Input, OnInit, Output, ViewChild } from '@angular/core';
import { FileUploader, FileUploaderOptions } from 'ng2-file-upload';

import { AlertNotificationService } from '../../overlay/services/alert-notification.service';
import { ApiResponse } from '../../shared';
import { AuthService } from '../../shared/auth.service';
import { VaultApiService } from '../vault-api.service';

@Component({
    selector: "app-vault-upload-picture",
    templateUrl: "./vault-upload-picture.component.html",
    styleUrls: ["./vault-upload-picture.component.scss"],
})
export class VaultUploadPictureComponent implements OnInit {
    private uploaderOptions: FileUploaderOptions = {
        autoUpload: true,
        isHTML5: true,
        removeAfterUpload: true,
        maxFileSize: 12582912,
        allowedMimeType: ["image/png", "image/jpg", "image/jpeg"],
    };
    @Input() itemPhoto: string;
    @Input() editMode: boolean = false;
    @Output() sendPhoto = new EventEmitter<string>();
    public uploader: FileUploader;
    @ViewChild("uploaderRef") uploaderRef: ElementRef;
    constructor(
        protected alertNotificationService: AlertNotificationService,
        protected vaultApiService: VaultApiService,
        private authService: AuthService) {}
    ngOnInit() {
        this.initFileUploader();
    }

    removePicture(): void {
        if(this.editMode)
        {
            this.itemPhoto = null;
            this.updatePhoto();
        }
        else{
            var photoName = this.itemPhoto.split("vault-service/");

            this.vaultApiService.deleteVaultItemPhoto(photoName[1]).subscribe(
                (data) => {
                    this.itemPhoto = null;
                    this.updatePhoto();
                },
                (error) => {
                    this.alertNotificationService.showWarning({
                        text: error.error.errors
                            ? error.error.errors[0]
                            : error.error.Message,
                    });
                }
            );
        }
    }

    private initFileUploader(): void {
        if (this.uploader) {
            this.uploader.destroy();
            this.uploader = null;
        }

        this.uploaderOptions.headers = [
            {
                name: "Authorization",
                value: this.authService.authorizationHeader(),
            },
        ];

        this.uploader = new FileUploader(this.uploaderOptions);
        this.uploader.onBeforeUploadItem = (fileItem) => {
            fileItem.withCredentials = false;
            fileItem.url = `${this.vaultApiService.baseUrl}/vault-item-photo`;
            fileItem.method = "POST";
        };

        this.uploader.onAfterAddingFile = () =>
            this.uploader.queue.splice(0, this.uploader.queue.length - 1);
        this.uploader.onSuccessItem = this.onSuccessAddPhoto;
        this.uploader.onWhenAddingFileFailed = this.onWhenAddingFileFailed;
    }

    private onWhenAddingFileFailed = async (fileItem: any) => {
        if (fileItem.size > this.uploaderOptions.maxFileSize) {
            this.alertNotificationService.showWarning({
                text:
                    "The file you have uploaded is bigger than " +
                    this.uploaderOptions.maxFileSize / 1024 / 1024 +
                    " MB",
            });
        }
        var imageType = fileItem.name.split(".").pop();
        if (
            this.uploaderOptions.allowedMimeType.find((type) =>
                type.match("image/" + imageType)
            ) == undefined
        ) {
            this.alertNotificationService.showWarning({
                text:
                    "The file format " +
                    imageType +
                    " is not supported. " +
                    "Supported formats are: " +
                    this.uploaderOptions.allowedMimeType.map(
                        (type) => " " + type.split("/")[1]
                    ),
            });
        }
    };

    private onSuccessAddPhoto = (uploader: any, response: string) => {
        const data = JSON.parse(response) as ApiResponse<any>;
        this.itemPhoto = data.records;
        this.updatePhoto();
    };

    onFileClick(event: any) {
        event.target.value = "";
    }

    updatePhoto(){
        this.sendPhoto.emit(this.itemPhoto);
    }
}
