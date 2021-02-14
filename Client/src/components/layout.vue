<template>
    <div>
        <!-- the app bar  -->
        <v-app-bar
            app
            absolute
            v-if="!$route.meta.hideNavigation"
            flat
            dense
            color="#fff"
            height="56"
            class="elevation-0">

            <!-- icon toggle -->
            <v-app-bar-nav-icon
                @click.stop="mini = !mini"
                large
                class="grey lighten-4">
                <v-icon>mdi mdi-menu</v-icon>
            </v-app-bar-nav-icon>
            <!-- ./ icon toggle -->

            <v-toolbar-title>{{ $route.name }}</v-toolbar-title>

            <v-spacer></v-spacer>

            <v-btn to="/massages" icon class="ml-2" color="indigo">
                <v-icon>mdi-chat-processing</v-icon>
            </v-btn>

            <!-- button notification -->
            <v-menu offset-y :nudge-width="200">
                <template v-slot:activator="{ on, attrs }">
                    <span v-bind="attrs" v-on="on" class="ml-5">
                        <v-badge :content="countNoti"
                            :value="countNoti"
                            color="primary"
                            overlap>
                            <v-icon >
                                mdi mdi-bell
                            </v-icon>
                        </v-badge>
                    </span>
                </template>

                <v-list class="text-center v-card-scroll" height="45vh" max-width="400">
                    <v-container>
                        <v-alert v-for="item in notification" :key="item.id"
                            @click="isReadTrue(item.id)"
                            color="cyan" class="mt-n1"
                            border="left" :class="item.isRead ? '' : 'blue lighten-5'"
                            elevation="2"
                            colored-border style="font-size: 10px">
                            <v-row no-gutters class="mt-n2 mb-n3">
                                <v-col cols="12" sm="12">
                                    <v-img class="float-right"
                                        src="../assets/img/user.svg"
                                        alt="zaid"
                                        width="20">
                                    </v-img>
                                    <span class="float-right mr-2 mt-1"> {{ item.userSender }} </span>
                                    <v-btn icon small class="float-left" color="red">
                                        <v-icon>mdi-close</v-icon>
                                    </v-btn>
                                </v-col>
                                <v-col cols="12" sm="12">
                                    <span class="float-right mr-6"> 
                                        <span>تم مشاركة وثيقة معك </span>
                                        <span class="indigo--text font-weight-bold">{{ item.documentInfo.title }}</span>
                                    </span>
                                </v-col>
                                <v-col cols="12" sm="12">
                                    <span class="float-right red--text mr-6"> {{ item.created | formatDateAgo}} </span>
                                </v-col>
                            </v-row>
                        </v-alert>
                    </v-container>
                </v-list>
            </v-menu>
            <!-- ./ button notification -->

            <!-- user profile  -->
            <user-profile></user-profile>
            <!-- ./ user profile  -->
        </v-app-bar>
        <!-- ./ the app bar  -->

        <!-- side bar app  -->
        <v-navigation-drawer
            v-model="drawer"
            :mini-variant.sync="mini"
            v-if="!$route.meta.hideNavigation"
            app
            permanent
            floating
            right
            :class="!mini ? '' : 'elevation-0'">
            <!-- logo app sideBar -->
            <div class="pa-2">
                <img
                    v-if="!mini"
                    class="img-size"
                    src="../assets/logo.jpg"
                    alt=""
                />
                <img
                    v-if="mini"
                    class="img-size-mini"
                    src="../assets/dms-logo.png"
                    alt=""
                />
            </div>
            <!-- ./ logo app sideBar -->

            <v-list shaped>
                <v-list-item
                    to="/dashboard"
                    color="primary"
                    class="mt-3"
                    v-if="getRole == 'Root'">
                    <v-icon>home</v-icon>
                    <v-list-item-title class="mr-5">
                        الاحصائيات
                    </v-list-item-title>
                </v-list-item>

                <v-list-item
                    to="/home"
                    color="primary"
                    class="mt-3"
                    v-if="getRole != 'Root'">
                    <v-icon>home</v-icon>
                    <v-list-item-title class="mr-5">
                        الاحصائيات
                    </v-list-item-title>
                </v-list-item>

                <v-list-item
                    color="primary"
                    class="my-2"
                    v-if="getRole == 'Root'"
                >
                    <v-icon>mdi-hospital-building</v-icon>
                    <v-list-item-title class="mr-5">
                        أدارة المؤسسات
                    </v-list-item-title>
                </v-list-item>

                <v-list-item
                    color="primary"
                    class="my-2"
                    v-if="getRole == 'Root'"
                >
                    <v-icon>mdi-account-multiple-plus</v-icon>
                    <v-list-item-title class="mr-5">
                        أدارة المستخدمين
                    </v-list-item-title>
                </v-list-item>

                <v-list-item to="/senderDocument" color="primary" class="my-2">
                    <v-icon>mdi-file-send</v-icon>

                    <v-list-item-title class="mr-5">
                        الوثائق الصادر
                    </v-list-item-title>
                </v-list-item>

                <v-list-item
                    to="/recipientDocument"
                    color="primary"
                    class="my-2"
                >
                    <v-icon>mdi-file-undo</v-icon>

                    <v-list-item-title class="mr-5">
                        الوثائق الواَرد
                    </v-list-item-title>
                </v-list-item>
            </v-list>

            <template v-slot:append>
                <div class="pa-2 d-flex justify-start">
                    <v-btn icon color="indigo" class="ml-2">
                        <v-icon>mdi-chat-processing</v-icon>
                    </v-btn>
                    <v-btn icon>
                        <v-icon>mdi-email-send</v-icon>
                    </v-btn>
                </div>
            </template>
        </v-navigation-drawer>
        <!-- ./ side bar app  -->
    </div>
</template>

<script>
import {
    HttpTransportType,
    HubConnectionBuilder,
    LogLevel
} from "@aspnet/signalr";
import axios from "../axios";
import UserProfile from "./UserProfile.vue";

export default {
    name: "layout",
    components: {
        UserProfile
    },

    data() {
        return {
            role: "",
            connection: "",
            drawer: true,
            mini: false,
            notification: [],
            dateNoti: null,
            countNoti: 0,
            token: localStorage.getItem("token"),
            alert: true,
        };
    },

    computed: {
        getRole() {
            return this.role;
        }
    },

    mounted() {
        // this.connection.start();
        this.connection.on("ShareCreated", send => {
            console.log(send);
            //   console.log(localStorage.getItem("userID"));

            if (send[0].reciepentId == localStorage.getItem("userID")) {
                this.getNoti();
            } else if (send[0].orginaztionId) {
                this.notification.push(
                    `تم مشاركة وثيقة معك في بتاريخ :  ${this.dateNoti} `
                );
            }
        });
    },

    // beforeCreate() {
    //     if (localStorage.getItem("token") == null) {
    //         this.$router.push("/login");
    //     }
    // },

    // inital Created Component
    created() {
        var today = new Date();
        var date =
            today.getFullYear() +
            "-" +
            (today.getMonth() + 1) +
            "-" +
            today.getDate();
        var time =
            today.getHours() +
            ":" +
            today.getMinutes() +
            ":" +
            today.getSeconds();
        this.dateNoti = date + " " + time;

        this.connection = new HubConnectionBuilder()
            .withUrl(process.env.VUE_APP_URL_SIGNALR, {
                skipNegotiation: true,
                transport: HttpTransportType.WebSockets
            })
            .configureLogging(LogLevel.Error)
            .build();
        // this.connection.Credentials = true;
        this.connection.start();
        // this.connection.on("ShareCreated", (send) => {
        //   if (send[0].reciepentId == localStorage.getItem("userID")) {
        //     console.log(send[0].reciepentId);

        //     this.notification.push(
        //       `تم ممشاركة وثيقة معك في الساعة ${this.dateNoti} `
        //     );
        //   }
        // });

        if (this.token != null) {
            this.getNoti();
        }

        this.$eventBus.$on("role", data => {
            this.role = data.role;
        });
        this.role = localStorage.getItem("role");
    },

    // the methods components
    methods: {
        isReadTrue(id) {
            //   console.log(id);
            axios
                .delete("documents/isReadDocument/" + id, {
                    headers: {
                        Authorization: `Bearer ${localStorage.getItem("token")}`
                    }
                })
                .then(() => {

                    if (this.$router.currentRoute.path != `/recipientDocument`) {
                        this.$router.push("/recipientDocument");
                        this.getNoti();
                    } else {
                        this.$store.dispatch("setSnackbar", {
                            text: `انت فعلاً في داخل هذا الصفحه `
                        });
                    }

                });
        },
        getNoti() {
            axios
                .get("documents/getAllReciepentDocs?skip=0&take=5", {
                    headers: {
                        Authorization: `Bearer ${localStorage.getItem("token")}`
                    }
                })
                .then(res => {
                    this.countNoti = 0;
                    this.notification = res.data.result.receved;
                    for (let i = 0; i < this.notification.length; i++) {
                        if (!this.notification[i].isRead) {
                            this.countNoti++;
                        }
                    }
                })
                .catch();
        }
    }
};
</script>

<style lang="scss" scoped>
.divider {
    display: table;
    font-size: 15px;
    text-align: center;
    width: 100%;
    margin: 4px auto;
    span {
        display: table-cell;
        position: relative;
        &:first-child {
            width: 50%;
            top: 13px;
            background-size: 100% 2px;
            background-position: 0 0, 0 100%;
            background-repeat: no-repeat;
            background-image: webkit-gradient(
                linear,
                0 0,
                0 100%,
                from(transparent),
                to(#2980b9)
            );
            background-image: -webkit-linear-gradient(
                180deg,
                transparent,
                #2980b9
            );
            background-image: -moz-linear-gradient(
                180deg,
                transparent,
                #2980b9
            );
            background-image: -o-linear-gradient(180deg, transparent, #2980b9);
            background-image: linear-gradient(90deg, transparent, #2980b9);
        }
        &:last-child {
            width: 50%;
            top: 13px;
            -moz-background-size: 100% 2px;
            background-size: 100% 2px;
            background-position: 0 0, 0 100%;
            background-repeat: no-repeat;
            background-image: webkit-gradient(
                linear,
                0 0,
                0 100%,
                from(#2980b9),
                to(transparent)
            );
            background-image: -webkit-linear-gradient(
                180deg,
                #2980b9,
                transparent
            );
            background-image: -moz-linear-gradient(
                180deg,
                #2980b9,
                transparent
            );
            background-image: -o-linear-gradient(180deg, #2980b9, transparent);
            background-image: linear-gradient(90deg, #2980b9, transparent);
        }
        &:nth-child(2) {
            color: #000;
            padding: 0px 5px;
            width: auto;
            white-space: nowrap;
        }
    }
}

.img-size {
    padding-right: 10px;
    width: 60%;
    // height: 70px;
}

.img-size-mini {
    width: 40px;
    height: 30px;
}


.v-card-scroll {
    display: flex !important;
    flex-direction: column;
}

.v-card__text {
    flex-grow: 1;
    overflow: auto;
}
</style>
