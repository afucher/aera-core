import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-client-list',
  templateUrl: './client-list.component.html',
  styleUrls: ['./client-list.component.css']
})
export class ClientListComponent implements OnInit {

  constructor() { }

  ngOnInit(): void {
  }

  public readonly fields: Array<any> = [
    { property: 'id', key: true },
    { property: 'nome', label: 'Nome', filter: true, gridColumns: 6 }
  ];

}
