import {defineStore} from "pinia";

export const useLoggedInStore = defineStore('isLoggedIn', {
    state(){
        return{
            isLoggedIn: localStorage.getItem('token') !== null,
            username: localStorage.getItem('username')??'',
            user_id: localStorage.getItem('user_id')
        }
    },
    actions:{
        login(token, username, user_id){
            this.isLoggedIn = true;
            localStorage.setItem('token', token);
            this.user_id = user_id;
            localStorage.setItem('user_id', user_id)
            this.username = username;
            localStorage.setItem('username', username)
        },
        logout(){
            localStorage.clear();
            this.isLoggedIn = false;
            this.user_id = null;
            this.username = "";
        }
    }
})
