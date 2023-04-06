<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Factories\HasFactory;
use Illuminate\Database\Eloquent\Model;

class Animal_stat extends Model
{
    protected $fillable = ['animals_id', 'Név', 'Éhség', 'Szomjúság', 'Jókedv','Mozgás','Egészség','Ügyesség'];

    public $timestamps = false;
    public function animal(){
        return $this->belongsTo(Animal::class, 'animals_id',"id",);
    }
}
