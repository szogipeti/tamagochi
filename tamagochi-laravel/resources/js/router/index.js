import {createRouter, createWebHashHistory} from 'vue-router';

const routes = [
    {
        path: '/',
        name: 'index',
        component: () => import('@/pages/IndexPage.vue'),
        meta: {
            title: "Főoldal",
            requiesAuth: false
        }
    },
    {
        path: '/login',
        name: 'login',
        component: () => import('@/pages/LoginPage.vue'),
        meta: {
            title: "Bejelentkezés",
            requiesAuth: false
        }
    }
]

export const router = createRouter({
  history: createWebHashHistory(),
  routes
});
