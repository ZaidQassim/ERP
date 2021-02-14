<template>
    <div>
        <v-row>
            <v-col cols="12" md="3">
                <v-card flat class="grey lighten-4 ma-0 pa-0">
                    <v-list-item three-line>
                        <v-list-item-content>
                            <div class="grey--text" style="font-size: 13px">
                                المؤسسات المشاركة
                            </div>
                            <v-list-item-title
                                class="headline primary--text font-weight-bold"
                            >
                                {{ infoDashbord.countOrginaztions }}
                            </v-list-item-title>
                        </v-list-item-content>

                        <v-list-item-avatar
                            tile
                            size="70"
                            color="blue"
                            class="mt-n4 rounded"
                        >
                            <v-icon large>mdi-hospital-building</v-icon>
                        </v-list-item-avatar>
                    </v-list-item>
                    <v-divider class="mx-2"></v-divider>
                    <v-card-actions class="caption grey--text">
                        <v-icon small>mdi-calendar</v-icon>
                        {{ infoDashbord.created | formatDateAgo }}
                    </v-card-actions>
                </v-card>
            </v-col>

            <v-col cols="12" md="3">
                <v-card flat class="grey lighten-4 ma-0 pa-0">
                    <v-list-item three-line>
                        <v-list-item-content>
                            <div class="grey--text" style="font-size: 13px">
                                عدد مستخدمين النظام
                            </div>
                            <v-list-item-title
                                class="headline primary--text font-weight-bold"
                            >
                                {{ infoDashbord.countuser }}
                            </v-list-item-title>
                        </v-list-item-content>

                        <v-list-item-avatar
                            tile
                            size="70"
                            color="red"
                            class="mt-n4 rounded"
                        >
                            <v-icon large>mdi-account-group</v-icon>
                        </v-list-item-avatar>
                    </v-list-item>
                    <v-divider class="mx-2"></v-divider>
                    <v-card-actions class="caption grey--text">
                        <v-icon small>mdi-calendar</v-icon>
                        {{ infoDashbord.created | formatDateAgo }}
                    </v-card-actions>
                </v-card>
            </v-col>

            <v-col cols="12" md="3">
                <v-card flat class="grey lighten-4 ma-0 pa-0">
                    <v-list-item three-line>
                        <v-list-item-content>
                            <div class="grey--text" style="font-size: 13px">
                                إجمالي الوثائق المرفوعة
                            </div>
                            <v-list-item-title
                                class="headline primary--text font-weight-bold"
                            >
                                {{ infoDashbord.counDocuments }}
                            </v-list-item-title>
                        </v-list-item-content>

                        <v-list-item-avatar
                            tile
                            size="70"
                            color="green"
                            class="mt-n4 rounded"
                        >
                            <v-icon large>mdi-file-document-multiple</v-icon>
                        </v-list-item-avatar>
                    </v-list-item>
                    <v-divider class="mx-2"></v-divider>
                    <v-card-actions class="caption grey--text">
                        <v-icon small>mdi-calendar</v-icon>
                        {{ infoDashbord.created | formatDateAgo }}
                    </v-card-actions>
                </v-card>
            </v-col>

            <v-col cols="12" md="3">
                <v-card flat class="grey lighten-4 ma-0 pa-0">
                    <v-list-item three-line>
                        <v-list-item-content>
                            <div class="grey--text" style="font-size: 13px">
                                الوثائق الموقعه
                            </div>
                            <v-list-item-title
                                class="headline primary--text font-weight-bold"
                            >
                                {{ infoDashbord.counDocumentomments }}
                            </v-list-item-title>
                        </v-list-item-content>

                        <v-list-item-avatar
                            tile
                            size="70"
                            color="amber"
                            class="mt-n4 rounded"
                        >
                            <v-icon large>mdi-signature-freehand</v-icon>
                        </v-list-item-avatar>
                    </v-list-item>
                    <v-divider class="mx-2"></v-divider>
                    <v-card-actions class="caption grey--text">
                        <v-icon small>mdi-calendar</v-icon>
                        {{ infoDashbord.created | formatDateAgo }}
                    </v-card-actions>
                </v-card>
            </v-col>
        </v-row>

        <v-row>
            <v-col cols="12" md="7" class="mt-6">
                <v-sheet color="white" elevation="1" rounded>
                    <v-container fluid>
                        <v-sheet
                            color="light-blue"
                            height="50"
                            rounded
                            class="mt-n8"
                            shaped
                        >
                            <div class="py-3 pr-2 white--text" dack>
                                أحصائيات مشاركة الوثائق
                            </div>
                        </v-sheet>
                        <v-data-table
                            height="200"
                            :headers="headersDocShared"
                            :items="docShared"
                            hide-default-footer
                            :loading="loadingDocShared"
                            loading-text="جاري تحميل جميع أحصائيات مشاركة الوثائق"
                            class="mt-4"
                        >
                            <template v-slot:[`item.countShare`]="{ item }">
                                <v-chip color="grey" label small>
                                    {{ item.countShare }}
                                </v-chip>
                            </template>
                        </v-data-table>
                    </v-container>
                </v-sheet>

                <v-card> </v-card>
            </v-col>

            <v-col cols="12" md="5">
                <apexchart
                    class="float-left"
                    type="donut"
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
                            أخر أجراءات وأحداث النظام
                        </div>
                    </v-sheet>
                    <v-data-table
                        :headers="headersUsersLogs"
                        :items="userslogs"
                        :loading="loadingUsersLog"
                        loading-text="جاري تحميل أخر أجراءات وأحداث النظام "
                        page.sync="page"
                        :items-per-page="itemsPerPage"
                        hide-default-footer
                        class="mt-4">
                        <template v-slot:[`item.date`]="{ item }">
                            <span dir="ltr">
                                {{ item.date | formatDateAgo }}
                            </span>
                        </template>
                    </v-data-table>
                    <v-divider></v-divider>
                    <v-pagination
                        v-model="page"  
                        :length="pageCount"
                        @input="next"
                        circle :total-visible="7"
                        class="py-6">
                    </v-pagination>
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
                    type: "donut"
                },
                labels: [
                    " الوثائق المرفوعه ",
                    " الوثائق الموقعه ",
                    " الوثائق المشاركه "
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
            headersDocShared: [
                {
                    text: "# ",
                    align: "start",
                    sortable: false,
                    value: "id"
                },
                { text: "عنوان الوثيقه المشاركه ", value: "nameDoc" },
                {
                    text: "عدد مرات المشاركه",
                    align: "center",
                    value: "countShare"
                }
            ],
            docShared: [],

            page: 1,
            pageCount: 0,
            itemsPerPage: 10,
            headersUsersLogs: [
                {
                    text: "رقم العمليه",
                    align: "start",
                    value: "id"
                },
                { text: "أسم المستخدم القائم بالحدث", value: "knownAs" },
                {
                    text: "نوع العمليه",
                    value: "logType"
                },
                {
                    text: "تاريخ العمليه ",
                    value: "date"
                }
            ],
            userslogs: [],
            loadingUsersLog: true
        };
    },

    created() {
        this.getCountSharedDoc();
        this.getInfoDashbord();
        this.getUsersLogs();
    },

    methods: {
        getCountSharedDoc() {
            axios
                .get("dashboards/getCountSharedDoc", {
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

        getInfoDashbord() {
            axios
                .get("dashboards/getInfoDashbord", {
                    headers: {
                        Authorization: `Bearer ${localStorage.getItem("token")}`
                    }
                })
                .then(res => {
                    this.infoDashbord = res.data.result;
                    this.series = [
                        res.data.result.counDocuments,
                        res.data.result.counDocumentomments,
                        res.data.result.counDocumentShared
                    ];
                })
                .catch(err => {});
        },

        getUsersLogs() {
            axios
                .get(
                    "users/GetUsersLogs?skip=0"+
                        "&take=" +
                        this.itemsPerPage,
                    {
                        headers: {
                            Authorization: `Bearer ${localStorage.getItem(
                                "token"
                            )}`
                        }   
                    }
                )
                .then(res => {
                    this.userslogs = res.data.result.info;
                    let pageOfCount = Math.ceil(res.data.result.count / 10);
                    this.pageCount = parseInt(pageOfCount);
                    this.loadingUsersLog = false;
                })
                .catch(err => {});
        },

        next(page) {
            let skip = page - 1;
            axios
                .get(
                    "users/GetUsersLogs?skip=" +
                        skip +
                        "&take=" +
                        this.itemsPerPage,{
                        headers: {
                            Authorization: `Bearer ${localStorage.getItem(
                                "token"
                            )}`
                        }
                    }
                )
                .then(res => {
                    this.userslogs = res.data.result.info;
                    this.loadingUsersLog = false;
                })
                .catch(err => {});
        }
    }
};
</script>
