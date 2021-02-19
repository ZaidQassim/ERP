<template>
    <div>
        <v-card flat tile>
            <v-card-title>
                أدارة الموظفين
                <v-spacer></v-spacer>
                <v-spacer></v-spacer>
                <v-spacer></v-spacer>
                <v-text-field
                    v-model="search"
                    append-icon="mdi-magnify"
                    label="أبحث هنا "
                    dense
                    solo
                    hide-details
                >
                </v-text-field>
            </v-card-title>

            <!-- btn add user  -->
            <v-card-text>
                <v-btn depressed color="secondary" @click="dialog = true">
                    <v-icon class="ml-1">mdi-plus-circle</v-icon>
                    تسجيل موظف
                </v-btn>
            </v-card-text>
            <!-- ./ btn add user -->

            <v-data-table
                :headers="headerEmployes"
                :items="employes"
                loader-height="10"
                :search="search"
                loading-text="جاري تحميل أخر أجراءات وأحداث النظام "
                page.sync="page"
                no-data-text="لا يوجد موظفين "
                :items-per-page="itemsPerPage"
                hide-default-footer>
                <template v-slot:[`item.age`]="{ item }">
                    <span> {{ item.age }} سنه </span>
                </template>

                <template v-slot:[`item.gender`]="{ item }">
                    <span v-if="item.gender == true">ذكر </span>
                    <span v-if="item.gender == false">أنثى </span>
                </template>

                <template v-slot:[`item.motherFullName`]="{ item }">
                    {{ item.motherFname }} {{ item.motherSname }}
                    {{ item.motherTname }}
                </template>

                <template v-slot:[`item.dateOfStartWork`]="{ item }">
                    <span>
                        {{ item.dateOfStartWork | formatDate }}
                    </span>
                </template>

                <template v-slot:[`item.dateofSigningContract`]="{ item }">
                    <span class="error--text">
                        {{ item.dateofSigningContract | formatDate }}
                    </span>
                </template>

                <template v-slot:[`item.state`]="{ item }">
                    <v-avatar
                        v-if="item.state == '1'"
                        color="green"
                        rounded
                        height="18"
                    >
                        مستمر</v-avatar
                    >
                    <v-avatar
                        v-if="item.state == '2'"
                        color="red"
                        rounded
                        height="18"
                    >
                        مستمر</v-avatar
                    >
                </template>
            </v-data-table>

            <v-divider></v-divider>

            <v-pagination
                v-model="page"
                :length="pageCount"
                @input="next"
                :total-visible="7"
                class="py-6"
            >
            </v-pagination>
        </v-card>

        <!-- form dialog add user -->
        <v-row justify="center">
            <v-dialog
                v-model="dialog"
                max-width="70%"
                scrollable
                :fullscreen="isfullscreen"
                persistent
                transition="dialog-bottom-transition"
            >
                <v-card class="pa-0 ma-0">
                    <v-toolbar flat dense>
                        <v-card-title> تسجيل موظف جديد </v-card-title>
                        <v-spacer></v-spacer>
                        <v-btn icon @click="isfullscreen = !isfullscreen">
                            <v-icon small>
                                {{
                                    isfullscreen
                                        ? "mdi-arrow-collapse"
                                        : "mdi-arrow-expand"
                                }}
                            </v-icon>
                        </v-btn>
                        <v-btn color="info" icon @click="close()">
                            <v-icon>mdi-close</v-icon>
                        </v-btn>
                    </v-toolbar>

                    <v-card-text style="min-height: 500px;" class="pa-0 ma-0">
                        <v-form ref="form" v-model="valid" lazy-validation>
                            <v-stepper v-model="e1">
                                <v-stepper-header>
                                    <v-stepper-step
                                        editable
                                        step="1"
                                        :complete="e1 > 1"
                                    >
                                        المعلومات الشخصيه للموظف
                                        <small>معلومات مطلوبه</small>
                                    </v-stepper-step>

                                    <v-divider></v-divider>

                                    <v-stepper-step
                                        editable
                                        step="2"
                                        :complete="e1 > 1"
                                    >
                                        معلومات العمل
                                    </v-stepper-step>
                                    <v-spacer></v-spacer>
                                    <v-spacer></v-spacer>
                                    <v-spacer></v-spacer>
                                    <v-spacer></v-spacer>
                                </v-stepper-header>

                                <v-stepper-items>
                                    <v-stepper-content :step="1">
                                        <v-row no-gutters>
                                            <v-col cols="12" md="3" class="pl-2">
                                                <small class="">
                                                    الاسم الاول
                                                    <span
                                                        class="red--text font-weight-bold"
                                                        >*</span
                                                    >
                                                </small>
                                                <v-text-field
                                                    v-model="employe.firstName"
                                                    :rules="requiredRules"
                                                    required
                                                    outlined
                                                    dense
                                                    color="secondary"
                                                    filled
                                                    placeholder="الاسم الاول للموظف"
                                                >
                                                </v-text-field>
                                            </v-col>

                                            <v-col cols="12" md="3" class="pl-2">
                                                <small class="">
                                                    الاسم الثاني
                                                    <span
                                                        class="red--text font-weight-bold"
                                                        >*</span
                                                    >
                                                </small>
                                                <v-text-field
                                                    v-model="employe.secondName"
                                                    :rules="requiredRules"
                                                    required
                                                    outlined
                                                    dense
                                                    color="secondary"
                                                    filled
                                                    placeholder="الاسم الثاني للموظف"
                                                >
                                                </v-text-field>
                                            </v-col>

                                            <v-col cols="12" md="3" class="pl-2">
                                                <small class="">
                                                    الاسم الثالث
                                                    <span
                                                        class="red--text font-weight-bold"
                                                        >*</span
                                                    >
                                                </small>
                                                <v-text-field
                                                    v-model="employe.thirdName"
                                                    :rules="requiredRules"
                                                    required
                                                    outlined
                                                    dense
                                                    color="secondary"
                                                    filled
                                                    placeholder="الاسم الثالث للموظف">
                                                </v-text-field>
                                            </v-col>

                                            <v-col cols="12" md="3" class="pl-2">
                                                <small class="">
                                                    الاسم الرابع
                                                    <span
                                                        class="red--text font-weight-bold"
                                                        >*</span
                                                    >
                                                </small>
                                                <v-text-field
                                                    v-model="employe.fourthName"
                                                    :rules="requiredRules"
                                                    required
                                                    outlined
                                                    dense
                                                    color="secondary"
                                                    filled
                                                    placeholder="الاسم الرابع للموظف"
                                                >
                                                </v-text-field>
                                            </v-col>
                                        </v-row>

                                        <v-row no-gutters class="mt-n3">
                                            <v-col cols="12" md="3" class="pl-2">
                                                <small>
                                                    اللقب 
                                                    <span class="red--text font-weight-bold">*</span>
                                                </small>
                                                <v-text-field
                                                    v-model="employe.family"
                                                    :rules="requiredRules"
                                                    required
                                                    outlined
                                                    dense
                                                    color="secondary"
                                                    filled
                                                    placeholder="اللقب">
                                                </v-text-field>
                                            </v-col>

                                            <v-col cols="12" md="3" class="pl-2">
                                                <small>
                                                    اسم الام الاول 
                                                    <span class="red--text font-weight-bold">*</span>
                                                </small>
                                                <v-text-field
                                                    v-model="employe.motherFname"
                                                    :rules="requiredRules"
                                                    required
                                                    outlined
                                                    dense
                                                    color="secondary"
                                                    filled
                                                    placeholder="اسم الام الاول ">
                                                </v-text-field>
                                            </v-col>

                                            <v-col cols="12" md="3" class="pl-2">
                                                <small>
                                                    اسم الام الثاني 
                                                    <span class="red--text font-weight-bold">*</span>
                                                </small>
                                                <v-text-field
                                                    v-model="employe.motherSname"
                                                    :rules="requiredRules"
                                                    required
                                                    outlined
                                                    dense
                                                    color="secondary"
                                                    filled
                                                    placeholder="اسم الام الثاني">
                                                </v-text-field>
                                            </v-col>

                                            <v-col cols="12" md="3" class="pl-2">
                                                <small>
                                                    اسم الام الثالث 
                                                    <span class="red--text font-weight-bold">*</span>
                                                </small>
                                                <v-text-field
                                                    v-model="employe.motherTname"
                                                    :rules="requiredRules"
                                                    required
                                                    outlined
                                                    dense
                                                    color="secondary"
                                                    filled
                                                    placeholder="اسم الام الثالث ">
                                                </v-text-field>
                                            </v-col>                                            
                                        </v-row>

                                        <v-row no-gutters class="mt-n2">
                                            <v-col cols="12" md="4" class="pl-2">
                                                <small>
                                                    تاريخ الميلاد 
                                                </small>
                                                <v-menu
                                                    ref="menu"
                                                    v-model="menu"
                                                    :close-on-content-click="false"
                                                    :return-value.sync="dateOfBirth"
                                                    transition="scale-transition"
                                                    offset-y
                                                    min-width="290px">
                                                    <template v-slot:activator="{ on, attrs }">
                                                        <v-text-field
                                                            v-model="dateOfBirth"
                                                            append-icon="mdi-calendar"
                                                            readonly
                                                            outlined
                                                            filled
                                                            color="secondary"
                                                            dense
                                                            v-bind="attrs"
                                                            v-on="on">
                                                        </v-text-field>
                                                    </template>

                                                    <v-date-picker
                                                        v-model="dateOfBirth"
                                                        no-title
                                                        color="secondary"
                                                        scrollable>
                                                        <v-btn
                                                            text
                                                            color="info"
                                                            @click="$refs.menu.save(dateOfBirth)">
                                                            حسناً
                                                        </v-btn>
                                                        <v-btn
                                                            text
                                                            color="error"
                                                            @click="menu = false">
                                                            الغاء
                                                        </v-btn>
                                                        <v-spacer></v-spacer>
                                                    </v-date-picker>
                                                </v-menu>
                                                
                                                
                                            </v-col>

                                            <v-col cols="12" md="3" class="pl-2">
                                                <small>
                                                    الجنس   
                                                </small>
                                                <v-select
                                                    v-model="employe.gender"
                                                    :items="gender"
                                                    item-text="name"
                                                    item-value="value"
                                                    filled outlined
                                                    label="أختار الجنس"
                                                    dense>
                                                </v-select>                                               
                                            </v-col>

                                            <v-col cols="12" md="5" class="pl-2">
                                                <small>
                                                    المستمسكات (هوية أحوال او بطاقة وطنية موحدة, بطاقه سكن, جواز سفر ) 
                                                    <span class="red--text font-weight-bold">*</span>
                                                </small>
                                                <v-file-input
                                                    @change="handleImagesUpload()"
                                                    v-model="employeAttachments"
                                                    multiple
                                                    counter
                                                    chips dense
                                                    show-size
                                                    accept="image/png, image/jpeg, image/bmp, image/jpg, .pdf"
                                                    color="secondary"
                                                    :loading="emAtloading"
                                                    append-icon="mdi-paperclip"
                                                    prepend-icon=""
                                                    label="حدد المستمسكات المراد رفعها"
                                                    outlined
                                                    filled
                                                    hide-details="auto"/>
                                            </v-col>
                                        </v-row>

                                         <v-row no-gutters class="mt-n2">
                                            <v-col cols="12" md="5" class="pl-2">
                                                <small>
                                                    البريد الاكتروني  
                                                    <span class="red--text font-weight-bold">*</span>
                                                    (على الاقل يجب كتابه بريد أكتروني واحد)
                                                </small>

                                                <div v-for="(item, i) in countEmployeEmails" :key="i">
                                                    <v-btn v-if="i == 0" depressed @click="addEmployeEmails()"
                                                        class="float-left mt-1" 
                                                        color="success" small icon> 
                                                        <v-icon>mdi-plus-circle</v-icon>
                                                    </v-btn>
                                                    <v-btn v-if="i > 0" depressed @click="removeEmployeEmails()"
                                                        class="float-left mt-1" 
                                                        color="error" small icon> 
                                                        <v-icon>mdi-minus-circle</v-icon>
                                                    </v-btn>
                                                    <v-text-field 
                                                        v-model="employeEmails[i]"
                                                        :rules="emailRules"
                                                        required
                                                        outlined
                                                        dense class="mb-n2"
                                                        color="secondary"
                                                        filled
                                                        placeholder="البريد ألاكتروني">
                                                    </v-text-field>
                                                </div>
                                                 
                                            </v-col>
                                        </v-row>

                                        <v-btn
                                            color="primary"
                                            small class="mt-5"
                                            elevation="2"
                                            plain
                                            raised
                                            @click="e1 = e1 + 1">
                                            التالي
                                        </v-btn>
                                    </v-stepper-content>

                                    <v-stepper-content :step="2">
                                        <v-row>
                                            <v-col cols="12" md="6">
                                                <v-subheader class="mr-n3">
                                                    أسم المستخدم
                                                    <span
                                                        class="red--text font-weight-bold"
                                                    >
                                                        *
                                                        <span
                                                            class="font-size-10 mr-1"
                                                            >"يكون باللغه
                                                            الانكليزيه"</span
                                                        >
                                                    </span>
                                                </v-subheader>
                                                <v-text-field
                                                    v-model="user.userName"
                                                    :rules="requiredRules"
                                                    required
                                                    outlined
                                                    dense
                                                    color="secondary"
                                                    filled
                                                    placeholder="أسم المستخدم"
                                                >
                                                </v-text-field>
                                            </v-col>
                                            <v-col cols="12" md="6">
                                                <v-subheader class="mr-n3">
                                                    أسم الكامل للمستخدم
                                                    <span
                                                        class="red--text font-weight-bold"
                                                        >*</span
                                                    >
                                                </v-subheader>
                                                <v-text-field
                                                    v-model="user.knownAs"
                                                    :rules="requiredRules"
                                                    required
                                                    outlined
                                                    color="secondary"
                                                    dense
                                                    filled
                                                    placeholder="أسم الكامل للمستخدم"
                                                >
                                                </v-text-field>
                                            </v-col>
                                        </v-row>

                                        <v-btn
                                            elevation="2"
                                            plain
                                            raised
                                            color="primary"
                                            small
                                            @click="e1 = 1"
                                        >
                                            السابِق
                                        </v-btn>
                                    </v-stepper-content>
                                </v-stepper-items>
                            </v-stepper>
                        </v-form>
                    </v-card-text>

                    <v-divider></v-divider>
                    <v-card-actions>
                        <v-btn
                            :loading="false"
                            @click="addEmploye()"
                            color="blue darken-1"
                            type="submit"
                            text
                            x-large>
                            حفظ
                        </v-btn>

                        <v-btn color="blue darken-1" text @click="close()">
                            إلغاء
                        </v-btn>

                        <v-spacer></v-spacer>
                        <span class="red--text font-weight-bold">*</span>
                        <span class="caption"> هذا الحقل مطلوب</span>
                    </v-card-actions>
                </v-card>
            </v-dialog>
        </v-row>
        <!-- ./ form dialog add user -->
    </div>
</template>

<script>
import axios from "../axios";

export default {
    data() {
        return {
            search: "",
            page: 1,
            pageCount: 0,
            itemsPerPage: 10,
            headerEmployes: [
                {
                    text: "#",
                    value: "id"
                },
                {
                    text: "الإسم الاول ",
                    value: "firstName"
                },
                {
                    text: "الثاني",
                    value: "secondName"
                },
                {
                    text: "الثالث",
                    value: "thirdName"
                },
                {
                    text: "الرابع",
                    value: "fourthName"
                },
                {
                    text: "اللقب",
                    value: "family"
                },
                {
                    text: "أسم الام الكامل",
                    value: "motherFullName"
                },
                {
                    text: "العمر",
                    value: "age"
                },
                {
                    text: "الجنس",
                    value: "gender"
                },
                {
                    text: "تاريخ بداء العمل",
                    value: "dateOfStartWork"
                },
                {
                    text: "تاريخ توقيع العقد",
                    value: "dateofSigningContract"
                },
                {
                    text: "حالة الموظف",
                    value: "state"
                }
            ],
            employes: [],
            dialog: false,
            e1: 1,
            employe: {},
            user: {},
            menu: false,
            dateOfBirth: new Date().toISOString().substr(0, 10),
            gender: [
                {
                    name: "ذكر",
                    value: true
                },
                {
                    name: "انثى",
                    value: false
                }
            ],
            employeAttachments: [],
            emAtloading: false,
            employeEmails: [],
            countEmployeEmails: 1,

             // valdation input
            valid: true,
            requiredRules: [v => !!v || "رجاءاً هذا الحقل مطلوب "],
            emailRules: [v => /.+@.+\..+/.test(v) || "E-mail must be valid"],


            selectedOrgsId: [],
            selectOrg: [],
            listOrg: [],
            password: "",
            confirmPassword: "",
            items: [],
            treeData: [],
            open: [],
            showPassword: false,
            isfullscreen: false,
            isEdit: false,
            dialogEditUser: false,
            isEditPasswordUser: false
        };
    },

    computed: {},

    created() {
        this.getEmployes();
    },

    watch: {},

    methods: {
        //  get All Employes
        getEmployes() {
            this.$store.commit("SET_LOADING", true);
            axios
                .get(
                    "employes/getEmployes?skip=0" +
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
                    this.employes = res.data.result.employesToReturn;
                    let pageOfCount = Math.ceil(res.data.result.count / 10);
                    this.pageCount = parseInt(pageOfCount);
                    this.$store.commit("SET_LOADING", false);
                })
                .catch(err => {});
        },

        deleteUser(id) {
            this.$swal
                .fire({
                    title: "هل تريد الاستمرار",
                    text:
                        "!! سوف يتم حذف  هذه المستخدم ولن يتمكن من أستخدم النظام ",
                    icon: "question",
                    showCancelButton: true,
                    confirmButtonColor: "#3085d6",
                    cancelButtonColor: "#d33",
                    cancelButtonText: "لا",
                    confirmButtonText: "نعم"
                })
                .then(result => {
                    if (result.isConfirmed) {
                        axios
                            .delete("users/" + id, {
                                headers: {
                                    Authorization: `Bearer ${localStorage.getItem(
                                        "token"
                                    )}`
                                }
                            })
                            .then(res => {
                                this.getUsers();
                                this.$store.dispatch("setSnackbar", {
                                    text: `${res.data}`
                                });
                            })
                            .catch(error => {
                                this.$store.dispatch("setSnackbar", {
                                    color: "error",
                                    text: `${error.message}`
                                });
                            });

                        this.$swal.fire({
                            icon: "success",
                            title: "تم الحذف ينجاح !",
                            showConfirmButton: false,
                            timer: 1500
                        });
                    }
                });
        },

       

        addEmploye() {
            console.log("employeEmails == " + this.employeEmails);
            this.employeEmails.forEach(element => {
                console.log("employeEmails == " + element);
            });
        },

        addEmployeEmails() {
            this.countEmployeEmails ++;
        },

        removeEmployeEmails() {
            this.countEmployeEmails --;
        },

        


        next(page) {
            let skip = page - 1;
            axios
                .get(
                    "employes/getEmployes?skip=" +
                        skip +
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
                    this.employes = res.data.result.employesToReturn;
                })
                .catch(err => {});
        },

        close() {
            this.dialog = false;
            this.dialogEditUser = false;
            this.employe = {};
            this.password = "";
            this.$refs.form.reset();
            this.$refs.form.resetValidation();
            this.selectOrg = [];
        },
    }
};
</script>

<style></style>
