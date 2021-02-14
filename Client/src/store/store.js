import Vue from "vue";
import Vuex from "vuex";
import axios from "../axios";

Vue.use(Vuex);

const store = new Vuex.Store({
    state: {
        snackbar: {},
        userId: localStorage.getItem("userID")
    },

    getters: {},

    mutations: {
        SET_SNACKBAR(state, snackbar) {
            state.snackbar = snackbar;
        }
    },

    actions: {
        setSnackbar({ commit }, snackbar) {
            snackbar.showing = true;
            snackbar.color = snackbar.color || "#424242";
            commit("SET_SNACKBAR", snackbar);
        }
    }
});

export default store;
