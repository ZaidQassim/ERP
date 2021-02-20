import Vue from "vue";
import Vuetify from "vuetify/lib";
import "../style/style.scss";
import "material-design-icons-iconfont/dist/material-design-icons.css";

Vue.use(Vuetify);

export default new Vuetify({
    rtl: true,
    icons: {
        iconfont: "md"
    },
    theme: {
        themes: {
            light: {
                primary: "#4e89ae",
                secondary: "#526C83",
                third: "#4e89ae",
                accent: "#ed6663",
                success: "#4CAF50",
                error: "#D32F2F",
                info: "#2196F3",
                bluegrey: "#f7f9fd",
                background: "#f7f9fd"
            },
            dark: {
                primary: "#4e89ae",
                secondary: "#e74c3c",
                third: "#E91E63",
                accent: "#E91E63",
                success: "#4CAF50",
                error: "#E91E63",
                info: "#4e89ae",
                bluegrey: "#2f3640",
                background: "#2f3640"
            }
        },
        options: { customProperties: true }
    }
});
