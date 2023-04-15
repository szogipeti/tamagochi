import {defineStore} from "pinia";
import {http} from "../utils/http";

export const useAnimalStore = defineStore('animal', {
    state(){
        return{
            animalId: localStorage.getItem('animal'),
        }
    },
    actions:{
        setAnimal(animalId){
            this.animalId = animalId;
            localStorage.setItem('animal', animalId)
        },
        removeAnimal(){
            const headers = {
                'Authorization': 'Bearer ' + localStorage.getItem('token')
            }
            http.delete(`/animals/stats/${this.animalId}`, { headers })
            this.animalId = null;
            localStorage.removeItem('animal');
        }
    }
});
