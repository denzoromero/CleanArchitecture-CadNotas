const controller = 'RDFA';

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

        const mascaraCell = document.createElement('td');
        mascaraCell.innerText = item.numeroLVM;

        const notafiscalCell = document.createElement('td');
        notafiscalCell.innerText = item.notaFiscal;

        const rdfaCell = document.createElement('td');
        const rdfaBtn = document.createElement('a');
        rdfaBtn.className = 'link-offset-2 link-offset-3-hover link-underline link-underline-opacity-0 link-underline-opacity-75-hover';
        rdfaBtn.textContent = 'RDFA';
        rdfaBtn.href = `/${controller}/RDFAPage?id=${item.idLVM}`;
        rdfaCell.appendChild(rdfaBtn);

        row.append(mascaraCell, notafiscalCell, rdfaCell);
        tbody.appendChild(row);
    });
}