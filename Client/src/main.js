import Vue from "vue";
import App from "./App.vue";
import router from "./router";
import store from "./store/store";
import vuetify from "./plugins/vuetify";
import VueSweetalert2 from "vue-sweetalert2";
import "sweetalert2/dist/sweetalert2.min.css";
import axios from "axios";
import style from "./style/style.scss";
import "./filters"
import VueCardFormat from "vue-credit-card-validation";


Vue.use(VueSweetalert2);
Vue.use(VueCardFormat);
Vue.config.productionTip = false;
Vue.prototype.$eventBus = new Vue();

new Vue({
    style,
    axios,
    router,
    vuetify,
    render: h => h(App),
    store,    
}).$mount("#app");
