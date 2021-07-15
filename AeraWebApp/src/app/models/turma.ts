import { Cliente } from './cliente';

export interface Turma {
  id?: number;
  curso?: string;
  cursoId?: string;
  horárioInicial?: string;
  horárioFinal?: string;
  dataInicial?: string;
  dataFinal?: string;
  quantidadeDeAulas?: number;
  alunos?: Cliente[];
  professor?: string;
}
