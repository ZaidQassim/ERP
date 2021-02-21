<template>
    <div>
        <v-card flat>
            <v-card-title class="mb-2">
                <v-btn
                    text
                    outlined
                    depressed
                    color="primary"
                    @click="dialog = true">
                    <v-icon class="ml-1">mdi-plus-circle</v-icon>
                    تسجيل ذمه لموظف في الشركه
                </v-btn>
                <v-divider class="mx-4 my-1" vertical> </v-divider>
                <v-spacer></v-spacer>

                <div class="search">
                    <input type="text" placeholder="أبحث هنا" v-model="search" />
                    <div class="symbol">
                        <svg class="cloud">
                            <use xlink:href="#cloud" />
                        </svg>
                        <svg class="lens">
                            <use xlink:href="#lens" />
                        </svg>
                    </div>
                </div>
                <!-- SVG -->
                <svg xmlns="http://www.w3.org/2000/svg" style="display: none;">
                    <symbol
                        xmlns="http://www.w3.org/2000/svg"
                        viewBox="0 0 35 35"
                        id="cloud"
                    >
                        <path
                            d="M31.714,25.543c3.335-2.17,4.27-6.612,2.084-9.922c-1.247-1.884-3.31-3.077-5.575-3.223h-0.021
	C27.148,6.68,21.624,2.89,15.862,3.931c-3.308,0.597-6.134,2.715-7.618,5.708c-4.763,0.2-8.46,4.194-8.257,8.919
	c0.202,4.726,4.227,8.392,8.991,8.192h4.873h13.934C27.784,26.751,30.252,26.54,31.714,25.543z"
                        />
                    </symbol>
                    <symbol
                        xmlns="http://www.w3.org/2000/svg"
                        viewBox="0 0 16 16"
                        id="lens"
                    >
                        <path
                            d="M15.656,13.692l-3.257-3.229c2.087-3.079,1.261-7.252-1.845-9.321c-3.106-2.068-7.315-1.25-9.402,1.83
	s-1.261,7.252,1.845,9.32c1.123,0.748,2.446,1.146,3.799,1.142c1.273-0.016,2.515-0.39,3.583-1.076l3.257,3.229
	c0.531,0.541,1.404,0.553,1.95,0.025c0.009-0.008,0.018-0.017,0.026-0.025C16.112,15.059,16.131,14.242,15.656,13.692z M2.845,6.631
	c0.023-2.188,1.832-3.942,4.039-3.918c2.206,0.024,3.976,1.816,3.951,4.004c-0.023,2.171-1.805,3.918-3.995,3.918
	C4.622,10.623,2.833,8.831,2.845,6.631L2.845,6.631z"
                        />
                    </symbol>
                </svg>

            </v-card-title>

            <v-data-table
                :headers="headerEmployeAccords"
                :items="employeAccords"
                loader-height="10"
                :search="search"
                loading-text="جاري جميع ذمة الموظفين "
                page.sync="page"
                @click:row="employeDetails"
                dense
                no-data-text="لا يوجد اي ذمه  لموظفين الشركه "
                :items-per-page="itemsPerPage"
                hide-default-footer>

                <template v-slot:[`item.dateOfReceiving`]="{ item }">
                    <span class="primary--text">
                        {{ item.dateOfReceiving | formatDate }}
                    </span>
                </template>

                <template v-slot:[`item.dateOfDelivery`]="{ item }">
                    <span class="error--text">
                        {{ item.dateOfDelivery | formatDate }}
                    </span>
                </template>

                <template v-slot:[`item.employeFullName`]="{ item }">
                    {{ item.employe.firstName }} {{ item.employe.secondName }}
                    {{ item.employe.thirdName }} {{ item.employe.fourthName }}
                </template>

                 <template v-slot:[`item.employe.state`]="{ item }">
                    <span class="green--text text-decoration-underline" 
                        v-if="item.employe.state == 1">مستمر في الوظيفه
                    </span>
                    <span class="error--text text-decoration-underline" 
                        v-if="item.employe.state == 2">متوقف عن الوظيف 
                    </span>
                </template>

                 <template v-slot:[`item.attachments`]="{ item }">
                    <v-avatar
                        v-if="item.attachments > 9"
                        class="my-2"
                        color="third"
                        size="22">
                        +9
                    </v-avatar>
                    <v-avatar 
                        v-if="item.attachments < 9"
                        class="my-2"
                        color="third"
                        size="22">
                        {{ item.attachments }}
                    </v-avatar>
                </template>

            </v-data-table>

            <v-divider></v-divider>

            <v-pagination
                v-model="page"
                :length="pageCount"
                @input="next"
                :total-visible="7"
                class="py-6">
            </v-pagination>
        </v-card>

        <!-- form dialog add employe -->
        <v-row justify="center">
            <v-dialog
                v-model="dialog"
                max-width="70%"
                scrollable
                :fullscreen="isfullscreen"
                persistent
                transition="dialog-bottom-transition">
                <v-card class="pa-0 ma-0">
                    <v-toolbar flat dense>
                        <v-card-title class="secondary--text"> تسجيل ذمة جديده </v-card-title>
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

                    <v-card-text style="min-height: 500px;">
                        <v-form ref="form" v-model="valid" lazy-validation>

                            <v-row no-gutters class="mt-4">
                                <v-col cols="12" md="6" class="pl-2">
                                    <small class="font-weight-bold">
                                        الموظف صاحب الذمة 
                                        <span class="red--text font-weight-bold">*</span>
                                    </small>
                                    <v-autocomplete
                                        clearable
                                        v-model="employeId"
                                        :items="lisEmployes"
                                        :rules="requiredRules"
                                        item-text="fullName"
                                        item-value="id"
                                        cache-items
                                        color="green"
                                        flat outlined
                                        dense filled
                                        no-data-text="الموظف غير موجود"
                                        label="ابحث عن اسم الموظف صاحب الذمة هنا">
                                    </v-autocomplete>
                                </v-col>  

                                <v-col cols="12" md="4" class="pl-2">
                                    <small class="font-weight-bold">
                                        تســلســل الذمة 
                                        <span class="red--text font-weight-bold">*</span>
                                    </small>
                                    <v-text-field
                                        v-model="employeAccord.sequenceNumber"
                                        :rules="requiredRules"
                                        required
                                        outlined
                                        dense
                                        color="secondary"
                                        filled
                                        placeholder="1 2 0 1 2">
                                    </v-text-field>
                                </v-col>                        
                            </v-row>

                            <v-row no-gutters class="mt-n2">
                                <v-col cols="12" md="4" class="pl-2">
                                    <small class="font-weight-bold">
                                        نوع الذمة
                                        <span class="red--text font-weight-bold">*</span>
                                    </small>
                                    <v-select
                                        v-model="employeAccord.type"
                                        :items="typeAccords"
                                        :rules="requiredRules"
                                        item-text="name"
                                        item-value="value"
                                        filled
                                        outlined
                                        label="أختار نوع الذمة"
                                        dense>
                                    </v-select>
                                </v-col>
                                
                                <v-col cols="12" md="4" class="pl-2">
                                    <small class="font-weight-bold">
                                        تاريخ استلام الذمة
                                        <span class="red--text font-weight-bold">*</span>
                                    </small>
                                    <v-menu
                                        ref="menudateOfReceiving"
                                        v-model="menudateOfReceiving"
                                        :close-on-content-click="false"
                                        :return-value.sync="dateOfReceiving"
                                        transition="scale-transition"
                                        offset-y
                                        min-width="290px">
                                        <template
                                            v-slot:activator="{ on, attrs }">
                                            <v-text-field
                                                v-model="dateOfReceiving"
                                                append-icon="mdi-calendar"
                                                readonly
                                                :rules="requiredRules"
                                                outlined
                                                filled
                                                color="secondary"
                                                dense
                                                v-bind="attrs"
                                                v-on="on">
                                            </v-text-field>
                                        </template>

                                        <v-date-picker
                                            v-model="dateOfReceiving"
                                            no-title
                                            color="secondary"
                                            scrollable>
                                            <v-btn
                                                text color="info"
                                                @click="$refs.menudateOfReceiving.save(dateOfReceiving)">
                                                حسناً
                                            </v-btn>
                                            <v-btn
                                                text
                                                color="error"
                                                @click=" menudateOfReceiving = false " >
                                                الغاء
                                            </v-btn>
                                            <v-spacer></v-spacer>
                                        </v-date-picker>
                                    </v-menu>
                                </v-col>

                                 <v-col cols="12" md="4" class="pl-2">
                                    <small class="font-weight-bold">
                                        تاريخ تســليـم الذمة
                                        <span class="red--text font-weight-bold">*</span>
                                        <span class="font-weight-bold">
                                            في حالة فارغ يعني ان الذمة مستمرة 
                                        </span>
                                    </small>
                                    <v-menu
                                        ref="menuDateOfDelivery"
                                        v-model="menuDateOfDelivery"
                                        :close-on-content-click="false"
                                        :return-value.sync="dateOfDelivery"
                                        transition="scale-transition"
                                        offset-y
                                        min-width="290px">
                                        <template
                                            v-slot:activator="{ on, attrs }">
                                            <v-text-field
                                                v-model="dateOfDelivery"
                                                append-icon="mdi-calendar"
                                                readonly
                                                :rules="requiredRules"
                                                outlined
                                                filled
                                                color="secondary"
                                                dense
                                                v-bind="attrs"
                                                v-on="on">
                                            </v-text-field>
                                        </template>

                                        <v-date-picker
                                            v-model="dateOfDelivery"
                                            no-title
                                            color="secondary"
                                            scrollable>
                                            <v-btn
                                                text color="info"
                                                @click="$refs.menuDateOfDelivery.save(dateOfDelivery)">
                                                حسناً
                                            </v-btn>
                                            <v-btn
                                                text
                                                color="error"
                                                @click=" menuDateOfDelivery = false " >
                                                الغاء
                                            </v-btn>
                                            <v-spacer></v-spacer>
                                        </v-date-picker>
                                    </v-menu>
                                </v-col>                               
                            </v-row>

                            <v-row no-gutters class="mt-n2">
                                <v-col cols="12" md="5" class="pl-2">
                                    <small class="font-weight-bold">
                                        نسخىة من الذمة
                                        <span class="red--text font-weight-bold ml-1">*</span>
                                        <span class="red--text">ملف pdf</span>
                                        او
                                        <span class="red--text">صوره</span>
                                    </small>    
                                    <v-file-input
                                        @change="handleEmployeAccordAttachmentsUpload1()"
                                        v-model="employeAccordAttachment1"
                                        multiple
                                        counter
                                        :rules="requiredRules"
                                        chips
                                        show-size
                                        accept="image/png, image/jpeg, image/bmp, image/jpg, .pdf"
                                        color="error"
                                        :loading="employeAccordAttachloading"
                                        append-icon="mdi-paperclip"
                                        prepend-icon=""
                                        label="حدد المستمسكات المراد رفعها"
                                        outlined
                                        filled/>
                                </v-col>

                                <v-col cols="12" md="5" class="pl-2">
                                    <small class="font-weight-bold">
                                        نسخة من طلب التقديم على الذمة ونسخة من وصل القبض والاستلام
                                        <span class="red--text font-weight-bold ml-1">*</span>
                                        <span class="red--text">ملف pdf</span>
                                        او
                                        <span class="red--text">صوره</span>
                                    </small>
                                    <v-file-input
                                        @change="handleEmployeAccordAttachmentsUpload2()"
                                        v-model="employeAccordAttachment2"
                                        multiple
                                        counter
                                        chips
                                        show-size
                                        accept="image/png, image/jpeg, image/bmp, image/jpg, .pdf"
                                        color="error"
                                        :loading="employeAccordAttachloading"
                                        append-icon="mdi-paperclip"
                                        prepend-icon=""
                                        label="حدد المستمسكات المراد رفعها"
                                        outlined
                                        filled/>
                                </v-col>
                            </v-row>

                            <v-row no-gutters>
                                <v-col cols="12" md="12">
                                    <small class="font-weight-bold mb-2">
                                        اكتب وصفاً لهذا الذمة 
                                        <span class="red--text font-weight-bold ml-1">*</span>
                                        <span class="red--text">اي ملاحضات في خصوص هذا الذمة</span>
                                    </small>                                     
                                    <vue-editor
                                        v-model="employeAccord.description"
                                        :editorToolbar="customToolbar"
                                    ></vue-editor>
                                </v-col>
                            </v-row>

                        </v-form>
                    </v-card-text>

                    <v-divider></v-divider>
                    <v-card-actions>

                        <v-btn color="error" text @click="close()">
                            إلغاء
                        </v-btn>

                        <v-btn
                            :loading="loadingbnt"
                            @click="addEmployeAccord()"
                            color="success"
                            type="submit"
                            depressed
                            class="px-4"
                            elevation="2"
                            raised>
                            حفظ
                            <v-icon class="mr-2">
                                mdi-content-save-cog-outline
                            </v-icon>
                        </v-btn>

                        <v-spacer></v-spacer>
                        <small>
                            <span class="red--text font-weight-bold">*</span>
                            تعني أن هذا الحقل مطلوب
                        </small>
                    </v-card-actions>
                </v-card>
            </v-dialog>
        </v-row>
        <!-- ./ form dialog add employe -->
    </div>
</template>

<script>
import axios from "../axios";
import { VueEditor } from "vue2-editor";

export default {
    data() {
        return {
            isfullscreen: false,
            search: "",
            page: 1,
            pageCount: 0,
            itemsPerPage: 11,
            headerEmployeAccords: [
                {
                    text: "#",
                    value: "id"
                },
                {
                    text: "نوع الذمه",
                    value: "type"
                },
                {
                    text: "تسلسل الذمة",
                    value: "sequenceNumber"
                },
                {
                    text: "وصف الذمة",
                    value: "description"
                },
                {
                    text: "تاريخ أستلام الذمة",
                    value: "dateOfReceiving"
                },
                {
                    text: "تاريخ تسليم الذمة",
                    value: "dateOfDelivery"
                },
                {
                    text: "الاسم الكامل لصاحب الذمة ",
                    value: "employeFullName"
                },
                {
                    text: "حالة صاحب الذمة",
                    value: "employe.state"
                },
                {
                    text: "ملفات ومرفقات الذمة",
                    value: "attachments",
                    align: "center",
                    sortable: false
                },
            ],
            employeAccords: [],
            employeAccord: {},
            dialog: false,
            lisEmployes: [],
            employeId: null,
            typeAccords: [
                {
                    name: "جهاز حاسوب",
                    value: "1"
                },
                {
                    name: "شاشة PC",
                    value: "2"
                },
                {
                    name: "ماوس",
                    value: "3"
                },
                {
                    name: "أيباد",
                    value: "4"
                },
                {
                    name: "كيبورد",
                    value: "5"
                },
                {
                    name: "اخرى",
                    value: "6"
                }
            ],
            menudateOfReceiving: false,
            dateOfReceiving: new Date().toISOString().substr(0, 10),
            menuDateOfDelivery: false,
            dateOfDelivery: null,
            employeAccordAttachloading: false,
            loadingbnt: false,
            customToolbar: [["bold", "italic", "underline"], [{ list: "ordered" }, { list: "bullet" }], ["image", "code-block"],
                [{ align: "left" }, { align: "center" }, { align: "right"}, { align: "justify"}],
                [{ color: "color-picker" }]],

            
            employeAccordAttachment1: [],
            employeAccordAttachment2: [],

            // valdation input
            valid: true,
            requiredRules: [v => !!v || "رجاءاً هذا الحقل مطلوب "],
        };
    },
    components: {
        VueEditor
    },

    created() {
        this.getEmployeAccords();
        this.getlistEmployes();
    },

    methods: {
        //  get All Employe Accords
        getEmployeAccords() {
            this.$store.commit("SET_LOADING", true);
            axios
                .get(
                    "employeAccords/getEmployeAccords?skip=0" +
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
                    this.employeAccords = res.data.result.employeAccordsToReturn;
                    let pageOfCount = Math.ceil(res.data.result.count / 10);
                    this.pageCount = parseInt(pageOfCount);
                    this.$store.commit("SET_LOADING", false);
                })
                .catch(err => {});
        },

         //  get All Employes
        getlistEmployes() {
            axios
                .get(
                    "employes/listEmployes", {
                        headers: {
                            Authorization: `Bearer ${localStorage.getItem(
                                "token"
                            )}`
                        }
                    }
                )
                .then(res => {
                    this.lisEmployes = res.data.result.listemployes;
                })
                .catch(err => {});
        },


        addEmployeAccord() {
            this.$store.commit("SET_LOADING", true);
            this.$refs.form.validate();
            this.loadingbnt = true;

            let employeAccordAttachmentsSelected = "";
            if (
                this.employeAccord.employeAccordAttachment1 != null &&
                this.employeAccord.employeAccordAttachment2 != null
            ) {
                employeAccordAttachmentsSelected = this.employeAccord.employeAccordAttachment1.concat(
                    this.employeAccord.employeAccordAttachment2
                );
            } else {
                employeAccordAttachmentsSelected = this.employeAccord.employeAccordAttachment2;
            }

            
            const data = {
                employeId: this.employeId,
                type: this.employeAccord.type,
                sequenceNumber: this.employeAccord.sequenceNumber,
                description: this.employeAccord.description,
                dateOfReceiving: this.dateOfReceiving,
                dateOfDelivery: this.dateOfDelivery,
                employeAccordAttachments: employeAccordAttachmentsSelected,
            };

            axios
                .post("employeAccords/addEmployeAccord", data, {
                    headers: {
                        Authorization: `Bearer ${localStorage.getItem("token")}`
                    }
                })
                .then(res => {
                    this.close();
                    this.$swal.fire({
                        icon: "success",
                        title: "تم تسجيل ذمة للموظف بنجاح",
                        confirmButtonText: "حسناً"
                    });
                    this.getEmployeAccords();
                })
                .catch(err => {
                    this.$store.dispatch("setSnackbar", {
                        color: "error",
                        text: `${err.message}`
                    });
                })
                .finally(() => {
                    this.$store.commit("SET_LOADING", true);
                    this.loadingbnt = false;
                });
        },

        employeDetails(item) {
            this.$router.push({
                path: `/employeDetails/${item.id}`,
                params: { id: item.id }
            });
        },

        handleEmployeAccordAttachmentsUpload1() {
            var paths = [];
            this.employeAccordAttachloading = true;
            let formData = new FormData();
            for (const i of Object.keys(this.employeAccordAttachment1)) {
                formData.append("files", this.employeAccordAttachment1[i]);
            }
            axios
                .post("uploadFile/postUploadFiles", formData, {
                    headers: {
                        Authorization: `Bearer ${localStorage.getItem("token")}`
                    }
                })
                .then(res => {
                    let filePaths = res.data.fileList;
                    filePaths.forEach((element, index) => {
                        paths.push({
                            path: element.paht,
                            name: element.name,
                            type: "1"
                        });
                    });
                })
                .catch(err => {})
                .finally(() => (this.employeAccordAttachloading = false));
            this.employeAccord.employeAccordAttachment1 = paths;
        },

        handleEmployeAccordAttachmentsUpload2() {
            var paths = [];
            this.employeAccordAttachloading = true;
            let formData = new FormData();
            for (const i of Object.keys(this.employeAccordAttachment2)) {
                formData.append("files", this.employeAccordAttachment2[i]);
            }
            axios
                .post("uploadFile/postUploadFiles", formData, {
                    headers: {
                        Authorization: `Bearer ${localStorage.getItem("token")}`
                    }
                })
                .then(res => {
                    let filePaths = res.data.fileList;
                    filePaths.forEach((element, index) => {
                        paths.push({
                            path: element.paht,
                            name: element.name,
                            type: "2"
                        });
                    });
                })
                .catch(err => {})
                .finally(() => (this.employeAccordAttachloading = false));
            this.employeAccord.employeAccordAttachment2 = paths;
        },

        next(page) {
            let skip = page - 1;
            axios
                .get(
                    "employeAccords/getEmployeAccords?skip=" +
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
                    this.employeAccords = res.data.result.employeAccordsToReturn;
                })
                .catch(err => {});
        },

        close() {
            this.dialog = false;
            this.employe = {};
            this.$refs.form.reset();
            this.$refs.form.resetValidation();
        }
    }
};
</script>

<style>
.card {
    position: absolute;
    right: 57%;
    z-index: 1;
    opacity: 0.6;
}
</style>
