function cadastrar() {

    let Departamento = {
        "NOME": $('#txtNomeLocalizacao').val()
        
    };

    $.ajax({
        url: "/Cadastro/CadastrarLocalizacao/",
        type: "POST",
        data: JSON.stringify(Departamento),
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

    let idLocalizacao = parent.find('.tdIdLocalizacao').text();
    let NomeLocalizacao = parent.find('.tdNomeLocalizacao').text();

   
    $('#txtIdLocalizacao').val(idLocalizacao);
    $('#txtNomeLocalizacao').val(NomeLocalizacao);
    

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

    let departamento = {
        "ID": $('#txtIdLocalizacao').val(),
        "NOME": $('#txtNomeLocalizacao').val(),
       
    };

    $.ajax({
        url: "/Cadastro/EditarLocalizacao/",
        type: "POST",
        data: JSON.stringify(departamento),
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

function excluir(valorIdLocal) {
    $('#btnExcluir').attr('href', '/Cadastro/ExcluirLocalizacao/' + valorIdLocal);
    $('#modalExcluir').modal('toggle');
}