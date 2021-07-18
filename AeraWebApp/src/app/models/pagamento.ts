export interface Pagamento {
  valor: number;
  parcela: number;
  totalDeParcelas: number;
  pago: boolean;
  idMatricula: number;
  dataDeVencimento: string;
}
