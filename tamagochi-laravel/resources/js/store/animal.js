import {defineStore} from "pinia";

export const useAnimalStore = defineStore('animal', {
    state(){
        return{
            animalId: localStorage.getItem('animal')
        }
    },
    actions:{
        setAnimal(animalId){
            this.animalId = animalId;
            localStorage.setItem('animal', animalId)
        }
    }
});
