<template>
    <div id="login" class="mx-auto p-3">
        <h3>Bejelentkezés</h3>
        <Form @submit="login" :validation-schema="schema">
            <div class="form-group">
                <Field type="email" placeholder="E-mail" name="email" />
                <error-message name="email" />
            </div>
            <div class="form-group">
                <Field type="password" placeholder="Jelszó" name="password" />
                <error-message name="password" />
            </div>
            <button type="submit" class="my-2">Bejelentkezés</button>
            <p class="alert alert-danger" v-if="error">{{error}}</p>
        </Form>
        <h5>Nincs még fiókod? <router-link to="/register">Regisztrálj itt!</router-link></h5>
    </div>
</template>

<script setup>
import {ref} from 'vue'
import {useRouter} from 'vue-router'
import {Form, Field, ErrorMessage} from "vee-validate"
import {http} from "../utils/http.js"
import * as yup from "yup"
import {useLoggedInStore} from "../store/isLoggedIn";
import {useAnimalStore} from "../store/animal";

const router = useRouter();
const loggedIn = useLoggedInStore();
const animalStore = useAnimalStore();

const schema = yup.object({
    email: yup.string()
        .email('Az e-mail formátuma nem megfelelő!')
        .min(6, 'Az e-mailnek minimum 6 karakter hosszúnak kell lennie!')
        .max(100, 'Az e-mail maximum 100 karakter hosszú lehet!')
        .required('Az e-mail megadása kötelező!'),
    password: yup.string()
        .min(6, 'A jelszónak minimum 6 karakter hosszúnak kell lennie!')
        .max(100, 'A jelszó maximum 100 karakter hosszú lehet')
        .required('A jelszó megadása kötelező!'),
});

const error = ref('')

const login = async function(userData){
    try {
        const resp = await http.post('login', userData);
        loggedIn.login(resp.data.token, resp.data.username, resp.data.id)
        if(resp.data.animal_id === undefined || resp.data.animal_id === null){
            router.push({name: 'animal_select'})
        }else{
            animalStore.setAnimal(resp.data.animal_id)
            router.push({name: 'index'})
        }
    } catch (e){
        console.log(e)
        error.value = e.response.data.data.message;
    }
}

</script>

<style scoped>
#login{
    width: 700px;
    margin: 30px auto;
    background-color: #DDD0C8;
    border: solid 3px #323232;
    box-shadow: 5px 10px #323232 ;
}
h3{
    color: #323232;
}
h5{
    color: #323232;
    text-decoration: none;
}
input{
    margin: 5px auto;
    padding: 10px 5px;
    border-radius: 0;
    width: 100%;

    border: 1px solid rgb(181, 189, 196);
    line-height: 24px;
    padding: 7px 8px;
    color: #323232;
    box-shadow: none;
}
a{
    color: #323232;
}
a:hover{
    font-weight: bolder;
}
button{

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
