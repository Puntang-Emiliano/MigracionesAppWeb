$(document).ready(function () {
    // Obtener el ID del artículo de la URL
    var articuloId = getUrlParameter('id'); // Función para obtener el ID del parámetro de la URL

    if (articuloId) {
        cargarArticulo(articuloId);
    }
});

function cargarArticulo(id) {
    $.ajax({
        url: "/cliente/Articulo/Details/" + id,  // Llamada al endpoint para obtener los detalles del artículo
        type: "GET",
        datatype: "json",
        success: function (data) {
            mostrarDetallesArticulo(data); // Llamar a la función que actualizará la vista con los detalles
        },
        error: function () {
            alert("Error al cargar los detalles del artículo.");
        }
    });
}

function mostrarDetallesArticulo(articulo) {
    // Verificar si la imagen existe, si no, mostrar una imagen por defecto
    var imagenUrl = articulo.imagen ? articulo.imagen : "/images/default.jpg";

    // Actualizar la vista con los detalles del artículo
    $('#articuloNombre').text(articulo.nombre);
    $('#articuloImagen').attr("src", imagenUrl);
    $('#articuloCategoria').text(articulo.categoria.nombre);
    $('#articuloPrecio').text("$" + articulo.precio.toFixed(2));  // Formato de precio
    $('#articuloHabilitada').text(articulo.habilitada ? "Sí" : "No");
    $('#articuloDescripcion').text(articulo.descripcion || "Sin descripción disponible.");
}

// Función para obtener el parámetro 'id' desde la URL
function getUrlParameter(name) {
    var url = window.location.href;
    var results = new RegExp('[\\?&]' + name + '=([^&#]*)').exec(url);
    if (results === null) {
        return null;
    } else {
        return results[1] || 0;
    }
}
