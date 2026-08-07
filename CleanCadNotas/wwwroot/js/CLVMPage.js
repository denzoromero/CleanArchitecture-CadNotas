
// let isSubmitting = false;
const controller = 'CLVM';

let procedimentoForm = document.getElementById('editProcedimentoForm');
let submitProcedimento = document.getElementById('submitProcedimentoBtn');
let modalForm = document.getElementById('modalForm');
let insertModal = document.getElementById('InsertCLVMModal');
let btnInsert = document.getElementById('btnNewInsert');
let submitBtn = document.getElementById('submitBtn');


let codigoInput = document.getElementById('CodigoCLVM');
let materialTableBody = document.getElementById('materialTableBody');
let codigoModal = document.getElementById('MaterialModalList');
const table = document.getElementById('clvmTable');

procedimentoForm.addEventListener("submit", async function (event) {
    event.preventDefault();

    try {

        const formData = new FormData(procedimentoForm);
        for (const [key, value] of formData.entries()) {
            console.log(key, value);
        }

        const entries = Object.fromEntries(new FormData(procedimentoForm).entries());
        console.log(entries);

        if (!entries.Id) {
            validationError("Id is required when editing");
        }

        const url = `/${controller}/EditProcedimento`;

        submitProcedimento.disabled = true;

        const result = await fetchJson(url, {
            method: 'POST',
            body: JSON.stringify(entries)
        });

        appendAlert(result.message, "success");

        const resultIdemp = await fetchJson(idempotencyPath);
        document.getElementById('idempotencyKeyProcedimento').value = resultIdemp.idempotencyKey;
        

    } catch (error) {
        handleError(error)
    } finally {
        submitProcedimento.disabled = false;
    }

});

codigoInput.addEventListener("change", async function (event) {

    if (!event.target.value) {
        return;
    }

    try {

        const url = `/${controller}/SearchCodigo?codigo=${event.target.value}`;

        const result = await fetchJson(url);

        if (result.length == 1) {
            console.log('1 result');
            populateCodigo(result[0]);
        } else if (result.length > 1) {
            console.log('result greater 1');
            populateMaterialTableBody(result);

            bootstrap.Modal.getOrCreateInstance(codigoModal).show();
            bootstrap.Modal.getOrCreateInstance(insertModal).hide();

        } else {
            console.log('no result');
        }


    } catch (error) {
        handleError(error)
    }

});

codigoModal.addEventListener('hidden.bs.modal', () => {
    bootstrap.Modal.getOrCreateInstance(insertModal).show();

});

// insertModal.addEventListener('hidden.bs.modal', (e) => {
//     if (isSubmitting) {
//         console.log('must not close');
//         e.preventDefault();
//     }
// });

const formSubmission = createModalSubmitController(insertModal);

// insertModal.addEventListener('hide.bs.modal', e => {
//     if (controllerSubmission.isSubmitting()) {
//         e.preventDefault();
//     }
// });

const populateMaterialTableBody = (items) => {

    materialTableBody.innerHTML = '';

    items.forEach(item => {

        const row = document.createElement('tr');
        row.id = item.id;

        const selectCell = document.createElement('td');
        const selectA = document.createElement('a');
        selectA.className = 'btn btn-link';
        selectA.textContent = 'Select';
        selectA.addEventListener('click', (e) => {
            e.preventDefault();
            populateCodigo(item);
            bootstrap.Modal.getOrCreateInstance(codigoModal).hide();
            bootstrap.Modal.getOrCreateInstance(insertModal).show();
        });
        selectCell.appendChild(selectA);

        const codigoCell = document.createElement('td');
        codigoCell.innerText = item.codigo;

        const descriptionCell = document.createElement('td');
        descriptionCell.innerText = item.descricao;

        row.append(selectCell, codigoCell, descriptionCell);
        materialTableBody.appendChild(row);
    });

}

const populateCodigo = (item) => {

    document.getElementById('CodigoCLVM').value = item.codigo;
    document.getElementById('IdMaterialCLVM').value = item.id;
    document.getElementById('TipoComponent').value = item.tipoComponenteMaterial;
    document.getElementById('EspecMaterialDetail').textContent = item.especificacao;
    document.getElementById('Diametro1Detail').textContent = item.diametro1;
    document.getElementById('Diametro2Detail').textContent = item.diametro2;
    document.getElementById('ComprimentoDetail').textContent = item.comprimento;
    document.getElementById('EspessuraDetail').textContent = item.espessura;
    document.getElementById('LarguraDetail').textContent = item.largura;
    document.getElementById('PesoDetail').textContent = item.peso;

}


btnInsert.addEventListener('click', () => {
    ResetInsertModal(modalForm, submitBtn);

    document.getElementById('CodigoCLVM').value = '';
    document.getElementById('IdMaterialCLVM').value = '';
    document.getElementById('TipoComponent').value = '';
    document.getElementById('EspecMaterialDetail').textContent = '';
    document.getElementById('Diametro1Detail').textContent = '';
    document.getElementById('Diametro2Detail').textContent = '';
    document.getElementById('ComprimentoDetail').textContent = '';
    document.getElementById('EspessuraDetail').textContent = '';
    document.getElementById('LarguraDetail').textContent = '';
    document.getElementById('PesoDetail').textContent = '';
});

modalForm.addEventListener("submit", async (e) => {
    e.preventDefault();

    try {

        console.log('form submit');

        const entries = Object.fromEntries(new FormData(modalForm).entries());
        entries.IdLVM = document.getElementById('IdLVM').value;
        entries.CodObra = document.getElementById('CodObra').value;
        entries.LVMOrigem = document.getElementById('LVMOrigem').value;

        entries.Programacao = entries.Programacao === "" ? null : parseInt(entries.Programacao);
        entries.Qtd = entries.Qtd === "" ? 0 : parseFloat(entries.Qtd);

        entries.IdLVM = entries.IdLVM === "" ? 0 : parseInt(entries.IdLVM);
        entries.Item = entries.Item === "" ? 0 : parseFloat(entries.Item);
        entries.PO = entries.PO === "" ? 0 : parseInt(entries.PO);
        entries.IdMaterialCLVM = entries.IdMaterialCLVM === "" ? 0 : parseInt(entries.IdMaterialCLVM);
        entries.DtInspecao = entries.DtInspecao === "" ? "0001-01-01T00:00:00" : new Date(entries.DtInspecao);

        entries.UnidadeMedida = entries.UnidadeMedida === "" ? 0 : parseInt(entries.UnidadeMedida);
        entries.Status = entries.Status === "" ? 0 : parseInt(entries.Status);

        console.log('entries', entries);

        console.log('json entries', JSON.stringify(entries));

        const Action = submitBtn.dataset.action;
        console.log(Action);

        var url;

        if (Action === 'Insert') {

            if (entries.Id) {
                validationError("Id should be empty when inserting");
            }

            url = `/${controller}/Insert`;

        } else if (Action === 'Edit') {

            if (!entries.Id) {
                validationError("Id is required when editing");
            }

            url = `/${controller}/Edit`;

        } else if (Action === 'Transfer') {
            if (!entries.Id) {
                validationError("Id is required when transfering");
            }

            url = `/${controller}/Transfer`;
        } else {
            validationError("No action.");
        }

        // isSubmitting = true;
        formSubmission.start();
        submitBtn.disabled = true;

        const result = await fetchJson(url, {
            method: 'POST',
            body: JSON.stringify(entries)
        });

        console.log(result);

        appendAlert(result.message, "success");

        formSubmission.end();

        setTimeout(() => {
            window.location.href = result.redirectUrl;
        }, 1500);


    } catch (error) {
        console.log(error);
        handleError(error)
    } finally {
        // isSubmitting = false;
        formSubmission.end();
        submitBtn.disabled = false;
    }

});

document.querySelectorAll('.btn-edit').forEach(button => {
    button.addEventListener('click', async function () {
        const id = this.dataset.id;

        try {

            if (!id) {
                validationError("No action.");
            }

            console.log(id);

            const result = await getCLVMData(id);

            console.log(result);

            OpenEditFormModal(result, modalForm, submitBtn, insertModal);
            document.getElementById('IdInspetor').textContent = result.idInspetor;

            populateCodigo(result.material);
        }
        catch (error) {
            handleError(error)
        }
  
    });
});

document.querySelectorAll('.btn-deact').forEach(button => {
    button.addEventListener('click', async function () {
 
        const confirmed = confirm('Deseja excluir o CLVM?');
        if (!confirmed) {
            return;
        }

        const id = this.dataset.id;
        console.log('transfer', id);

        try {

            const lvmid = document.getElementById('IdLVM').value;

            const url = `/${controller}/Deactivate?Id=${id}&IdLVM=${lvmid}`;

            const result = await fetchJson(url, {
                method: 'POST',
            });

            appendAlert(result.message, "success");

            setTimeout(() => {
                window.location.href = result.redirectUrl;
            }, 1500);

        }
        catch (error) {
            console.log(error);
            handleError(error)
        }

    });
});


// table.addEventListener('click', async (e) => {
//     const button = e.target.closest('.btn-edit');

//     if (!button) {
//         return;
//     }

//     try {

//         const id = button.dataset.id;
//         if (!id) {
//             validationError("No action.");
//         }

//         const result = await getCLVMData(id);

//         console.log(result);

//         OpenEditFormModal(result, modalForm, submitBtn, insertModal);
//         populateCodigo(result.material);

//     } catch (error) {
//         handleError(error)
//     }
// });

// function createModalSubmitController(modalElement) {
//     let isSubmitting = false;

//     return {
//         start() {
//             isSubmitting = true;
//             modalElement.setAttribute('data-bs-backdrop', 'static');
//             modalElement.setAttribute('data-bs-keyboard', 'false');
//         },

//         end() {
//             isSubmitting = false;
//             modalElement.setAttribute('data-bs-backdrop', 'true');
//             modalElement.setAttribute('data-bs-keyboard', 'true');
//         },

//         isSubmitting() {
//             return isSubmitting;
//         }
//     };
// }

const getCLVMData = async (id) => {

    try {

        const url = `/${controller}/GetCLVMData?id=${id}`;

        const result = await fetchJson(url);

        return result;

    }
    catch (error) {
        handleError(error)
    }
    
}

function createModalSubmitController(modalElement) {
    let isSubmitting = false;

    modalElement.addEventListener('hide.bs.modal', (e) => {
        if (isSubmitting) {
            e.preventDefault();
        }
    });

    return {
        start() {
            isSubmitting = true;
        },

        end() {
            isSubmitting = false;
        },

        get isSubmitting() {
            return isSubmitting;
        }
    };
}