import {Component, NgZone} from 'angular2/core';
import {UPLOAD_DIRECTIVES} from 'ng2-uploader';

@Component({
    selector: 'fileuploader',
    templateUrl: 'app/common/fileuploader.view.html',
    directives: [UPLOAD_DIRECTIVES],
})
export class FileUploaderComponent {
    zone: NgZone;
    uploadFile: any;
    progress: number = 0;
    options: Object = {
        url: 'https://api.imgur.com/3/',
        //url: 'http://localhost:8081/api/file',
        debug: true,
        authTokenPrefix: 'Client-ID',
        authToken: 'cfc1c4d88be16f5',
        //withCredentials: true
        //url: 'http://ng2-uploader.com:10050/upload'
    };
    files: any[] = [];


    constructor() {
        this.zone = new NgZone({ enableLongStackTrace: false });
    }

    handleBasicUpload(data): void {
        //this.files[0] = data;
        this.zone.run(() => {
            this.progress = data.progress.percent;
        });
    }

    handleMultipleUpload(data): void {
        let index = this.files.findIndex(x => x.id === data.id);
        if (index === -1) {
            this.files.push(data);
        }
        else {
            //this.files[index] = data;
            this.zone.run(() => {
                this.files[index] = data;
            });
        }

        let total = 0, uploaded = 0;
        this.files.forEach(resp => {
            total += resp.progress.total;
            uploaded += resp.progress.loaded;
        });

        this.progress = Math.floor(uploaded / (total / 100));
    }

    handleDropUpload(data): void {
        let index = this.files.findIndex(x => x.id === data.id);
        if (index === -1) {
            this.files.push(data);
        }
        else {
            //this.files[index] = data;
            this.zone.run(() => {
                this.files[index] = data;
            });
        }

        let total = 0, uploaded = 0;
        this.files.forEach(resp => {
            total += resp.progress.total;
            uploaded += resp.progress.loaded;
        });

        this.progress = Math.floor(uploaded / (total / 100));
    }
}