$(document).ready(function () {
    cargarArticulos();
});

function cargarArticulos() {
    $.ajax({
        url: "/cliente/Articulo/GetAll",
        type: "GET",
        datatype: "json",
        success: function (data) {
           
            generarCards(data.data);
        },
        error: function () {
            alert("Error al cargar los artículos");
        }
    });
}

function generarCards(articulos) {
    
    var cardsContainer = $("#cardsContainer");
    cardsContainer.empty();

   
    $.each(articulos, function (index, articulo) {
        var habilitada = articulo.habilitada ? "Sí" : "No";

        // Construimos la URL para el botón "Ver más"
        var detalleUrl = `/Cliente/Articulo/DetalleArt/${articulo.id}`;

        // Generamos el HTML de la card
        var cardHtml = `
            <div class="col-md-4">
                <div class="card mb-4">
                    <div class="card-body" style="display: flex; flex-direction: column; align-items: center; height: 500px;">
                        <div style="height: 80%; width: 100%; overflow: hidden; display: flex; align-items: center; justify-content: center;">
                            <img class="card-img-top" src="${articulo.imagen}" alt="Imagen del artículo" style="max-height: 100%; width: auto; object-fit: contain;">
                        </div>
                        <div style="flex: 1; text-align: center;">
                            <h5 class="card-title">${articulo.nombre}</h5>
                            <p class="card-text"><strong>Categoría:</strong> ${articulo.categoria.nombre}</p>
                            <p class="card-text"><strong>Habilitada:</strong> ${habilitada}</p>
                            <p class="card-text"><strong>Precio:</strong> $${articulo.precio}</p>
                            <a href="${detalleUrl}" class="btn btn-unificado">Ver más</a>
                        </div>
                    </div>
                </div>
            </div>
        `;

        
        cardsContainer.append(cardHtml);
    });
}
