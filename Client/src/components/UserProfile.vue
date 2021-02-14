<template>
    <div>
        <!-- user profile  -->
        <v-menu
            :nudge-width="200"
            offset-y
            elevation="0">
            <template v-slot:activator="{ on, attrs }" class="pl-2">
                <div class="d-flex" v-bind="attrs" v-on="on">
                    <p class="mt-4 ml-3">{{ userInfo.knownAs }}</p>
                    <img src="../assets/img/user.svg" alt="John" width="30" />
                </div>
            </template>

            <v-card class="text-center" max-width="400" tile elevation="0">
                <v-container>
                    <v-img
                        class="mx-auto"
                        src="../assets/img/user.svg"
                        alt="zaid"
                        width="90"
                    >
                    </v-img>
                    <h3 class="text-center">{{ userInfo.knownAs }}</h3>
                    <v-card-text>
                        <div class="grey--text mt-n2">
                            {{ userInfo.email }}
                        </div>
                        <!-- <div class="my-4 subtitle-1">
                            <div class="d-flex justify-space-between mb-6">
                                <span>
                                    <div class="d-flex flex-column">
                                        <span
                                            class="indigo--text"
                                            v-if="userInfo.id == 1"
                                        >
                                            الكل
                                        </span>
                                        <span
                                            class="indigo--text"
                                            v-if="!userInfo.id == 1"
                                        >
                                            {{ userInfo.countOrgForUser }}
                                        </span>
                                        <span class="caption">
                                            المؤسسات المسؤل عنها
                                        </span>
                                    </div>
                                </span>
                                <span>
                                    <div class="d-flex flex-column">
                                        <span class="green--text">
                                            {{ userInfo.countDoucmentForUser }}
                                        </span>
                                        <span class="caption">
                                            وثائقي
                                        </span>
                                    </div>
                                </span>
                                <span>
                                    <div class="d-flex flex-column">
                                        <span class="red--text">
                                            {{
                                                userInfo.countSenderDoucmentForUser
                                            }}
                                        </span>
                                        <span class="caption">
                                            الصادره
                                        </span>
                                    </div>
                                </span>
                                <span>
                                    <div class="d-flex flex-column">
                                        <span class="red--text">
                                            {{ userInfo.countOrgForUser }}
                                        </span>
                                        <span class="caption">
                                            الوارده
                                        </span>
                                    </div>
                                </span>
                                <span>
                                    <div class="d-flex flex-column">
                                        <span class="red--text">
                                            {{
                                                userInfo.countCommentDoucmentForUser
                                            }}
                                        </span>
                                        <span class="caption">
                                            تواقيعي
                                        </span>
                                    </div>
                                </span>
                            </div>
                        </div> -->
                        <div>
                            <div class="text-right my-6">
                                <v-btn
                                    @click="history()"
                                    text
                                    outlined
                                    block
                                    rounded>
                                    سجل أحداث
                                </v-btn>
                            </div>
                            <div>
                               --------------------------
                            </div>
                        </div>
                    </v-card-text>

                    <v-divider></v-divider>

                    <div class="pa-6    ">
                        <v-btn depressed outlined plain @click="logout()">
                            تسجيل خروج
                            <v-icon class="mr-2">power_settings_new</v-icon>
                        </v-btn>
                    </div>

                    <v-divider></v-divider>

                    <p class="grey--text button mt-3">
                        سياسة الخصوصية . شروط الخدمة
                    </p>
                </v-container>
            </v-card>
        </v-menu>
        <!-- ./ user profile  -->
    </div>
</template>
<script>
import axios from "../axios";

export default {
    data() {
        return {
            userId: null,
            userInfo: []
        };
    },
    created() {
        this.$eventBus.$on("userId", data => {
            this.userId = data.userId;
        });
        this.userId = localStorage.getItem("userID");
        this.getUserInfo();
    },
    methods: {
        getUserInfo() {
            axios
                .get("users/getUsersById/" + this.userId, {
                    headers: {
                        Authorization: `Bearer ${localStorage.getItem("token")}`
                    }
                })
                .then(res => {
                    this.userInfo = res.data.result.usersToReturn;
                })
                .catch(err => {
                    this.$store.dispatch("setSnackbar", {
                        text: `${err.message}`
                    });
                })
                .finally(() => {});
        },

        history() {
            this.$router.push({
                path: `/history/${this.userId}`,
                params: { userId: this.userId }
            });
        },

        logout() {
            localStorage.removeItem("token");
            this.$router.push("/login");
        }
    },

    computed: {
        getUserId() {
            return this.userId;
        },

        getRole() {
            return this.role;
        }
    }
};
</script>
<style lang=""></style>
