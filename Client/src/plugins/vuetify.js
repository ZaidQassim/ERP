import Vue from 'vue';
import Vuetify from 'vuetify/lib';
import '../style/style.scss';
import 'material-design-icons-iconfont/dist/material-design-icons.css';

Vue.use(Vuetify);

export default new Vuetify({
    rtl: true,
    icons: {
        iconfont: "md"
    },
    theme: {
        themes: {
            light: {
                primary: "#f6b93b",
                secondary: "#2196F3",
                third: "#3F3D56",
                accent: "#F8F8F8",
                success: "#218c74",
                error: "#D32F2F",
                info: "#F2B43C"
            }
        }
    }
});
