<template>
    <div class="container">
        <div class="row">
            <div id="profdiv">
                <Form class="fromdata">
                    <font-awesome-icon class="profile" icon="fa-solid fa-circle-user" />
                    <h1 class="profile">{{ user.username }}</h1>
                    <p id="email" class="mx-auto">Email: {{ user.email }}</p>
                    <div class="nagy">
                        <button v-if="animalStore.animalId !== null" class="buttons" @click="resetAnimal">
                        Háziállat visszaállítása
                        </button>
                        <router-link v-else class="buttons" to="/animals/select">
                            Háziállat létrehozása
                        </router-link>
                    </div>
                    <div class=" nagy">
                        <button id="btnlogout" class="buttons" @click="logout">Kijelentkezés</button>
                    </div>
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
import {useAnimalStore} from "../store/animal";

const isLoggedInStore = useLoggedInStore();
const animalStore = useAnimalStore();

const router = useRouter();
const user = reactive({});

const logout = () => {
    isLoggedInStore.logout();
    router.push('/login');
}

const resetAnimal = function (){
    http.delete(`/animals/stats/${animalStore.animalId}`)
    animalStore.removeAnimal();
    router.push('animals/select');
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
    left: 50%;
    margin-top: 10px;
    margin-bottom: 10px;
    outline: 0;
    background: 0 0;
    background-color: #d49167;
    border: 1px solid #323232;
    border-radius: 0;
    cursor: pointer;
    display: inline-flex;
    font-size: 20px;
    overflow: hidden;
    padding: 12px 16px;
    text-decoration: none;
    text-overflow: ellipsis;
    transition: all .14s ease-out;
    white-space: nowrap;

}
button:hover {
    box-shadow: 4px 4px 0 #323232;
    transform: translate(-4px,-4px);
}
a{
    margin-top: 10px;
    margin-bottom: 10px;
    outline: 0;
    background: 0 0;
    background-color: #d49167;
    color: #323232;
    border: 1px solid #323232;
    border-radius: 0;
    cursor: pointer;
    display: inline-flex;
    font-size: 20px;
    overflow: hidden;
    padding: 12px 16px;
    text-decoration: none;
    text-overflow: ellipsis;
    transition: all .14s ease-out;
    white-space: nowrap;
}
a:hover{
    box-shadow: 4px 4px 0 #323232;
    transform: translate(-4px,-4px);
}
#btnlogout{
    width: 30%;
}
#email{
    width: 50%;
    text-align: center;
    font-size: 150%;
    background-color: #DDD0C8;
    border: 2px solid #d49167;
    margin-bottom: 20px;

}
.profile{
    width: 100%;
    text-align: center;
    font-weight: bolder;
    font-size: 150%;
    margin-top: 10px;
}
#profdiv{
    margin-top: 20px;
    margin-bottom: 30px;
    left: 15%;
    background-color: #DDD0C8;
    border: 3px solid #d87e47;
    box-shadow: 5px 10px #d87e47;
}
.nagy{
    width: 100%;
}
</style>
