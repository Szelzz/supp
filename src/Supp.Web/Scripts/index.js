import 'milligram/dist/milligram.css'
import '../Styles/main.scss'
import Vue from 'vue'
import App from './main.vue'

Vue.config.productionTip = false

/* eslint-disable no-new */
new Vue(App).$mount('#app')


console.log('test')