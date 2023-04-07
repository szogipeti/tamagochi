<template>
    <div class="card mx-auto my-5 p-3" v-if="!loggedIn.isLoggedIn">
        <h2 class="card-title my-1">Jelentkezz be, hogy megnézd a háziállatod!</h2>
        <router-link class="btn btn-primary mx-auto my-1" to="/login">Bejelentkezés</router-link>
    </div>
</template>

<script setup>
import {RouterLink} from 'vue-router';
import {useLoggedInStore} from "../store/isLoggedIn";

const loggedIn = useLoggedInStore();
</script>
<style scoped>
.card{
    width: 700px;
}
router-link{
    width: 20em;
}
</style>
