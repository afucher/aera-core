import { PoDynamicFormField } from '@po-ui/ng-components';

export interface Cliente {
  id?: number;
  nome: string;
  email?: string;
  celular?: string;
  cpf?: string;
  turmas?: any[];
  pagamentos?: any[];
}

export const clienteFields: Array<PoDynamicFormField> = [
  {
    property: 'id',
    label: 'Id',
    required: true,
    gridColumns: 2,
    gridSmColumns: 2,
    order: 1,
    disabled: true
  },
  { property: 'nome', label: 'Nome', required: true, maxLength: 50, gridColumns: 6, gridSmColumns: 12},
  { property: 'cpf', label: 'CPF', mask: '999.999.999-99', gridColumns: 3, gridSmColumns: 12},
  { property: 'email', label: 'Email', gridColumns: 4, gridSmColumns: 12, divider: 'Contato'},
  { property: 'telefone', label: 'Telefone', gridColumns: 2, gridSmColumns: 12, mask: '(99) 9999-9999'},
  { property: 'celular', label: 'Celular', gridColumns: 2, gridSmColumns: 12, mask: '(99) 9999?9-9999'},
  { property: 'telefone_comercial', label: 'Comercial', gridColumns: 2, gridSmColumns: 12, mask: '(99) 9999-9999'},
  { property: 'cep', label: 'CEP', gridColumns: 2, gridSmColumns: 12, mask: '99999-999', divider: 'Endereço'},
  { property: 'address1', label: 'Endereço', gridColumns: 6, gridSmColumns: 12},
  { property: 'cidade', label: 'Cidade', gridColumns: 4, gridSmColumns: 12},
  { property: 'estado', label: 'Estado', gridColumns: 2, gridSmColumns: 12, maxLength: 2},
  { property: 'address2', label: 'End. Compl. 1', gridColumns: 5, gridSmColumns: 12},
  { property: 'address3', label: 'End. Compl. 2', gridColumns: 5, gridSmColumns: 12},
  { property: 'profissao', label: 'Profissão', gridColumns: 4, gridSmColumns: 12},
  { property: 'nivel_educacao', label: 'Nível Educação', gridColumns: 2, gridSmColumns: 12, options: ['3G', '3I', '2G', '2I']},
  { property: 'data_nascimento', label: 'Data de Nascimento', gridColumns: 2, gridSmColumns: 12, type: 'date'},
  { property: 'hora_nascimento', label: 'Hora de Nascimento', gridColumns: 2, gridSmColumns: 12, type: 'time'},
  { property: 'local_nascimento', label: 'Local de Nascimento', gridColumns: 4, gridSmColumns: 12},
  { property: 'codigoAuxiliar', label: 'Código Auxiliar', gridColumns: 4, gridSmColumns: 12},
  { property: 'observacao', label: 'Observação', gridColumns: 6, gridSmColumns: 12, rows: 3}
];
