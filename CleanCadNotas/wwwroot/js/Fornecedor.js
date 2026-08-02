

let currentPage = 0;

let filterInput = document.getElementById('Filtro');
let ativoChkbox = document.getElementById('Registro');

let forncedorForm = document.getElementById('fornecedorForm');
let submitBtn = document.getElementById('submitBtn');

let btnInsert = document.getElementById('btnNewInsert');

const modalElement = document.getElementById('exampleModal');



document.getElementById("searchForm").addEventListener("submit", async function (event) {
    event.preventDefault();

    currentPage = 0;

    await SearchFornecedor();

});

modalElement.addEventListener('hidden.bs.modal', () => {
    forncedorForm.reset();
    submitBtn.dataset.action = 'Insert';
    submitBtn.classList.replace('btn-warning', 'btn-primary');
    submitBtn.innerText = 'Salvar';

    // Clear hidden fields too
    document.getElementById('fornecedorId').value = '';
});

forncedorForm.addEventListener("submit", async (e) => {
    e.preventDefault();

    SubmitForm();
});


btnInsert.addEventListener('click', async () => {

    forncedorForm.reset();

    const result = await fetchJson(idempotencyPath);

    forncedorForm.elements["IdempotencyKey"].value = result.idempotencyKey;

    submitBtn.dataset.action = 'Insert';
    submitBtn.classList.replace('btn-warning', 'btn-primary');
    submitBtn.innerText = 'Salvar';
});

const SubmitForm = async () => {

    try {

        const fornecedor = Object.fromEntries(new FormData(forncedorForm).entries());

        const isEdit = submitBtn.dataset.action === "Edit";

        if (isEdit && !fornecedor.Id) {
            validationError("Id is required when editing");
        }

        if (!isEdit && fornecedor.Id) {
            validationError("Id should be empty when inserting");
        }

        const url = isEdit ? '/Fornecedor/Edit' : '/Fornecedor/Insert';

        submitBtn.disabled = true;

        const result = await fetchJson(url, {
            method: 'POST',
            body: JSON.stringify(fornecedor)
        });

        appendAlert(result.message, "success");

        setTimeout(() => {
            window.location.href = result.redirectUrl;
        }, 1500);


    } catch (error) {
        handleError(error)
    } finally {
        submitBtn.disabled = false;
    }

}



const SearchFornecedor = async () => {

    let url = '/Fornecedor/Search';

    const params = new URLSearchParams({
        Filter: filterInput.value,
        Ativo: ativoChkbox.checked,
        PageNo: currentPage
    });

    try {

        let completeGetUrl = `${url}?${params.toString()}`;

        const result = await fetchJson(completeGetUrl);

        console.log(result);
        populateTable(result.items);
        updatePaginationControls(result.totalPages);


    } catch (error) {
        handleError(error)
    }

};



const populateTable = (items) => {

    const tbody = document.getElementById('tableBody');
    tbody.innerHTML = '';

    items.forEach(item => {

        const row = document.createElement('tr');
        row.id = item.id;

        if (item.ativo == 0) row.classList.add('table-danger');

        const fornCell = document.createElement('td');
        fornCell.textContent = item.fornecedor;

        const fanCell = document.createElement('td');
        fanCell.textContent = item.fantasia;

        const ieCell = document.createElement('td');
        ieCell.textContent = item.ie;

        const cnpjCell = document.createElement('td');
        cnpjCell.textContent = item.cnpj;

        const emailCell = document.createElement('td');
        emailCell.textContent = item.eMail;

        const blankCell = document.createElement('td');

        const editBtn = document.createElement('a');
        editBtn.className = 'btn btn-info d-flex align-items-center justify-content-center';
        editBtn.textContent = 'Edit';
        editBtn.addEventListener('click', (e) => {
            e.preventDefault();
            openEditModal(item);
        });

        blankCell.appendChild(editBtn);

        row.appendChild(fornCell);
        row.appendChild(fanCell);
        row.appendChild(ieCell);
        row.appendChild(cnpjCell);
        row.appendChild(emailCell);
        row.appendChild(blankCell);
        tbody.appendChild(row);

    });

}


const updatePaginationControls = async (totalPages) => {

    const paginationControls = document.getElementById('paginationControls');
    paginationControls.innerHTML = '';

    const windowSize = 9;
    let startPage = Math.max(0, currentPage - 4);
    let endPage = startPage + windowSize;

    if (endPage > totalPages) {
        endPage = totalPages;
        startPage = Math.max(0, endPage - windowSize);
    }

    const prevButton = document.createElement('li');
    prevButton.className = 'page-item' + (currentPage === 0 ? ' disabled' : '');
    prevButton.innerHTML = `<a class="page-link" href="#" aria-label="Previous"><span aria-hidden="true">&laquo;</span></a>`;
    prevButton.addEventListener('click', async (event) => {
        event.preventDefault();
        if (currentPage > 0) {
            currentPage--;
            await SearchFornecedor();
        }
    });
    paginationControls.appendChild(prevButton);


    for (let i = startPage; i < endPage; i++) {
        addPageItem(i);
    }

    if (endPage < totalPages) {
        const ellipsis = document.createElement('li');
        ellipsis.className = 'page-item disabled';
        ellipsis.innerHTML = `<span class="page-link">...</span>`;
        paginationControls.appendChild(ellipsis);
        addPageItem(totalPages - 1);
    }

    const nextButton = document.createElement('li');
    nextButton.className = 'page-item' + (currentPage === totalPages - 1 ? ' disabled' : '');
    nextButton.innerHTML = `<a class="page-link" href="#" aria-label="Next"><span aria-hidden="true">&raquo;</span></a>`;
    nextButton.addEventListener('click', async (event) => {
        event.preventDefault();
        if (currentPage < totalPages - 1) {
            currentPage++;
            await SearchFornecedor();
        }
    });
    paginationControls.appendChild(nextButton);

};

async function addPageItem(i) {
    const pageItem = document.createElement('li');
    const pageLinkClass = `page-link`;
    pageItem.className = 'page-item' + (i === currentPage ? ' active' : '');
    pageItem.innerHTML = `<a class="${pageLinkClass}" href="#">${i + 1}</a>`;
    pageItem.addEventListener('click', async (event) => {
        event.preventDefault();
        currentPage = i;
        await SearchFornecedor();
    });
    paginationControls.appendChild(pageItem);
}


const openEditModal = async (item) => {

    const result = await fetchJson(idempotencyPath);
    item.IdempotencyKey = result.idempotencyKey;
    populateForm(forncedorForm, item);
    submitBtn.dataset.action = 'Edit';
    submitBtn.classList.replace('btn-primary', 'btn-warning');
    submitBtn.innerText = 'Edit';

    new bootstrap.Modal(modalElement).show();

}









