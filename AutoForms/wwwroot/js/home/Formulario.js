document.addEventListener("DOMContentLoaded", function () {
    retornarRegistroRespostaInformacoes($('#txtIdDocumento').val());
});

function retornarRegistroRespostaInformacoes(idDocumento) {

    let documento = { "idDocumento": idDocumento };

    $.ajax({
        url: "/Home/RetornarListaDeInformacoesResposta/",
        type: "POST",
        data: documento,
        contentType: "application/x-www-form-urlencoded; charset=utf-8",
        success: function (data) {
            if (data != null) {

                if (data.iD_DEPARTAMENTO == 0) {
                    $('#txtIdDepartamento').val("").change();
                } else {
                    $('#txtIdDepartamento').val(data.iD_DEPARTAMENTO).change();
                }

                if (data.iD_LOCALIZACAO == 0) {
                    $('#txtIdLocalizacao').val("").change();
                } else {
                    $('#txtIdLocalizacao').val(data.iD_LOCALIZACAO).change();
                }

                $('#txtAuditado').val(data.usuariO_AUDITADO);

            }
            else {
            }
        },
        error: function (response) {
            window.location.href = response.responseJSON.redirectToUrl;
        }
    });
};

function retornarRegistroResposta(idDocumento) {

    let documento = { "idDocumento": idDocumento };

    $.ajax({
        url: "/Home/RetornarListaDeRespostaIdDocumento/",
        type: "POST",
        data: documento,
        contentType: "application/x-www-form-urlencoded; charset=utf-8",
        success: function (data) {
            if (data != null) {
                $.each(data, function (key, value) {
                    $("input[id^='pergunta_']").each(function () {
                        if (value != "null") {
                            $(`#resposta_${key}`).val(value);
                        } else {
                            $(`input[id^='resposta_${key}'][value='${0}']`);
                        }
                    });
                });
            }
        },
        error: function (response) {
            window.location.href = response.responseJSON.redirectToUrl;
        }
    });
};

function retornarRegistroRespostaObservacao(idDocumento) {

    let documento = { "idDocumento": idDocumento };

    $.ajax({
        url: "/Home/RetornarListaDeInformacoesObservacao/",
        type: "POST",
        data: documento,
        contentType: "application/x-www-form-urlencoded; charset=utf-8",
        success: function (data) {
            if (data != null) {
                $.each(data, function (key, value) {
                    $("textarea[id^='chkIdObservacao_']").each(function () {
                        $(`textarea[id^='chkIdObservacao_${key}']`).val(value);
                    });
                });
            }
            else {
            }
        },
        error: function (response) {
            window.location.href = response.responseJSON.redirectToUrl;
        }
    });
};

function salvarInformacoes() {

    let documentoFormulario = {
        "ID": $('#txtIdDocumento').val(),
        "ID_DEPARTAMENTO": $('#txtIdDepartamento').val(),
        "ID_LOCALIZACAO": $('#txtIdLocalizacao').val(),
        "USUARIO_AUDITADO": $('#txtAuditado').val()
    };


    $.ajax({
        url: "/Home/SalvarInformacoes/",
        type: "POST",
        data: JSON.stringify(documentoFormulario),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            if (result.success) {
                Next1();
            }
            else {
                location.reload(true);
            }


        },
        error: function (result) {
            if (!result.Error)
                location.reload(true);
        }
    });
};

function salvarObservacao() {

    var formulario = [];

    $("input[id=txtObservacaoId]").each(function (i) {

        var idDocumento = $("#txtIdDocumento").val();
        var idObservacao = this.value;
        var textoObservacao = $(`#chkIdObservacao_${idObservacao}`).val();

        const item = {
            "ID_DOCUMENTO": idDocumento,
            "ID_OBSERVACAO": idObservacao,
            "TEXTO_OBSERVACAO": textoObservacao,
            "idDocumento": idDocumento
        };

        formulario.push(item);
    });

    $.ajax({
        url: "/Home/SalvarObservacao/",
        type: "POST",
        data: JSON.stringify(formulario),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            window.location.href = response.redirectToUrl;
        },
        error: function (response) {
            window.location.href = response.redirectToUrl;
        }
    });
};

function salvarRespostaAutomaticamente(idPergunta) {

    var registroResposta = [];

    $("input[id^='pergunta_']").keyup(function (i) {
        var idPergunta = this.value;
        var respostaValor = $(`#resposta_${idPergunta}`).val() == '' ? 0 : $(`#resposta_${idPergunta}`).val();
        var categoria = $(`#categoria_${idPergunta}`).val();
        var meta = $(`#txtMeta`).val();
        var idDocumento = $(`#txtIdDocumento`).val();

        const item = {
            "ID_PERGUNTA": idPergunta,
            "RESPOSTA": respostaValor,
            "ID_CATEGORIA": categoria,
            "META": meta,
            "ID_DOCUMENTO": idDocumento
        };

        registroResposta.push(item);

        console.log(registroResposta)
    });


    $.ajax({
        url: "/Home/SalvarRespostaAutomaticamente/",
        type: "POST",
        data: JSON.stringify(registroResposta),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data != null) {
                $.each(data, function (key, value) {
                    $("input[id^='mediaCategoria_']").each(function () {
                        $(`input[id^='mediaCategoria_${key}']`).val(`${value}%`);
                    });
                });
            }
        },
        error: function (response) {
            alert("DEU errado!");
        }
    });
};

function somenteNumeros(event) {
    var key = event.keyCode;
    return (event.charCode >= 48 && event.charCode <= 57)
}

function verificarMedia(pergunta, resposta) {

    var registroResposta = [];

    $("input[id^='pergunta_']").each(function (i) {
        var idPergunta = this.value;
        var respostaValor = $(`#resposta_${idPergunta}`).val() == '' ? 0 : $(`#resposta_${idPergunta}`).val();
        var categoria = $(`#categoria_${idPergunta}`).val();
        var meta = $(`#txtMeta`).val();
        var idDocumento = $(`#txtIdDocumento`).val();

        const item = {
            "ID_PERGUNTA": idPergunta,
            "RESPOSTA": respostaValor,
            "ID_CATEGORIA": categoria,
            "META": meta,
            "ID_DOCUMENTO": idDocumento
        };

        registroResposta.push(item);

    });

    const registro = {
        "ID_PERGUNTA": pergunta,
        "RESPOSTA": resposta == '' ? 0 : resposta,
        "ID_DOCUMENTO": $(`#txtIdDocumento`).val(),
        "RegistroResposta": registroResposta
    };

    $.ajax({
        url: "/Home/VerificarMediaResposta/",
        type: "POST",
        data: JSON.stringify(registro),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data != null) {
                $.each(data, function (key, value) {
                    $("input[id^='mediaCategoria_']").each(function () {
                        $(`input[id^='mediaCategoria_${key}']`).val(`${value}%`);
                    });
                });

            }
        },
        error: function (response) {
            alert("DEU errado!");
        }
    });
};

function valorMinMax(idPergunta) {
    let meta = $(`#txtMeta`).val();
    let valor = $(`#resposta_${idPergunta}`).val() == '' ? 0 : $(`#resposta_${idPergunta}`).val();

    if (valor < 0) {
        valor = 0;
    }
    if (valor >= parseInt(meta)) {
        $(`#resposta_${idPergunta}`).val(meta);
    }

}

function excluir(valorIdDocumento) {
    $('#btnExcluir').attr('href', '/Home/ExcluirFormularioGerado/' + valorIdDocumento);
    $('#modalExcluir').modal('toggle');
}

$("#remove").click(function () {
    $(`input[id^='chkIdPergunta_']`).removeAttr('checked', 'checked');
    $('#remove').prop('disabled', true);
    $('#Next2').prop('disabled', false);


});

/*SEPARANDO OS FORMS EM VARIAVEIS */
var Form1 = document.getElementById("Form1")
var Form2 = document.getElementById("Form2")
var Form3 = document.getElementById("Form3")

/*SEPARANDO OS BUTTONS EM VARIAVEIS */
var Next2 = document.getElementById("Next2")
var Next3 = document.getElementById("Next3")
var Back1 = document.getElementById("Back1")
var Back2 = document.getElementById("Back2")

function Next1() {

    let formulario1 = document.getElementById("Form1")
    let formulario2 = document.getElementById("Form2")
    formulario1.style.display = "none"
    formulario2.style.display = "block"
    $(formulario2).removeClass("d-none");

}

Back1.onclick = function () {
    /*VOLTANDO DO SEGUNDO STEP PARA O PRIMEIRO*/
    Form2.style.display = "none"
    Form1.style.display = "block"
    $(Form1).removeClass("d-none");

}

function botaoAvancar2() {
    /*INDO DO SEGUNDO STEP PARA O TERCEIRO*/
    Form2.style.display = "none"
    Form3.style.display = "block"
    $(Form3).removeClass("d-none");
}

Back2.onclick = function () {
    /*VOLTANDO DO TERCEIRO STEP PARA O SEGUNDO*/
    Form3.style.display = "none"
    Form2.style.display = "block"
    $(Form2).removeClass("d-none");
}

Next3.onclick = function () {
    /*LOGICA PARA INSERIR NO BANCO DE DADOS AS OBSERVAÇÕES*/
    salvarObservacao();
}
