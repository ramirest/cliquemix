// JavaScript Document
//adiciona mascara de cnpj
function MascaraCNPJ(cnpj) {
    if (mascaraInteiro(cnpj) == false) {
        event.returnValue = false;
    }
    return formataCampo(cnpj, '00.000.000/0000-00', event);
}

//adiciona mascara de cep
function MascaraCep(cep) {
    if (mascaraInteiro(cep) == false) {
        event.returnValue = false;
    }
    return formataCampo(cep, '00.000-000', event);
}

//adiciona mascara de data
function MascaraData(data) {
    if (mascaraInteiro(data) == false) {
        event.returnValue = false;
    }
        return formataCampo(data, '00/00/0000', event);
}

//adiciona mascara ao telefone
function MascaraTelefone(tel) {
    if (mascaraInteiro(tel) == false) {
        event.returnValue = false;
    }
    return formataCampo(tel, '(00) 0000-0000', event);
}

//adiciona mascara ao CPF
function MascaraCPF(cpf) {
    if (mascaraInteiro(cpf) == false) {
        event.returnValue = false;
    }
    return formataCampo(cpf, '000.000.000-00', event);
}

//valida telefone
function ValidaTelefone(tel) {
    exp = /\(\d{2}\)\ \d{4}\-\d{4}/;
    if (!exp.test(tel.value))
        alert('Numero de Telefone Invalido!');
}

//valida CEP
function ValidaCep(cep) {
    exp = /\d{2}\.\d{3}\-\d{3}/
    if (!exp.test(cep.value))
        alert('Numero de Cep Invalido!');
}

//valida data
function ValidaData(data) {
    
    exp = /\d{2}\/\d{2}\/\d{4}/
    if (!exp.test(data.value)) {
        data.value = "";
        alert('Data Invalida!');
    }
    else if (!isStrData(data.value)) {
        data.value = "";
        data.focus();
    }
}

//valida o CPF digitado
function ValidarCPF(Objcpf) {
    var cpf = Objcpf.value;
    exp = /\.|\-/g
    cpf = cpf.toString().replace(exp, "");
    var digitoDigitado = eval(cpf.charAt(9) + cpf.charAt(10));
    var soma1 = 0, soma2 = 0;
    var vlr = 11;

    for (i = 0; i < 9; i++) {
        soma1 += eval(cpf.charAt(i) * (vlr - 1));
        soma2 += eval(cpf.charAt(i) * vlr);
        vlr--;
    }
    soma1 = (((soma1 * 10) % 11) == 10 ? 0 : ((soma1 * 10) % 11));
    soma2 = (((soma2 + (2 * soma1)) * 10) % 11);

    var digitoGerado = (soma1 * 10) + soma2;
    if (digitoGerado != digitoDigitado)
        alert('CPF Invalido!');
}

//valida numero inteiro com mascara
function mascaraInteiro() {
    if (event.keyCode < 48 || event.keyCode > 57) {
        event.returnValue = false;
        return false;
    }
    return true;
}


//valida o CNPJ digitado
function ValidarCNPJ(ObjCnpj) {
    var cnpj = ObjCnpj.value;
    var valida = new Array(6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2);
    var dig1 = new Number;
    var dig2 = new Number;

    exp = /\.|\-|\//g
    cnpj = cnpj.toString().replace(exp, "");
    var digito = new Number(eval(cnpj.charAt(12) + cnpj.charAt(13)));

    for (i = 0; i < valida.length; i++) {
        dig1 += (i > 0 ? (cnpj.charAt(i - 1) * valida[i]) : 0);
        dig2 += cnpj.charAt(i) * valida[i];
    }
    dig1 = (((dig1 % 11) < 2) ? 0 : (11 - (dig1 % 11)));
    dig2 = (((dig2 % 11) < 2) ? 0 : (11 - (dig2 % 11)));

    if (((dig1 * 10) + dig2) != digito)
        alert('CNPJ Invalido!');

}

//formata de forma generica os campos
function formataCampo(campo, Mascara, evento) {
    var boleanoMascara;
    var Digitato = evento.keyCode;
    exp = /\-|\.|\/|\(|\)| /g;
    campoSoNumeros = campo.value.toString().replace(exp, "", " ");
    var posicaoCampo = 0;
    var NovoValorCampo = "";
    var TamanhoMascara = campoSoNumeros.length;

    if (Digitato != 8) { // backspace 
        for (i = 0; i < TamanhoMascara; i++) {

            boleanoMascara = ((Mascara.charAt(i) == "-") || (Mascara.charAt(i) == ".")
								|| (Mascara.charAt(i) == "/"))
            boleanoMascara = boleanoMascara || ((Mascara.charAt(i) == "(")
								|| (Mascara.charAt(i) == ")") || (Mascara.charAt(i) == " "))
            if (boleanoMascara) {
                NovoValorCampo += Mascara.charAt(i);
                TamanhoMascara++;
            } else {
                NovoValorCampo += campoSoNumeros.charAt(posicaoCampo);
                posicaoCampo++;
            }
        }
        campo.value = NovoValorCampo;
        return true;
    } else {
        return true;
    }
}

function validacaoEmail(field) {
    usuario = field.value.substring(0, field.value.indexOf("@"));
    dominio = field.value.substring(field.value.indexOf("@")+ 1, field.value.length);

    if ((usuario.length >=1) &&
        (dominio.length >=3) && 
        (usuario.search("@")==-1) && 
        (dominio.search("@")==-1) &&
        (usuario.search(" ")==-1) && 
        (dominio.search(" ")==-1) &&
        (dominio.search(".")!=-1) &&      
        (dominio.indexOf(".") >=1)&& 
        (dominio.lastIndexOf(".") < dominio.length - 1)) {
            //Caso o valor for Válido, altera o estado de exibição das divs
            $("#msgValidar_Email").attr("class", "alert alert-success");
            //Informa que o valor está válido
            document.getElementById("msgValidar_Email").innerHTML =
                "<strong>OK!</strong> O E-mail informado é válido!";
        }
    else {
        //Caso o valor for inválido, altera o estado de exibição das divs
        $("#msgValidar_Email").attr("class", "alert alert-danger");
        //Informa que o valor está inválido
        document.getElementById("msgValidar_Email").innerHTML =
            "<strong>Incorreto!</strong> O E-mail informado é inválido!";
        document.getElementById("msgValidar_Email").focus();
    }
}


/* JavaScript: Funções para validação de Data/Hora em JS: */
/**
 * Retorna True se a String dada for uma data válida
 *
 * @param string dado Data no formato DD/MM/AAAA
 * @return boolean true se Data Ok
 */
function isStrData(dado) {

    var diasMes = new Array(31, 29, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31);
    var dia = 0, mes = 0, ano = 0;
    var result = false;
    // Pré-analisa o String:
    if (dado != "") {
        if ((dado.length == 10) && (dado.substr(2, 1) == "/") && (dado.substr(5, 1) == "/")) {
            
            // Levanta Campos:
            var d = dado.substr(0, 2);
            var m = dado.substr(3, 2);
            var a = dado.substr(6, 4);

            if ((d > 0) && (d <= 31)) {
                dia = d;
            } else {
                alert("Dia "+d+" inválido");
            }

            if ((m > 0) && (m <= 12)) {
                mes = m;
            } else {
                alert("Mês " + m + " inválido");
            }

            if ((a > 1900) && (a <= 2099)) {
                ano = a;
            } else {
                alert("Ano " + a + " inválido");
            }

            // Analisa Ano e Mês:
            if ((ano > 0) && (ano <= 2099) && (mes >= 1) && (mes <= 12)) {
                // Analisa Dia:
                if ((dia >= 1) && (dia <= diasMes[mes - 1])) {
                    // Analisa os casos não-bissextos:
                    if ((mes == 2) && ((ano % 4 != 0) || (ano % 100 == 0) && (ano % 400 != 0))) {
                        if (dia <= 28) result = true;
                    } else {
                        result = true;
                    }
                }
            } else {
                result = false;
            }
        }
    }
    return result;
}

/**
 * Retorna True se a String dada for uma data válida
 *
 * @param string dado Hora nos formatos HH:MM ou HH:MM:SS
 * @return boolean true se Hora Ok
 */
function isStrHora(dado) {

    var hor, min, seg;
    var result = false;

    // Pré-analisa o String:
    if (dado != "") {
        if (((dado.length == 5) || (dado.length == 8)) && (dado.substr(2, 1) == ":")) {
            // Levanta Campos:
            if (isStrNum(dado.substr(0, 2), 0)) hor = dado.substr(0, 2); else hor = -1;
            if (isStrNum(dado.substr(3, 2), 0)) min = dado.substr(3, 2); else min = -1;

            // Analisa a Hora:
            if ((hor >= 0) && (hor <= 23)) {
                // Analisa o Minuto:
                if ((min >= 0) && (min <= 59)) {
                    // Verifica se tem segundo:
                    if (dado.length == 8) {
                        // Pré-analisa:
                        if (dado.substr(5, 1) == ":") {
                            // Levanta e verifica segundos:
                            if (isStrNum(dado.substr(6, 2), 0)) seg = dado.substr(6, 2); else seg = -1;
                            if ((seg >= 0) && (seg <= 59)) {
                                result = true;
                            }
                        }
                    } else {
                        result = true;
                    }
                }
            }
        }
    }
    return result;
}

/**
 * Retorna True se a String dada for uma DataHora
 * nos formatos "DD/MM/AAAA" ou "DD/MM/AAAA HH:MM"
 * ou "DD/MM/AAAA HH:MM:SS"
 * @param string dado Data/Hora no formato DD/MM/AAAA,
 *                    DD/MM/AAAA HH:MM ou DD/MM/AAAA HH:MM:SS
 * @return boolean true se Data/Hora Ok
 */
function isStrDataHora(dado) {

    var result = false;

    // Pré-Analisa o String:
    if (dado != "") {
        // Analisa a Data:
        if (dado.length >= 10) {
            if (isStrData(dado.substr(0, 10))) {
                // Analisa a Hora, se disponível:
                if (dado.length > 10) {
                    if ((dado.substr(10, 1) == " ") && isStrHora(dado.substr(11))) {
                        result = true;
                    }
                } else {
                    result = true;
                }
            }
        }
    }
    return result;
}


/* Máscaras ER */
function mascara(o, f) {
    v_obj = o;
    v_fun = f;
    setTimeout("execmascara()", 1);
}

function execmascara() {
    v_obj.value = v_fun(v_obj.value);
}

function mtel(v) {
    v = v.replace(/\D/g, ""); //Remove tudo o que não é dígito
    v = v.replace(/^(\d{2})(\d)/g, "($1) $2"); //Coloca parênteses em volta dos dois primeiros dígitos
    v = v.replace(/(\d)(\d{4})$/, "$1-$2"); //Coloca hífen entre o quarto e o quinto dígitos
    return v;
}