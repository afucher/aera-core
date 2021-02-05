import { DownloadService } from '../../download.service';
import { Component, Input, OnInit } from '@angular/core';
import { Cliente } from 'src/app/models/cliente';

@Component({
  selector: 'botao-download-atestado',
  templateUrl: './download-atestado.component.html',
  styleUrls: ['./download-atestado.component.css']
})
export class DownloadAtestadoComponent implements OnInit {

  @Input()
  public aluno: Cliente;

  estaCarregando: boolean = false;

  downloadFile(data: Blob) {
    const blob = new Blob([data], { type: "application/octet-stream" });
    const link = document.createElement('a');
    link.href = window.URL.createObjectURL(blob);
    link.download = `atestado_${this.aluno.nome}_${this.aluno.cpf}.pdf`;
    link.click();
    window.URL.revokeObjectURL(link.href);

  }

  constructor(private _downloadService: DownloadService) { }

  ngOnInit(): void {
  }

  fazerDownload() {
    this.estaCarregando = true;
    this._downloadService.atestado(this.aluno.id)
      .subscribe(data => this.downloadFile(data),
                 error => this.estaCarregando = false,
                 () => this.estaCarregando = false);
  }

}
