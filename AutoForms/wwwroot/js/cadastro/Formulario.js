function cadastrar() {

    let formulario = {
        "NOME": $('#txtNomeFormulario').val(),
        "META": $('#txtMeta').val(),
        "ID_DEPARTAMENTO": $('#txtIdDepartamento').val()
    };

    $.ajax({
        url: "/Cadastro/CadastrarFormulario/",
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

}

function editar() {

    let parent = $(event.target).parents().closest('tr');

    let idFormulario = parent.find('.tdIdFormulario').text();
    let nomeFormulario = parent.find('.tdNomeFormulario').text();
    let meta = parent.find('.tdMeta').text();
    let nomeDepartamento = parent.find('.tdIdDepartamento').text();
   
    $('#txtIdFormulario').val(idFormulario);
    $('#txtNomeFormulario').val(nomeFormulario);
    $('#txtMeta').val(meta);
    $('#txtIdDepartamento').val(nomeDepartamento).change();
    

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

    let formulario = {
        "ID": $('#txtIdFormulario').val(),
        "NOME": $('#txtNomeFormulario').val(),
        "META": $('#txtMeta').val(),
        "ID_DEPARTAMENTO": $('#txtIdDepartamento').val()
       
    };

    $.ajax({
        url: "/Cadastro/EditarFormulario/",
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

function excluir(valorIdForm) {
    $('#btnExcluir').attr('href', '/Cadastro/ExcluirFormulario/' + valorIdForm);
    $('#modalExcluir').modal('toggle');
}