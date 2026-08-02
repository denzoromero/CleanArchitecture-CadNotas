let currentPage = 0;
let searchForm = document.getElementById('searchForm');
let btnInsert = document.getElementById('btnNewInsert');
let modalForm = document.getElementById('modalForm');
let submitBtn = document.getElementById('submitBtn');
const modalElement = document.getElementById('InsertObraModal');

searchForm.addEventListener("submit", async function (event) {
    event.preventDefault();

    await loadPage(0);
});

btnInsert.addEventListener('click', () => {
    ResetInsertModal(modalForm, submitBtn);
});

modalForm.addEventListener("submit", async (e) => {
    e.preventDefault();

    SubmitObra();

});

async function loadPage(page) {

    currentPage = page;

    const result = await SearchForm('Obra', searchForm, page);

    populateTable(result.items);

    PaginationControl(result.totalPages, currentPage, loadPage);
}

const populateTable = (items) => {

    const tbody = document.getElementById('tableBody');
    tbody.innerHTML = '';

    items.forEach(item => {

        const row = document.createElement('tr');
        row.id = item.id;

        const firstRow = document.createElement('td');
        firstRow.textContent = item.obra;

        const secondRow = document.createElement('td');
        secondRow.textContent = item.cliente;

        const thirdRow = document.createElement('td');
        thirdRow.textContent = item.descricao;

        const fourthRow = document.createElement('td');
        fourthRow.textContent = item.contrato;

        const blankCell = document.createElement('td');

        const editBtn = document.createElement('a');
        editBtn.className = 'btn btn-info d-flex align-items-center justify-content-center';
        editBtn.textContent = 'Edit';
        editBtn.addEventListener('click', (e) => {
            e.preventDefault();
            openEditModal(item);
        });
        blankCell.appendChild(editBtn);


        row.appendChild(firstRow);
        row.appendChild(secondRow);
        row.appendChild(thirdRow);
        row.appendChild(fourthRow);
        row.appendChild(blankCell);
        tbody.appendChild(row);

    });

}





const SubmitObra = async () => {

    try {

        const value = Object.fromEntries(new FormData(modalForm).entries());

        value.Transferencia = modalForm.elements["Transferencia"].checked;

        console.log(value);

        const isEdit = submitBtn.dataset.action === "Edit";

        if (isEdit && !value.Id) {
            validationError("Id is required when editing");
        }

        if (!isEdit && value.Id) {
            validationError("Id should be empty when inserting");
        }


        const url = isEdit ? '/Obra/Edit' : '/Obra/Insert';

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

};

const openEditModal = (item) => {
    OpenEditFormModal(item, modalForm, submitBtn, modalElement);
    console.log(item)
    if (item.transferencia === "1") {
        console.log('trigger');
        modalForm.elements["Transferencia"].checked = true;
    } else {
        modalForm.elements["Transferencia"].checked = false;
    }

}