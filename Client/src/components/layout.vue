<template>
    <div>
        <!-- the app bar  -->
        <v-app-bar
            app
            v-if="!$route.meta.hideNavigation"
            :class="$vuetify.theme.dark ? '' : 'bluegrey'"
            height="50"
            lights-out
            class="elevation-0">

            <v-progress-linear
                :active="loading"
                :indeterminate="loading"
                absolute
                top
                color="indigo">
            </v-progress-linear>

            <v-app-bar-nav-icon @click="mini = !mini" large class="white">
                <v-icon>mdi mdi-menu</v-icon>
            </v-app-bar-nav-icon>

            <v-breadcrumbs :items="itemsbreadcrumbs"></v-breadcrumbs>

            <v-spacer></v-spacer>

            <settings class="ml-2"></settings>

            <span class="ml-3">
                <v-badge color="red" overlap dot left>
                    <v-btn icon small>
                        <v-icon>
                            mdi-email-multiple-outline
                        </v-icon>
                    </v-btn>
                </v-badge>
            </span>

            <!-- button notification -->
            <v-menu offset-y :nudge-width="200">
                <template v-slot:activator="{ on, attrs }">
                    <span v-bind="attrs" v-on="on" class="ml-2">
                        <v-badge
                            :content="countNoti"
                            :value="countNoti"
                            color="primary"
                            overlap>
                            <v-btn icon>
                                <v-icon>
                                    mdi-bell-ring-outline
                                </v-icon>
                            </v-btn>
                        </v-badge>
                    </span>
                </template>

                <v-list
                    class="text-center v-card-scroll"
                    height="45vh"
                    max-width="400"
                >
                    <v-container>
                        <v-alert
                            v-for="item in notification"
                            :key="item.id"
                            @click="isReadTrue(item.id)"
                            color="cyan"
                            class="mt-n1"
                            border="left"
                            :class="item.isRead ? '' : 'blue lighten-5'"
                            elevation="2"
                            colored-border
                            style="font-size: 10px"
                        >
                            <v-row no-gutters class="mt-n2 mb-n3">
                                <v-col cols="12" sm="12">
                                    <v-img
                                        class="float-right"
                                        src="../assets/img/user.svg"
                                        alt="zaid"
                                        width="20"
                                    >
                                    </v-img>
                                    <span class="float-right mr-2 mt-1">
                                        {{ item.userSender }}
                                    </span>
                                    <v-btn
                                        icon
                                        small
                                        class="float-left"
                                        color="red"
                                    >
                                        <v-icon>mdi-close</v-icon>
                                    </v-btn>
                                </v-col>
                                <v-col cols="12" sm="12">
                                    <span class="float-right mr-6">
                                        <span>تم مشاركة وثيقة معك </span>
                                        <span
                                            class="indigo--text font-weight-bold"
                                            >{{ item.documentInfo.title }}</span
                                        >
                                    </span>
                                </v-col>
                                <v-col cols="12" sm="12">
                                    <span class="float-right red--text mr-6">
                                        {{ item.created | formatDateAgo }}
                                    </span>
                                </v-col>
                            </v-row>
                        </v-alert>
                    </v-container>
                </v-list>
            </v-menu>
            <!-- ./ button notification -->

            <user-profile></user-profile>

            <!-- ./ user profile  -->
        </v-app-bar>
        <!-- ./ the app bar  -->

        <!-- side bar app  -->
        <v-navigation-drawer
            v-model="drawer"
            :mini-variant="mini"
            v-if="!$route.meta.hideNavigation"
            app
            class="black"
            bottom
            permanent
            :expand-on-hover="mini"
            floating
            right>
            <v-img class="img-drawer"
                src="../assets/register.jpg"
                alt="zaid"/>

            <v-list-item dark class="my-1 px-6 text-center">
                <img
                    width="45"
                    :class="mini ? 'mr-n5' : ''"
                    src="../assets/logo-login.png"
                />
                <small class="mr-2">    
                    نظام أدارة الموظفين <br />
                    في الشركة
                </small>
            </v-list-item>

            <v-divider v-if="mini" class="mt-2 mx-3 white"></v-divider>

            <v-list shaped dark class="mt-4">
                <v-list-item to="/" color="amber" class="mt-3">
                    <v-icon>home</v-icon>
                    <v-list-item-title class="mr-5">
                        الرئسيه
                    </v-list-item-title>
                </v-list-item>

                <v-list-item to="/employe" color="amber" class="my-2">
                    <v-icon>mdi-account-cog</v-icon>
                    <v-list-item-title class="mr-5">
                        أدارة الموظفين
                    </v-list-item-title>
                </v-list-item>

                <!-- group employeAccord -->
                <v-list-group prepend-icon="mdi-package-variant">
                    <template v-slot:activator>
                        <v-list-item-title class="ma-0 pa-0">
                            أدارة الذمه
                        </v-list-item-title>
                    </template>

                    <v-list-item
                        to="/employeAccord"
                        color="amber"
                        class="my-2"                    >
                        <v-icon>mdi-account-convert</v-icon>
                        <v-list-item-title class="mr-3">
                            ذمة موظفين الشركة
                        </v-list-item-title>
                    </v-list-item>

                    <v-list-item to="/" color="amber">
                        <v-icon>mdi-handshake-outline</v-icon>
                        <v-list-item-title class="mr-3">
                            دمة خارجيـــه
                        </v-list-item-title>
                    </v-list-item>
                </v-list-group>
                <!-- ./ group employeAccord -->
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
import { mapState } from "vuex";
import Settings from "./Settings.vue";
    
export default {
    name: "layout",
    components: {
        UserProfile,
        Settings
    },

    data() {
        return {
            itemsbreadcrumbs: [],
            role: "",
            connection: "",
            drawer: true,
            mini: false,
            notification: [],
            dateNoti: null,
            countNoti: 0,
            token: localStorage.getItem("token"),
            alert: true
            // xmini: true,
        };
    },
    
    watch: {       
        "$route"(to, from) {
            if (to.path == '/') {
                this.itemsbreadcrumbs = [
                    {
                        text: "الرئسيه",
                        disabled: false,
                        href: "/"
                    }
                ];
            } else {
                this.itemsbreadcrumbs = [
                    {
                        text: "الرئسيه",
                        disabled: false,
                        href: "/"
                    },
                    {
                        text: to.name ,
                        disabled: false,
                        href: to.path
                    },
                ];
            }
            
        },
        
    },

    computed: {
        getRole() {
            return this.role;
        },
        ...mapState(["loading"])
    },

    mounted() {
        // this.connection.on("ShareCreated", send => {
        //     if (send[0].reciepentId == localStorage.getItem("userID")) {
        //         this.getNoti();
        //     } else if (send[0].orginaztionId) {
        //         this.notification.push(
        //             `تم مشاركة وثيقة معك في بتاريخ :  ${this.dateNoti} `
        //         );
        //     }
        // });
    },

    // beforeCreate() {
    //     if (localStorage.getItem("token") == null) {
    //         this.$router.push("/login");
    //     }
    // },

    // inital Created Component
    created() {
        // var today = new Date();
        // var date =
        //     today.getFullYear() +
        //     "-" +
        //     (today.getMonth() + 1) +
        //     "-" +
        //     today.getDate();
        // var time =
        //     today.getHours() +
        //     ":" +
        //     today.getMinutes() +
        //     ":" +
        //     today.getSeconds();
        // this.dateNoti = date + " " + time;

        // this.connection = new HubConnectionBuilder()
        //     .withUrl(process.env.VUE_APP_URL_SIGNALR, {
        //         skipNegotiation: true,
        //         transport: HttpTransportType.WebSockets
        //     })
        //     .configureLogging(LogLevel.Error)
        //     .build();
        // this.connection.start();

        // if (this.token != null) {
        //     this.getNoti();
        // }

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
                    if (
                        this.$router.currentRoute.path != `/recipientDocument`
                    ) {
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

.img-drawer{
    position: absolute;
    opacity: .3;
    width: 100%;
    height: 100%;
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
