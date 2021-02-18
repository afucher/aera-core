import { PoDynamicFormField } from "@po-ui/ng-components";

export interface Curso {
  id?: number;
  nome: string;
}

export const campos: Array<PoDynamicFormField> = [
  {
    property: 'id',
    label: 'Id',
    required: true,
    gridColumns: 2,
    gridSmColumns: 2,
    order: 1,
    disabled: true
  },
  {
    property: 'nome',
    label: 'Nome',
    required: true,
    minLength: 4,
    maxLength: 50,
    gridColumns: 6,
    gridSmColumns: 12,
    order: 2,
  },
{property: 'cargaHorária', label: 'Carga Horária', gridColumns: 2, gridSmColumns: 4, required: true, type: 'number'},
{property: 'descrição', label: 'Descrição', rows: 3, maxLength: 255}]
