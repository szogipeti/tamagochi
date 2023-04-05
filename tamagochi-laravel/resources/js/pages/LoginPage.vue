<template>
    <div id="login" class="mx-auto p-3">
        <h2>Bejelentkezés</h2>
        <Form @submit="login" :validation-schema="schema">
            <div class="form-group">
                <Field class="form-control" type="email" placeholder="E-mail" name="email" />
                <error-message name="email" />
            </div>
            <div class="form-group">
                <Field class="form-control" type="password" placeholder="Password" name="password" />
                <error-message name="password" />
            </div>
            <button type="submit" class="btn btn-primary my-2">Bejelentkezés</button>
        </Form>
        <h5>Nincs még fiókod? <router-link to="">Regisztrálj itt!</router-link></h5>
    </div>
</template>

<script setup>
import {Form, Field, ErrorMessage} from "vee-validate"
import {http} from "../utils/http.js"
import * as yup from "yup"

const schema = yup.object({
    email: yup.string().email().min(6).max(100).required(),
    password: yup.string().min(6).max(100).required()
});

const login = async function(){

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
    border: 2px solid lightgray;
}

</style>
