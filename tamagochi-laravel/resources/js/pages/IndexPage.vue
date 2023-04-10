<template>
    <div class="card mx-auto my-5 p-3" v-if="!loggedIn.isLoggedIn">
        <h2 class="card-title my-1">Jelentkezz be, hogy megnézd a háziállatod!</h2>
        <router-link class="bejelentkez mx-auto my-1" to="/login">Bejelentkezés</router-link>
        <router-link class="regi mx-auto my-1" to="/register">Regisztráció</router-link>
    </div>
    <div class="container" v-else>
        <div class="row">
            <div class="col-3">
                <stat-box v-if="animalLoaded" :name="animal.name" :hunger="animal.hunger" :thirst="animal.thirst"
                          :happiness="animal.happiness" :activity="animal.activity" :health="animal.health"
                          :dexterity="animal.dexterity" :created_at="animal.created_at"/>
            </div>
            <div class="col-9">
                <activity-box />
            </div>
        </div>
    </div>
</template>

<script setup>
import {RouterLink} from 'vue-router';
import {useLoggedInStore} from "../store/isLoggedIn";
import {useAnimalStore} from "../store/animal";
import StatBox from "../components/StatBox.vue";
import {http} from "../utils/http";
import {onMounted, reactive, ref, watch} from "vue";
import ActivityBox from "../components/ActivityBox.vue";

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
}

const updateStatsLastState = function (){
    const hungerDuration = Math.floor((new Date() - Date.parse(animal.last_hunger)) / (1000 * 60 * 60 * 0.5));
    if(animal.hunger - hungerDuration > 0){
        animal.hunger -= hungerDuration;
    }

    const thirstDuration = Math.floor((new Date() - Date.parse(animal.last_thirst)) / (1000 * 60 * 60 * 0.5))
    if(animal.thirst - thirstDuration > 0){
        animal.thirst -= thirstDuration;
    }

    const happinessDuration = Math.floor((new Date() - Date.parse(animal.last_happiness)) / (1000 * 60 * 60 * 1.5))
    if(animal.happiness - happinessDuration > 0){
        animal.happiness -= happinessDuration;
    }

    const activityDuration = Math.floor((new Date() - Date.parse(animal.last_activity)) / (1000 * 60 * 60 * 1.5))
    if(animal.activity - activityDuration > 0){
        animal.activity -= activityDuration;
    }

    const healthDuration = Math.floor((new Date() - Date.parse(animal.last_health)) / (1000 * 60 * 60 * 2))
    if(animal.health - healthDuration > 0){
        animal.health -= healthDuration;
    }

    const dexterityDuration = Math.floor((new Date() - Date.parse(animal.last_dexterity)) / (1000 * 60 * 60 * 2))
    if(animal.dexterity - dexterityDuration > 0){
        animal.dexterity -= dexterityDuration;
    }
}

const minuteTimeout = 1000 * 60;

const hungerCountDown = ref(0);
const thirstCountDown = ref(0);
const happinessCountDown = ref(0);
const activityCountDown = ref(0);
const healthCountDown = ref(0);
const dexterityCountDown = ref(0);

const startTimers = function (){
    hungerCountDown.value = 30 - Math.round((new Date() - Date.parse(animal.last_hunger)) / minuteTimeout) % 30;
    thirstCountDown.value = 30 - Math.round(((new Date() - Date.parse(animal.last_thirst)) / minuteTimeout)) % 30;
    happinessCountDown.value = 90 - Math.round(((new Date() - Date.parse(animal.last_happiness)) / minuteTimeout)) % 90;
    activityCountDown.value = 90 - Math.round(((new Date() - Date.parse(animal.last_activity)) / minuteTimeout)) % 90;
    healthCountDown.value = 120 - Math.round(((new Date() - Date.parse(animal.last_health)) / minuteTimeout)) % 120;
    healthCountDown.value = 60 - Math.round(((new Date() - Date.parse(animal.last_dexterity)) / minuteTimeout)) % 60;

    hungerTimer();
    thirstTimer();
    happinessTimer();
    activityTimer();
    healthTimer();
    dexterityTimer();
}

const hungerTimer = function (){
    if(hungerCountDown.value > 0){
        setTimeout(() => {
            hungerCountDown.value--;
            hungerTimer()
        }, minuteTimeout)
    }
}

const thirstTimer = function(){
    if(thirstCountDown.value > 0){
        setTimeout(() => {
            thirstCountDown.value--;
            thirstTimer()
        }, minuteTimeout)
    }
}


const happinessTimer = function(){
    return new Promise(function (){
        if(happinessCountDown.value > 0){
            setTimeout(() => {
                happinessCountDown.value--;
                happinessTimer()
            }, minuteTimeout)
        }
    });
}


const activityTimer = function(){
    return new Promise(function (){
        if(activityCountDown.value > 0){
            setTimeout(() => {
                activityCountDown.value--;
                activityTimer()
            }, minuteTimeout)
        }
    });
}


const healthTimer = function(){
    return new Promise(function (){
        if(healthCountDown.value > 0){
            setTimeout(() => {
                healthCountDown.value--;
                healthTimer()
            }, minuteTimeout)
        }
    });
}

const dexterityTimer = function(){
    return new Promise(function (){
        if(dexterityCountDown.value > 0){
            setTimeout(() => {
                dexterityCountDown.value--;
                dexterityTimer()
            }, minuteTimeout)
        }
    });
}

watch(hungerCountDown, (newHungerCountDown) => {
    if(newHungerCountDown <= 0){
        hungerCountDown.value = 30;
        getAnimal();
        hungerTimer();
    }
})

watch(thirstCountDown, (newThirstCountDown) => {
    if(newThirstCountDown <= 0){
        thirstCountDown.value = 30;
        getAnimal();
        thirstTimer();
    }
})

watch(happinessCountDown, (newHappinessCountDown) => {
    if(newHappinessCountDown <= 0){
        happinessCountDown.value = 90;
        getAnimal();
        happinessTimer();
    }
})

watch(activityCountDown, (newActivityCountDown) => {
    if(newActivityCountDown <= 0){
        activityCountDown.value = 90;
        getAnimal();
        activityTimer();
    }
})

watch(healthCountDown, (newHealthCountDown) => {
    if(newHealthCountDown <= 0){
        healthCountDown.value = 120;
        getAnimal();
        healthTimer();
    }
})

watch(dexterityCountDown, (newDexterityCountDown) => {
    if(newDexterityCountDown <= 0){
        dexterityCountDown.value = 120;
        getAnimal();
        dexterityTimer();
    }
})

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
    getAnimal().then(() => startTimers());
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
