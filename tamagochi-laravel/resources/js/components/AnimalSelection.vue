<template>
<div class="card">
    <h5>Válaszd ki a kezdő állatodat!</h5>
    <select name="animal" id="animal">
        <option v-for="animal in animals" :key="animal.id" :value="animal.id">{{animal.name}}</option>
    </select>
</div>
</template>
<script setup>
import {http} from '@/utils/http.js'
import {onMounted, reactive} from "vue";

const animals = reactive([]);

const getAnimals = async function(){
    const resp = await http.get('/animals')
    for(const animal of resp.data.data){
        animals.push(animal);
    }
}

onMounted(() => {
    getAnimals();
})
</script>
<style scoped>

</style>
