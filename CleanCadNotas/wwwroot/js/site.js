// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

const idempotencyPath = '/Common/GenerateIdempotencyKey';


document.addEventListener('DOMContentLoaded', function () {

    //Dark-Light
    loadThemePreference();

    //dropdown hover
    var dropdowns = document.querySelectorAll('.dropdown-submenu');

    dropdowns.forEach(function (dropdown) {
        dropdown.addEventListener('mouseover', function () {
            var submenu = this.querySelector('.dropdown-menu');
            if (submenu) {
                submenu.classList.add('show');
            }
        });

        dropdown.addEventListener('mouseout', function () {
            var submenu = this.querySelector('.dropdown-menu');
            if (submenu) {
                submenu.classList.remove('show');
            }
        });
    });


    //decimal inputs
    var inputs = document.querySelectorAll('.decimalinputs');
    inputs.forEach(function (input) {
        input.addEventListener('keypress', restrictInput);
    });

    //digits only inputs
    var digitInputs = document.querySelectorAll('.digitsonly');
    digitInputs.forEach(function (input) {
        input.addEventListener('keypress', restrictDigitsOnly);
    });

    const SearchloaderContainer = document.querySelector('.loader-container-Search');

    // const Searchbuttons = document.querySelectorAll('.ShowSearchAnimation');

    // Searchbuttons.forEach(function (Searchbutton) {
    //     Searchbutton.addEventListener('click', function () {
    //         SearchloaderContainer.style.display = 'flex'; // Show the loader container
    //     });
    // });

});

function restrictDigitsOnly(event) {
    var charCode = event.which ? event.which : event.keyCode;
    var key = event.key;

    if (
        (charCode >= 48 && charCode <= 57) || // Numbers 0-9
        charCode === 8 || // Backspace
        charCode === 9 || // Tab
        charCode === 37 || // Left arrow
        charCode === 39 || // Right arrow
        (charCode === 46 && key !== ".") // Delete (but not .)
    ) {
        return true;
    } else {
        event.preventDefault();
        return false;
    }
}

function restrictInput(event) {
    var charCode = event.which ? event.which : event.keyCode;
    if (
        (charCode >= 48 && charCode <= 57) || // Numbers 0-9
        charCode === 46 || // Period (.)
        charCode === 44 || // Comma (,)
        charCode === 8 || // Backspace
        charCode === 9 || // Tab
        charCode === 37 || // Left arrow
        charCode === 39 || // Right arrow
        charCode === 46 // Delete
    ) {
        return true;
    } else {
        event.preventDefault();
        return false;
    }
}

function loadThemePreference() {
    const themePreference = localStorage.getItem("theme");
    const themeToggle = document.querySelector("#theme-toggle");

    if (themePreference === "dark") {
        themeToggle.checked = true;
    } else {
        themeToggle.checked = false;
    }

    // Trigger the toggleTheme function to apply the correct theme
    toggleTheme();
}

function toggleTheme() {
    const body = document.querySelector("body");

    const themeToggle = document.querySelector("#theme-toggle");
    const themeLabel = document.querySelector("#theme-label");
    const themeIcon = document.querySelector("#theme-icon");

    const buttonOutline = document.querySelectorAll(".ButtonOutline");
    const buttonOtherOutline = document.querySelectorAll(".btnOtherOutline");
    const Tables = document.querySelectorAll(".table");
    const Modals = document.querySelectorAll(".modal-content");
    const Accordion = document.querySelectorAll(".AccordBody");

    const EnsaiosHeader = document.querySelectorAll(".EnsaiosHeader");

    if (themeToggle.checked) {

        console.log("Dark is Active.");

        body.dataset.theme = "dark";

        themeLabel.textContent = "Dark";
        themeLabel.style.color = "#f9f9f9";

        themeIcon.src = "/images/moon-stars-fill.svg";

        buttonOutline.forEach(button => {
            button.classList.remove("btn-outline-dark");
            button.classList.add("btn-outline-light");
        });

        buttonOtherOutline.forEach(button => {
            button.classList.remove("btn-outline-info");
            button.classList.add("btn-outline-warning");
        });

        Tables.forEach(table => {
            table.classList.add("table-dark");
        });

        Modals.forEach(modal => {
            modal.classList.add("bg-dark");
        });

        Accordion.forEach(accord => {
            accord.classList.add("bg-dark");
        });

        EnsaiosHeader.forEach(ensaiosheader => {
            ensaiosheader.classList.remove("bg-light-subtle");
            ensaiosheader.classList.add("bg-dark-subtle");
        });

        localStorage.setItem("theme", "dark");
    } else {

        console.log("Light is Active.");

        body.dataset.theme = "light";

        themeLabel.textContent = "Light";
        themeLabel.style.color = "#353839";

        themeIcon.src = "/images/brightness-high-fill.svg";

        buttonOutline.forEach(button => {
            button.classList.remove("btn-outline-light");
            button.classList.add("btn-outline-dark");
        });

        buttonOtherOutline.forEach(button => {
            button.classList.remove("btn-outline-warning");
            button.classList.add("btn-outline-info");
        });

        Tables.forEach(table => {
            table.classList.remove("table-dark");
        });

        Modals.forEach(modal => {
            modal.classList.remove("bg-dark");
        });

        Accordion.forEach(accord => {
            accord.classList.remove("bg-dark");
        });

        EnsaiosHeader.forEach(ensaiosheader => {
            ensaiosheader.classList.add("bg-info");
            ensaiosheader.classList.remove("bg-dark-subtle");
        });

        localStorage.setItem("theme", "light");
    }
}


async function fetchJson(url, options = {}) {

    const defaultOptions = {
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
            ...options.headers
        },
        ...options
    };

    const response = await fetch(url, defaultOptions);

    if (!response.ok) {

        const errorData = await response.json();

        console.log(errorData);

        throw {
            status: response.status,
            type: errorData.type,
            message: errorData.message,
            errors: errorData.errors
        };
    }

    return await response.json();

}

function handleError(error) {

    switch (error.type) {

        case "Business":
            appendAlertWithoutAnimation(error.message, "danger");
            break;

        case "Validation":
            console.log(error.errors);
            const errors = `
                <ul>
                    ${error.errors.map(x => `<li>${x.errorMessage}</li>`).join("")}
                </ul>
            `;
            appendAlertWithoutAnimation(errors,"danger");
            break;

        case "JavascriptError":
            appendAlertWithoutAnimation(error.message, "warning");
            break;

        case "Infrastructure":
            appendAlertWithoutAnimation(error.message, "danger");
            break;

        case "Unauthorized":
            window.location.href = "/Home/Unauthorized";
            break;

        case "Unexpected":
            window.location.href = "/Home/Error";
            break;

        default:
            appendAlertWithoutAnimation("Unexpected error","danger");
            break;
    }

}

function validationError(message) {
    throw {
        type: "JavascriptError",
        message: message
    };
}



const appendAlertWithoutAnimation = (message, type) => {
    const alertPlaceholder = document.getElementById('liveAlertPlaceholder');
    if (!alertPlaceholder) {
        console.error('Alert placeholder not found');
        return;
    }

    const wrapper = document.createElement('div');
    wrapper.innerHTML = [
        `<div class="alert alert-${type} alert-dismissible" role="alert">`,
        `   <div>${message}</div>`,
        '   <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"><img src="/images/x-lg.svg" /></button>',
        '</div>'
    ].join('');

    alertPlaceholder.append(wrapper);
};

const appendAlert = (message, type) => {
    const alertPlaceholder = document.getElementById('liveAlertPlaceholder');
    if (!alertPlaceholder) {
        console.error('Alert placeholder not found');
        return;
    }

    const wrapper = document.createElement('div');
    wrapper.innerHTML = [
        `<div class="alert alert-${type} alert-dismissible" role="alert">`,
        `   <div>${message}</div>`,
        '   <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"><img src="/images/x-lg.svg" /></button>',
        '</div>'
    ].join('');

    alertPlaceholder.append(wrapper);

    // Automatically close the alert after 3 seconds with fade-out effect
    setTimeout(() => {
        wrapper.classList.add('fade-out');
        setTimeout(() => {
            wrapper.remove();
        }, 500); // Match this duration with the fadeOut animation duration
    }, 3000);
};


const populateDropDownList = (idSelect, items) => {

    const ddlComponent = document.getElementById(idSelect);
    if (!ddlComponent) return;

    ddlComponent.innerHTML = '';

    const defaultOption = document.createElement('option');
    defaultOption.value = '';
    defaultOption.textContent = 'Selecionar...';
    ddlComponent.appendChild(defaultOption);

    if (!items || items.length === 0) return;

    items.forEach(item => {

        const option = document.createElement('option');
        option.value = item.id;
        option.textContent = item.name;
        ddlComponent.appendChild(option);
    });

}

// const populateForm = (form, data) => {
//     for (const field of form.elements) {

//         const key = Object.keys(data).find(k => k.toLowerCase() === field.name.toLowerCase());

//         if (key) {
//             field.value = data[key] ?? '';
//         }
//     }
// };

const populateForm = (form, data) => {
    for (const field of form.elements) {

        const key = Object.keys(data)
            .find(k => k.toLowerCase() === field.name.toLowerCase());

        if (!key) continue;

        if (field.tagName === 'SELECT' && field.multiple) {

            const values = data[key] ?? [];

            for (const option of field.options) {
                option.selected = values.includes(Number(option.value));
            }

            continue;
        }

        if (field.type === 'date' && data[key]) {
            field.value = data[key].split('T')[0];
            continue;
        }

        field.value = data[key] ?? '';
    }
};




document.getElementById("ddlEstado")?.addEventListener("change", async (e) => {

    let url = `/Common/GetCities?Id=${e.target.value}`;

    try {

        const result = await fetchJson(url);

        populateDropDownList('ddlCidade', result);

    }
    catch (error) {
        handleError(error)
    }

});




const SearchForm = async (controller, form, currentPage) => {

    let url = `/${controller}/Search`;

    const formEntry = getFormData(form, {
        PageNo: currentPage
    });

    const completeGetUrl = `${url}?${new URLSearchParams(formEntry).toString()}`;

    try {

        const result = await fetchJson(completeGetUrl);

        return result;

    } catch (error) {
        handleError(error)
    }


}

// const getFormData = (form, extraData = {}) => ({
//     ...Object.fromEntries(new FormData(form).entries()),
//     ...extraData
// });

const getFormData = (form, extraData = {}) => {
    const data = {};

    for (const element of form.elements) {
        if (!element.name) continue;

        data[element.name] =
            element.type === "checkbox"
                ? element.checked
                : element.value;
    }

    return {
        ...data,
        ...extraData
    };
};



const ResetInsertModal = async (form, submitBtn) => {

    form.reset();
    const result = await fetchJson(idempotencyPath);

    form.elements["IdempotencyKey"].value = result.idempotencyKey;
    form.elements["Id"].value = "";

    submitBtn.dataset.action = 'Insert';
    submitBtn.classList.replace('btn-warning', 'btn-primary');
    submitBtn.innerText = 'Salvar';

}

const SubmitModalForm = async (form, submitBtn, controller) => {

    try {

        const entries = Object.fromEntries(new FormData(form).entries());

        const isEdit = submitBtn.dataset.action === "Edit";

        if (isEdit && !entries.Id) {
            validationError("Id is required when editing");
        }

        if (!isEdit && entries.Id) {
            validationError("Id should be empty when inserting");
        }

        const url = isEdit ? `/${controller}/Edit` : `/${controller}/Insert`;

        submitBtn.disabled = true;

        const result = await fetchJson(url, {
            method: 'POST',
            body: JSON.stringify(entries)
        });

        return result;


    } catch (error) {
        handleError(error)
    } finally {
        submitBtn.disabled = false;
    }

}

const SubmitModalFormEntries = async (entries, submitBtn, controller) => {

    try {

        const isEdit = submitBtn.dataset.action === "Edit";

        if (isEdit && !entries.Id) {
            validationError("Id is required when editing");
        }

        if (!isEdit && entries.Id) {
            validationError("Id should be empty when inserting");
        }

        const url = isEdit ? `/${controller}/Edit` : `/${controller}/Insert`;

        submitBtn.disabled = true;

        const result = await fetchJson(url, {
            method: 'POST',
            body: JSON.stringify(entries)
        });

        return result;

    }
    catch (error) {
        handleError(error)
    } finally {
        submitBtn.disabled = false;
    }
}

const OpenEditFormModal = async (item, form, submitBtn, modalElement) => {

    const result = await fetchJson(idempotencyPath);
    item.IdempotencyKey = result.idempotencyKey;

    populateForm(form, item);

    submitBtn.dataset.action = 'Edit';
    submitBtn.classList.replace('btn-primary', 'btn-warning');
    submitBtn.innerText = 'Edit';

    new bootstrap.Modal(modalElement).show();

}


const PaginationControl = (totalPages, currentPage, onPageChanged) => {

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
            await onPageChanged(currentPage - 1);
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
            await onPageChanged(currentPage + 1);
        }
    });
    paginationControls.appendChild(nextButton);

    function addPageItem(page) {
        const pageItem = document.createElement('li');
        const pageLinkClass = `page-link`;
        pageItem.className = 'page-item' + (page === currentPage ? ' active' : '');
        pageItem.innerHTML = `<a class="${pageLinkClass}" href="#">${page + 1}</a>`;
        pageItem.addEventListener('click', async (event) => {
            event.preventDefault();
            await onPageChanged(page);
        });
        paginationControls.appendChild(pageItem);
    }


}

