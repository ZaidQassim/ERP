import Vue from "vue";
import Vuex from "vuex";
import axios from "../axios";

Vue.use(Vuex);

const store = new Vuex.Store({
    state: {
        snackbar: {},
        userId: localStorage.getItem("userID"),
        loading: false
    },

    getters: {},

    mutations: {
        SET_SNACKBAR(state, snackbar) {
            state.snackbar = snackbar;
        },
        SET_LOADING(state, loading) {
            state.loading = loading;
        }
    },

    actions: {
        setSnackbar({ commit }, snackbar) {
            snackbar.showing = true;
            snackbar.color = snackbar.color || "#43658b";
            commit("SET_SNACKBAR", snackbar);
        },
    }
});

export default store;
