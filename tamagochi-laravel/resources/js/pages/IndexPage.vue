<template>
    <div class="card mx-auto my-5 p-3" v-if="!loggedIn.isLoggedIn">
        <h2 class="card-title my-1">Jelentkezz be, hogy megnézd a háziállatod!</h2>
        <router-link class="bejelentkez mx-auto my-1" to="/login">Bejelentkezés</router-link>
        <router-link class="regi mx-auto my-1" to="/register">Regisztráció</router-link>
    </div>
    <div class="container" v-else>
        <div class="row">
            <div class="col-3">
                <stat-box @lose-stats="loseStats" v-if="animalLoaded" :name="animal.name" :hunger="animal.hunger" :thirst="animal.thirst"
                          :happiness="animal.happiness" :activity="animal.activity" :health="animal.health"
                          :dexterity="animal.dexterity" :created_at="animal.created_at"/>
            </div>
            <div class="col-9"></div>
        </div>
    </div>
</template>

<script setup>
import {RouterLink} from 'vue-router';
import {useLoggedInStore} from "../store/isLoggedIn";
import {useAnimalStore} from "../store/animal";
import StatBox from "../components/StatBox.vue";
import {http} from "../utils/http";
import {onMounted, reactive, ref} from "vue";

const loggedIn = useLoggedInStore();
const animalStore = useAnimalStore();

const animal = reactive({});
const animalLoaded = ref(false)

const getAnimal = async function () {
    const resp = await http.get(`/animals/stats/${animalStore.animalId}`)
    for (const key in resp.data.data) {
        animal[key] = resp.data.data[key]
    }
    updateStatsLastState();
    animalLoaded.value = true;
    console.log(resp)
    console.log(animal)
}

const updateStatsLastState = function (){
    const iteration = Math.floor((new Date() - Date.parse(animal.last_hunger)) / (1000 * 60 * 30));
    if(animal.hunger - iteration > 0){
        animal.hunger -= iteration;
    }
}

const loseStats = function (){
    animal.hunger--;
    animal.thirst--;
    animal.happiness--;
    animal.activity--;
    animal.health--;
    animal.dexterity--;

    const body = {
        'hunger': animal.hunger,
        'thirst': animal.thirst,
        'happiness': animal.happiness,
        'activity': animal.activity,
        'health': animal.health,
        'dexterity': animal.dexterity
    }
    http.put(`/animals/stats/${animalStore.animalId}/update`, body)
}

onMounted(() => {
    getAnimal();
})



</script>
<style scoped>
.card {
    width: 700px;
    border-radius: 0;
    background-color: #DDD0C8;
    color: #323232;
    border: solid 3px #d87e47 ;
    box-shadow: 5px 5px #d87e47;
}

router-link {
    width: 20em;

}
.regi{
    outline: 0;
    background: 0 0;
    border: 1px solid #323232;
    border-radius: 0;
    cursor: pointer;
    display: inline-flex;
    font-size: 16px;
    overflow: hidden;
    padding: 12px 16px;
    text-decoration: none;
    text-overflow: ellipsis;
    transition: all .14s ease-out;
    white-space: nowrap;
    color: #323232;
}
.bejelentkez{
    outline: 0;
    background: 0 0;
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
    color: #323232;
}
.regi:hover,.bejelentkez:hover{
    box-shadow: 4px 4px 0 #323232;
    transform: translate(-4px,-4px);
}
</style>
