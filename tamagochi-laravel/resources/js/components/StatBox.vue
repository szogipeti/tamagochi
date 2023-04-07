<template>
    <div class="p-3 m-3">
        <h3>{{ props.name.charAt(0).toUpperCase() + props.name.slice(1) }} adatai</h3>
        <table class="table table-responsive">
            <tr>
                <th>Éhség:</th>
                <td>{{ props.hunger }}</td>
            </tr>
            <tr>
                <th>Szomjúság:</th>
                <td>{{ props.thirst }}</td>
            </tr>
            <tr>
                <th>Jókedv:</th>
                <td>{{ props.happiness }}</td>
            </tr>
            <tr>
                <th>Mozgás:</th>
                <td>{{ props.activity }}</td>
            </tr>
            <tr>
                <th>Egészség:</th>
                <td>{{ props.health }}</td>
            </tr>
            <tr>
                <th>Ügyesség:</th>
                <td>{{ props.dexterity }}</td>
            </tr>
            <tr>
                <th>Kor:</th>
                <td>{{ age }} év</td>
            </tr>
        </table>
    </div>
</template>

<script setup>
import {watch, ref, onMounted} from "vue";

const props = defineProps({
    name: String,
    hunger: Number,
    thirst: Number,
    happiness: Number,
    activity: Number,
    health: Number,
    dexterity: Number,
    created_at: String
})

const age = ref(Math.ceil((Date.now() - Date.parse(props.created_at)) / (1000 * 60)));

const countDown = ref(
    60 -
    Math.ceil(
        (Date.now() - Date.parse(props.created_at)) / (1000) -
        Math.floor((Date.now() - Date.parse(props.created_at)) / (1000) / 60) * 60
    )
);

const timer = function () {
    if (countDown.value > 0) {
        setTimeout(() => {
            countDown.value--;
            timer();
        }, 1000)
    }
}

watch(countDown, (newCountDown) => {
    if (newCountDown <= 0) {
        age.value++;
        emit('loseStats')
        countDown.value = 60;
        timer();
    }
})

const emit = defineEmits([
    'loseStats'
])

onMounted(() => {
    timer();
})

</script>

<style scoped>

</style>
