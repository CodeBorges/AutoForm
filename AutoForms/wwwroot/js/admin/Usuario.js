function cadastrar() {

    $('#btnCadastrar').attr('disabled', true);

    let usuario = {
        "NOME": $('#txtNomeUsuario').val(),
        "USUARIO": $('#txtLogin').val(),
        "SENHA": $('#txtSenha').val(),
        "MUDAR_SENHA": $('#txtMudarSenha').val() == 'true' ? true : false,
        "EMAIL": $('#txtEmail').val(),
        "ID_DEPARTAMENTO": $('#txtIdDepartamento').val(),
        "ID_TIPO_USUARIO": $('#txtIdTipoUsuario').val()
    };

    $.ajax({
        url: "/Admin/CadastrarUsuario/",
        type: "POST",
        data: JSON.stringify(usuario),
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

    let idUsuario = parent.find('.tdIdUsuario').text();
    let idDepartamento = parent.find('.tdIdDepartamento').text();
    let idTipoUsuario = parent.find('.tdIdTipoUsuario').text();
    let nomeUsuario = parent.find('.tdNomeUsuario').text();
    let login = parent.find('.tdLogin').text();
    let senha = parent.find('.tdSenha').text();
    let email = parent.find('.tdEmail').text();
    let mudarSenha = parent.find('.tdMudarSenha').text() == 'Sim' ? 'true' : 'false';

    $('#txtIdUsuario').val(idUsuario).change();
    $('#txtIdDepartamento').val(idDepartamento).change();
    $('#txtIdTipoUsuario').val(idTipoUsuario).change();
    $('#txtNomeUsuario').val(nomeUsuario);
    $('#txtLogin').val(login);
    $('#txtSenha').val(senha);
    $('#txtEmail').val(email);
    $('#txtMudarSenha').val(mudarSenha).change();


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

    let usuario = {
        "ID": $('#txtIdUsuario').val(),
        "NOME": $('#txtNomeUsuario').val(),
        "USUARIO": $('#txtLogin').val(),
        "SENHA": $('#txtSenha').val(),
        "MUDAR_SENHA": $('#txtMudarSenha').val() == 'true' ? true : false,
        "EMAIL": $('#txtEmail').val(),
        "ID_DEPARTAMENTO": $('#txtIdDepartamento').val(),
        "ID_TIPO_USUARIO": $('#txtIdTipoUsuario').val()

    };

    $.ajax({
        url: "/Admin/EditarUsuario/",
        type: "POST",
        data: JSON.stringify(usuario),
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

function excluir(valorIdUsuario) {
    $('#btnExcluir').attr('href', '/Admin/ExcluirUsuario/' + valorIdUsuario);
    $('#modalExcluir').modal('toggle');
}