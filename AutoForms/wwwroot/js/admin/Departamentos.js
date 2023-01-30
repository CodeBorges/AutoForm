function cadastrar() {

    let Departamento = {
        "NOME": $('#txtNomeDepartamento').val(),
        
    };

    $.ajax({
        url: "/Admin/CadastrarDepartamento/",
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

    let idDepartamento = parent.find('.tdIdDepartamento').text();
    let NomeDepartamento = parent.find('.tdNomeDepartamento').text();

   
    $('#txtIdDepartamento').val(idDepartamento);
    $('#txtNomeDepartamento').val(NomeDepartamento);
    

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
        "ID": $('#txtIdDepartamento').val(),
        "NOME": $('#txtNomeDepartamento').val(),
       
    };

    $.ajax({
        url: "/Admin/EditarDepartamento/",
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

function excluir(valorIdDep) {
    $('#btnExcluir').attr('href', '/Admin/ExcluirDepartamento/' + valorIdDep);
    $('#modalExcluir').modal('toggle');
}