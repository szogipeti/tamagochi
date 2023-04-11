<template>
    <div id="maindiv" class="card mx-auto" style="width: 50em">
        <h5>Hozd létre a kezdő állatodat!</h5>
        <Form @submit="createAnimal" :validation-schema="schema">
            <div class="mx-auto" style="width: 40em">
                <div class="form-group my-3">
                    <label class="form-label" for="name">Név:</label>
                    <Field class="form-control" placeholder="pl.: Gombóc" type="string" name="name" />
                    <error-message name="name" />
                </div>
                <div class="form-group my-3">
                    <label class="form-label" for="type">Állatfajta:</label>
                    <Field as="select" class="form-select" name="type" id="type">
                        <option v-for="animal in animals" :key="animal.id" :value="animal.id">{{animal.name}}</option>
                    </Field>
                    <error-message name="type" />
                </div>
            </div>
            <button type="submit" class="mx-auto d-block">Létrehozás</button>
            <div class="alert alert-danger m-3" v-if="hasError">
                <div v-for="(value, key) in errors" :key="key">
                    <p class="my-auto" v-for="error in value" :key="error">{{error}}</p>
                </div>
            </div>
        </Form>
    </div>
</template>
<script setup>
import {http} from '@/utils/http.js'
import {onMounted, reactive, ref, watch} from "vue";
import {useAnimalStore} from "../store/animal.js";
import {useRouter} from "vue-router";
import {useLoggedInStore} from "../store/isLoggedIn";
import {Form, Field, ErrorMessage} from "vee-validate";
import * as yup from 'yup';

const schema = yup.object({
    name: yup.string()
        .max(25, 'A név maximum 25 karakter hosszú lehet!')
        .required('A név kitöltése kötelező!'),
    type: yup.number()
        .required('Az állat kiválasztása kötelező!')
})

const animal = useAnimalStore();
const router = useRouter();
const loggedIn = useLoggedInStore();

const animals = reactive([]);
const hasError = ref(false);

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

let errors = reactive({})

const createAnimal = async function(animalData){
    errors = reactive({})
    hasError.value = false;
    const body = {
        'name': animalData.name,
        'animal_id': animalData.type,
        'user_id': loggedIn.user_id
    };

    try {
        const resp = await http.post('/animals/stats', body)
        animal.setAnimal(resp.data.data.id);
    } catch (e){
        const errorMessages = e.response.data.errors
        for (const key in errorMessages){
            errors[key] = errorMessages[key];
        }
        hasError.value = true
    }
}


onMounted(() => {
    if(animal.animalId !== null){
        router.push({name: 'index'})
    }
    getAnimals();
})
</script>
<style scoped>
h5{
    text-align: center;
    color: #323232;
    font-weight: bolder;
    font-size: 120%;
    margin-top: 10px;
}
#maindiv{
    background-color: #DDD0C8;
    border: solid 3px #323232;
    box-shadow: 5px 10px #323232 ;
    border-radius: 0;
    margin-top: 20px;
}
button{
    margin-top: 10px;
    margin-bottom: 10px;
    text-align: center;
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
}
button:hover {
    box-shadow: 4px 4px 0 #323232;
    transform: translate(-4px,-4px);
}
</style>
