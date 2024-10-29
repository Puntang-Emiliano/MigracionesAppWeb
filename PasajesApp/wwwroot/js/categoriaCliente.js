﻿var dataTable;

$(document).ready(function () {
    cargarDatatable();
});


function cargarDatatable() {
    dataTable = $("#tblCategoriasC").DataTable({
        "ajax": {
            "url": "/cliente/categoria/GetAll",
            "type": "GET",
            "datatype": "json"

        },
        "columns": [
            { "data": "id", "width": "20%", "render": function (data) { return data; } },
            { "data": "nombre", "width": "60%" },
            { "data": "habilitada", "width": "20%" }
        ],
        "language": {
            "decimal": "",
            "emptyTable": "No hay registros",
            "info": "Mostrando _START_ a _END_ de _TOTAL_ Entradas",
            "infoEmpty": "Mostrando 0 to 0 of 0 Entradas",
            "infoFiltered": "(Filtrado de _MAX_ total entradas)",
            "infoPostFix": "",
            "thousands": ",",
            "lengthMenu": "Mostrar _MENU_ Entradas",
            "loadingRecords": "Cargando...",
            "processing": "Procesando...",
            "search": "Buscar:",
            "zeroRecords": "Sin resultados encontrados",
            "paginate": {
                "first": "Primero",
                "last": "Ultimo",
                "next": "Siguiente",
                "previous": "Anterior"
            }
        },
        "width": "100%"
    });
}