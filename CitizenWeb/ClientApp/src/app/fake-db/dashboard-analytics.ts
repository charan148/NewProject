export class AnalyticsDashboardDb
{
    public static widgets = {
        widget1: {
            chartType: 'line',
            datasets : {
                '2018': [
                    {
                        label: 'Total Requests',
                        data: [330, 500, 480, 340, 480, 580, 400, 560, 620, 560, 470, 430],
                        fill: 'start'

                    },
                    //{
                    //    label: 'Resolved Requests',
                    //    data: [190, 300, 340, 220, 290, 390, 250, 380, 410, 380, 320, 290],
                    //    fill: 'start'

                    //},
                    //{
                    //    label: 'Pending Requests',
                    //    data: [140, 200, 140, 120, 190, 190, 150, 180, 210, 180, 150, 140],
                    //    fill: 'start'
                    //}                                                           
                    
                ],
                '2019': [
                    {
                        label: 'Total Requests',
                        data: [360, 490, 530, 370, 570, 510, 440, 370, 510, 520, 560, 520],
                        fill: 'start'

                    },
                    //{
                    //    label: 'Resolved Requests',
                    //    data: [220, 290, 390, 250, 380, 320, 290, 190, 300, 340, 410, 380],
                    //    fill: 'start'

                    //},
                    //{
                    //    label: 'Pending Requests',
                    //    data: [140, 200, 140, 120, 190, 190, 150, 180, 210, 180, 150, 140],
                    //    fill: 'start'

                    //}                                                             
                ],
                '2020': [
                    {
                        label: 'Total Requests',
                        data: [510, 440, 620, 650, 270], //, 420, 570, 510, 410, 480, 330, 470
                        fill: 'start'

                    },
                    //{
                    //    label: 'Resolved Requests',
                    //    data: [390, 250, 380, 410, 190, 300, 380, 320, 290, 340, 220, 290],
                    //    fill: 'start'

                    //},
                    //{
                    //    label: 'Pending Requests',
                    //    data: [120, 190, 240, 240, 80, 120, 190, 190, 120, 140, 110, 180],
                    //    fill: 'start'

                    //}                                                                               
                ]

            },
            labels   : ['JAN', 'FEB', 'MAR', 'APR', 'MAY', 'JUN', 'JUL', 'AUG', 'SEP', 'OCT', 'NOV', 'DEC'],
            colors   : [
                {
                    borderColor              : '#42a5f5',
                    backgroundColor          : '#42a5f5',
                    pointBackgroundColor     : '#42a5f5',
                    pointHoverBackgroundColor: '#42a5f5',
                    pointBorderColor         : '#ffffff',
                    pointHoverBorderColor    : '#ffffff'
                },
                {
                    borderColor: '#8334eb',
                    backgroundColor: '#8334eb',
                    pointBackgroundColor: '#8334eb',
                    pointHoverBackgroundColor: '#8334eb',
                    pointBorderColor: '#ffffff',
                    pointHoverBorderColor: '#ffffff'
                },
                {
                    borderColor: '#ff9900', //'rgba(30, 136, 229, 0.87)'
                    backgroundColor: '#ff9900',
                    pointBackgroundColor: '#ff9900',
                    pointHoverBackgroundColor: '#ff9900',
                    pointBorderColor: '#ffffff',
                    pointHoverBorderColor: '#ffffff'
                }
            ],
            options  : {
                spanGaps           : false,
                legend             : {
                    display: false
                },
                maintainAspectRatio: false,
                tooltips: {
                    position: 'nearest',
                    mode: 'index',
                    intersect: false
                },
                layout             : {
                    padding: {
                        top  : 32,
                        left : 32,
                        right: 32
                    }
                },
                elements           : {
                    point: {
                        radius          : 4,
                        borderWidth     : 2,
                        hoverRadius     : 4,
                        hoverBorderWidth: 2
                    },
                    line : {
                        tension: 0
                    }
                },
                scales             : {
                    xAxes: [
                        {
                            gridLines: {
                                display       : false,
                                //drawBorder    : false,
                                //tickMarkLength: 18
                            },
                            ticks    : {
                                fontColor: '#ffffff'
                            }
                        }
                    ],
                    yAxes: [
                        {
                            display: false,
                            ticks  : {
                                min     : 0,
                                max     : 800,
                                stepSize: 50
                            }
                        }
                    ]
                },
                plugins            : {
                    filler      : {
                        propagate: false
                    },
                    xLabelsOnTop: {
                        active: true
                    }

                }
            }
        },
        widget2: {
            conversion: {
                value   : 2663,
                ofTarget: 13
            },
            chartType : 'bar',
            datasets  : [
                {
                    label: 'Requests',
                    data : [221, 428, 492, 471, 413, 344, 294]
                }
            ],
            labels    : ['Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday', 'Sunday'],
            colors    : [
                {
                    borderColor    : '#42a5f5',
                    backgroundColor: '#42a5f5'
                }
            ],
            options   : {
                spanGaps           : false,
                legend             : {
                    display: false
                },
                maintainAspectRatio: false,
                layout             : {
                    padding: {
                        top   : 24,
                        left  : 16,
                        right : 16,
                        bottom: 16
                    }
                },
                scales             : {
                    xAxes: [
                        {
                            display: false
                        }
                    ],
                    yAxes: [
                        {
                            display: false,
                            ticks  : {
                                min: 100,
                                max: 500
                            }
                        }
                    ]
                }
            }
        },
        widget3: {
            conversion: {
                value: 2064,
                ofTarget: 9
            },
            chartType: 'bar',
            datasets: [
                {
                    label: 'Requests',
                    data: [121, 409, 302, 371, 313, 344, 204]
                }
            ],
            labels: ['Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday', 'Sunday'],
            colors: [
                {
                    borderColor: '#5c84f1',
                    backgroundColor: '#5c84f1'
                }
            ],
            options: {
                spanGaps: false,
                legend: {
                    display: false
                },
                maintainAspectRatio: false,
                layout: {
                    padding: {
                        top: 24,
                        left: 16,
                        right: 16,
                        bottom: 16
                    }
                },
                scales: {
                    xAxes: [
                        {
                            display: false
                        }
                    ],
                    yAxes: [
                        {
                            display: false,
                            ticks: {
                                min: 100,
                                max: 500
                            }
                        }
                    ]
                }
            }
        },
        //widget3: {
        //    impressions: {
        //        value   : '87k',
        //        ofTarget: 12
        //    },
        //    chartType  : 'line',
        //    datasets   : [
        //        {
        //            label: 'Impression',
        //            data : [67000, 54000, 82000, 57000, 72000, 57000, 87000, 72000, 89000, 98700, 112000, 136000, 110000, 149000, 98000],
        //            fill : false
        //        }
        //    ],
        //    labels     : ['Jan 1', 'Jan 2', 'Jan 3', 'Jan 4', 'Jan 5', 'Jan 6', 'Jan 7', 'Jan 8', 'Jan 9', 'Jan 10', 'Jan 11', 'Jan 12', 'Jan 13', 'Jan 14', 'Jan 15'],
        //    colors     : [
        //        {
        //            borderColor: '#5c84f1'
        //        }
        //    ],
        //    options    : {
        //        spanGaps           : false,
        //        legend             : {
        //            display: false
        //        },
        //        maintainAspectRatio: false,
        //        elements           : {
        //            point: {
        //                radius          : 2,
        //                borderWidth     : 1,
        //                hoverRadius     : 2,
        //                hoverBorderWidth: 1
        //            },
        //            line : {
        //                tension: 0
        //            }
        //        },
        //        layout             : {
        //            padding: {
        //                top   : 24,
        //                left  : 16,
        //                right : 16,
        //                bottom: 16
        //            }
        //        },
        //        scales             : {
        //            xAxes: [
        //                {
        //                    display: false
        //                }
        //            ],
        //            yAxes: [
        //                {
        //                    display: false,
        //                    ticks  : {
        //                        // min: 100,
        //                        // max: 500
        //                    }
        //                }
        //            ]
        //        }
        //    }
        //},
        widget4: {
            visits   : {
                value   : 4727,
                ofTarget: -9
            },
            chartType: 'bar',
            datasets : [
                {
                    label: 'Requests',
                    data: [342, 837, 794, 842, 726, 688, 498]
                }
            ],
            labels   : ['Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday', 'Sunday'],
            colors   : [
                {
                    borderColor    : '#f44336',
                    backgroundColor: '#f44336'
                }
            ],
            options  : {
                spanGaps           : false,
                legend             : {
                    display: false
                },
                maintainAspectRatio: false,
                layout             : {
                    padding: {
                        top   : 24,
                        left  : 16,
                        right : 16,
                        bottom: 16
                    }
                },
                scales             : {
                    xAxes: [
                        {
                            display: false
                        }
                    ],
                    yAxes: [
                        {
                            display: false,
                            ticks  : {
                                min: 150,
                                max: 900
                            }
                        }
                    ]
                }
            }
        },
        //widget2: {
        //    title: 'Resolved Complaints',
        //    data: {
        //        label: 'RESOLVED',
        //        count: 492,
        //        extra: {
        //            label: '6 mins ago',

        //        }
        //    },
        //    detail: 'You can show some detailed information about this widget in here.'
        //},
        //widget3: {
        //    title: 'Pending Complaints',
        //    data: {
        //        label: 'PENDING',
        //        count: 342,
        //        extra: {
        //            label: '6 mins ago'
        //        }
        //    },
        //    detail: 'You can show some detailed information about this widget in here.'
        //},
        //widget4: {
        //    title: 'Complaints Weekly',
        //    data: {
        //        label: 'COMPLAINTS THIS WEEK',
        //        count: 755,
        //        extra: {
        //            label: '6 mins ago',
        //        }
        //    },
        //    detail: 'You can show some detailed information about this widget in here.'
        //},
        widget5: {
            chartType: 'line',
            datasets : {
                'yesterday': [
                    {
                        label: 'Requests',
                        data : [190, 300, 340, 220, 290, 390, 250, 380, 410, 380, 320, 290],
                        fill : 'start'

                    },
                    {
                        label: 'Citizens',
                        data : [2200, 2900, 3900, 2500, 3800, 3200, 2900, 1900, 3000, 3400, 4100, 3800],
                        fill : 'start'
                    }
                ],
                'today'    : [
                    {
                        label: 'Requests',
                        data : [410, 380, 320, 290, 190, 390, 250, 380, 300, 340, 220, 290],
                        fill : 'start'
                    },
                    {
                        label: 'Citizens',
                        data : [3000, 3400, 4100, 3800, 2200, 3200, 2900, 1900, 2900, 3900, 2500, 3800],
                        fill : 'start'

                    }
                ]
            },
            labels   : ['12am', '2am', '4am', '6am', '8am', '10am', '12pm', '2pm', '4pm', '6pm', '8pm', '10pm'],
            colors   : [
                {
                    borderColor              : '#3949ab',
                    backgroundColor          : '#3949ab',
                    pointBackgroundColor     : '#3949ab',
                    pointHoverBackgroundColor: '#3949ab',
                    pointBorderColor         : '#ffffff',
                    pointHoverBorderColor    : '#ffffff'
                },
                {
                    borderColor              : 'rgba(30, 136, 229, 0.87)',
                    backgroundColor          : 'rgba(30, 136, 229, 0.87)',
                    pointBackgroundColor     : 'rgba(30, 136, 229, 0.87)',
                    pointHoverBackgroundColor: 'rgba(30, 136, 229, 0.87)',
                    pointBorderColor         : '#ffffff',
                    pointHoverBorderColor    : '#ffffff'
                }
            ],
            options  : {
                spanGaps           : false,
                legend             : {
                    display: false
                },
                maintainAspectRatio: false,
                tooltips           : {
                    position : 'nearest',
                    mode     : 'index',
                    intersect: false
                },
                layout             : {
                    padding: {
                        left : 24,
                        right: 32
                    }
                },
                elements           : {
                    point: {
                        radius          : 4,
                        borderWidth     : 2,
                        hoverRadius     : 4,
                        hoverBorderWidth: 2
                    }
                },
                scales             : {
                    xAxes: [
                        {
                            gridLines: {
                                display: false
                            },
                            ticks    : {
                                fontColor: 'rgba(0,0,0,0.54)'
                            }
                        }
                    ],
                    yAxes: [
                        {
                            gridLines: {
                                tickMarkLength: 16
                            },
                            ticks    : {
                                stepSize: 1000
                            }
                        }
                    ]
                },
                plugins            : {
                    filler: {
                        propagate: false
                    }
                }
            }
        },
        widget6: {
            markers: [
                {
                    lat  : 52,
                    lng  : -73,
                    label: '120'
                },
                {
                    lat  : 37,
                    lng  : -104,
                    label: '498'
                },
                {
                    lat  : 21,
                    lng  : -7,
                    label: '443'
                },
                {
                    lat  : 55,
                    lng  : 75,
                    label: '332'
                },
                {
                    lat  : 51,
                    lng  : 7,
                    label: '50'
                },
                {
                    lat  : 31,
                    lng  : 12,
                    label: '221'
                },
                {
                    lat  : 45,
                    lng  : 44,
                    label: '455'
                },
                {
                    lat  : -26,
                    lng  : 134,
                    label: '231'
                },
                {
                    lat  : -9,
                    lng  : -60,
                    label: '67'
                },
                {
                    lat  : 33,
                    lng  : 104,
                    label: '665'
                }
            ],
            styles : [
                {
                    'featureType': 'administrative',
                    'elementType': 'labels.text.fill',
                    'stylers'    : [
                        {
                            'color': '#444444'
                        }
                    ]
                },
                {
                    'featureType': 'landscape',
                    'elementType': 'all',
                    'stylers'    : [
                        {
                            'color': '#f2f2f2'
                        }
                    ]
                },
                {
                    'featureType': 'poi',
                    'elementType': 'all',
                    'stylers'    : [
                        {
                            'visibility': 'off'
                        }
                    ]
                },
                {
                    'featureType': 'road',
                    'elementType': 'all',
                    'stylers'    : [
                        {
                            'saturation': -100
                        },
                        {
                            'lightness': 45
                        }
                    ]
                },
                {
                    'featureType': 'road.highway',
                    'elementType': 'all',
                    'stylers'    : [
                        {
                            'visibility': 'simplified'
                        }
                    ]
                },
                {
                    'featureType': 'road.arterial',
                    'elementType': 'labels.icon',
                    'stylers'    : [
                        {
                            'visibility': 'off'
                        }
                    ]
                },
                {
                    'featureType': 'transit',
                    'elementType': 'all',
                    'stylers'    : [
                        {
                            'visibility': 'off'
                        }
                    ]
                },
                {
                    'featureType': 'water',
                    'elementType': 'all',
                    'stylers'    : [
                        {
                            'color': '#039be5'
                        },
                        {
                            'visibility': 'on'
                        }
                    ]
                }
            ]
        },
        widget7: {
            scheme : {
                domain: ['#4867d2', '#5c84f1', '#89a9f4']
            },
            devices: [
                {
                    name  : 'Desktop',
                    value : 92.8,
                    change: -0.6
                },
                {
                    name  : 'Mobile',
                    value : 6.1,
                    change: 0.7
                },
                {
                    name  : 'Tablet',
                    value : 1.1,
                    change: 0.1
                }
            ]
        },
        widget8: {
            scheme : {
                domain: ['#5c84f1']
            },
            today  : '12,540',
            change : {
                value     : 321,
                percentage: 2.05
            },
            data   : [
                {
                    name: 'Requests',
                    series: [
                        {
                            name : 'Jan 1',
                            value: 540
                        },
                        {
                            name : 'Jan 2',
                            value: 539
                        },
                        {
                            name : 'Jan 3',
                            value: 538
                        },
                        {
                            name : 'Jan 4',
                            value: 539
                        },
                        {
                            name : 'Jan 5',
                            value: 540
                        },
                        {
                            name : 'Jan 6',
                            value: 539
                        },
                        {
                            name : 'Jan 7',
                            value: 540
                        }
                    ]
                }
            ],
            dataMin: 538,
            dataMax: 541
        },
        widget9: {
            rows: [
                {
                    title     : 'Holiday Travel',
                    clicks    : 3621,
                    conversion: 90
                },
                {
                    title     : 'Get Away Deals',
                    clicks    : 703,
                    conversion: 7
                },
                {
                    title     : 'Airfare',
                    clicks    : 532,
                    conversion: 0
                },
                {
                    title     : 'Vacation',
                    clicks    : 201,
                    conversion: 8
                },
                {
                    title     : 'Hotels',
                    clicks    : 94,
                    conversion: 4
                }
            ]
        },
         widget10: {
            conversion: {
                value: 409,
                ofTarget: 13
            },
            chartType: 'bar',
            datasets: [
                {
                    label: 'Requests',
                    data: [221, 101, 20]
                }
            ],
            labels: ['Completed', 'In Progress', 'Open'],
            colors: [
                {
                    borderColor: '#5c84f1',
                    backgroundColor: '#5c84f1'
                }
            ],
            options: {
                spanGaps: false,
                legend: {
                    display: false
                },
                maintainAspectRatio: false,
                layout: {
                    padding: {
                        top: 24,
                        left: 16,
                        right: 16,
                        bottom: 16
                    }
                },
                scales: {
                    xAxes: [
                        {
                            display: false
                        }
                    ],
                    yAxes: [
                        {
                            display: false,
                            ticks: {
                                min: 0,
                                max: 500
                            }
                        }
                    ]
                }
            }
        },
        widget11: {
            conversion: {
                value: 409,
                ofTarget: 13
            },
            chartType: 'bar',
            datasets: [
                {
                    label: 'Requests',
                    data: [2663, 1564, 500]
                }
            ],
            labels: ['Completed', 'In Progress', 'Open'],
            colors: [
                {
                    borderColor: '#5c84f1',
                    backgroundColor: '#5c84f1'
                }
            ],
            options: {
                spanGaps: false,
                legend: {
                    display: false
                },
                maintainAspectRatio: false,
                layout: {
                    padding: {
                        top: 24,
                        left: 16,
                        right: 16,
                        bottom: 16
                    }
                },
                scales: {
                    xAxes: [
                        {
                            display: false
                        }
                    ],
                    yAxes: [
                        {
                            display: false,
                            ticks: {
                                min: 0,
                                max: 3000
                            }
                        }
                    ]
                }
            }
        },
        widget12: {
            conversion: {
                value: 409,
                ofTarget: 13
            },
            chartType: 'bar',
            datasets: [
                {
                    label: 'Requests',
                    data: [4500, 4020, 2000]
                }
            ],
            labels: ['Completed', 'In Progress', 'Open'],
            colors: [
                {
                    borderColor: '#5c84f1',
                    backgroundColor: '#5c84f1'
                }
            ],
            options: {
                spanGaps: false,
                legend: {
                    display: false
                },
                maintainAspectRatio: false,
                layout: {
                    padding: {
                        top: 24,
                        left: 16,
                        right: 16,
                        bottom: 16
                    }
                },
                scales: {
                    xAxes: [
                        {
                            display: false
                        }
                    ],
                    yAxes: [
                        {
                            display: false,
                            ticks: {
                                min: 50,
                                max: 5000
                            }
                        }
                    ]
                }
            }
        },
    };
}
