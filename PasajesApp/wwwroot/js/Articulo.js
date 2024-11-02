var dataTable;

$(document).ready(function () {
    // Verificar si la tabla ya está inicializada y destruirla si es así
    if ($.fn.DataTable.isDataTable('#tblArticulos')) {
        $('#tblArticulos').DataTable().destroy();
    }

    cargarDatatable();
});

function cargarDatatable() {
    dataTable = $("#tblArticulos").DataTable({
        destroy: true, // Asegura que la tabla se destruya antes de recrearla
        "ajax": {
            "url": "/admin/Articulo/GetAll",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "id", "width": "5%" },
            { "data": "nombre", "width": "25%" },
            { "data": "categoria.nombre", "width": "30%" },
            { "data": "precio", "width": "20%" },
            { "data": "habilitada", "width": "10%" },
            {
                "data": "id",
                "render": function (data) {
                    return `
                        <div class="d-flex justify-content-center gap-2"> 
                            <a href="/Admin/Articulo/Edit/${data}" class="btn btn-success text-white" style="cursor:pointer; width:110px;">
                                <i class="far fa-edit"></i> Editar
                            </a>
                            <a onclick=Delete("/Admin/Articulo/Delete/${data}") class="btn btn-danger text-white" style="cursor:pointer; width:110px;">
                                <i class="far fa-trash-alt"></i> Borrar
                            </a>
                        </div>
                    `;
                },
                "width": "30%"
            }
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
                "last": "Último",
                "next": "Siguiente",
                "previous": "Anterior"
            }
        },
        "width": "100%"
    });
}

function Delete(url) {
    Swal.fire({
        title: "¿Está seguro de borrar?",
        text: "Este contenido no se puede recuperar!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#DD6B55",
        confirmButtonText: "Sí, borrar!",
        cancelButtonText: "Cancelar",
        reverseButtons: true
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
                    toastr.error('Error al eliminar el registro.');
                }
            });
        }
    });
}
