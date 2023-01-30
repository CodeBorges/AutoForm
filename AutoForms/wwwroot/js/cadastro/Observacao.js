document.addEventListener("DOMContentLoaded", function () {
    carregarFormularioCategoria();
});

function carregarFormularioCategoria() {
    $('#txtIdFormulario').change(function () {

        let idFormulario = { "idFormulario": this.value };

        $.ajax({
            url: "/Cadastro/CarregarObservacaoPorIdFormulario/",
            type: "POST",
            data: idFormulario,
            contentType: "application/x-www-form-urlencoded; charset=utf-8",
            success: function (response) {
                $("#bodyPartialObservacao").html(response);
            },
            error: function (response) {
                window.location.href = response.responseJSON.redirectToUrl;
            }
        });
    });
};

function cadastrar() {

    let observacao = {
        "NOME": $('#txtNomeObservacao').val(),
        "ID_FORMULARIO": $('#txtIdFormulario').val()
    };

    $.ajax({
        url: "/Cadastro/CadastrarObservacao/",
        type: "POST",
        data: JSON.stringify(observacao),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            window.location.href = response.redirectToUrl;
        },
        error: function (response) {
            window.location.href = response.redirectToUrl;
        }
    });

}

function editar() {

    let parent = $(event.target).parents().closest('tr');

    let idObservacao = parent.find('.tdIdObservacao').text();
    let nomeFormulario = parent.find('.tdIdFormulario').text();
    let nomeObservacao = parent.find('.tdNomeObservacao').text();

    $('#txtIdObservacao').val(idObservacao);
    $('#txtIdFormulario').val(nomeFormulario).change();
    $('#txtNomeObservacao').val(nomeObservacao);


    $('#btnCadastrar').attr('hidden', true);
    $('#btnEditar').attr('hidden', false);
    $('#btnCancelar').attr('hidden', false);
};

function cancelar() {
    $('input[type="checkbox"]').attr('checked', false);
    $('#btnCadastrar').attr('hidden', false);
    $('#btnEditar').attr('hidden', true);
    $('#btnCancelar').attr('hidden', true);
};

function salvar() {

    let observacao = {
        "ID": $('#txtIdObservacao').val(),
        "ID_FORMULARIO": $('#txtIdFormulario').val(),
        "NOME": $('#txtNomeObservacao').val()

    };

    $.ajax({
        url: "/Cadastro/EditarObservacao/",
        type: "POST",
        data: JSON.stringify(observacao),
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

function excluir(valorIdObservacao) {
    $('#btnExcluir').attr('href', '/Cadastro/ExcluirObservacao/' + valorIdObservacao);
    $('#modalExcluir').modal('toggle');
}