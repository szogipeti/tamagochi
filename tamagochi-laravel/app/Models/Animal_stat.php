<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Factories\HasFactory;
use Illuminate\Database\Eloquent\Model;

class Animal_stat extends Model
{
    protected $fillable = ['animal_id', 'name', 'hunger', 'thirst', 'happiness','activity','health','dexterity'];

    public $timestamps = false;
    public function animal(){
        return $this->hasOne(Animal::class, 'animal_id',"id",);
    }
}
