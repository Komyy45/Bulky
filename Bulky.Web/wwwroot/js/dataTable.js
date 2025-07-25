function initDataTable(tableId, entity, columns) {
    $(`#${tableId}`).DataTable({
        serverSide: true,
        processing: true,
        ajax: {
            url: `/${entity}/GetAll`,
            method: "POST"
        },
        columns
    });
}