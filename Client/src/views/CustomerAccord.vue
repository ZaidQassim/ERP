<template>
    <div>
        <v-card flat>
            <v-card-title class="mb-4">
               
                <v-btn text
                    outlined
                    depressed
                    color="primary" @click="dialog = true">
                    <v-icon class="ml-1">mdi-plus-circle</v-icon>
                    تسجيل ذمة خارجيه
                </v-btn>
                <v-divider class="mx-4" inset vertical> </v-divider>
                <small>
                    أدارة  ذمــة الخارجيــه
                </small>
                <v-spacer></v-spacer>
                <v-spacer></v-spacer>
                <v-spacer></v-spacer>

                <!--  search -->
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
                <!--  ./ search -->
                
            </v-card-title>

            <v-data-table
                :headers="headerCustomerAccords"
                :items="customerAccords"
                loader-height="10"
                :search="search"
                loading-text="جاري جميع ذمة خارجيه "
                page.sync="page"
                @click:row="employeDetails"
                dense
                no-data-text="لا يوجد اي ذمة خارجيه مسجله "
                :items-per-page="itemsPerPage"
                hide-default-footer>
                
                <template v-slot:[`item.imageUrl`]>
                    <v-avatar color="bluegrey" class="ma-1" size="35">
                        <v-img src="../assets/img/user.svg"></v-img>
                    </v-avatar>
                </template>

                <template v-slot:[`item.age`]="{ item }">
                    <span> {{ item.age }} سنه </span>
                </template>

                <template v-slot:[`item.gender`]="{ item }">
                    <span v-if="item.gender == 'male'">ذكر </span>
                    <span v-if="item.gender == 'female'">أنثى </span>
                </template>

                <template v-slot:[`item.accord.type`]="{ item }">
                    <span class="primary--text font-weight-bold">
                        {{ item.accord.type }}
                    </span>
                </template>

                <template v-slot:[`item.accord.dateOfReceiving`]="{ item }">
                    <span>
                        {{ item.accord.dateOfReceiving | formatDate }}
                    </span>
                </template>

                <template v-slot:[`item.accord.dateOfDelivery`]="{ item }">
                    <span class="primary--text">
                        {{ item.accord.dateOfDelivery | formatDate }}
                    </span>
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
                        <v-card-title> تسجيل ذمة خارجيه  جديده </v-card-title>
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

                    <v-card-text style="min-height: 500px;" class="px-3 ma-0">
                        <v-form ref="form" v-model="valid" lazy-validation>
                            <v-stepper v-model="e1">
                                <v-stepper-header>
                                    <v-stepper-step
                                        complete editable
                                        step="1">
                                        المعلومات الشخصيه للزبون صاحب الذمة
                                        <small class="mt-1">معلومات مطلوبه وضروريه</small>
                                    </v-stepper-step>

                                    <v-divider></v-divider>

                                    <v-stepper-step
                                        editable
                                        step="2"
                                        :complete="e1 > 1">
                                         تفاصيل و معلومات الذمة 
                                        <small class="mt-1" v-if="customerAccord.thirdName">
                                            سوف تسجل هذا الذمه باسم هذا الزبون
                                            <span class="green--text">
                                                {{ customerAccord.firstName }}
                                                {{ customerAccord.secondName }}
                                                {{ customerAccord.thirdName }}
                                            </span>
                                        </small>
                                    </v-stepper-step>
                                    <v-spacer></v-spacer>
                                    <v-spacer></v-spacer>
                                </v-stepper-header>

                                <v-stepper-items>
                                    <v-stepper-content :step="1">

                                        <v-row no-gutters>
                                            <v-col cols="12" md="3" class="pl-2">
                                                <small class="">
                                                    الاسم الاول للزيون
                                                    <span class="red--text font-weight-bold">*</span>
                                                </small>
                                                <v-text-field
                                                    v-model="customerAccord.firstName"
                                                    :rules="requiredRules"
                                                    required
                                                    outlined
                                                    dense
                                                    color="secondary"
                                                    filled
                                                    placeholder=" اكتب الاسم الاول للزيون">
                                                </v-text-field>
                                            </v-col>

                                            <v-col
                                                cols="12"
                                                md="3"
                                                class="pl-2">
                                                <small class="">
                                                    الاسم الثاني
                                                    <span
                                                        class="red--text font-weight-bold"
                                                        >*</span>
                                                </small>
                                                <v-text-field
                                                    v-model="customerAccord.secondName"
                                                    :rules="requiredRules"
                                                    required
                                                    outlined
                                                    dense
                                                    color="secondary"
                                                    filled
                                                    placeholder="اكتب الاسم الثاني للزيون ">
                                                </v-text-field>
                                            </v-col>

                                            <v-col
                                                cols="12"
                                                md="3"
                                                class="pl-2">
                                                <small class="">
                                                    الاسم الثالث
                                                    <span
                                                        class="red--text font-weight-bold"
                                                        >*</span>
                                                </small>
                                                <v-text-field
                                                    v-model="customerAccord.thirdName"
                                                    :rules="requiredRules"
                                                    required
                                                    outlined
                                                    dense
                                                    color="secondary"
                                                    filled
                                                    placeholder="اكتب الاسم الثالث للزيون">
                                                </v-text-field>
                                            </v-col>

                                            <v-col
                                                cols="12"
                                                md="3"
                                                class="pl-2">
                                                <small class="">
                                                    الاسم الرابع
                                                </small>
                                                <v-text-field
                                                    v-model="customerAccord.fourthName"
                                                    required
                                                    outlined
                                                    dense
                                                    color="secondary"
                                                    filled
                                                    placeholder="الاسم الرابع للزيون">
                                                </v-text-field>
                                            </v-col>
                                        </v-row>                               

                                        <v-row no-gutters class="mt-n2">
                                            <v-col cols="12" md="3" class="pl-2">
                                                <small>
                                                    اللقب
                                                </small>
                                                <v-text-field
                                                    v-model="customerAccord.family"
                                                    required
                                                    outlined
                                                    dense
                                                    color="secondary"
                                                    filled
                                                    placeholder="اللقب">
                                                </v-text-field>
                                            </v-col>

                                            <v-col
                                                cols="12"
                                                md="4"
                                                class="pl-2">
                                                <small>
                                                    تاريخ الميلاد
                                                </small>
                                                <v-menu
                                                    ref="menudateOfBirth"
                                                    v-model="menudateOfBirth"
                                                    :close-on-content-click="
                                                        false
                                                    "
                                                    :return-value.sync="
                                                        dateOfBirth
                                                    "
                                                    transition="scale-transition"
                                                    offset-y
                                                    min-width="290px"
                                                >
                                                    <template
                                                        v-slot:activator="{
                                                            on,
                                                            attrs
                                                        }"
                                                    >
                                                        <v-text-field
                                                            v-model="
                                                                dateOfBirth
                                                            "
                                                            append-icon="mdi-calendar"
                                                            readonly
                                                            :rules="
                                                                requiredRules
                                                            "
                                                            outlined
                                                            filled
                                                            color="secondary"
                                                            dense
                                                            v-bind="attrs"
                                                            v-on="on"
                                                        >
                                                        </v-text-field>
                                                    </template>

                                                    <v-date-picker
                                                        v-model="dateOfBirth"
                                                        no-title
                                                        color="secondary"
                                                        scrollable
                                                    >
                                                        <v-btn
                                                            text
                                                            color="info"
                                                            @click="
                                                                $refs.menudateOfBirth.save(
                                                                    dateOfBirth
                                                                )
                                                            "
                                                        >
                                                            حسناً
                                                        </v-btn>
                                                        <v-btn
                                                            text
                                                            color="error"
                                                            @click="
                                                                menu = false
                                                            "
                                                        >
                                                            الغاء
                                                        </v-btn>
                                                        <v-spacer></v-spacer>
                                                    </v-date-picker>
                                                </v-menu>
                                            </v-col>

                                            <v-col cols="12" md="3" class="pl-2">
                                                <small>
                                                    جنس الزبون
                                                </small>
                                                <v-select
                                                    v-model="customerAccord.gender"
                                                    :items="gender"
                                                    item-text="name"
                                                    item-value="value"
                                                    filled
                                                    outlined
                                                    label="أختار الجنس"
                                                    dense>
                                                </v-select>
                                            </v-col>
                                        </v-row>

                                        <v-row no-gutters class="mt-n2">
                                            <v-col
                                                cols="12" md="6">
                                                <small>
                                                    هل تريد ادراج  مرفقات أو مستمسكات للزبون ؟ 
                                                   <br>
                                                    <span class="green--text"
                                                        >هوية أحوال</span
                                                    >
                                                    ,
                                                    <span class="green--text"
                                                        >بطاقة وطنية موحدة</span
                                                    >
                                                    ,
                                                    <span class="green--text"
                                                        >بطاقه سكن</span
                                                    >,
                                                    <span class="green--text"
                                                        >جواز سفر</span
                                                    >,
                                                </small>
                                                <v-file-input
                                                    @change="handleCustomerAttachmentsUpload()"
                                                    v-model="customerAttachments"
                                                    multiple
                                                    counter
                                                    :rules="requiredRules"
                                                    chips
                                                    show-size
                                                    accept="image/png, image/jpeg, image/bmp, image/jpg, .pdf"
                                                    color="green"
                                                    :loading="loadingCustomerAttachment"
                                                    append-icon="mdi-paperclip"
                                                    prepend-icon=""
                                                    label="حدد اي من المستمسكات و المرفقات المراد رفعها"
                                                    outlined
                                                    filled/>
                                            </v-col>
                                        </v-row>

                                        <v-row no-gutters class="mt-n2">
                                            <v-col cols="12" md="4" class="pl-2" 
                                                v-for="(item, i) in coountCustomerTelephoneNumber" :key="i">
                                                <small v-if="i == 0">
                                                    رقم موبايل الزبون
                                                    <span
                                                        class="red--text font-weight-bold"
                                                        >*</span>
                                                    "على الاقل يجب كتابه
                                                    <b>رقم موبايل</b> واحد"
                                                </small>
                                                <small v-if="i > 0">
                                                    رقم موبايل أضافي
                                                </small>
                                                <div>
                                                    <v-btn
                                                        v-if="i == 0"
                                                        depressed
                                                        @click="addEmployeTelephoneNumber()"
                                                        class="float-left mt-1"
                                                        color="success"
                                                        small
                                                        icon>
                                                        <v-icon
                                                            >mdi-plus-circle</v-icon>
                                                    </v-btn>
                                                    <v-btn
                                                        v-if="i > 0"
                                                        depressed
                                                        @click="removeEmployeTelephoneNumber()"
                                                        class="float-left mt-1"
                                                        color="error"
                                                        small
                                                        icon>
                                                        <v-icon>mdi-minus-circle</v-icon>
                                                    </v-btn>
                                                    <v-text-field
                                                        v-model="customerTelephoneNumbers[i]"
                                                        required
                                                        :rules="requiredRules"
                                                        type="number"
                                                        outlined
                                                        dense
                                                        class="mb-n2"
                                                        color="secondary"
                                                        filled
                                                        placeholder="0000 0000 078">
                                                    </v-text-field>
                                                </div>
                                            </v-col>
                                        </v-row>

                                        <v-row no-gutters class="mt-n2">
                                            <v-col
                                                cols="12" md="4" class="pl-2"
                                                v-for="(item, i) in countCustomerEmails" :key="i">
                                                <small v-if="i == 0">
                                                    البريد الاكتروني للزبون
                                                    على الاقل يجب كتابه
                                                    <b>بريد أكتروني</b> واحد
                                                </small>
                                                <small v-if="i > 0">
                                                    بريد الاكتروني أضــافي
                                                </small>

                                                <div>
                                                    <v-btn
                                                        v-if="i == 0"
                                                        depressed
                                                        @click="addEmployeEmails()"
                                                        class="float-left mt-1"
                                                        color="success"
                                                        small
                                                        icon>
                                                        <v-icon>mdi-plus-circle</v-icon>
                                                    </v-btn>
                                                    <v-btn
                                                        v-if="i > 0"
                                                        depressed
                                                        @click="removeEmployeEmails()"
                                                        class="float-left mt-1"
                                                        color="error"
                                                        small
                                                        icon>
                                                        <v-icon
                                                            >mdi-minus-circle</v-icon
                                                        >
                                                    </v-btn>
                                                    <v-text-field
                                                        v-model="customerEmails[i]"
                                                        :rules="emailRules"
                                                        required
                                                        outlined
                                                        dense
                                                        class="mb-n2"
                                                        color="secondary"
                                                        filled
                                                        placeholder="zaid@... .com"
                                                    >
                                                    </v-text-field>
                                                </div>
                                            </v-col>
                                        </v-row>
                                    </v-stepper-content>

                                    <v-stepper-content :step="2">
                                        <v-row no-gutters class="mt-4">
                                            <v-col cols="12" md="4" class="pl-2">
                                                <small class="font-weight-bold">
                                                    تســلســل الذمة 
                                                    <span class="red--text font-weight-bold">*</span>
                                                </small>
                                                <v-text-field
                                                    v-model="customerAccord.sequenceNumber"
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
                                                    v-model="customerAccord.type"
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
                                                    <span class="green--text">ملف pdf</span>
                                                    او
                                                    <span class="green--text">صوره</span>
                                                </small>    
                                                <v-file-input
                                                    @change="handleCustomerAccordAttachment1Upload()"
                                                    v-model="customerAccordAttachment1"
                                                    multiple
                                                    counter
                                                    :rules="requiredRules"
                                                    chips
                                                    show-size
                                                    accept="image/png, image/jpeg, image/bmp, image/jpg, .pdf"
                                                    color="error"
                                                    :loading="loadingCustomerAccordAttachment1"
                                                    append-icon="mdi-paperclip"
                                                    prepend-icon=""
                                                    label="حدد المستمسكات المراد رفعها"
                                                    outlined
                                                    filled/>
                                            </v-col>

                                            <v-col cols="12" md="5" class="pl-2">
                                                <small class="font-weight-bold">
                                                    نسخة من طلب التقديم على الذمة ونسخة من وصل القبض والاستلام
                                                    <span class="green--text">ملف pdf</span>
                                                    ,
                                                    <span class="green--text">صوره</span>
                                                </small>
                                                <v-file-input
                                                    @change="handleCustomerAccordAttachment2Upload()"
                                                    v-model="customerAccordAttachment2"
                                                    multiple
                                                    counter
                                                    chips
                                                    show-size
                                                    accept="image/png, image/jpeg, image/bmp, image/jpg, .pdf"
                                                    color="error"
                                                    :loading="loadingCustomerAccordAttachment2"
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
                                                    <span class="green--text">اي ملاحضات في خصوص هذا الذمة</span>
                                                </small>                                     
                                                <vue-editor
                                                    v-model="customerAccords.description"
                                                    :editorToolbar="customToolbar"
                                                ></vue-editor>
                                            </v-col>
                                        </v-row>

                                    </v-stepper-content>
                                </v-stepper-items>
                            </v-stepper>
                        </v-form>
                    </v-card-text>

                    <v-divider></v-divider>
                    <v-card-actions>
                        <v-btn
                            :disabled="e1 != 2"
                            color="primary"
                            text
                            @click="e1 = 1">
                            <v-icon>mdi-chevron-double-right</v-icon>
                            السابِق
                        </v-btn>

                        <v-btn
                            :disabled="e1 != 1"
                            color="primary"
                            text
                            @click="e1 = e1 + 1">
                            التالي
                            <v-icon>mdi-chevron-double-left</v-icon>
                        </v-btn>

                        <v-btn color="error" text @click="close()">
                            إلغاء
                        </v-btn>

                        <v-btn
                            :disabled="
                                !customerAccord.firstName ||
                                !customerAccord.secondName ||
                                !customerAccord.thirdName ||
                                !customerAccord.family ||
                                !dateOfBirth ||
                                !dateOfReceiving ||
                                !customerAccordAttachment1[0] ||
                                !customerTelephoneNumbers[0]"
                            :loading="loadingBtnAdd"
                            @click="addCustomerAccord()"
                            color="success"
                            type="submit"
                            depressed
                            class="px-4"
                            elevation="2"
                            raised>
                            حفظ
                            <v-icon class="mr-2"
                                >mdi-content-save-cog-outline</v-icon
                            >
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
            dialog: false,
            loadingBtnAdd: false,
            isfullscreen: false,
            search: "",
            page: 1,
            pageCount: 0,
            itemsPerPage: 10,
            headerCustomerAccords: [
                {
                    text: "صورة الزبزن",
                    value: "imageUrl",
                    align: "center",
                    sortable: false
                },                
                {
                    text: "أسم الزبون الاول ",
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
                    text: "العمر",
                    value: "age"
                },
                {
                    text: "الجنس",
                    value: "gender"
                },
                {
                    text: "رقم الموبايل",
                    value: "telephoneNumber"
                },
                {
                    text: "نوع الذمة المٌسمة",
                    value: "accord.type"
                },
                {
                    text: "تــسلسل الذمه",
                    value: "accord.sequenceNumber"
                },
                {
                    text: "تاريخ استلام الذمة",
                    value: "accord.dateOfReceiving"
                },
                {
                    text: "تاريخ تسليم الذمة",
                    value: "accord.dateOfDelivery"
                },
            ],
            customerAccords: [],
            customerAccord: {},
            e1: 1,
            menudateOfBirth: false,
            dateOfBirth: new Date().toISOString().substr(0, 10),
            menudateOfReceiving: false,
            dateOfReceiving: new Date().toISOString().substr(0, 10),
            menuDateOfDelivery: false,
            dateOfDelivery: null,
            gender: [
                {
                    name: "ذكر",
                    value: "male"
                },
                {
                    name: "انثى",
                    value: "female"
                }
            ],
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
            customToolbar: [["bold", "italic", "underline"], [{ list: "ordered" }, { list: "bullet" }], ["image", "code-block"],
                [{ align: "left" }, { align: "center" }, { align: "right"}, { align: "justify"}],
                [{ color: "color-picker" }]],

            loadingCustomerAttachment: false,
            customerAttachments: [],
            loadingCustomerAccordAttachment1: false,
            customerAccordAttachment1: [],
            loadingCustomerAccordAttachment2: false,
            customerAccordAttachment2: [],

            customerEmails: [],
            countCustomerEmails: 1,
            customerTelephoneNumbers: [],
            coountCustomerTelephoneNumber: 1,

            // valdation input
            valid: true,
            requiredRules: [v => !!v || "رجاءاً هذا الحقل مطلوب "],
            emailRules: [v => /.+@.+\..+/.test(v) || "E-mail must be valid"],
        };
    },

    components: {
        VueEditor
    },

    created() {
        this.getCustomerAccords();
    },

    methods: {
        //  get All Customer Accords
        getCustomerAccords() {
            this.$store.commit("SET_LOADING", true);
            axios
                .get(
                    "customerAccords/getCustomerAccords?skip=0" +
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
                    this.customerAccords = res.data.result.customerAccordsToReturn;
                    let pageOfCount = Math.ceil(res.data.result.count / 10);
                    this.pageCount = parseInt(pageOfCount);
                    this.$store.commit("SET_LOADING", false);
                })
                .catch(err => {});
        },


        addCustomerAccord() {
            this.$store.commit("SET_LOADING", true);
            this.$refs.form.validate()
            this.loadingBtnAdd = true;

            let customerAccordAttachmentsSelected = "";
            if (
                this.customerAccord.customerAccordAttachment1 != null &&
                this.customerAccord.customerAccordAttachment2 != null
            ) {
                customerAccordAttachmentsSelected = this.customerAccord.customerAccordAttachment1.concat(
                    this.customerAccord.customerAccordAttachment2
                );
            } else {
                customerAccordAttachmentsSelected = this.customerAccord.customerAccordAttachment2;
            }

            let telephoneNumbers = [];
            this.customerTelephoneNumbers.forEach(element => {
                telephoneNumbers.push({
                    number: element
                });
            });

            let emails = [];
            this.customerEmails.forEach(element => {
                emails.push({
                    email: element
                });
            });

            const data = {
                firstName: this.customerAccord.firstName,
                secondName: this.customerAccord.secondName,
                thirdName: this.customerAccord.thirdName,
                fourthName: this.customerAccord.fourthName,
                family: this.customerAccord.family,
                dateOfBirth: this.dateOfBirth,
                gender: this.customerAccord.gender,
                customerAttachments: this.customerAccord.customerAttachments,
                customerEmails: emails,
                customerTelephoneNumbers: telephoneNumbers,
                type: this.customerAccord.type,
                sequenceNumber: this.customerAccord.sequenceNumber,
                description: this.customerAccord.description,
                dateOfReceiving: this.dateOfReceiving,
                dateOfDelivery: this.dateOfDelivery,
                customerAccordAttachments: customerAccordAttachmentsSelected

            };

            axios
                .post("customerAccords/addCustomerAccord", data, {
                    headers: {
                        Authorization: `Bearer ${localStorage.getItem("token")}`
                    }
                })
                .then(res => {
                    this.close();
                    this.$swal.fire({
                        icon: "success",
                        title: "تم تسجيل الموظف بنجاح",
                        confirmButtonText: "حسناً"
                    });
                    this.getCustomerAccords();
                })
                .catch(err => {
                    this.$store.dispatch("setSnackbar", {
                        color: "error",
                        text: `${err.message}`
                    });
                })
                .finally(() => {
                    this.$store.commit("SET_LOADING", true);
                    this.loadingBtnAdd = false;
                });
        },

        employeDetails(item) {
            this.$router.push({
                path: `/employeDetails/${item.id}`,
                params: { id: item.id }
            });
        },


        handleCustomerAttachmentsUpload() {
            var paths = [];
            this.loadingCustomerAttachment = true;
            let formData = new FormData();
            for (const i of Object.keys(this.customerAttachments)) {
                formData.append("files", this.customerAttachments[i]);
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
                .finally(() => (this.loadingCustomerAttachment = false));
            this.customerAccord.customerAttachments = paths;
        },


        handleCustomerAccordAttachment1Upload() {
            var paths = [];
            this.loadingCustomerAccordAttachment1 = true;
            let formData = new FormData();
            for (const i of Object.keys(this.customerAccordAttachment1)) {
                formData.append("files", this.customerAccordAttachment1[i]);
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
                .finally(() => (this.loadingCustomerAccordAttachment1 = false));
            this.customerAccord.customerAccordAttachment1 = paths;
        },

        handleCustomerAccordAttachment2Upload() {
            var paths = [];
            this.loadingCustomerAccordAttachment2 = true;
            let formData = new FormData();
            for (const i of Object.keys(this.customerAccordAttachment2)) {
                formData.append("files", this.customerAccordAttachment2[i]);
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
                .finally(() => (this.loadingCustomerAccordAttachment2 = false));
            this.customerAccord.customerAccordAttachment2 = paths;
        },
        

        addEmployeTelephoneNumber() {
            this.coountCustomerTelephoneNumber++;
        },

        removeEmployeTelephoneNumber() {
            this.coountCustomerTelephoneNumber--;
        },

        addEmployeEmails() {
            this.countCustomerEmails++;
        },

        removeEmployeEmails() {
            this.countCustomerEmails--;
        },


        next(page) {
            let skip = page - 1;
            axios
                .get(
                    "customerAccords/getCustomerAccords?skip=" +
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
                    this.customerAccords = res.data.result.customerAccordsToReturn;
                })
                .catch(err => {});
        },

        close() {
            this.dialog = false;
            this.customerAccord = {};
            this.$refs.form.reset();
            this.$refs.form.resetValidation();
            this.customerEmails = [];
            this.countCustomerEmails = 1;
            this.customerTelephoneNumbers = [];
            this.coountCustomerTelephoneNumber = 1;
            this.e1 = 1
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
