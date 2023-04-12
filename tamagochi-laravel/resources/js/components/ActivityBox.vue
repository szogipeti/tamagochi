<template>
    <div class="container p-3 my-3">
        <div class="row">
            <div class="col" style="border-bottom: 1px solid black">
                <img :src="image" class="img-fluid" alt="">
            </div>
        </div>
        <div class="row pt-3">
            <div class="col-12 col-md-6">
                <button @click="$emit('feed')" class="w-100 my-1">Etetés</button>
            </div>
            <div class="col-12 col-md-6">
                <button @click="$emit('drink')" class="w-100 my-1">Itatás</button>
            </div>
            <div class="col-12 col-md-6 col-xxl-3">
                <button @click="$emit('hunt')" class=" w-100 my-1">Vadászat</button>
            </div>
            <div class="col-12 col-md-6 col-xxl-3">
                <button @click="$emit('play')" class=" w-100 my-1">Játék</button>
            </div>
            <div class="col-12 col-md-6 col-xxl-3">
                <button @click="$emit('checkup')" class=" w-100 my-1">Orvosi vizsgálat</button>
            </div>
            <div class="col-12 col-md-6 col-xxl-3">
                <button @click="$emit('medication')" class=" w-100 my-1">Gyógyszeres kezelés</button>
            </div>
        </div>
    </div>
</template>

<script setup>
import {http} from '../utils/http';
import {onMounted, ref} from 'vue';

const props = defineProps({
    animal_id:Number
})

const image = ref('');

const emits = defineEmits([
    'feed',
    'drink',
    'hunt',
    'play',
    'checkup',
    'medication'
])

const getAnimalType = async function(){
    const resp = await http.get(`animals/${props.animal_id}`);
    image.value = resp.data.data.image;
}

onMounted(() => {
    getAnimalType();
})
</script>

<style scoped>
.container{
    border: solid 3px #323232;
    box-shadow: 5px 10px #323232 ;
    background-color: #d87e47;
    color: #323232;
}
button{
    display: block;
    text-align: center;
    outline: 0;
    background: 0 0;
    background-color: #d49167;
    border: 1px solid #323232;
    border-radius: 0;
    cursor: pointer;
    font-size: 18px;
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
img{
    height: 300px;
    margin: 10px auto;
    display: block;
}
</style>
