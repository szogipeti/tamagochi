<template>
<div>
    <img src="" alt="">
    <div class="container">
        <div class="row">
            <div class="col-3">
                <button @click="$emit('hunt')" class="btn btn-primary">Vadászat</button>
            </div>
            <div class="col-3">
                <button @click="$emit('play')" class="btn btn-primary">Játék</button>
            </div>
            <div class="col-3">
                <button @click="$emit('checkup')" class="btn btn-primary">Orvosi vizsgálat</button>
            </div>
            <div class="col-3">
                <button @click="$emit('medication')" class="btn btn-primary">Gyógyszeres kezelés</button>
            </div>
        </div>
    </div>
</div>
</template>

<script setup>
const emits = defineEmits([
    'hunt',
    'play',
    'checkup',
    'medication'
])
</script>

<style scoped>

</style>
