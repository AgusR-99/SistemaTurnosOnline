window.datatableInit = function datatableInit(table) {
    $(document).ready(function () {
        $(table).DataTable();
    });
}

window.datatableRemove = function datatableRemove(table) {
    $(document).ready(function () {
        $(table).DataTable().destroy();
    });

    var elem = document.querySelector(table + '_wrapper');
    elem.parentNode.removeChild(elem);
}