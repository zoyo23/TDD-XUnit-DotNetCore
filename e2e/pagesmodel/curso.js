import { Selector } from "testcafe";
import Page from "./page";

export default class Curso extends Page {
  constructor() {
    super();
    this.url = `${this.urlBase}/Curso/Novo`;

    this.inputNome = Selector('[name="Nome"]');
    this.inputDescricao = Selector('[name="Descricao"]');
    this.inputCargaHoraria = Selector('[name="CargaHoraria"]');
    this.selectPublicoAlvo = Selector('[name="PublicoAlvo"]');
    this.opcaoEmpreendedor = Selector('option[value="Empreendedor"]');
    this.inputValor = Selector('[name="Valor"]');
    this.btnSalvar = Selector('.btn-success');
  }
}