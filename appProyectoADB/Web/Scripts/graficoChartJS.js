function grafico(titulo, tituloEtiquetas, tipo, colores, etiquetas, valores) {
    var data =
    {
        labels: etiquetas,
        datasets: [{
            label: titulo,
            backgroundColor: colores,
            borderWidth: 2,
            data: valores
        }]
    };

    var ctx1 = document.getElementById("grafico").getContext("2d");
    window.myBar = new Chart(ctx1,
        {
            type: tipo,
            data: data,
            options:
            {

                animation: {
                    duration: 1000 // general animation time
                },

                hover: {
                    animationDuration: 3000 // duration of animations when hovering an item
                },
                responsiveAnimationDuration: 3000, // animation duration after a resize
                legend: { display: true },
                title:
                {
                    display: true,
                    text: tituloEtiquetas
                },
                responsive: true,
                maintainAspectRatio: true,

            }
        });

}