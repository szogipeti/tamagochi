<template>
    <div class="p-3 m-3">
        <h3>{{ props.name.charAt(0).toUpperCase() + props.name.slice(1) }} adatai</h3>
        <table class="table table-responsive">
            <tr>
                <th>Éhség:</th>
                <td>{{ props.hunger }}/100</td>
            </tr>
            <tr>
                <th>Szomjúság:</th>
                <td>{{ props.thirst }}/100</td>
            </tr>
            <tr>
                <th>Jókedv:</th>
                <td>{{ props.happiness }}/100</td>
            </tr>
            <tr>
                <th>Mozgás:</th>
                <td>{{ props.activity }}/100</td>
            </tr>
            <tr>
                <th>Egészség:</th>
                <td>{{ props.health }}/100</td>
            </tr>
            <tr>
                <th>Ügyesség:</th>
                <td>{{ props.dexterity }}/100</td>
            </tr>
            <tr>
                <th>Kor:</th>
                <td>{{ age }} nap</td>
            </tr>
        </table>
        <p class="text-center">{{action_count}} lépésed van még ma</p>
    </div>
</template>

<script setup>
import {ref} from "vue";

const props = defineProps({
    name: String,
    hunger: Number,
    thirst: Number,
    happiness: Number,
    activity: Number,
    health: Number,
    dexterity: Number,
    created_at: String,
    action_count: Number
})

const age = ref(Math.ceil((Date.now() - Date.parse(props.created_at)) / (1000 * 60 * 60 * 24)));

</script>

<style scoped>
div{
    border: solid 3px #323232;
    box-shadow: 5px 10px #323232 ;
    background-color: #d49167;
    color: #323232;
}
table{
    border: solid 3px #d49167;
    color: #323232;
}
tr:nth-child(odd){
    background-color: #DDD0C8;
}
tr:hover{
    background-color: #d87e47;
}
</style>
