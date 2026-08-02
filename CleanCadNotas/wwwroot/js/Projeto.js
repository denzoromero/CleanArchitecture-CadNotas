let currentPage = 0;
let searchForm = document.getElementById('searchForm');
let btnInsert = document.getElementById('btnNewInsert');
let modalForm = document.getElementById('modalForm');
let submitBtn = document.getElementById('submitBtn');
const modalElement = document.getElementById('InsertProjetoModal');


searchForm.addEventListener("submit", async function (event) {
    event.preventDefault();

    await loadPage(0);
});

btnInsert.addEventListener('click', () => {
    ResetInsertModal(modalForm, submitBtn);
});

modalForm.addEventListener("submit", async (e) => {
    e.preventDefault();

    const formData = new FormData(modalForm);

    const entries = Object.fromEntries(formData.entries());
    entries.IdObras = formData.getAll("IdObras").map(Number);

    console.log(entries);

    const result = await SubmitModalFormEntries(entries, submitBtn, 'Projeto');

    appendAlert(result.message, "success");

    setTimeout(() => {
        window.location.href = result.redirectUrl;
    }, 1500);
});

async function loadPage(page) {

    currentPage = page;

    const result = await SearchForm('Projeto', searchForm, page);

    populateTable(result.items);

    PaginationControl(result.totalPages, currentPage, loadPage);
}

const populateTable = (items) => {

    const tbody = document.getElementById('tableBody');
    tbody.innerHTML = '';

    items.forEach(item => {

        const row = document.createElement('tr');
        row.id = item.id;

        const projetoCell = document.createElement('td');
        projetoCell.textContent = item.projeto;

        const obrasCell = document.createElement('td');
        obrasCell.textContent = item.obras;

        const blankCell = document.createElement('td');
        const editBtn = document.createElement('a');
        editBtn.className = 'btn btn-info d-flex align-items-center justify-content-center';
        editBtn.textContent = 'Edit';
        editBtn.addEventListener('click', (e) => {
            e.preventDefault();
            OpenEditFormModal(item, modalForm, submitBtn, modalElement);
        });
        blankCell.appendChild(editBtn);

        row.append(projetoCell, obrasCell, blankCell);
        tbody.appendChild(row);

    });

}


// const OpenEditModal = async (item) => {

//     const result = await fetchJson(idempotencyPath);
//     item.IdempotencyKey = result.idempotencyKey;

//     for (const field of modalForm.elements) {

//         const key = Object.keys(item)
//             .find(k => k.toLowerCase() === field.name.toLowerCase());

//         if (!key) continue;

//         if (field.tagName === 'SELECT' && field.multiple) {

//             const values = item[key] ?? [];

//             for (const option of field.options) {
//                 option.selected = values.includes(Number(option.value));
//             }

//             continue;
//         }

//         field.value = item[key] ?? '';
//     }

//     // const selectObras = modalForm.querySelector('[name="IdObras"]');

//     // modalForm.querySelector('[name="Projeto"]').value = item.projeto;

//     // for (const option of selectObras.options) {
//     //     option.selected = item.idObras.includes(Number(option.value));
//     // }

//     new bootstrap.Modal(modalElement).show();
// }

