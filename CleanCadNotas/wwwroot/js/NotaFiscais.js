const controller = 'NotaFiscais';

let currentPage = 0;
let searchForm = document.getElementById('searchForm');
let btnInsert = document.getElementById('btnNewInsert');
let modalForm = document.getElementById('modalForm');
let submitBtn = document.getElementById('submitBtn');
const modalElement = document.getElementById('InsertLVMModal');

const obraModalDropdown = document.getElementById('ddlObraLVM');
const requerSelect = document.querySelectorAll(".RequerLVM");



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

        const obraCell = document.createElement('td');
        obraCell.innerText = item.obra;

        const numerLVMCell = document.createElement('td');
        numerLVMCell.innerText = item.numeroLVM;

        const lvmCell = document.createElement('td');
        lvmCell.innerText = item.lvm;

        const dataCell = document.createElement('td');
        dataCell.innerText = item.dataString;

        const NotaFiscalCell = document.createElement('td');
        NotaFiscalCell.innerText = item.notaFiscal;

        const OCCell = document.createElement('td');
        OCCell.innerText = item.oc;

        const InvoiceCell = document.createElement('td');
        InvoiceCell.innerText = item.invoice;

        const QtdItemCell = document.createElement('td');
        QtdItemCell.innerText = item.qtdItem;

        const MaterialCell = document.createElement('td');
        MaterialCell.innerText = item.material;

        const DisciplinaCell = document.createElement('td');
        DisciplinaCell.innerText = item.disciplina;

        const TipoOCCell = document.createElement('td');
        TipoOCCell.innerText = item.tipoOC;

        const blankCell = document.createElement('td');

        const editBtn = document.createElement('a');
        editBtn.className = 'btn btn-info d-flex align-items-center justify-content-center';
        editBtn.textContent = 'Edit';
        editBtn.addEventListener('click', (e) => {
            e.preventDefault();
            OpenEditModalNF(item);
        });
        blankCell.appendChild(editBtn);

        row.append(obraCell, numerLVMCell, lvmCell, dataCell, NotaFiscalCell, OCCell, InvoiceCell, QtdItemCell, MaterialCell, DisciplinaCell, TipoOCCell, blankCell);
        tbody.appendChild(row);



    });

}

const OpenEditModalNF = async (item) => {

    console.log(item);

    const result = await fetchJson(idempotencyPath);
    item.IdempotencyKey = result.idempotencyKey;

    populateForm(modalForm, item);
    updateRequestLVM();

    document.getElementById('NoLVM').textContent = item.nlvm;
    document.getElementById('LabelMascaraLVM').textContent = item.mascaraLVM;

    submitBtn.dataset.action = 'Edit';
    submitBtn.classList.replace('btn-primary', 'btn-warning');
    submitBtn.innerText = 'Edit';

    new bootstrap.Modal(modalElement).show();

}


const updateRequestLVM = () => {

    const isDisabled = obraModalDropdown.value === "0";

    for (const element of requerSelect) {
        element.disabled = isDisabled;
        element.checked = false;
    }

    document.getElementById('MascaraLVM').value = '';
    document.getElementById('LabelMascaraLVM').textContent = '';

    document.getElementById('NLVM').value = '';
    document.getElementById('NoLVM').textContent = '';

}



obraModalDropdown.addEventListener("change", (event) => {
    updateRequestLVM();
});

requerSelect.forEach((elem) => {
    elem.addEventListener("change", async function (event) {

        const mascaraLabel = document.getElementById('LabelMascaraLVM');
        const mascaraInput = document.getElementById('MascaraLVM');
        const nolvmLabel = document.getElementById('NoLVM');
        const nolvmInput = document.getElementById('NLVM');

        const nolvmSelect = document.getElementById('NLVMSelect');

        const ddlComponent = document.getElementById('NLVMSelect');

        if (event.target.value === '1') {
            console.log('1');

            const url = `/${controller}/CreateMascara?IdObra=${obraModalDropdown.value}`;

            try {

                const result = await fetchJson(url);

                nolvmLabel.hidden = false;
                mascaraLabel.textContent = result.mascaraLVM;
                mascaraInput.value = result.mascaraLVM;
                nolvmLabel.textContent = result.nlvm;
                nolvmInput.value = result.nlvm;
                ddlComponent.classList.add('visually-hidden');


            } catch (error) {
                handleError(error)
            }


        } else if (event.target.value === '2') {
            console.log('2');

            mascara = `${obraModalDropdown.options[obraModalDropdown.selectedIndex].text}-SLVM`;
            lvm = 'SLVM';

            mascaraLabel.textContent = mascara;
            mascaraInput.value = mascara;
            nolvmLabel.textContent = lvm;
            nolvmInput.value = lvm;
            ddlComponent.classList.add('visually-hidden');

        } else if (event.target.value === '3') {

            const url = `/${controller}/PopulateNLVMSelect?IdObra=${obraModalDropdown.value}`;

            mascaraLabel.textContent = '';
            mascaraInput.value = '';
            nolvmLabel.textContent = '';
            nolvmInput.value = '';

            try {

                const result = await fetchJson(url);

                ddlComponent.innerHTML = '';

                const defaultOption = document.createElement('option');
                defaultOption.value = '';
                defaultOption.textContent = 'Selecionar...';
                ddlComponent.appendChild(defaultOption);

                result.value.forEach(item => {

                    const option = document.createElement('option');
                    option.value = item;
                    option.textContent = item;
                    ddlComponent.appendChild(option);
                });

                ddlComponent.classList.remove('visually-hidden');
            }
            catch (error) {

                if (error.type == 'Business') {
                    event.target.checked = false;
                    event.target.disabled = true;
                    ddlComponent.classList.add('visually-hidden');
                }

                handleError(error);
            }


        } else {
            console.log('else');
        }

    });
});


updateRequestLVM();