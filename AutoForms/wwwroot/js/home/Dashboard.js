document.addEventListener("DOMContentLoaded", function () {
    verificarAlterarSenha();
});

function verificarAlterarSenha() {

    var idUsuario = $("#txtIdUsuario").val();

    let usuario = { "idUsuario": idUsuario };

    $.ajax({
        url: "/Home/VerificarAlterarSenha/",
        type: "POST",
        data: usuario,
        contentType: "application/x-www-form-urlencoded; charset=utf-8",
        success: function (data) {
            if (data) {
                $('#modalAlterar').modal('toggle');
            }
            
        },
        error: function (response) {
            window.location.href = response.responseJSON.redirectToUrl;
        }
    });
};