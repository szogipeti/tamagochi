<template>
<div>
    <img src="" alt="">
    <div class="container">
        <div class="row">
            <div class="col-12 col-md-6">
                <button @click="$emit('feed')" class="w-100 my-3">Etetés</button>
            </div>
            <div class="col-12 col-md-6">
                <button @click="$emit('drink')" class="w-100 my-3">Itatás</button>
            </div>
            <div class="col-12 col-md-6 col-xxl-3">
                <button @click="$emit('hunt')" class=" w-100 my-3">Vadászat</button>
            </div>
            <div class="col-12 col-md-6 col-xxl-3">
                <button @click="$emit('play')" class=" w-100 my-3">Játék</button>
            </div>
            <div class="col-12 col-md-6 col-xxl-3">
                <button @click="$emit('checkup')" class=" w-100 my-3">Orvosi vizsgálat</button>
            </div>
            <div class="col-12 col-md-6 col-xxl-3">
                <button @click="$emit('medication')" class=" w-100 my-3">Gyógyszeres kezelés</button>
            </div>
        </div>
    </div>
</div>
</template>

<script setup>
const emits = defineEmits([
    'feed',
    'drink',
    'hunt',
    'play',
    'checkup',
    'medication'
])
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
</style>
