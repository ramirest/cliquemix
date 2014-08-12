function UpdateAnuncio(item) {
    $.ajax(
    {
        type: 'GET',
        url: '/Anuncio/UpdateAnuncio/?id=' + item,
        dataType: 'html',
        cache: false,
        async: true
    });
}

function VisualizarAnuncio(item) {
    $.ajax(
    {
        type: 'GET',
        url: '/Anuncio/VisualizarAnuncio/?id=' + item,
        dataType: 'html',
        cache: false,
        async: true
    });
}