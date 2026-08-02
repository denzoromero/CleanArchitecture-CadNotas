let currentPage = 0;
let searchForm = document.getElementById('searchForm');
let btnInsert = document.getElementById('btnNewInsert');
let modalForm = document.getElementById('modalForm');
let submitBtn = document.getElementById('submitBtn');
const modalElement = document.getElementById('InsertTipoOCModal');


searchForm.addEventListener("submit", async function (event) {
    event.preventDefault();

    await loadPage(0);
});

btnInsert.addEventListener('click', () => {
    ResetInsertModal(modalForm, submitBtn);
});

modalForm.addEventListener("submit", async (e) => {
    e.preventDefault();

    const result = await SubmitModalForm(modalForm, submitBtn, 'TipoOC');

    appendAlert(result.message, "success");

    setTimeout(() => {
        window.location.href = result.redirectUrl;
    }, 1500);
});

async function loadPage(page) {

    currentPage = page;

    const result = await SearchForm('TipoOC', searchForm, page);

    populateTable(result.items);

    PaginationControl(result.totalPages, currentPage, loadPage);
}

const populateTable = (items) => {

    const tbody = document.getElementById('tableBody');
    tbody.innerHTML = '';

    items.forEach(item => {

        const row = document.createElement('tr');
        row.id = item.id;

        const nomeCell = document.createElement('td');
        nomeCell.innerText = item.nome;

        const blankCell = document.createElement('td');

        const editBtn = document.createElement('a');
        editBtn.className = 'btn btn-info d-flex align-items-center justify-content-center';
        editBtn.textContent = 'Edit';
        editBtn.addEventListener('click', (e) => {
            e.preventDefault();
            OpenEditFormModal(item, modalForm, submitBtn, modalElement);
        });
        blankCell.appendChild(editBtn);

        row.append(nomeCell, blankCell);
        tbody.appendChild(row);

    });

}
