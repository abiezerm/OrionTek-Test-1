import Vue from "vue";
import App from "./App.vue";
import router from "./router";
import { Auth0Plugin } from "./auth";
import hljs from "highlight.js/lib/core";
import "highlight.js/styles/github.css";

import { library } from "@fortawesome/fontawesome-svg-core";
import { faLink, faUser, faPowerOff } from "@fortawesome/free-solid-svg-icons";
import { FontAwesomeIcon } from "@fortawesome/vue-fontawesome";
import { domain, clientId, audience } from "../auth_config.json";
import Vuex from 'vuex';
import VueSweetalert2 from 'vue-sweetalert2';
import 'sweetalert2/dist/sweetalert2.min.css';

Vue.use(VueSweetalert2);

Vue.config.productionTip = false;

Vue.use(hljs.vuePlugin);

Vue.use(Vuex);

const store = new Vuex.Store({
    state: {
        token: null
    },
    mutations: {
        setToken(state, token) {
            state.token = token;
            window.console.log('save on localstore')
            window.localStorage.setItem('token', token)
        }
    },
    getters: {
        getToken(state) {
            return state.token;
        }
    }
})

Vue.use(Auth0Plugin, {
    domain,
    clientId,
    audience,
    onRedirectCallback: appState => {
        router.push(
            appState && appState.targetUrl ?
            appState.targetUrl :
            window.location.pathname
        );
    }
});

library.add(faLink, faUser, faPowerOff);
Vue.component("font-awesome-icon", FontAwesomeIcon);

new Vue({
    router,
    store,
    render: h => h(App)
}).$mount("#app");