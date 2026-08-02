let currentPage = 0;
let searchForm = document.getElementById('searchForm');
let btnInsert = document.getElementById('btnNewInsert');
let modalForm = document.getElementById('modalForm');
let submitBtn = document.getElementById('submitBtn');
const modalElement = document.getElementById('InsertMaterialCLVMModal');

const controller = 'MaterialCLVM';

searchForm.addEventListener("submit", async function (event) {
    event.preventDefault();

    await loadPage(0);
});

btnInsert.addEventListener('click', () => {
    ResetInsertModal(modalForm, submitBtn);
});

modalForm.addEventListener("submit", async (e) => {
    e.preventDefault();

    const result = await SubmitModalForm(modalForm, submitBtn, controller);

    appendAlert(result.message, "success");

    setTimeout(() => {
        window.location.href = result.redirectUrl;
    }, 1500);
});



async function loadPage(page) {

    currentPage = page;

    const result = await SearchForm(controller, searchForm, page);

    populateTable(result.items);

    PaginationControl(result.totalPages, currentPage, loadPage);
}

const populateTable = (items) => {

    const tbody = document.getElementById('tableBody');
    tbody.innerHTML = '';

    items.forEach(item => {

        const row = document.createElement('tr');
        row.id = item.id;

        const codigoCell = document.createElement('td');
        codigoCell.innerText = item.codigo;

        const especCell = document.createElement('td');
        especCell.innerText = item.especificacao;

        const descricaoCell = document.createElement('td');
        descricaoCell.innerText = item.descricao;

        const diametro1Cell = document.createElement('td');
        diametro1Cell.innerText = item.diametro1;

        const diametro2Cell = document.createElement('td');
        diametro2Cell.innerText = item.diametro2;

        const comprimentoCell = document.createElement('td');
        comprimentoCell.innerText = item.comprimento;

        const espessuraCell = document.createElement('td');
        espessuraCell.innerText = item.espessura;

        const larguraCell = document.createElement('td');
        larguraCell.innerText = item.largura;

        const pesoCell = document.createElement('td');
        pesoCell.innerText = item.peso;

        const blankCell = document.createElement('td');

        const editBtn = document.createElement('a');
        editBtn.className = 'btn btn-info d-flex align-items-center justify-content-center';
        editBtn.textContent = 'Edit';
        editBtn.addEventListener('click', (e) => {
            e.preventDefault();
            OpenEditFormModal(item, modalForm, submitBtn, modalElement);
        });
        blankCell.appendChild(editBtn);

        row.append(codigoCell, especCell, descricaoCell, diametro1Cell, diametro2Cell, comprimentoCell, espessuraCell, larguraCell, pesoCell, blankCell);
        tbody.appendChild(row);

    });

}
