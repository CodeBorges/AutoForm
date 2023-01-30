function cadastrar() {

    let tipoUsuario = {
        "NOME": $('#txtNomeTipoUsuario').val(),
        
    };

    $.ajax({
        url: "/Admin/CadastrarTipoUsuario/",
        type: "POST",
        data: JSON.stringify(tipoUsuario),
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

    let IdTIpoUsuario = parent.find('.tdIdTipoUsuario').text();
    let NomeTipoUsuario = parent.find('.tdNomeTipoUsuario').text();

   
    $('#txtIdTipoUsuario').val(IdTIpoUsuario);
    $('#txtNomeTipoUsuario').val(NomeTipoUsuario);
    

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

    let tipoUsuario = {
        "ID": $('#txtIdTipoUsuario').val(),
        "NOME": $('#txtNomeTipoUsuario').val(),
       
    };

    $.ajax({
        url: "/Admin/EditarTipoUsuario/",
        type: "POST",
        data: JSON.stringify(tipoUsuario),
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

function excluir(valorIdTipoUsuario) {
    $('#btnExcluir').attr('href', '/Admin/ExcluirTipoUsuario/' + valorIdTipoUsuario);
    $('#modalExcluir').modal('toggle'); 
}