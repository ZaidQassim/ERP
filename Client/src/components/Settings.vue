<template>
    <div>
        <!-- setting   -->
        <v-menu :nudge-width="200" offset-y elevation="0" :close-on-content-click="false">
            <template v-slot:activator="{ on, attrs }">
                <span v-bind="attrs" v-on="on" class="ml-2">
                    <v-btn icon>
                        <v-icon>
                            mdi-cog-outline
                        </v-icon>
                    </v-btn>
                </span>
            </template>

            <v-card class="text-center" width="450">
                <v-container>
                    <v-img
                        class="mx-auto"
                        src="../assets/logo-login.png"
                        width="120">
                    </v-img>
                    <h3 class="text-center">إعــــدادات النظــام </h3>
                    <v-card-text>
                        <div>
                            <v-switch
                                v-model="$vuetify.theme.dark"
                                inset
                                class="ml-3"
                                :label="
                                    $vuetify.theme.dark ? 'مظهر فاتح' : 'مظهر داكن'"
                                persistent-hint
                                hide-details>
                            </v-switch>
                        </div>
                    </v-card-text>

                    <v-divider></v-divider>

                    <div class="pa-6">
                       
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
