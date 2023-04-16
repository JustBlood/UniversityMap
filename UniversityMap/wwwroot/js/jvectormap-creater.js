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
                    'Л_п': 'Stairs',
                    'Л_в': 'Stairs',
                    'Л_л': 'Stairs',
                    'WC-m': 'Toilet',
                    'WC-w': 'Toilet',
                    '.': 'unknown',
                    'p1': 'Hallway',
                    'p2': 'Hallway',
                    'p3': 'Hallway',
                    'p4': 'Hallway',
                    'p5': 'Hallway',
                    'p6': 'Hallway',
                    'p7': 'Hallway',
                    'p8': 'Hallway',
                    'p9': 'Hallway',
                    'p10': 'Hallway',
                    'p11': 'Hallway',
                    'p12': 'Hallway'

                    // При добавлении большего количества элементов
                    // надо ставить запятые
                },
                scale: { // типы цветов, название группы - код цвета
                    "Stairs": 'grey',
                    "Toilet": 'red',
                    "unknown": 'blue',
                    "Hallway": 'grey'
                    // При добавлении большего количества элементов
                    // надо ставить запятые
                },
            }]
        },
        regionStyle: {
            initial: { // заполнение полигонов цветом и обводкой по умолчанию
                fill: "green",
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