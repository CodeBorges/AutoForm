$(document).ready(function () {
    setTimeout(function () {
        $(".alert").fadeOut("slow", function () {
            $(this).alert('close');
        });
        $(".esconderToast").fadeOut("slow", function () {
            $(this).alert('close');
        });
    }, 5000);
});

function ocultarToast() {
    $(".esconderToast").fadeOut("slow", function () {
        $(this).alert('close');
    });
}