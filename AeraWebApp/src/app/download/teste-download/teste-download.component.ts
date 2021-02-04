import { DownloadService } from './../../download.service';
import { Component, OnInit } from '@angular/core';
import { HttpHeaders } from '@angular/common/http';

@Component({
  selector: 'app-teste-download',
  templateUrl: './teste-download.component.html',
  styleUrls: ['./teste-download.component.css']
})
export class TesteDownloadComponent implements OnInit {

  downloadFile(data: Blob) {
    const blob = new Blob([data], { type: "application/octet-stream" });
    const url= window.URL.createObjectURL(blob);
    const link = document.createElement('a');
    link.href = window.URL.createObjectURL(blob);
    link.download = 'samplePDFFile.pdf';
    link.click();
    window.URL.revokeObjectURL(link.href);
    //window.open(url);
  }

  constructor(private _downloadService: DownloadService) { }

  ngOnInit(): void {
  }

  fazerDownload() {
    this._downloadService.listaDePresenca(48).subscribe(data => this.downloadFile(data)),//console.log(data),
                 error => console.log('Error downloading the file.'),
                 () => console.info('OK');
  }

}
