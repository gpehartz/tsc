import {Component, Output, EventEmitter} from '@angular/core';

@Component({
    selector: 'imgur',
    styleUrls: ['app/common/imgur.css'],
    templateUrl: 'app/common/imgur.view.html'
})

export class ImgurComponent {
    @Output() onUploaded = new EventEmitter();
    @Output() onError= new EventEmitter();

    private url: string;
    private response: string;

    fileChangeEvent(fileInput: any) {

        var fileToUpload = fileInput.target.files[0];

        var formData = new FormData();
        formData.append("image", fileToUpload);

        var xhr = new XMLHttpRequest();
        xhr.open("POST", "https://api.imgur.com/3/image");
        
        xhr.onload = () => {
            if (xhr.readyState === 4) {
                if (xhr.status === 200) {
                    this.url = JSON.parse(xhr.responseText).data.link;
                    this.onUploaded.emit(this.url);
                } else {
                    this.response = xhr.responseText;
                    this.onError.emit(this.response);
                }
            }
        }

        xhr.setRequestHeader('Authorization', 'Client-ID cfc1c4d88be16f5');

        xhr.send(formData);
    }
}