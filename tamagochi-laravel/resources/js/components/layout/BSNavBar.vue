<template>
  <nav class="navbar navbar-expand-lg">
    <div class="container-fluid">
      <router-link class="navbar-brand profil" to="/">Tamagochi</router-link>
        <div class="d-flex order-lg-1 justify-content-end align-items-center">
            <div class="d-flex align-items-center ms-auto ms-md-0 profil">
                <router-link v-if="!loggedIn.isLoggedIn" to="/login" class="nav-link mx-2" >Bejelentkezés</router-link>
                <router-link v-else to="" class="d-flex justify-content-end align-items-center">
                    <p class="ms-1 my-auto me-2">{{loggedIn.username}}</p>
                    <font-awesome-icon class="mx-2" :icon="['fas', 'fa-circle-user']" size="2xl" />
                </router-link>
            </div>
            <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarSupportedContent"
                    aria-controls="navbarSupportedContent" aria-expanded="false" aria-label="Toggle navigation">
                <span class="navbar-toggler-icon"></span>
            </button>
        </div>
      <div class="collapse navbar-collapse order-0" id="navbarSupportedContent">
        <ul class="navbar-nav me-auto mb-2 mb-lg-0">
          <li class="nav-item profil">
            <a class="nav-link active" href="#">Főoldal</a>
          </li>
          <li class="nav-item profil">
            <a class="nav-link" href="#">Link</a>
          </li>
        </ul>
      </div>
    </div>
  </nav>
</template>
<script setup>
import {FontAwesomeIcon} from "@fortawesome/vue-fontawesome";
import {useLoggedInStore} from "../../store/isLoggedIn";

const loggedIn = useLoggedInStore();
</script>

<style scoped>
a{
    text-decoration: none;
    color: #323232;
}
.profil:hover{
  font-weight: bolder;
}
.profil:active{
  font-weight: bold;
}
nav{
  background-color: #DDD0C8;
}
</style>
