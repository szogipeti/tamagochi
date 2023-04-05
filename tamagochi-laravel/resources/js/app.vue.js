import {createApp} from 'vue';
import {createPinia} from 'pinia';
import {router} from '@/router/index.js'
import BSAlert from '@/components/layout/BSAlert.vue'
import App from '@/App.vue';

import {library} from '@fortawesome/fontawesome-svg-core'
import {FontAwesomeIcon} from '@fortawesome/vue-fontawesome'
import {faCircleUser} from '@fortawesome/free-solid-svg-icons'
library.add(faCircleUser)

const app = createApp(App);
app.use(createPinia());
app.use(router);
app.component('ErrorComponent', BSAlert);
app.component('font-awesome-icon', FontAwesomeIcon)
app.mount("#app");
