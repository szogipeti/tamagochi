import {createRouter, createWebHashHistory} from 'vue-router';
import {authGuard} from "./guards/AuthGuard.js";

const routes = [
    {
        path: '/',
        name: 'index',
        component: () => import('@/pages/IndexPage.vue'),
        meta: {
            title: "Főoldal",
            requriesAuth: false
        }
    },
    {
        path: '/login',
        name: 'login',
        component: () => import('@/pages/LoginPage.vue'),
        meta: {
            title: "Bejelentkezés",
            requiresAuth: false
        }
    },
    {
        path: '/register',
        name: 'register',
        component: () => import('@/pages/RegisterPage.vue'),
        meta: {
            title: "Regisztráció",
            requiresAuth: false
        }
    },
    {
        path: '/animals/select',
        name: 'animal_select',
        component: () => import('@/pages/AnimalSelectionPage.vue'),
        meta:{
            title: "Válaszd ki a háziállatodat",
            requiresAuth: true
        }
    },
    {
        name: 'profile',
        path: '/profile',
        component: Profile,
        meta: {
            requiresAuth: true,
        }
    }
]

export const router = createRouter({
  history: createWebHashHistory(),
  routes
});
router.beforeEach(authGuard);
