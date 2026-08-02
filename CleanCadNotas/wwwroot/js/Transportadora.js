
let currentPage = 0;
let searchForm = document.getElementById('searchForm');

let btnInsert = document.getElementById('btnNewInsert');

let modalForm = document.getElementById('modalForm');

let submitBtn = document.getElementById('submitBtn');

const modalElement = document.getElementById('InsertTransportadoraModal');


document.getElementById('searchForm').addEventListener("submit", async function (event) {
    event.preventDefault();

    await loadPage(0);
});

const populateTable = (items) => {

    const tbody = document.getElementById('tableBody');
    tbody.innerHTML = '';

    items.forEach(item => {

        const row = document.createElement('tr');
        row.id = item.id;

        const firstCell = document.createElement('td');
        firstCell.textContent = item.nome;

        const ieCell = document.createElement('td');
        ieCell.textContent = item.ie;

        const cnpjCell = document.createElement('td');
        cnpjCell.textContent = item.cnpj;

        const blankCell = document.createElement('td');

        const editBtn = document.createElement('a');
        editBtn.className = 'btn btn-info d-flex align-items-center justify-content-center';
        editBtn.textContent = 'Edit';
        editBtn.addEventListener('click', (e) => {
            e.preventDefault();
            openEditModal(item);
        });
        blankCell.appendChild(editBtn);


        row.appendChild(firstCell);
        row.appendChild(ieCell);
        row.appendChild(cnpjCell);
        row.appendChild(blankCell);
        tbody.appendChild(row);

    });

}

btnInsert.addEventListener('click', () => {
    ResetInsertModal(modalForm, submitBtn);
});


modalForm.addEventListener("submit", async (e) => {
    e.preventDefault();

    SubmitTransportadora();

});

const SubmitTransportadora = async () => {

    try {

        const value = Object.fromEntries(new FormData(modalForm).entries());

        if (value.Numero === "") {
            value.Numero = null;
        }

        const isEdit = submitBtn.dataset.action === "Edit";

        if (isEdit && !value.Id) {
            validationError("Id is required when editing");
        }

        if (!isEdit && value.Id) {
            validationError("Id should be empty when inserting");
        }

        const url = isEdit ? '/Transportadora/Edit' : '/Transportadora/Insert';

        submitBtn.disabled = true;

        const result = await fetchJson(url, {
            method: 'POST',
            body: JSON.stringify(value)
        });

        appendAlert(result.message, "success");

        setTimeout(() => {
            window.location.href = result.redirectUrl;
        }, 1500);

    }
    catch (error) {
        handleError(error)
    } finally {
        submitBtn.disabled = false;
    }

}

const openEditModal = (item) => {
    OpenEditFormModal(item, modalForm, submitBtn, modalElement);
}

async function loadPage(page) {

    currentPage = page;

    const result = await SearchForm('Transportadora',searchForm,page);

    populateTable(result.items);

    PaginationControl(result.totalPages,currentPage,loadPage);
}













