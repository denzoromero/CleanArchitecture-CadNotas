

const controller = 'CLVM';

let modalForm = document.getElementById('generateReportForm');
let submitBtn = document.getElementById('btnSubmit');

modalForm.addEventListener("submit", async (e) => {
    e.preventDefault();

    try {
        const entries = Object.fromEntries(new FormData(e.target).entries());
        console.log(entries);

        entries.IdRelatorio = entries.IdRelatorio === "" ? 0 : parseInt(entries.IdRelatorio);
        entries.Status = entries.Status === "" ? 0 : parseInt(entries.Status);

        let url = `/${controller}/GenerateReport`;

        submitBtn.disabled = true;

        const result = await fetchJson(url, {
            method: 'POST',
            body: JSON.stringify(entries)
        });

        // const html = await result.text();

        // console.log(html);

        var printWindow = window.open('', '_blank');
        printWindow.document.open();
        printWindow.document.write(result);
        printWindow.document.close();
        printWindow.onload = () => {
            printWindow.focus();
            printWindow.print();
        };


    } catch (error) {
        handleError(error)
    } finally {
        submitBtn.disabled = false;
    }

});