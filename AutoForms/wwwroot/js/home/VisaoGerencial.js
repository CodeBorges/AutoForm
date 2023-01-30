document.addEventListener("DOMContentLoaded", function () {
    
});

$(document).on('click', 'ul li', function () {
    $(this).addClass('ativo').siblings().removeClass('ativo')
});

function exibirCard() {
    document.getElementById("card").style.display = "flex";
    document.getElementById("linha").style.display = "none";
}

function exibirLinha() {
    document.getElementById("card").style.display = "none";
    document.getElementById("linha").style.display = "block";
}

