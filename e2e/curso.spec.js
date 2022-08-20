import { Selector } from 'testcafe';
import Curso from './pagesmodel/curso';

const _INTERVALO_MS_ = 1 * 1000;
const curso = new Curso();

fixture('Curso')
  .page(curso.url);

test('Deve Criar um novo curso', async t => {

  await t
    .typeText(curso.inputNome, `Curso Teste Café - ${Date.now().toLocaleString()}`)
    .wait(_INTERVALO_MS_)
    .typeText(curso.inputDescricao, 'Uma descrição bem interessante sobre um curso de teste E2E (End to End)')
    .wait(_INTERVALO_MS_)
    .typeText(curso.inputCargaHoraria, '10')
    .wait(_INTERVALO_MS_)
    .click(curso.selectPublicoAlvo)
    .wait(_INTERVALO_MS_)
    .click(curso.opcaoEmpreendedor)
    .wait(_INTERVALO_MS_)
    .typeText(curso.inputValor, '1000')
    .wait(_INTERVALO_MS_);

  await t
    .click(curso.btnSalvar);

  await t
    .expect(curso.tituloDaPagina.innerText)
    .eql('Listagem de cursos - CursoOnline.Web');

});

test('Deve validar campos obrigatórios', async t => {
  await t
    .click(curso.btnSalvar)
    .wait(_INTERVALO_MS_);

  await t
    .expect(curso.toastMessage.withText('Nome Inválido.').count).eql(1)
    .expect(curso.toastMessage.withText('Carga horária deve ser maior que 1 hora.').count).eql(1)
    .expect(curso.toastMessage.withText('Valor deve ser maior que R$1,00.').count).eql(1);
});