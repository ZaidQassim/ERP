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
                                المؤسسات السؤول عنها
                            </span>

                            <v-list-item-title class="font-weight-bold">
                                {{ infoDashbord.myOrgs }}
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
                                وثائقي
                            </span>
                            <v-list-item-title class="font-weight-bold">
                                {{ infoDashbord.myDocuments }}
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
                                الصادره
                            </span>

                            <v-list-item-title class="font-weight-bold">
                                {{ infoDashbord.mySender }}
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
                                الوثائق الوارده
                            </span>

                            <v-list-item-title class="font-weight-bold">
                                {{ infoDashbord.myReciepent }}
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
                                عدد تواقيعي
                            </span>
                            <v-list-item-title class="font-weight-bold">
                                {{ infoDashbord.myDocumentomments }}
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

        <v-row>
            <v-col cols="12" md="7" class="mt-6">
                <v-card
                    :loading="loadingDocShared"
                    color="white"
                    elevation="1"
                    rounded
                    class="v-card-scroll"
                    height="35vh"
                >
                    <v-container fluid>
                        <v-sheet
                            color="light-blue"
                            height="50"
                            rounded
                            class="mt-n8"
                            shaped
                        >
                            <div class="py-3 pr-2 white--text" dack>
                                أحصائيات مشاركات وثائقي
                            </div>
                        </v-sheet>
                    </v-container>

                    <v-card-text>
                        <v-alert dense class="mt-n4">
                            <div
                                v-if="docShared != ''"
                                class="font-weight-bold"
                                style="font-size: 10px"
                            >
                                <span>
                                    #
                                </span>
                                <span class="pr-8">
                                    أسم الوثيقه المشاركه
                                </span>
                                <span class="float-left">
                                    عدد مرات المشاركة
                                </span>
                            </div>
                        </v-alert>

                        <v-alert
                            v-for="(item, i) in docShared"
                            :key="i"
                            dense
                            class="my-2"
                            elevation="2"
                        >
                            <div class="grey--text" style="font-size: 10px">
                                <span>
                                    #
                                </span>
                                <span class="pr-8">
                                    {{ item.nameDoc }}
                                </span>
                                <span class="float-left">
                                    <v-chip label small>
                                        {{ item.countShare }}
                                    </v-chip>
                                </span>
                            </div>
                        </v-alert>
                        <v-alert
                            v-if="docShared == ''"
                            outlined
                            type="warning"
                            dense
                            icon="mdi-cloud-alert"
                            border="left"
                            width="300"
                            class="text-center d-flex align-center ma-auto mt-10"
                        >
                            <span>
                                لا توجد بيانات
                            </span>
                        </v-alert>
                    </v-card-text>
                </v-card>

                <v-card> </v-card>
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
            <v-sheet color="white" elevation="1" rounded>
                <v-container fluid>
                    <v-sheet
                        color="primary"
                        height="50"
                        rounded
                        class="mt-n8"
                        shaped>
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
                        class="mt-4">
                        <template v-slot:[`item.created`]="{ item }">
                            <span dir="ltr">
                                {{ item.created | formatDateAgo }}
                            </span>
                        </template>
                    </v-data-table>
                </v-container>
            </v-sheet>
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
                    " الوثائق المرفوعه ",
                    " الوثائق الصادره ",
                    " الوثائق الوارده "
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
