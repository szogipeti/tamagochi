<template>
    <div class="card mx-auto" style="width: 50em">
        <h5 class="m-3">Válaszd ki a kezdő állatodat!</h5>
        <select class="form-select mx-auto my-3" id="animal" style="width: 40em" v-model="selectedAnimal">
            <option v-for="animal in animals" :key="animal.id" :value="animal.id">{{animal.name}}</option>
        </select>
        <button class="btn btn-primary w-50 mx-auto my-3" @click="selectAnimal">Kiválasztás</button>
    </div>
</template>
<script setup>
import {http} from '@/utils/http.js'
import {onMounted, reactive, ref, watch} from "vue";
import {useAnimalStore} from "../store/animal.js";
import {useRouter} from "vue-router";

const selectedAnimal = ref();
const animals = reactive([]);
const animal = useAnimalStore();
const router = useRouter();

watch(
    () => animal.animalId,
    (animalId) => {
        if(animalId !== null){
            router.push({name: 'index'})
        }
    }
)

const getAnimals = async function(){
    const resp = await http.get('/animals')
    for(const animal of resp.data.data){
        animals.push(animal);
    }
}

const selectAnimal = function(){
    console.log(selectedAnimal.value)
    animal.setAnimal(selectedAnimal.value)
}

onMounted(() => {
    if(animal.animalId !== null){
        router.push({name: 'index'})
    }
    getAnimals();
})
</script>
<style scoped>

</style>
