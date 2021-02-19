import Vue from "vue";

import moment from "moment";

Vue.filter("formatDate", function(value) {
    if (value) {
        return moment().format("l");
    }
});

Vue.filter("formatDateAgo", function(value) {
    if (value) {
        return moment(String(value)).fromNow(); // 4 years
    }
});

Vue.filter("uperCase", function(v) {
    return v.toUpperCase();
});
