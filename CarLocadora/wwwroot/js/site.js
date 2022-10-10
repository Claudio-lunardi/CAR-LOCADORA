$(document).ready(function () {

    $('.maskTelefone').inputmask({ mask: ['(99) 9999-99999'] });
    $('.maskCelular').inputmask({ mask: ['(99) 99999-9999'] });
    $('.maskPlaca').inputmask({ mask: ['AAA9*99'] });
    $('.maskCPF').inputmask({ mask: ['999.999.999-99'] });
    $('.maskCNPJ').inputmask({ mask: ['99.999.999/9999-99'] });

    //CONFIGURAÇÃO GRID
    $('#myTable').DataTable({
        "language": {
            "url": "//cdn.datatables.net/plug-ins/9dcbecd42ad/i18n/Portuguese-Brasil.json"
        },
        "lengthMenu": [10, 15, 20, 30, 100]
    });


    //VERIFICANDO SE O CAMPO FOI EDITADO
    var inicial = $("#Form").serialize();
    $("#Form").on("submit", function (e) {

        var atual = $("#Form").serialize();
        if (atual !== inicial)
        {
            $("#BtnEditar").addEventLitener("click", function () {
                $("#Form").submit();
            })
        }
        else if ($("#alertaErro").is(":hidden"))
        {
            e.preventDefault();
            toastr.warning("Nenhum campo foi editado");
        }
    });
});