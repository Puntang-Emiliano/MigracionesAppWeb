$(document).ready(function () {
    cargarArticulos();
});

function cargarArticulos() {
    $.ajax({
        "url": "/cliente/Articulo/GetAll",
        "type": "GET",
        "datatype": "json",
        "success": function (data) {
            // Llamar función que genera las cards
            generarCards(data.data);
        },
        "error": function () {
            alert("Error al cargar los artículos");
        }
    });
}

function generarCards(articulos) {
    // Limpiamos el contenedor de cards
    var cardsContainer = $("#cardsContainer");
    cardsContainer.empty();

    // Recorremos los artículos y generamos una card para cada uno
    $.each(articulos, function (index, articulo) {
        var habilitada = articulo.habilitada ? "Sí" : "No";

        // Generamos el HTML de la card
        var cardHtml = `
            <div class="col-md-4">
    <div class="card mb-4">
        <div class="card-body" style="display: flex; flex-direction: column; align-items: center; height: 500px;"> <!-- Ajusta la altura de la tarjeta aquí -->
            <div style="height: 80%; width: 100%; overflow: hidden; display: flex; align-items: center; justify-content: center;">
                <img class="card-img-top" src="${articulo.imagen}" alt="Imagen del artículo" style="max-height: 100%; width: auto; object-fit: contain;">
            </div>
            <div style="flex: 1; text-align: center;"> <!-- Este div contendrá el resto del contenido -->
                <h5 class="card-title">${articulo.nombre}</h5>
                <p class="card-text"><strong>Categoría:</strong> ${articulo.categoria.nombre}</p>
                <p class="card-text"><strong>Habilitada:</strong> ${habilitada}</p>
                <p class="card-text"><strong>Precio:</strong> $${articulo.precio}</p>
                <a href="/cliente/Articulo/Details/${articulo.id}" class="btn btn-primary">Ver más</a>
            </div>
        </div>
    </div>
</div>

        `;

        // Añadimos la card al contenedor
        cardsContainer.append(cardHtml);
    });
}