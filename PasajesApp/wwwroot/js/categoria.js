var dataTable;

$(document).ready(function () {
    // Destruye la instancia existente de DataTable si existe
    if ($.fn.DataTable.isDataTable('#tblCategorias')) {
        $('#tblCategorias').DataTable().clear().destroy();
    }

    // Inicializa DataTable
    cargarDatatable();
});

function cargarDatatable() {
    if (!$.fn.DataTable.isDataTable('#tblCategorias')) {
        dataTable = $("#tblCategorias").DataTable({
            "ajax": {
                "url": "/admin/categoria/GetAll",
                "type": "GET",
                "datatype": "json"
            },
            "columns": [
                { "data": "id", "width": "5%" },
                { "data": "nombre", "width": "40%" },
                { "data": "habilitada", "width": "10%" },
                {
                    "data": "id",
                    "render": function (data) {
                        return `<div class="text-center">
                                            <a href="/Admin/Categoria/Edit/${data}" class="btn btn-success text-white" style="width:140px;">
                                                <i class="far fa-edit"></i> Editar
                                            </a>
                                            &nbsp;
                                            <a onclick=Delete("/Admin/Categoria/Delete/${data}") class="btn btn-danger text-white" style="width:140px;">
                                                <i class="far fa-trash-alt"></i> Borrar
                                            </a>
                                        </div>`;
                    }, "width": "40%"
                }
            ],
            "language": {
                "emptyTable": "No hay registros",
                "info": "Mostrando START a END de TOTAL Entradas",
                "lengthMenu": "Mostrar MENU Entradas",
                "loadingRecords": "Cargando...",
                "processing": "Procesando...",
                "search": "Buscar:",
                "zeroRecords": "Sin resultados encontrados",
                "paginate": {
                    "first": "Primero",
                    "last": "Último",
                    "next": "Siguiente",
                    "previous": "Anterior"
                }
            },
            "autoWidth": false,
            "width": "100%"
        });
    }
}

function Delete(url) {
    Swal.fire({
        title: "¿Está seguro de borrar?",
        text: "¡Este contenido no se puede recuperar!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#DD6B55",
        confirmButtonText: "Sí, borrar!",
        cancelButtonText: "Cancelar",
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                type: 'DELETE',
                url: url,
                success: function (data) {
                    console.log("AJAX request successful", data);
                    if (data.success) {
                        toastr.success(data.message);
                        dataTable.ajax.reload();
                    } else {
                        toastr.error(data.message);
                    }
                },
                error: function (xhr, status, error) {
                    console.error("AJAX request failed", status, error);
                }
            });
        } else {
            console.log("User canceled deletion");
        }
    });
}