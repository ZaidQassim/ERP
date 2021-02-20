<template>
    <div>
        <v-card flat tile>
            <v-card-title>
                أدارة الموظفين
                <v-divider
                    class="mx-4"
                    inset
                    vertical>
                </v-divider>
                <v-spacer></v-spacer>
                <v-spacer></v-spacer>
                <v-spacer></v-spacer>
                <v-text-field
                    v-model="search"
                    append-icon="mdi-magnify"
                    label="أبحث هنا "
                    dense
                    solo
                    hide-details>
                </v-text-field>
            </v-card-title>

            <!-- btn add user  -->
            <v-card-text>
                <v-btn depressed color="primary" @click="dialog = true">
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
                loading-text="جاري جميع الموظفين "
                page.sync="page"
                dense
                no-data-text="لا يوجد موظفين "
                :items-per-page="itemsPerPage"
                hide-default-footer>

                <template v-slot:[`item.imageUrl`]>
                    <v-avatar
                        color="bluegrey"
                        class="ma-1"
                        size="35">
                        <v-img src="../assets/img/user.svg"></v-img>
                    </v-avatar>
                </template>


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
                        color="green lighten-2"
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

                    <v-card-text style="min-height: 500px;" class="px-3 ma-0">
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
                                                    @change="handleEmployeAttachmentsUpload()"
                                                    v-model="employeAttachments"
                                                    multiple
                                                    counter
                                                    chips dense
                                                    show-size
                                                    accept="image/png, image/jpeg, image/bmp, image/jpg, .pdf"
                                                    color="primary"
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
                                            <v-col cols="12" md="4" class="pl-2" v-for="(item, i) in coountEmployeTelephoneNumber" :key="i">
                                                <small v-if="i == 0" >
                                                    رقم موبايل   
                                                    <span class="red--text font-weight-bold">*</span>
                                                    "على الاقل يجب كتابه <b>رقم موبايل</b> واحد"
                                                </small>
                                                <small v-if="i > 0">
                                                    رقم موبايل أضافي
                                                </small>
                                                <div>
                                                    <v-btn v-if="i == 0" depressed @click="addEmployeTelephoneNumber()"
                                                        class="float-left mt-1" 
                                                        color="success" small icon> 
                                                        <v-icon>mdi-plus-circle</v-icon>
                                                    </v-btn>
                                                    <v-btn v-if="i > 0" depressed @click="removeEmployeTelephoneNumber()"
                                                        class="float-left mt-1" 
                                                        color="error" small icon> 
                                                        <v-icon>mdi-minus-circle</v-icon>
                                                    </v-btn>
                                                    <v-text-field 
                                                        v-model="employeTelephoneNumbers[i]"
                                                        required
                                                        outlined
                                                        dense class="mb-n2"
                                                        color="secondary"
                                                        filled
                                                        placeholder="0000 0000 078">
                                                    </v-text-field>
                                                </div>
                                                 
                                            </v-col>
                                          
                                        </v-row>

                                        <v-row no-gutters class="mt-n2">
                                            <v-col cols="12" md="4" class="pl-2" 
                                                 v-for="(item, i) in countEmployeEmails" :key="i">
                                                <small v-if="i == 0" >
                                                    البريد الاكتروني  
                                                    <span class="red--text font-weight-bold">*</span>
                                                    على الاقل يجب كتابه <b>بريد أكتروني</b> واحد
                                                </small>
                                                <small v-if="i > 0">
                                                    بريد الاكتروني أضــافي
                                                </small>

                                                <div>
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
                                                        placeholder="zaid@... .com">
                                                    </v-text-field>
                                                </div>
                                            </v-col>
                                        </v-row>

                                        <v-row no-gutters>
                                            <v-img 
                                                class="card"
                                                src="../assets/card.svg">
                                            </v-img>
                                            <v-col cols="12" md="8" class="pl-2">
                                                <v-alert border="left" text :color="employePaymentCards.type == 2 ? '' : 'primary'"
                                                    :class="employePaymentCards.type == 1 ? 'yellow lighten-1' : '' 
                                                    || employePaymentCards.type == 2 ? 'green lighten-2' : ''
                                                    || employePaymentCards.type == 3 ? 'black white--text' : ''">
                                                    <small>
                                                        بطاقة دفع مصرفية  لمصرف معين او محفظة الكترونية 
                                                        <span class="red--text font-weight-bold">*</span>
                                                        <small>
                                                            على الاقل أضف بطاقة دفع واحده
                                                        </small>                                                            
                                                    </small>

                                                    <v-row no-gutters class="mt-2">
                                                        <v-col cols="12" md="3" class="pl-2">
                                                            <small>
                                                                نوع البطاقه 
                                                                <span class="red--text font-weight-bold">*</span>
                                                            </small>
                                                            <v-select
                                                                v-model="employePaymentCards.type"
                                                                :items="typeEmployePaymentCard"
                                                                item-text="name"
                                                                item-value="value"
                                                                solo outlined 
                                                                :rules="requiredRules"
                                                                label="نوع البطاقه"
                                                                dense>
                                                            </v-select>
                                                        </v-col>

                                                        <v-col cols="12" md="3" class="pl-2">
                                                            <small>
                                                                تاريخ انتهاء الصلاحيه 
                                                            </small>
                                                            <v-text-field
                                                                v-model="employePaymentCards.dateExpired"
                                                                v-cardformat:formatCardExpiry
                                                                outlined
                                                                solo 
                                                                color="secondary"
                                                                dense>
                                                            </v-text-field>
                                                        </v-col>  

                                                        <v-col cols="12" md="6">
                                                            <small>
                                                                عنوان البطاقه
                                                            </small>
                                                            <v-text-field 
                                                                v-model="employePaymentCards.name"
                                                                required
                                                                outlined solo
                                                                dense
                                                                color="secondary"
                                                                placeholder="عنوان البطاقه">
                                                            </v-text-field>
                                                        </v-col>
                                                    </v-row>

                                                    <v-row no-gutters class="mt-n3">
                                                        <v-col cols="12" md="10" class="pl-2">
                                                            <small>
                                                                رقم البطاقه
                                                                <span class="red--text font-weight-bold">*</span>
                                                            </small>
                                                            <v-text-field 
                                                                v-model="employePaymentCards.numberCard"
                                                                required
                                                                v-cardformat:formatCardNumber
                                                                outlined solo
                                                                dense class="mb-n2"
                                                                color="secondary"
                                                                placeholder="0000 1111 2222 3333 4444">
                                                            </v-text-field>
                                                        </v-col>
                                                        <v-col cols="12" md="2">
                                                            <small>
                                                                الرمز السري 
                                                            </small>
                                                            <v-text-field 
                                                                v-model="employePaymentCards.code"
                                                                required
                                                                v-cardformat:formatCardCVC
                                                                counter="4"
                                                                outlined solo
                                                                dense class="mb-n2"
                                                                color="secondary"
                                                                placeholder="0000">
                                                            </v-text-field>
                                                        </v-col>
                                                    </v-row>                                                   
                                                </v-alert>
                                            </v-col>
                                        </v-row>

                                    </v-stepper-content>

                                    <v-stepper-content :step="2">
                                        <v-row no-gutters class="mt-n2">
                                            <v-col cols="12" md="4" class="pl-2">
                                                <small>
                                                    تاريخ بدء العمل (المباشرة)
                                                    <span class="red--text font-weight-bold">*</span>
                                                </small>
                                                <v-menu
                                                    ref="menu"
                                                    v-model="menudateOfStartWork"
                                                    :close-on-content-click="false"
                                                    :return-value.sync="dateOfStartWork"
                                                    transition="scale-transition"
                                                    offset-y
                                                    min-width="290px">
                                                    <template v-slot:activator="{ on, attrs }">
                                                        <v-text-field
                                                            v-model="dateOfStartWork"
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
                                                        v-model="dateOfStartWork"
                                                        no-title
                                                        color="secondary"
                                                        scrollable>
                                                        <v-btn
                                                            text
                                                            color="info"
                                                            @click="$refs.menu.save(dateOfStartWork)">
                                                            حسناً
                                                        </v-btn>
                                                        <v-btn
                                                            text
                                                            color="error"
                                                            @click="menudateOfStartWork = false">
                                                            الغاء
                                                        </v-btn>
                                                        <v-spacer></v-spacer>
                                                    </v-date-picker>
                                                </v-menu>
                                            </v-col>

                                            <v-col cols="12" md="4" class="pl-2">
                                                <small>
                                                    تاريخ توقيع العقد 
                                                    <span class="red--text font-weight-bold">*</span>
                                                </small>
                                                <v-menu
                                                    ref="menu"
                                                    v-model="menudateofSigningContract"
                                                    :close-on-content-click="false"
                                                    :return-value.sync="dateofSigningContract"
                                                    transition="scale-transition"
                                                    offset-y
                                                    min-width="290px">
                                                    <template v-slot:activator="{ on, attrs }">
                                                        <v-text-field
                                                            v-model="dateofSigningContract"
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
                                                        v-model="dateofSigningContract"
                                                        no-title
                                                        color="secondary"
                                                        scrollable>
                                                        <v-btn
                                                            text
                                                            color="info"
                                                            @click="$refs.menu.save(dateofSigningContract)">
                                                            حسناً
                                                        </v-btn>
                                                        <v-btn
                                                            text
                                                            color="error"
                                                            @click="menudateofSigningContract = false">
                                                            الغاء
                                                        </v-btn>
                                                        <v-spacer></v-spacer>
                                                    </v-date-picker>
                                                </v-menu>
                                            </v-col>
                                        </v-row>

                                        <v-row no-gutters class="mt-n2">
                                            <v-col cols="12" md="3" class="pl-2">
                                                <small>
                                                    الحالة   
                                                    <span class="red--text font-weight-bold">*</span>
                                                </small>
                                                <v-select
                                                    v-model="employe.state"
                                                    :items="employestate"
                                                    item-text="name"
                                                    item-value="value"
                                                    filled outlined
                                                    placeholder="أختار حالة الموظف"
                                                    dense>
                                                </v-select>                                               
                                            </v-col>

                                            <v-col cols="12" md="3" class="pl-2" v-if="employe.state == 2">
                                                <small>
                                                    تاريخ التوقف 
                                                    <span class="red--text font-weight-bold">*</span>
                                                </small>
                                                <v-text-field
                                                    :disabled="employe.state != 2"
                                                    v-model="employe.firstName"
                                                    required
                                                    outlined
                                                    dense
                                                    color="secondary"
                                                    filled
                                                    placeholder="18 / 02 / 2021">
                                                </v-text-field>
                                            </v-col>

                                            <v-col cols="12" md="6" class="pl-2" v-if="employe.state == 2">
                                                <small>
                                                    سبب التوقف 
                                                    <span class="red--text font-weight-bold">*</span>
                                                </small>
                                                <v-text-field
                                                    :disabled="employe.state != 2"
                                                    v-model="employe.firstName"
                                                    required
                                                    outlined
                                                    dense
                                                    color="secondary"
                                                    filled
                                                    placeholder="سبب توقف الموظف عن العمل ">
                                                </v-text-field>
                                            </v-col>
                                        </v-row>

                                        <v-row no-gutters class="mt-n2">
                                            <v-col cols="12" md="5" class="pl-2">
                                                <small>
                                                    نسخة من عقد العمل (صوره او ملف pdf ) 
                                                    <span class="red--text font-weight-bold">*</span>
                                                </small>
                                                <v-file-input
                                                    @change="handleEmployeAttachmentsUpload()"
                                                    v-model="employeAttachments"
                                                    multiple
                                                    counter
                                                    chips
                                                    show-size
                                                    accept="image/png, image/jpeg, image/bmp, image/jpg, .pdf"
                                                    color="primary"
                                                    :loading="emAtloading"
                                                    append-icon="mdi-paperclip"
                                                    prepend-icon=""
                                                    label="حدد الملفات المراد رفعها"
                                                    outlined
                                                    filled
                                                    hide-details="auto"/>
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
                            v-if="e1 == 1"
                            color="primary"
                            text
                            @click="e1 = e1 + 1">
                            التالي
                        </v-btn>
                        <v-btn
                            v-if="e1 == 2"
                            color="primary"
                            text
                            @click="e1 = 1">
                            السابِق
                        </v-btn>

                        <v-btn color="error" text @click="close()">
                            إلغاء
                        </v-btn>

                        <v-btn
                            v-if="e1 == 2"
                            :loading="false"
                            @click="addEmploye()"
                            color="success"
                            type="submit"
                            depressed
                            elevation="2"
                            outlined
                            raised> 
                            حفظ
                            <v-icon class="mr-2">mdi-content-save-cog-outline</v-icon>
                        </v-btn>                        

                        <v-spacer></v-spacer>
                        <span class="red--text font-weight-bold">*</span>
                        <span class="caption"> هذا الحقل مطلوب</span>
                    </v-card-actions>
                </v-card>
            </v-dialog>
        </v-row>
        <!-- ./ form dialog add employe -->
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
                    value: "imageUrl",
                    align: "center",
                    sortable: false,
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
                    text: "حالة الموظف",
                    value: "state"
                },
                {
                    text: "تاريخ بداء العمل",
                    value: "dateOfStartWork"
                },
                {
                    text: "تاريخ توقيع العقد",
                    value: "dateofSigningContract"
                },
                
            ],
            employes: [],
            dialog: false,
            e1: 1,
            employe: {},
            menu: false,
            menudateOfStartWork: false,
            menudateofSigningContract: false,
            dateOfBirth: new Date().toISOString().substr(0, 10),
            dateOfStartWork: new Date().toISOString().substr(0, 10),
            dateofSigningContract: new Date().toISOString().substr(0, 10),
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
            employeTelephoneNumbers: [],
            coountEmployeTelephoneNumber: 1,
            employePaymentCards: {},
            typeEmployePaymentCard: [
                {
                    name: "كي كارد",
                    value: 1
                },
                {
                    name: "ماستر كارد",
                    value: 2
                },
                {
                    name: "زين كاش",
                    value: 3
                },
                {
                    name: "بطاقة سوج",
                    value: 4
                }
            ],
            employestate: [
                {
                    name: "مستمر",
                    value: 1
                },
                {
                    name: "متوقف",
                    value: 2
                }
            ],


             // valdation input
            valid: true,
            requiredRules: [v => !!v || "رجاءاً هذا الحقل مطلوب "],
            emailRules: [v => /.+@.+\..+/.test(v) || "E-mail must be valid"],

            paymentCardRules: [
                v => !!v || ' !! هذا الحق مطلوب',
                v => !!v && v.length == 16 || 'رقم البطاقه الاكترونيه يجب أن يكون 16 رقم',
            ],



            user: {},
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
            this.$store.commit("SET_LOADING", true);

            // const data = {
            //     title: this.employe.firstName,
            //     documentNumber: this.document.documentNumber,
            //     description: this.document.description,
            //     orgId: this.id,
            //     documentAttachments: documentAttachmentsSelected,
            //     attachments: this.document.Attachments,
            //     lastDate: this.date,
            //     isSecure: this.isSecure,
            //     passcode: this.document.passcode
            // };
            let paymentCards = [];
            paymentCards.push(this.employePaymentCards);
            this.employe.employePaymentCards = paymentCards;

            let telephoneNumbers = [];
                this.employeTelephoneNumbers.forEach((element) => {
                    telephoneNumbers.push({
                        number: element,
                    });
                });
            this.employe.employeTelephoneNumbers = telephoneNumbers;

            let emails = [];
                this.employeEmails.forEach((element) => {
                    emails.push({
                        email: element,
                    });
                });
            this.employe.employeEmails = emails;

            axios
                .post("employes/addEmploye", this.employe, {
                    headers: {
                        Authorization: `Bearer ${localStorage.getItem("token")}`
                    }
                })
                .then(res => {
                    this.employe = {};
                    this.close();
                    this.getEmployes();
                })
                .catch(err => {
                })
                .finally(() =>{
                    this.$store.commit("SET_LOADING", true);
                });
        },



        handleEmployeAttachmentsUpload() {
            var paths = [];
            this.emAtloading = true;
            let formData = new FormData();
            for (const i of Object.keys(this.employeAttachments)) {
                formData.append("files", this.employeAttachments[i]);
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
                        // console.log(element + index);
                    });
                })
                .catch(err => {})
                .finally(() => (this.emAtloading = false));
            this.employe.employeAttachments = paths;
        },


        addEmployeTelephoneNumber() {
            this.coountEmployeTelephoneNumber ++;
        },

        removeEmployeTelephoneNumber() {
            this.coountEmployeTelephoneNumber --;
        },

        addEmployeEmails() {
            this.countEmployeEmails ++;
        },

        removeEmployeEmails() {
            this.countEmployeEmails --;
        },

        addEmployePaymentCard() {
            this.countEmployePaymentCard++;
        },

        removeEmployePaymentCard() {
            this.countEmployePaymentCard --;
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
            this.employe = {};
            this.$refs.form.reset();
            this.$refs.form.resetValidation();
            this.employeEmails=  [];
            this.countEmployeEmails= 1;
            this.employeTelephoneNumbers= [];
            this.coountEmployeTelephoneNumber= 1;
        },
    }
};
</script>

<style>
.card { 
    position: absolute;
    right: 57%;
    z-index: 1;
    opacity: .6;
}

</style>
