import { Selector } from "testcafe";

export default class Page {
  constructor() {
    this.urlBase = 'localhost:46437';

    this.tituloDaPagina = Selector('title');
    this.toastMessage = Selector('.toast-message');
  }
}