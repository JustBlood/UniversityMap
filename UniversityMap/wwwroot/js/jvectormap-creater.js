$(function () {
    $('#map').vectorMap({
        map: 'map',

        // backgroundColor: 'transparent',
        // создает прозрачный фон (ставить после разукрашивания аудиторий)

        series: {
            regions: [{
                values: { // регионы (айдишники) и типы их цвета (см. ниже)
                    id9: 'Study'
                    // При добавлении большего количества элементов 
                    // надо ставить запятые
                },
                scale: { // типы цветов, название группы - код цвета
                    "Study": "#73bb43"
                    // При добавлении большего количества элементов
                    // надо ставить запятые
                },
            }]
        },
        regionLabelStyle: {
            initial: {
                'font-size': '1vh',
                fill: '#B90E32',
            },
            hover: {
                fill: 'black',
            }
        },
        labels: {
            regions: {
                render: function (code) {
                    return code;
                },
            },
        }
    });
})