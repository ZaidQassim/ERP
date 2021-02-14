<template>
    <div>
        <div>
            <v-card
                width="420"
                class="pa-6 mt-10 mx-auto rounded-lg"
                elevation="6">
                <v-card flat class="d-flex justify-center">
                    <div>
                        <v-img width="130" src="../assets/logo.jpg" />
                    </div>
                </v-card>

                <v-card-title>
                    <h4>تسجيل دخول</h4>
                </v-card-title>
                <v-card-text class="mt-n3">
                    مرحبًا بك في نظام إدارة المستندات
                </v-card-text>

                <v-form
                    @submit.prevent="submit"
                    ref="form"
                    v-model="valid"
                    lazy-validation
                >
                    <v-card-actions>
                        <v-text-field
                            v-model="user.username"
                            placeholder="إسم المستخدم"
                            outlined
                            color="secondary"
                            :rules="required"
                        >
                        </v-text-field>
                    </v-card-actions>
                    <v-card-actions class="mt-n4">
                        <v-text-field
                            v-model="user.password"
                            placeholder="كلمة المرور"
                            outlined
                            color="secondary"
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
                            rounded
                            block
                            dark
                            x-large
                            color="primary"
                        >
                            تسجيل الدخول
                        </v-btn>
                    </v-card-actions>
                </v-form>
            </v-card>

            <!-- <p disabled class="text-center grey--text my-10">
                جديد على الـ D M S
            </p> -->
        </div>
        <v-footer color="white mb-4 footer" padless>
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
.footer {
    position: absolute;
    bottom: 0px;
    left: 0;
    right: 0;
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
