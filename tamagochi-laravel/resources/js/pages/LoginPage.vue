<template>
    <div id="login" class="mx-auto p-3">
        <h3>Bejelentkezés</h3>
        <Form @submit="login" :validation-schema="schema">
            <div class="form-group">
                <Field class="form-control" type="email" placeholder="E-mail" name="email" />
                <error-message name="email" />
            </div>
            <div class="form-group">
                <Field class="form-control" type="password" placeholder="Jelszó" name="password" />
                <error-message name="password" />
            </div>
            <button type="submit" class="btn btn-primary my-2">Bejelentkezés</button>
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

const router = useRouter();
const loggedIn = useLoggedInStore();

const schema = yup.object({
    email: yup.string()
        .email('Az e-mail formátuma nem megfelelő!')
        .min(6, 'Az e-mailnek minimum 6 hosszúnak kell lennie!')
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
        loggedIn.login(resp.data.token, resp.data.username)
        router.push({name: 'animal_select'})
    } catch (e){
        error.value = e.response.data.data.message;
    }
}

</script>

<style scoped>
#login{
    width: 700px;
    margin: 30px auto;
    background-color: white;
    border: 2px solid lightgray;
    box-shadow: 10px 10px 10px darkgray;
}

input{
    margin: 5px auto;
    padding: 10px 5px;
}

</style>
