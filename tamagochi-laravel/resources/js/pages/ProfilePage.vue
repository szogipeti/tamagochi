<template>
    <div class="container">
        <div class="row">
            <div id="profdiv">
                <Form class="fromdata">
                    <font-awesome-icon class="profile" icon="fa-solid fa-circle-user" />
                    <h1 class="profile">{{ user.username }}</h1>
                    <p id="email">Email: {{ user.email }}</p>
                    <router-link class="buttons" to="">
                        Háziállat visszaállítása
                    </router-link>
                    <button id="btnlogout" class="buttons" @click="logout">Kijelentkezés</button>
                </Form>
            </div>
        </div>
    </div>
</template>

<script setup>
import {onMounted, reactive, ref} from 'vue';
import {useRouter} from 'vue-router';
import {Form, Field} from 'vee-validate';
import {http} from "../utils/http";
import {FontAwesomeIcon} from "@fortawesome/vue-fontawesome";
import {useLoggedInStore} from "../store/isLoggedIn.js";

const isLoggedInStore = useLoggedInStore();

const router = useRouter();
const user = reactive({});

const logout = () => {
    isLoggedInStore.logout();
    router.push('/login');
}
defineExpose({logout});

async function getUserData() {
    const userdata = await http.get("profile",{headers: {Authorization: `Bearer ${localStorage.getItem('token')}`}})
    for (const key in userdata.data) {
        user[key] = userdata.data[key]
    }
}


onMounted(() => {
    getUserData();
})
</script>

<style scoped>
.fa-circle-user{
    width: 100px;
    height: 100px;
}

.buttons{
    display: block;
    width: 100%;
    border: 2px solid #18afa5;
    background-color: #18afa5;
    padding: 14px 28px;
    font-size: 150%;
    text-align: center;
    color: black;
    text-decoration: none;
}
#btnlogout{
    width: 70%;
    margin-left: auto;
    margin-right: auto;
    margin-top: 20px;
    padding-top: 5px;
    padding-bottom: 5px;
}
#email{
    width: 100%;
    text-align: center;
    font-size: 150%;
    background-color: white;
    border: 2px solid #18afa5;
    margin-bottom: 20px;

}
.profile{
    width: 100%;
    text-align: center;
    font-size: 150%;
    margin-top: 10px;
}
#profdiv{
    background-color: gainsboro;
    border: 2px solid lightgray;
    margin-top: 20px;
    margin-bottom: 30px;
    box-shadow:10px 10px 10px darkgrey;
    left: 15%;
}
</style>
