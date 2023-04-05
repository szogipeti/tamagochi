<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Factories\HasFactory;
use Illuminate\Database\Eloquent\Model;

class Animals extends Model
{
    use HasFactory;
    protected $fillable = ['Név',];
    public $timestamps = false;
    public function animal(){
        return $this->hasMany(Animal_stat::class,"animals_id",);
    }
}
