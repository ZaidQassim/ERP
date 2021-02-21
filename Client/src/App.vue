<template>
    <v-app
        class="app"
        :style="{ background: $vuetify.theme.themes[theme].background }">
        <layout />

        <v-main>
            <v-progress-linear
                color=""
                indeterminate
                reverse
            ></v-progress-linear>

            <v-container fluid class="mt-n2">
                <transition
                    name="router-anim"
                    enter-active-class="animated zoomIn">
                    <router-view />
                </transition>
            </v-container>
        </v-main>

        <v-snackbar
            v-model="snackbar.showing"
            :timeout="3000"
            left
            :color="snackbar.color">
            {{ snackbar.text }}
            <template v-slot:action="{ attrs }">
                <v-btn
                    color="blue"
                    text
                    v-bind="attrs"
                    @click="snackbar.vshowing = false"
                >
                    Close
                </v-btn>
            </template>
        </v-snackbar>
    </v-app>
</template>

<script>
import { mapState } from "vuex";
import layout from "./components/layout";
export default {
    data() {
        return {
           
        };
    },
    components: {
        layout
    },
    computed: {
        ...mapState(["snackbar"]),
        theme() {
            return this.$vuetify.theme.dark ? "dark" : "light";
        }
    }
};
</script>

<style>
@import "https://cdn.jsdelivr.net/npm/animate.css@3.5.1";

.fade-enter-active,
.fade-leave-active {
    transition: opacity 0.1s;
}

.fade-enter,
.fade-leave-to {
    opacity: 0;
}

/* #app {
    background: #f8f9fc;
} */
</style>
