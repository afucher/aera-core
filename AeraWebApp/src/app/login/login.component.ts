import { Router } from '@angular/router';
import { AutenticacaoService } from './../autenticacao.service';
import { Component, OnInit } from '@angular/core';
import { PoPageLogin } from '@po-ui/ng-templates/public-api';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  carregando = false;

  constructor(private autenticacao: AutenticacaoService, private router: Router ) { }

  ngOnInit(): void {
  }

  onSubmit(login: PoPageLogin) {
    this.carregando = true;
    this.autenticacao.login(login.login, login.password)
    .subscribe(() => {
      this.router.navigate(['/']);
    },
    error => this.carregando = false,
    () => this.carregando = false);
  }

}
