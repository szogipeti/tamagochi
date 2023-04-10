<template>
    <div id="register" class="mx-auto p-3">
        <h3>Hozz létre egy új fiókot!</h3>
        <Form @submit="register" :validation-schema="schema">
            <div class="form-group">
                <Field type="string" placeholder="Felhasználónév" name="username" />
                <error-message name="username" />
            </div>
            <div class="form-group">
                <Field type="email" placeholder="E-mail" name="email" />
                <error-message name="email" />
            </div>
            <div class="form-group">
                <Field type="password" placeholder="Jelszó" name="password" />
                <error-message name="password" />
            </div>
            <div class="form-group">
                <Field type="password" placeholder="Jelszó megerősítése" name="password_confirmation" />
                <error-message name="password_confirmation" />
            </div>
            <button type="submit">Regisztráció</button>
            <div class="alert alert-danger" v-if="hasError">
                <div v-for="(value, key) in errors" :key="key">
                    <p class="my-auto" v-for="error in value" :key="error">{{error}}</p>
                </div>
            </div>
        </Form>
        <h5>Van már fiókod? <router-link to="/login">Jelentkezz be itt!</router-link></h5>
    </div>
</template>

<script setup>
import {reactive, ref} from 'vue'
import {useRouter} from "vue-router";
import {http} from '../utils/http'
import {Form, Field, ErrorMessage} from "vee-validate";
import * as yup from "yup";

const router = useRouter();

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
    password_confirmation : yup.string()
        .required('A jelszó megerősítésének megadása kötelező!')
        .oneOf([yup.ref('password')], 'A jelszó megerősítésének meg kell egyeznie a jelszóval!')
})

let errors = reactive({})
const hasError = ref(false)

const register = async function(userData){
    errors = reactive({})
    hasError.value = false;
    try {
        const resp = await http.post('register', userData);
        router.push({name: 'login'});
    } catch (e){
        const errorMessages = e.response.data.errors
        for (const key in errorMessages){
            errors[key] = errorMessages[key];
        }
        hasError.value = true
    }
}

</script>
<style scoped>
#register{
    width: 700px;
    margin: 30px auto;
    background-color: #DDD0C8;
    border: 3px solid #d87e47;
    box-shadow: 5px 10px #d87e47;
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
    margin-top: 10px;
    margin-bottom: 10px;
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
