function ModalLogout() {
    $('#modalLogout').modal('toggle');
}

function ModalAlterar() {
    $('#modalAlterar').modal('toggle');
}

function VerificarNovaSenha() {
    var novaSenha = document.getElementById("txtNovaSenha").value;
    var repetirNovaSenha = document.getElementById("txtRepetirSenha").value;

    if (novaSenha != repetirNovaSenha || !novaSenha || !repetirNovaSenha) {
        $('#txtNovaSenha').css('border', '1px solid red');
        $('#txtRepetirSenha').css('border', '1px solid red');
        $('#txtMensagem').removeAttr("hidden");
        $('#txtMensagem').css({
            'color': 'red',
            'font-size': '13px',
            'user-select': 'none'
        });
        $('#btnAlterar').attr('disabled', true);
    }
    else {
        $('#txtNovaSenha').css('border', '1px solid green');
        $('#txtRepetirSenha').css('border', '1px solid green');
        $('#txtMensagem').attr("hidden", true);
        $('#btnAlterar').attr('disabled', false);
    }
}

function VerificarSenhaAtual() {
    var senhaUsuario = document.getElementById("txtSenhaUsuario").innerText;
    var senhaAtual = $('#txtSenhaAtual').val();

    if (senhaUsuario != senhaAtual) {
        $('#txtSenhaAtual').css('border', '1px solid red');
        $('#txtMensagemSenhaAtual').removeAttr("hidden");
        $('#txtMensagemSenhaAtual').css({
            'color': 'red',
            'font-size': '13px',
            'user-select': 'none'
        });
        $('#btnAlterar').attr('disabled', true);
    }
    else {
        $('#txtSenhaAtual').css('border', '1px solid green');
        $('#txtMensagemSenhaAtual').attr("hidden", true);
        $('#btnAlterar').attr('disabled', false);
    }
}

$('#mostrarSenha').change(function () {
    if ($(this).is(":checked")) {
        $('#txtSenha').prop('type', 'text');
        $('#txtRepetirSenha').prop('type', 'text');
        $('#txtNovaSenha').prop('type', 'text');

    } else {
        $('#txtSenha').prop('type', 'password');
        $('#txtRepetirSenha').prop('type', 'password');
        $('#txtNovaSenha').prop('type', 'password');
    }
})

function retornarFormulario() {

    var nomeFormulario = $("#nomeFormulario").val();

    let formulario = { "nomeFormulario": nomeFormulario };

    $.ajax({
        url: "/Home/RetornarFormularioPorNome/",
        type: "POST",
        data: formulario,
        contentType: "application/x-www-form-urlencoded; charset=utf-8",
        success: function (data) {
            window.location.href = data.redirectToUrl;
        },
        error: function (response) {
            window.location.href = response.responseJSON.redirectToUrl;
        }
    });
};
