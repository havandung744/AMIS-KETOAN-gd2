import { createApp } from 'vue'
import App from './App.vue'
import Paginate from "vuejs-paginate-next";

import router from './js/router'
import axios from 'axios'
import VueAxios from 'vue-axios'
import ElementPlus from 'element-plus'
import 'element-plus/dist/index.css'
import vi from 'element-plus/es/locale/lang/vi'

const app = createApp(App);
app.use(ElementPlus, {
    locale: vi,
})

app.use(router, VueAxios, axios, Paginate).mount('#app')