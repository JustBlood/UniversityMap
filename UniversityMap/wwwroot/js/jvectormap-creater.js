$(function () {

    /*$('#focus-single').click(function () {
        $('#map1').vectorMap('set', 'focus', { region: id9, animate: true });
    });*/ // В будущем возможно добавим фокус региону (особенно при функции поиска надо будет)

    $('#map').vectorMap({
        map: 'map',

        backgroundColor: 'transparent',
        // создает прозрачный фон

        /*markers: [{
            coords: [60, 110],
            name: 'panoram',
            style: { fill: 'yellow' }
        }],*/ // на случай, если будем делать панорамы через кнопки, а не полигоны

        series: {
            regions: [{
                values: { // регионы (айдишники) и типы их цвета (см. ниже)
                    // имена писались на русском языке, поэтому тут тоже надо писать на русском
                    '302А': 'Study',
                    '323А': 'Study',
                    '307А': 'Study',
                    '308А': 'Study',
                    '315А': 'Study',
                    '316А': 'Study',
                    '322А': 'Study',

                    'Л_п': 'Stairs',
                    'Л_в': 'Stairs',
                    'Л_л': 'Stairs',

                    'WC-m': 'Toilet',
                    'WC-w': 'Toilet',
                    '.': 'unknown'

                    // При добавлении большего количества элементов
                    // надо ставить запятые
                },
                scale: { // типы цветов, название группы - код цвета
                    "Study": 'green',
                    "Stairs": 'grey',
                    "Toilet": 'red',
                    "unknown": 'blue'
                    // При добавлении большего количества элементов
                    // надо ставить запятые
                },
            }]
        },
        regionStyle: {
            initial: { // заполнение полигонов цветом и обводкой по умолчанию
                fill: "orange",
                "fill-opacity": 0.2, // непрозрачность
                stroke: "black",     // обводка
                "stroke-width": 0.5,
                "stroke-opacity": 1,
            }
        },
        regionLabelStyle: { // номера аудиторий (айдишники)
            initial: {
                'font-family': 'cursive',
                'font-size': '1.5vh',
                fill: 'black',
            },
            hover: {

            }
        },
        labels: {
            regions: {
                render: function (code) {
                    if (code[0] == 'p') {
                        return;
                    }
                    return code;
                },
            },
        }
    });
})
