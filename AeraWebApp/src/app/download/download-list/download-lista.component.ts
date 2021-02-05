import { DownloadService } from '../../download.service';
import { Component, Input, OnInit } from '@angular/core';
import { Turma } from 'src/app/models/turma';

@Component({
  selector: 'botao-download-lista-presenca',
  templateUrl: './download-lista.component.html',
  styleUrls: ['./download-lista.component.css']
})
export class DownloadListaComponent implements OnInit {

  @Input()
  public turma: Turma;

  estaCarregando: boolean = false;

  downloadFile(data: Blob) {
    const blob = new Blob([data], { type: "application/octet-stream" });
    const link = document.createElement('a');
    link.href = window.URL.createObjectURL(blob);
    link.download = `listaDePresenca_${this.turma.curso}.pdf`;
    link.click();
    window.URL.revokeObjectURL(link.href);

  }

  constructor(private _downloadService: DownloadService) { }

  ngOnInit(): void {
  }

  fazerDownload() {
    this.estaCarregando = true;
    this._downloadService.listaDePresenca(this.turma.id)
      .subscribe(data => this.downloadFile(data),
                 error => this.estaCarregando = false,
                 () => this.estaCarregando = false);
  }

}
