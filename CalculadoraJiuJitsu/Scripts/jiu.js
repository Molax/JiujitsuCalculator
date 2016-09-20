window.jiu = {};

jiu.global = {

    lugar : {},
    modalidade: {},
    faixa: {},
    tempo: {}

};

jiu.fn = {
    postaface: function () {
        window.location.href = "/Resultado/Index";
    },

    RetornaFaixa : function () {
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
    RetornaModalidade : function () {
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
    RetornaLugar : function () {
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

};

jiu.config = function () {

    jiu.delegate();

};

jiu.init = function () {

    jiu.config();

}();