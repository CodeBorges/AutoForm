document.addEventListener("DOMContentLoaded", function () {
    carregarFormularioCategoria();
});

function carregarFormularioCategoria() {

    $('#txtIdFormulario').change(function () {

        let ID_FORMULARIO = { "idFormulario": this.value };

        $.ajax({
            url: "/Cadastro/CarregarFormularioCategoriaPorIdFormulario/",
            type: "POST",
            data: ID_FORMULARIO,
            contentType: "application/x-www-form-urlencoded; charset=utf-8",
            success: function (response) {
                $("#bodyPartialCategoria").html(response);
            },
            error: function (response) {
                window.location.href = response.responseJSON.redirectToUrl;
            }
        });
    });
};

function cadastrar() {

    let categoria = {
        "NOME": $('#txtNomeCategoria').val(),
        "CONCEITO": $('#txtConceito').val(),
        "ID_FORMULARIO": $('#txtIdFormulario').val()
    };

    $.ajax({
        url: "/Cadastro/CadastrarCategoria/",
        type: "POST",
        data: JSON.stringify(categoria),
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

    let idFormularioCategoria = parent.find('.tdIdFormularioCategoria').text;
    let idFormulario = parent.find('.tdIdFormulario').text();
    let nomeCategoria = parent.find('.tdNomeCategoria').text();
    let conceito = parent.find('.tdConceito').text();
    let idCategoria = parent.find('.tdIdCategoria').text();

    $('#btnCadastrar').attr('hidden', true);
    $('#btnEditar').attr('hidden', false);
    $('#btnCancelar').attr('hidden', false);

    $('#txtIdFormulario').val(idFormulario).change();
    $('#txtNomeCategoria').val(nomeCategoria);
    $('#txtConceito').val(conceito);
    $('#txtIdCategoria').val(idCategoria);
    $('#txtId').val(idFormularioCategoria);

};

function cancelar() {
    $('input[type="checkbox"]').attr('checked', false);
    $('#btnCadastrar').attr('hidden', false);
    $('#btnEditar').attr('hidden', true);
    $('#btnCancelar').attr('hidden', true);
};

function salvar() {

    let categoria = {
        "ID": $('#txtIdCategoria').val(),
        "ID_FORMULARIO": $('#txtIdFormulario').val(),
        "NOME": $('#txtNomeCategoria').val(),
        "CONCEITO": $('#txtConceito').val()

    };

    $.ajax({
        url: "/Cadastro/EditarCategoria/",
        type: "POST",
        data: JSON.stringify(categoria),
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

function excluir(valorIdCategoria) {
    $('#btnExcluir').attr('href', '/Cadastro/ExcluirCategoria/' + valorIdCategoria);
    $('#modalExcluir').modal('toggle');
}