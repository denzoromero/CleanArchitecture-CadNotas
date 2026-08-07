

const controller = 'CLVM';

let currentPage = 0;
let searchForm = document.getElementById('searchForm');
const tbody = document.getElementById('tableBody');
const countLabel = document.getElementById('totalCountLabel');

searchForm.addEventListener("submit", async function (event) {
    event.preventDefault();

    await loadPage(0);
});

async function loadPage(page) {

    currentPage = page;

    const result = await SearchForm(controller, searchForm, page);

    populateTable(result.items);

    countLabel.textContent = result.totalCount;

    PaginationControl(result.totalPages, currentPage, loadPage);
}

const populateTable = (items) => {

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

        const clvmCell = document.createElement('td');
        const clvmBtn = document.createElement('a');
        clvmBtn.className = 'link-offset-2 link-offset-3-hover link-underline link-underline-opacity-0 link-underline-opacity-75-hover';
        clvmBtn.textContent = 'CLVM';
        clvmBtn.href = `/CLVM/CLVMPage?id=${item.id}`;
        clvmCell.appendChild(clvmBtn);

        console.log(item);

        const exportCell = document.createElement('td');
        const controlTubBtn = document.createElement('a');
        controlTubBtn.className = 'btn btn-link';
        controlTubBtn.textContent = 'Controltub';
        controlTubBtn.href = `/${controller}/ExportControlTub?Mascara=${item.id}`;

        const controlStruBtn = document.createElement('a');
        controlStruBtn.className = 'btn btn-link';
        controlStruBtn.textContent = 'ControlStru';
        controlStruBtn.href = `/${controller}/ExportControlStru?Mascara=${item.id}`;

        exportCell.appendChild(controlTubBtn);
        exportCell.appendChild(controlStruBtn);

        const printCell = document.createElement('td');
        const printBtn = document.createElement('a');
        printBtn.className = 'btn btn-link';
        printBtn.textContent = 'Imprimir';
        printBtn.href = `/${controller}/PrintCLVMPage?IdLVM=${item.id}`;
        printCell.appendChild(printBtn);




        row.append(obraCell, numerLVMCell, lvmCell, dataCell, NotaFiscalCell, OCCell, InvoiceCell, QtdItemCell, MaterialCell, DisciplinaCell, TipoOCCell, clvmCell, exportCell, printCell);
        tbody.appendChild(row);

    });

}