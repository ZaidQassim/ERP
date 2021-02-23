<template>
    <div>
        <v-row no-gutters>
            <v-col>
                <v-alert
                    class="text-center ml-3"
                    border="left"
                    elevation="3"
                    colored-border
                    color="deep-purple accent-4"
                >
                    <div class="d-flex justify-space-between">
                        <div style="font-size: 10px">
                            <span class="grey--text">
                                عدد مستخدمين النظام
                            </span>

                            <v-list-item-title class="font-weight-bold">
                                1
                            </v-list-item-title>
                        </div>
                        <v-divider vertical></v-divider>
                        <div>
                            <v-icon color="deep-purple accent-4" large
                                >mdi-hospital-building</v-icon
                            >
                        </div>
                    </div>
                </v-alert>
            </v-col>

            <v-col>
                <v-alert
                    class="text-center ml-3"
                    border="left"
                    elevation="3"
                    colored-border
                    color="amber"
                >
                    <div class="d-flex justify-space-between">
                        <div style="font-size: 10px" class="pr-4">
                            <span class="grey--text">
                                اجمالي الموظــفين
                            </span>
                            <v-list-item-title class="font-weight-bold">
                                5
                            </v-list-item-title>
                        </div>
                        <v-divider vertical></v-divider>
                        <div>
                            <v-icon color="amber" large
                                >mdi-file-document-multiple</v-icon
                            >
                        </div>
                    </div>
                </v-alert>
            </v-col>

            <v-col>
                <v-alert
                    class="text-center ml-3"
                    border="left"
                    elevation="3"
                    colored-border
                    color="green"
                >
                    <div class="d-flex justify-space-between">
                        <div style="font-size: 10px" class="pr-6">
                            <span class="grey--text">
                                اٍجمالي الذمة في الشركه
                            </span>

                            <v-list-item-title class="font-weight-bold">
                                3
                            </v-list-item-title>
                        </div>
                        <v-divider vertical></v-divider>
                        <div>
                            <v-icon color="green" large>mdi-file-send</v-icon>
                        </div>
                    </div>
                </v-alert>
            </v-col>

            <v-col>
                <v-alert
                    class="text-center ml-3"
                    border="left"
                    elevation="3"
                    colored-border
                    color="red"
                >
                    <div class="d-flex justify-space-between">
                        <div style="font-size: 10px" class="pr-4">
                            <span class="grey--text">
                                اِجمالي الذمة الخارجيه
                            </span>

                            <v-list-item-title class="font-weight-bold">
                                2
                            </v-list-item-title>
                        </div>
                        <v-divider vertical></v-divider>
                        <div>
                            <v-icon color="red" large>mdi-file-undo</v-icon>
                        </div>
                    </div>
                </v-alert>
            </v-col>

            <v-col>
                <v-alert
                    class="text-center ml-3"
                    border="left"
                    elevation="3"
                    colored-border
                    color="lime darken-2"
                >
                    <div class="d-flex justify-space-between">
                        <div style="font-size: 10px" class="pr-2">
                            <span class="grey--text">
                                الذمة يجب ان تسـلم
                            </span>
                            <v-list-item-title class="font-weight-bold">
                                10
                            </v-list-item-title>
                        </div>
                        <v-divider vertical></v-divider>
                        <div>
                            <v-icon color="lime darken-2" large
                                >mdi-signature-freehand</v-icon
                            >
                        </div>
                    </div>
                </v-alert>
            </v-col>
        </v-row>

        <v-row class="mt-10">
            <v-col cols="12" md="7" class="mt-6">
                <apexchart type="line" height="350" :options="chartOptions2" :series="series2"></apexchart>

            </v-col>

            <v-col cols="12" md="5">
                <apexchart
                    class="float-left"
                    type="pie"
                    width="380"
                    :options="chartOptions"
                    :series="series"
                ></apexchart>
            </v-col>
        </v-row>

        <div class="mt-6">
            <!-- <v-sheet color="white" elevation="1" rounded>
                <v-container fluid>
                    <v-sheet
                        color="primary"
                        height="50"
                        rounded
                        class="mt-n8"
                        shaped
                    >
                        <div class="py-3 pr-2 white--text" dack>
                            أحصائيات تواقيع وثائقي
                        </div>
                    </v-sheet>
                    <v-data-table
                        :headers="headersDocComments"
                        :items="docComments"
                        :loading="loadingdocComments"
                        loading-text="جاري تحميل أخر أجراءات وأحداث النظام "
                        hide-default-footer
                        no-data-text=" لا تـــــــوجــد بيانـــات حتى ألان"
                        class="mt-4"
                    >
                        <template v-slot:[`item.created`]="{ item }">
                            <span dir="ltr">
                                {{ item.created | formatDateAgo }}
                            </span>
                        </template>
                    </v-data-table>
                </v-container>
            </v-sheet> -->
        </div>
    </div>
</template>
<script>
import VueApexCharts from "vue-apexcharts";
import axios from "../axios";

export default {
    components: {
        apexchart: VueApexCharts
    },

    data() {
        return {
            infoDashbord: {},

            series: [1, 1, 1],
            chartOptions: {
                chart: {
                    width: 340,
                    type: "pie"
                },
                labels: [
                    " الذمة في الشركه ",
                    " الذمة الخاجيه ",
                    " الذمة يجب ان تسلم "
                ],
                responsive: [
                    {
                        breakpoint: 440,
                        options: {
                            chart: {
                                width: 200
                            },
                            legend: {
                                position: "bottom"
                            }
                        }
                    }
                ]
            },

            series2: [
                {
                    name: "TEAM A",
                    type: "column",
                    data: [23, 11, 22, 27, 13, 22, 37, 21, 44, 22, 30]
                },
                {
                    name: "TEAM B",
                    type: "area",
                    data: [44, 55, 41, 67, 22, 43, 21, 41, 56, 27, 43]
                },
                {
                    name: "TEAM C",
                    type: "line",
                    data: [30, 25, 36, 30, 45, 35, 64, 52, 59, 36, 39]
                }
            ],
            chartOptions2: {
                chart: {
                    height: 350,
                    type: "line",
                    stacked: false
                },
                stroke: {
                    width: [0, 2, 5],
                    curve: "smooth"
                },
                plotOptions: {
                    bar: {
                        columnWidth: "50%"
                    }
                },

                fill: {
                    opacity: [0.85, 0.25, 1],
                    gradient: {
                        inverseColors: false,
                        shade: "light",
                        type: "vertical",
                        opacityFrom: 0.85,
                        opacityTo: 0.55,
                        stops: [0, 100, 100, 100]
                    }
                },
                labels: [
                    "01/01/2003",
                    "02/01/2003",
                    "03/01/2003",
                    "04/01/2003",
                    "05/01/2003",
                    "06/01/2003",
                    "07/01/2003",
                    "08/01/2003",
                    "09/01/2003",
                    "10/01/2003",
                    "11/01/2003"
                ],
                markers: {
                    size: 0
                },
                xaxis: {
                    type: "datetime"
                },
                yaxis: {
                    title: {
                        text: "Points"
                    },
                    min: 0
                },
                tooltip: {
                    shared: true,
                    intersect: false,
                    y: {
                        formatter: function(y) {
                            if (typeof y !== "undefined") {
                                return y.toFixed(0) + " points";
                            }
                            return y;
                        }
                    }
                }
            },

            loadingDocShared: true,
            docShared: [],

            headersDocComments: [
                {
                    text: "#",
                    align: "start",
                    value: "id"
                },
                { text: "أسم الوثيقة", value: "title" },
                {
                    text: "المستخدم القام بالتوقيع",
                    value: "knownAs"
                },
                {
                    text: "تاريخ التوقيع ",
                    value: "created"
                }
            ],
            docComments: [],
            loadingdocComments: true
        };
    },

    created() {
        this.getCountSharedDoc();
        this.getInfoDashbordByUser();
        this.getDocByUserComments();
    },

    methods: {
        getInfoDashbordByUser() {
            axios
                .get("dashboards/getInfoDashbordByUser", {
                    headers: {
                        Authorization: `Bearer ${localStorage.getItem("token")}`
                    }
                })
                .then(res => {
                    this.infoDashbord = res.data.result;
                    this.series = [
                        res.data.result.myDocuments,
                        res.data.result.mySender,
                        res.data.result.myReciepent
                    ];
                })
                .catch(err => {});
        },

        getCountSharedDoc() {
            axios
                .get("dashboards/getCountSharedDocByUser", {
                    headers: {
                        Authorization: `Bearer ${localStorage.getItem("token")}`
                    }
                })
                .then(res => {
                    this.docShared = res.data;
                    this.loadingDocShared = false;
                })
                .catch(err => {});
        },

        getDocByUserComments() {
            axios
                .get("dashboards/getDocByUserComments", {
                    headers: {
                        Authorization: `Bearer ${localStorage.getItem("token")}`
                    }
                })
                .then(res => {
                    this.docComments = res.data.result.info.documentComments;
                })
                .catch(err => {})
                .finally(() => {
                    this.loadingdocComments = false;
                });
        }
    }
};
</script>

<style>
.v-card-scroll {
    display: flex !important;
    flex-direction: column;
}

.v-card__text {
    flex-grow: 1;
    overflow: auto;
}
</style>
