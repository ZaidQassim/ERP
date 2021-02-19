<template>
    <div>
        <v-row no-gutters>
            <v-col cols="12" md="6" style="margin: auto;">
                <v-row align="center" justify="center">
                    <v-card width="450" flat tile class="pa-6 mt-10 mx-auto">
                        <v-card flat class="d-flex justify-center">
                            <div>
                                <v-img width="130" src="../assets/logo-login.png" />
                            </div>
                        </v-card>

                        <v-card-title>
                            <h4>تسجيل دخول</h4>
                        </v-card-title>
                        <v-card-text class="mt-n3">
                            مرحبًا بك في نظام إدارة تخطيط موارد المؤسسات
                        </v-card-text>

                        <v-form
                            @submit.prevent="submit"
                            ref="form"
                            v-model="valid"
                            lazy-validation>
                            <v-card-actions>
                                <v-text-field
                                    v-model="user.username"
                                    placeholder="إسم المستخدم"
                                    outlined
                                    color="third"
                                    :rules="required"
                                >
                                </v-text-field>
                            </v-card-actions>
                            <v-card-actions class="mt-n4">
                                <v-text-field
                                    v-model="user.password"
                                    placeholder="كلمة المرور"
                                    outlined
                                    color="third"
                                    :rules="required"
                                    :append-icon="
                                        showPassword ? 'mdi-eye' : 'mdi-eye-off'
                                    "
                                    :type="showPassword ? 'text' : 'password'"
                                    @click:append="showPassword = !showPassword"
                                >
                                </v-text-field>
                            </v-card-actions>
                            <v-btn class="mt-n10" dark small text color="blue">
                                هل نسيت كلمة المرور ؟
                            </v-btn>
                            <v-card-actions>
                                <v-btn
                                    :loading="loading"
                                    type="submit"
                                    dark
                                    large
                                    color="secondary"
                                >
                                    تسجيل الدخول
                                </v-btn>
                            </v-card-actions>
                        </v-form>
                    </v-card>
                </v-row>
            </v-col>

            <v-col cols="6" md="6" lg="5" class="hidden-sm-and-down ">
                <v-img class="bg-login" src="../assets/bg-login.png" />
            </v-col>
        </v-row>

        <v-footer color="mb-4 footer" padless>
            <v-row justify="center" class="grey--text">
                <span class="black--text caption font-weight-bold">
                    <strong> © المعرفه</strong> {{ new Date().getFullYear() }}
                </span>
                <a href="#" class="footer-link">
                    اتفاقية المستخدم
                </a>
                <a href="#" class="footer-link">
                    سياسة الخصوصية
                </a>
                <a href="#" class="footer-link">
                    إرشادات المجتمع
                </a>
                <a href="#" class="footer-link">
                    سياسة ملفات تعريف الارتباط
                </a>
                <a href="#" class="footer-link">
                    سياسة حقوق النشر
                </a>
            </v-row>
        </v-footer>
    </div>
</template>

<script>
import axios from "../axios";
export default {
    data() {
        return {
            user: {},
            loading: false,
            valid: true,
            showPassword: false,
            required: [v => !!v || "هذا الحقل مطلوب"],
            icons: [
                "mdi-facebook",
                "mdi-twitter",
                "mdi-linkedin",
                "mdi-instagram"
            ]
        };
    },

    methods: {
        submit() {
            this.$refs.form.validate();

            if (this.user.password != null && this.user.username != null) {
                this.loading = true;
                axios
                    .post("auth/Login", this.user)
                    .then(res => {
                        localStorage.setItem("userID", res.data.user.id);
                        localStorage.setItem("token", res.data.token);
                        localStorage.setItem(
                            "username",
                            res.data.user.username
                        );
                        localStorage.setItem("role", res.data.roles[0]);

                        this.$eventBus.$emit("userId", {
                            userId: res.data.user.id
                        });

                        this.$eventBus.$emit("role", {
                            role: res.data.roles[0]
                        });

                        if (res.data.roles[0] == "Admin") {
                            this.$router.push("/home");
                        } else if (res.data.roles[0] == "Root") {
                            this.$router.push("/dashboard");
                        }

                        this.$store.dispatch("setSnackbar", {
                            text: `تم تسجيل الدخول بنجاح`
                        });
                    })
                    .catch(err => {
                        this.$store.dispatch("setSnackbar", {
                            text: `إسم المستخدم او كلمة المرور خطأ`
                        });
                    })
                    .finally(() => {
                        this.loading = false;
                    });
            }
            if (this.user.username == null) {
                this.$store.dispatch("setSnackbar", {
                    text: `يرجى كتابة أسم المستخدم  `
                });
            }
            if (this.user.password == null) {
                this.$store.dispatch("setSnackbar", {
                    text: ` يرجى كتابة كلمة المرور `
                });
            }
            if (this.user.password == null && this.user.username == null) {
                this.$store.dispatch("setSnackbar", {
                    text: `يرجى كتابة  اسم المستخدم وكلمة المرور`
                });
            }
        }
    }
};
</script>

<style lang="scss" scoped>
.bg-login {
    height: 100%;
    opacity: 0.7;
    margin: auto;
    width: 100%;
    margin-left: -30px;
    margin-top: 30px;
}

.footer {
    position: absolute;
    bottom: 0px;
    left: 0;
    right: 0;
    background: #f7f9fd;
}

.footer-link {
    padding: 0 8px;
    color: grey;
    text-decoration-line: none;
    font-size: 12px;
}

.footer-link:hover {
    text-decoration-line: underline;
}
</style>
