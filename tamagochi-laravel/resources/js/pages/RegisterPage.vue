<template>
    <div id="register" class="mx-auto p-3">
        <h3>Hozz létre egy új fiókot!</h3>
        <Form @submit="register" :validation-schema="schema">
            <div class="form-group">
                <Field class="form-control" type="string" placeholder="Felhasználónév" name="username" />
                <error-message name="username" />
            </div>
            <div class="form-group">
                <Field class="form-control" type="email" placeholder="E-mail" name="email" />
                <error-message name="email" />
            </div>
            <div class="form-group">
                <Field class="form-control" type="password" placeholder="Jelszó" name="password" />
                <error-message name="password" />
            </div>
            <div class="form-group">
                <Field class="form-control" type="password" placeholder="Jelszó megerősítése" name="password_confirm" />
                <error-message name="password_confirm" />
            </div>
            <button type="submit" class="my-2 btn btn-primary">Regisztráció</button>
        </Form>
        <h5>Van már fiókod? <router-link to="/login">Jelentkezz be itt!</router-link></h5>
    </div>
</template>

<script setup>
import {ref} from 'vue'
import {http} from '../utils/http'
import {Form, Field, ErrorMessage} from "vee-validate";
import * as yup from "yup";

const schema = yup.object({
    username: yup.string()
        .min(6, 'A felhasználónévnek minimum 6 karakter hosszúnak kell lennie!')
        .max(100, 'A felhasználónév maximum 100 karakter hosszú lehet!')
        .required('A felhasználónév megadása kötelező!'),
    email : yup.string()
        .email('Az e-mail formátuma nem megfelelő!')
        .min(6, 'Az e-mailnek minimum 6 hosszúnak kell lennie!')
        .max(100, 'Az e-mail maximum 100 karakter hosszú lehet!')
        .required('Az e-mail megadása kötelező!'),
    password : yup.string()
        .min(6, 'A jelszónak minimum 6 karakter hosszúnak kell lennie!')
        .max(100, 'A jelszó maximum 100 karakter hosszú lehet')
        .required('A jelszó megadása kötelező!'),
    password_confirm : yup.string()
        .required('A jelszó megerősítésének megadása kötelező!')
        .oneOf([yup.ref('password')], 'A jelszó megerősítésének meg kell egyeznie a jelszóval!')
})

</script>
<style scoped>
#register{
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
