﻿$(function () {
    $('#map').vectorMap({
        map: 'map',
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