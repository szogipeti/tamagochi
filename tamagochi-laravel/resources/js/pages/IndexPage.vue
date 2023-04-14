<template>
    <div class="card mx-auto my-5 p-3" v-if="!loggedIn.isLoggedIn">
        <h2 class="card-title my-1">Jelentkezz be, hogy megnézd a háziállatod!</h2>
        <router-link class="bejelentkez mx-auto" to="/login">Bejelentkezés</router-link>
        <router-link class="regi mx-auto" to="/register">Regisztráció</router-link>
    </div>
    <div class="container" v-else v-if="animalLoaded">
        <div class="row">
            <div class="col-12 col-md-4 col-xl-3">
                <stat-box :name="animal.name" :hunger="animal.hunger" :thirst="animal.thirst"
                          :happiness="animal.happiness" :activity="animal.activity" :health="animal.health"
                          :dexterity="animal.dexterity" :age="age" :action_count="animal.action_count "/>
            </div>
            <div class="col-12 col-md-8 col-xl-9">
                <activity-box @feed="feed" @drink="drink" @hunt="hunt" @play="play" @checkup="checkup" @medication="medication" :age="age" :animal_id="animal.animal_id"/>
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
const age = ref(0);

const animal = reactive({});
const animalLoaded = ref(false)

const formatDate = function (date){
    const month = (date.getMonth() + 1).toString().padStart(2, '0');
    const day = date.getDate().toString().padStart(2, '0');
    const hour = date.getHours().toString().padStart(2, '0');
    const minutes = date.getMinutes().toString().padStart(2, '0');
    const seconds = date.getSeconds().toString().padStart(2, '0');
    return `${date.getFullYear()}-${month}-${day} ${hour}:${minutes}:${seconds}`
}

const tryToDoAction = function (){
    if(animal.action_count <= 0){
        alert("Ma már nincs több lépésed!")
        return false;
    }
    animal.action_count--;
    return true;
}

const feed = function (){

    if(!tryToDoAction()){
        return;
    }


    const date = formatDate(new Date());

    animal.hunger += 20;
    animal.last_hunger = date;
    if(animal.hunger > 100){
        animal.hunger = 100;
    }

    animal.movement -= 10;
    animal.last_movement = date;
    if(animal.movement < 1){
        animal.movement = 1;
    }

    animal.dexterity -= 10;
    animal.last_dexterity = date;
    if(animal.dexterity < 1){
        animal.dexterity = 1;
    }

    updateAnimal();
}

const drink = function (){
    if(!tryToDoAction()){
        return;
    }
    const date = formatDate(new Date());

    animal.thirst += 20;
    animal.last_thirst = date;
    if(animal.thirst > 100){
        animal.thirst = 100;
    }

    updateAnimal();
}

const hunt = function () {
    if(!tryToDoAction()){
        return;
    }

    const date = formatDate(new Date());

    const random = Math.floor(1 + Math.random()  * 100);
    const avg = (animal.happiness + animal.activity + animal.dexterity + animal.health) / 4

    console.log(random)
    console.log(avg)
    console.log(random < avg)

    if(random < avg){
        animal.hunger += 20;
        animal.last_hunger = date;
        if (animal.hunger > 100) {
            animal.hunger = 100;
        }

        animal.happiness += 10;
        animal.last_happiness = date;
        if (animal.happiness > 100) {
            animal.happiness = 100;
        }

        animal.dexterity += 15;
        animal.last_dexterity = date;
        if (animal.dexterity > 100) {
            animal.dexterity = 100;
        }
    }else{
        animal.activity += 20;
        animal.last_activity = date;
        if(animal.activity > 100){
            animal.activity = 100;
        }
    }

    updateAnimal();
}

const play = function () {
    if(!tryToDoAction()){
        return;
    }

    const date = formatDate(new Date())

    animal.happiness += 20;
    animal.last_happiness = date;
    if(animal.happiness > 100){
        animal.happiness = 100;
    }

    animal.health += 5;
    animal.last_health = date
    if(animal.health > 100){
        animal.health = 100;
    }

    animal.dexterity += 10;
    animal.last_dexterity = date;
    if(animal.dexterity > 100){
        animal.dexterity = 100;
    }

    animal.activity += 10;
    animal.last_activity = date;
    if(animal.activity > 100){
        animal.activity = 100;
    }

    updateAnimal();
}

const checkup = function () {

}

const medication = function () {
    if(!tryToDoAction()){
        return;
    }

    const date = formatDate(new Date())

    animal.health += 30;
    animal.last_health = date;
    if(animal.health > 100){
        animal.health = 100;
    }

    animal.happiness -= 20;
    animal.last_happiness = date;
    if(animal.happiness < 1){
        animal.happiness = 1;
    }

    updateAnimal();
}

const getAnimal = async function () {
    const headers = {
        'Authorization': 'Bearer ' + localStorage.getItem('token')
    }
    const resp = await http.get(`/animals/stats/${animalStore.animalId}`, { headers })
    for (const key in resp.data.data) {
        animal[key] = resp.data.data[key]
    }
    updateStatsLastState();
    age.value = Math.ceil((Date.now() - Date.parse(animal.created_at)) / (1000 * 60 * 60 * 24));
    animalLoaded.value = true;

}

const updateStatsLastState = function () {
    if(new Date().getDate() - new Date(animal.last_action).getDate() > 0){
        animal.action_count = 10;
        animal.last_action = formatDate(new Date())
        updateAnimal();
    }

    const hungerDuration = Math.floor((new Date() - Date.parse(animal.last_hunger)) / (1000 * 60 * 60 * 0.5));

    if (animal.hunger - hungerDuration > 0 && age<300) {

        animal.hunger -= hungerDuration;
    }else{
        animal.hunger = 0;
        animalDied('hunger')
    }

    const thirstDuration = Math.floor((new Date() - Date.parse(animal.last_thirst)) / (1000 * 60 * 60 * 0.5))
    if (animal.thirst - thirstDuration >  0 && age >100 && age<300 ) {
        animal.thirst -= thirstDuration;
    }else{
        animal.thirst = 0;
        animalDied('thirst')
    }

    const happinessDuration = Math.floor((new Date() - Date.parse(animal.last_happiness)) / (1000 * 60 * 60 * 1.5))
    if (animal.happiness - happinessDuration > 0 && age >199 && age <300) {
        animal.happiness -= happinessDuration;
    }else{
        animal.happiness = 0;
    }

    const activityDuration = Math.floor((new Date() - Date.parse(animal.last_activity)) / (1000 * 60 * 60 * 1.5))
    if (animal.activity - activityDuration > 0 && age >199 && age <300) {
        animal.activity -= activityDuration;
    }else{
        animal.activity = 0;
    }

    const healthDuration = Math.floor((new Date() - Date.parse(animal.last_health)) / (1000 * 60 * 60 * 2))
    if (animal.health - healthDuration > 0 && age >100 && age<300) {
        animal.health -= healthDuration;
    }else{
        animal.health = 0;
        animalDied('health')
    }

    const dexterityDuration = Math.floor((new Date() - Date.parse(animal.last_dexterity)) / (1000 * 60 * 60 * 2))
    if (animal.dexterity - dexterityDuration > 0 && age >199 && age <300) {
        animal.dexterity -= dexterityDuration;
    }else{
        animal.dexterity = 0;
    }
}

const updateAnimal = function (){
    const headers = {
        'Authorization': 'Bearer ' + localStorage.getItem('token')
    }
    http.put(`animals/stats/${animal.id}/update`, animal, { headers })
}

const animalDied = function (diedFrom){
    switch (diedFrom){
        case 'hunger':
            window.alert(`${animal.name} éhen halt. Hozz létre egy új háziállatot!`)
            break;
        case 'thirst':
            window.alert(`${animal.name} szomjan halt. Hozz létre egy új háziállatot!`)
            break;
        case 'health':
            window.alert(`${animal.name} meghalt betegségben. Hozz létre egy új háziállatot!`)
            break;
        default:
            window.alert(`${animal.name} meghalt. Hozz létre egy új háziállatot!`)
    }

    animalStore.removeAnimal();
    router.push({name: 'animals/select'})
}

const minuteTimeout = 1000 * 60;

const hungerCountDown = ref(0);
const thirstCountDown = ref(0);
const happinessCountDown = ref(0);
const activityCountDown = ref(0);
const healthCountDown = ref(0);
const dexterityCountDown = ref(0);
const hungerCount = ref(0);
const thirstCount = ref(0);
const happinessCount = ref(0);
const activityCount = ref( 0);
const healthCount = ref(0);
const dexterityCount = ref(0);


const Counter = function (){
    if (age<100)
    {
        hungerCount.value = 20;
        activityCount.value = 120;
        thirstCount.value = 90;
        happinessCount.value = 90;
        dexterityCount.value = 60;
    }
    else  if(age<200)
    {
        hungerCount.value = 40;
        activityCount.value = 100;
        thirstCount.value = 90;
        happinessCount.value = 90;
        dexterityCount.value = 60;
        healthCount.value = 130;
    }
    else if (age<300)
    {
        hungerCount.value = 40;
        activityCount.value = 100;
        thirstCount.value = 90;
        dexterityCount.value = 60;
        healthCount.value = 130;
    }


}
const startTimers = function () {
    hungerCountDown.value = hungerCount - Math.round((new Date() - Date.parse(animal.last_hunger)) / minuteTimeout) % hungerCount;
    thirstCountDown.value = thirstCount - Math.round(((new Date() - Date.parse(animal.last_thirst)) / minuteTimeout)) % thirstCount;
    happinessCountDown.value = happinessCount - Math.round(((new Date() - Date.parse(animal.last_happiness)) / minuteTimeout)) % happinessCount;
    activityCountDown.value = activityCount - Math.round(((new Date() - Date.parse(animal.last_activity)) / minuteTimeout)) % activityCount;
    dexterityCountDown.value = dexterityCount - Math.round(((new Date() - Date.parse(animal.last_dexterity)) / minuteTimeout)) % dexterityCount;
    healthCountDown.value = healthCount - Math.round(((new Date() - Date.parse(animal.last_health)) / minuteTimeout)) % healthCount;


    hungerTimer();
    thirstTimer();
    happinessTimer();
    activityTimer();
    healthTimer();
    dexterityTimer();
}

const hungerTimer = function () {
    if (hungerCountDown.value > 0) {
        setTimeout(() => {
            hungerCountDown.value--;
            hungerTimer()
        }, minuteTimeout)
    }
}

const thirstTimer = function () {
    if (thirstCountDown.value > 0) {
        setTimeout(() => {
            thirstCountDown.value--;
            thirstTimer()
        }, minuteTimeout)
    }
}

const happinessTimer = function () {
    return new Promise(function () {
        if (happinessCountDown.value > 0) {
            setTimeout(() => {
                happinessCountDown.value--;
                happinessTimer()
            }, minuteTimeout)
        }
    });
}

const activityTimer = function () {
    return new Promise(function () {
        if (activityCountDown.value > 0) {
            setTimeout(() => {
                activityCountDown.value--;
                activityTimer()
            }, minuteTimeout)
        }
    });
}

const healthTimer = function () {
    return new Promise(function () {
        if (healthCountDown.value > 0) {
            setTimeout(() => {s
                healthCountDown.value--;
                healthTimer()
            }, minuteTimeout)
        }
    });
}

const dexterityTimer = function () {
    return new Promise(function () {
        if (dexterityCountDown.value > 0) {
            setTimeout(() => {
                dexterityCountDown.value--;
                dexterityTimer()
            }, minuteTimeout)
        }
    });
}

watch(hungerCountDown, (newHungerCountDown) => {
    if (newHungerCountDown <= 0) {
        hungerCountDown.value = 30;
        getAnimal();
        hungerTimer();
    }
})

watch(thirstCountDown, (newThirstCountDown) => {
    if (newThirstCountDown <= 0) {
        thirstCountDown.value = 30;
        getAnimal();
        thirstTimer();
    }
})

watch(happinessCountDown, (newHappinessCountDown) => {
    if (newHappinessCountDown <= 0) {
        happinessCountDown.value = 90;
        getAnimal();
        happinessTimer();
    }
})

watch(activityCountDown, (newActivityCountDown) => {
    if (newActivityCountDown <= 0) {
        activityCountDown.value = 90;
        getAnimal();
        activityTimer();
    }
})

watch(healthCountDown, (newHealthCountDown) => {
    if (newHealthCountDown <= 0) {
        healthCountDown.value = 120;
        getAnimal();
        healthTimer();
    }
})

watch(dexterityCountDown, (newDexterityCountDown) => {
    if (newDexterityCountDown <= 0) {
        dexterityCountDown.value = 120;
        getAnimal();
        dexterityTimer();
    }
})

onMounted(() => {
    getAnimal().then(() =>{
        Counter()
        startTimers()
    });
})


</script>
<style scoped>
.card {
    width: 700px;
    border-radius: 0;
    background-color: #DDD0C8;
    color: #323232;
    border: solid 3px #323232;
    box-shadow: 5px 10px #323232 ;
}

router-link {
    width: 20em;

}

.regi {
    margin-top: 10px;
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

.bejelentkez {
    font-weight: bolder;
    margin-top: 10px;
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
    color: #323232;
}

.regi:hover, .bejelentkez:hover {
    box-shadow: 4px 4px 0 #323232;
    transform: translate(-4px, -4px);
}
</style>
