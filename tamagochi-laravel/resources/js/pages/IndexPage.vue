<template>
    <div class="card mx-auto my-5 p-3" v-if="!loggedIn.isLoggedIn">
        <h2 class="card-title my-1">Jelentkezz be, hogy megnézd a háziállatod!</h2>
        <router-link class="btn btn-primary mx-auto my-1" to="/login">Bejelentkezés</router-link>
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
}

router-link {
    width: 20em;
}
</style>
