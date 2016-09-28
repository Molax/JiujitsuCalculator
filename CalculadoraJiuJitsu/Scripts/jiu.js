window.jiu = {};

jiu.global = {

    lugar: {},
    modalidade: {},
    faixa: {},
    tempo: {},
    camp: 0
};

jiu.fn = {
    postaface: function () {
        window.location.href = "/Resultado/Index";
    },

    RetornaFaixa: function () {
        $.ajax({

            url: 'api/API_JIU/RetornaFaixa',
            type: 'GET',
            dataType: 'json',
            success: function (data) {
                jiu.global.faixa = data;
                console.log(jiu.global.faixa)
            }
        });
    },
    RetornaTempo: function () {
        $.ajax({

            url: 'api/API_JIU/RetornaTempo',
            type: 'GET',
            dataType: 'json',
            success: function (data) {
                debugger;
                jiu.global.tempo = data;
                console.log(jiu.global.tempo)
            }
        });
    },

    calcula: function () {
        debugger;
        if (jiu.global.camp == 0) {
            $('#camp1').show();

        }
        if (jiu.global.camp == 1) {
            $('#camp2').show();
        }
        if (jiu.global.camp == 2) {
            $('#camp3').show();
        }
        if (jiu.global.camp == 3) {
            $('#camp4').show();
        }
        if (jiu.global.camp == 4) {
            $('#camp5').show();
        }
        jiu.global.camp += 1;
    },

    fazponto: function () {
        $.ajax({

            url: 'CalculaPontos',
            dataType: 'json',
            data: {
                "faixa": $('#faixa').val(),
                "tempo": $('#tempo').val(),
                "categoria": $('#categoria').val(),
                "faixaCamp1": $('#faixaCamp1').val(),
                "colocacaoCamp1": $('#colocacaoCamp1').val(),
                "categoriaCamp1": $('#categoriaCamp1').val(),
                "faixaCamp2": $('#faixaCamp2').val(),
                "colocacaoCamp2": $('#colocacaoCamp2').val(),
                "categoriaCamp2": $('#categoriaCamp2').val(),
                "faixaCamp3": $('#faixaCamp3').val(),
                "colocacaoCamp3": $('#colocacaoCamp3').val(),
                "categoriaCamp3": $('#categoriaCamp3').val(),
                "faixaCamp4": $('#faixaCamp4').val(),
                "colocacaoCamp4": $('#colocacaoCamp4').val(),
                "categoriaCamp4": $('#categoriaCamp4').val(),
                "faixaCamp5": $('#faixaCamp5').val(),
                "colocacaoCamp5": $('#colocacaoCamp5').val(),
                "categoriaCamp5": $('#categoriaCamp5').val(),
            },
            success: function (e) {
                debugger;
                window.location.href = 'Calcular';
            },
            error: function (e) {
                
            }

        });
    },
    RetornaModalidade: function () {
        $.ajax({

            url: 'api/API_JIU/RetornaModalidade',
            type: 'GET',
            dataType: 'json',
            success: function (data) {

                jiu.global.modalidade = data;
                console.log(jiu.global.modalidade)
            }
        });
    },
    RetornaLugar: function () {
        $.ajax({

            url: 'api/API_JIU/RetornaLugar',
            type: 'GET',
            dataType: 'json',
            success: function (data) {

                jiu.global.lugar = data;
                console.log(jiu.global.lugar)
            }
        });
    }

};

jiu.delegate = function () {

    $('body').delegate('#postar', 'click', function () { jiu.fn.postaface(); });
    $('body').delegate('#addcamp', 'click', function () { jiu.fn.calcula(); });
    $('body').delegate('#calcula', 'click', function () { jiu.fn.fazponto(); });

};

jiu.config = function () {

    jiu.delegate();

};

jiu.init = function () {
    $('#camp1').hide();
    $('#camp2').hide();
    $('#camp3').hide();
    $('#camp4').hide();
    $('#camp5').hide();
    jiu.config();

}();