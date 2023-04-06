import {defineStore} from "pinia";

export const useLoggedInStore = defineStore('isLoggedIn', {
    state(){
        return{
            isLoggedIn: localStorage.getItem('token') !== null,
            username: localStorage.getItem('username')??''
        }
    },
    actions:{
        login(token, username){
            this.isLoggedIn = true;
            localStorage.setItem('token', token);
            this.setUsername(username)
        },
        setUsername(username){
            this.username = username;
            localStorage.setItem('username', username)
        }
    }
})
