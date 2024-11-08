var dataTable;

$(document).ready(function () {
    // Destruye la instancia existente de DataTable si existe
    if ($.fn.DataTable.isDataTable('#tblUsuarios')) {
        $('#tblUsuarios').DataTable().clear().destroy();
    }

    // Inicializa DataTable
    cargarDatatable();
});

function cargarDatatable() {
    if (!$.fn.DataTable.isDataTable('#tblUsuarios')) {
        dataTable = $("#tblUsuarios").DataTable({
            "ajax": {
                "url": "/admin/usuario/GetAll",
                "type": "GET",
                "datatype": "json"
            },
            "columns": [
                { "data": "id", "width": "5%" },
                { "data": "nombre", "width": "20%" },
                { "data": "email", "width": "20%" },
                { "data": "rol", "width": "15%" },
                { "data": "estado", "width": "10%" },
                {
                    "data": "id",
                    "render": function (data) {
                        return `<div class="text-center">
                                    <a href="/Admin/Usuario/Edit/${data}" class="btn btn-success text-white" style="width:140px;">
                                        <i class="far fa-edit"></i> Editar
                                    </a>
                                    &nbsp;
                                    <a onclick=Delete("/Admin/Usuario/Delete/${data}") class="btn btn-danger text-white" style="width:140px;">
                                        <i class="far fa-trash-alt"></i> Borrar
                                    </a>
                                </div>`;
                    }, "width": "30%"
                }
            ],
            "language": {
                "emptyTable": "No hay registros",
                "info": "Mostrando _START_ a _END_ de _TOTAL_ entradas",
                "lengthMenu": "Mostrar _MENU_ entradas",
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
                    if (data.success) {
                        toastr.success(data.message);
                        dataTable.ajax.reload();
                    } else {
                        toastr.error(data.message);
                    }
                },
                error: function (xhr, status, error) {
                    console.error("Error al borrar el usuario:", error);
                }
            });
        }
    });
}
