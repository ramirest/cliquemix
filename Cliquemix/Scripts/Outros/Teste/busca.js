/*
     Titulo - Estado
     Mensagem - Cidade
     Data - Pais
     Fonte
*/

var ID = function (id) { return document.getElementById(id); },
createObj = function (tag) { return document.createElement(tag); },
buscar = function (texto) {
    var div = ID('mostrarBusca'),
     i = 0,
     noticiasEncontradas = [],
     table = createObj('table'),
     tr = createObj('tr'),
     titulo = createObj('th'),
     mensagem = createObj('th'),
     data = createObj('th'),
     fonte = createObj('th');
    titulo.innerHTML = 'T&iacute;tulo';
    mensagem.innerHTML = 'Mensagem (resumo)';
    data.innerHTML = 'Data';
    fonte.innerHTML = 'Fonte';
    texto = (texto && texto != '') ? texto.toLowerCase() : '';

    tr.appendChild(titulo);
    tr.appendChild(mensagem);
    tr.appendChild(data);
    tr.appendChild(fonte);
    table.appendChild(tr);
    for (var n = 0; n < Noticias.length; n++) {
        var news = Noticias[n],
        encontrado = false;
        if (news.Titulo.toLowerCase().indexOf(texto) > -1)
            encontrado = true;
        if (news.Mensagem.toLowerCase().indexOf(texto) > -1)
            encontrado = true;
        if (news.Data.toLowerCase().indexOf(texto) > -1)
            encontrado = true;
        if (news.Fonte.toLowerCase().indexOf(texto) > -1)
            encontrado = true;
        if (encontrado)
            noticiasEncontradas.push(news);
    }
    while (i < noticiasEncontradas.length) {
        var noticia = noticiasEncontradas[i];
        tr = createObj('tr');
        if ((i % 2) == 0)
            tr.setAttribute('class', 'zebra');
        titulo = createObj('td');
        mensagem = createObj('td');
        data = createObj('td');
        fonte = createObj('td');
        titulo.innerHTML = noticia.Titulo;
        if (noticia.Mensagem.length > 200)
            mensagem.innerHTML = noticia.Mensagem.substr(0, 197) + '...';
        else
            mensagem.innerHTML = noticia.Mensagem;
        data.innerHTMl = noticia.Data;
        fonte.innerHTML = noticia.Fonte;
        tr.appendChild(titulo);
        tr.appendChild(mensagem);
        tr.appendChild(data);
        tr.appendChild(fonte);
        table.appendChild(tr);
        i++;
    }
    if (noticiasEncontradas.length <= 0) {
        tr = createObj('tr');
        titulo = createObj('td');
        titulo.setAttribute('colspan', 4);
        titulo.innerHTML = 'Nenhuma not&iacute;cia encontrada com o texto <strong style="color:red">' + texto + '</strong>';
        tr.appendChild(titulo);
        table.appendChild(tr);
    }
    div.innerHTML = '';
    div.appendChild(table);
};