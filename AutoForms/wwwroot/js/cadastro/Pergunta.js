document.addEventListener("DOMContentLoaded", function () {
    listarCategorias();
    carregarPergunta();
});

function listarCategorias() {
    $('#txtIdFormulario').change(function () {

        let idFormulario = { "idFormulario": this.value };

        $.ajax({
            url: "/Cadastro/RetornarListaCategoriaPorIdFormulario/",
            type: "POST",
            data: idFormulario,
            contentType: "application/x-www-form-urlencoded; charset=utf-8",
            success: function (data) {

                var ddlVerificar = $("#txtIdCategoria option:selected").val();

                var ddlConfigurar = $('#txtIdCategoria');
                ddlConfigurar
                    .empty()
                    .append('<option value="" selected>Selecione</option>');

                $.each(data, function (value, key) {
                    ddlConfigurar.append(
                        $('<option></option>', {
                            text: key,
                            value: value
                        })
                    );

                });

                if (ddlVerificar != undefined)
                $('#txtIdCategoria').val(ddlVerificar).change();
            },

            error: function (response) {
                window.location.href = response.responseJSON.redirectToUrl;
            }
        });
    });
};

function carregarPergunta() {
    $('#txtIdCategoria').change(function () {

        let idCategoria = { "idCategoria": this.value };

        $.ajax({
            url: "/Cadastro/CarregarPerguntaPorIdCategoria/",
            type: "POST",
            data: idCategoria,
            contentType: "application/x-www-form-urlencoded; charset=utf-8",
            success: function (response) {
                $("#bodyPartialPergunta").html(response);
            },
            error: function (response) {
                window.location.href = response.responseJSON.redirectToUrl;
            }
        });
    });
};

function cadastrar() {

    let pergunta = {
        "NOME": $('#txtPergunta').val(),
        "ID_FORMULARIO": $('#txtIdFormulario').val(),
        "ID_CATEGORIA": $('#txtIdCategoria').val()
    };

    $.ajax({
        url: "/Cadastro/CadastrarPergunta/",
        type: "POST",
        data: JSON.stringify(pergunta),
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


    let idPergunta = parent.find('.tdIdPergunta').text();
    let idFormulario = parent.find('.tdIdFormulario').text();
    let idCategoria = parent.find('.tdIdCategoria').text();
    let NomePergunta = parent.find('.tdNomePergunta').text();


    $('#txtIdPergunta').val(idPergunta);
    $('#txtIdFormulario').val(idFormulario).change();
    $('#txtIdCategoria').val(idCategoria).change();
    $('#txtPergunta').val(NomePergunta);


    $('#btnCadastrar').attr('hidden', true);
    $('#btnEditar').attr('hidden', false);
    $('#btnCancelar').attr('hidden', false);

};

function cancelar() {
    $('input[type="checkbox"]').attr('checked', false);
    $('#txtIdCategoria option').remove();
    $('#btnCadastrar').attr('hidden', false);
    $('#btnEditar').attr('hidden', true);
    $('#btnCancelar').attr('hidden', true);
};

function salvar() {

    let pergunta = {
        "ID": $('#txtIdPergunta').val(),
        "NOME": $('#txtPergunta').val(),
        "ID_FORMULARIO": $('#txtIdFormulario').val(),
        "ID_CATEGORIA": $('#txtIdCategoria').val()

    };

    $.ajax({
        url: "/Cadastro/EditarPergunta/",
        type: "POST",
        data: JSON.stringify(pergunta),
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

function excluir(valorIdPergunta) {
    $('#btnExcluir').attr('href', '/Cadastro/ExcluirPergunta/' + valorIdPergunta);
    $('#modalExcluir').modal('toggle');
}