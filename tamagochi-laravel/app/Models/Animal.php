<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Factories\HasFactory;
use Illuminate\Database\Eloquent\Model;

class Animal extends Model
{
    protected $fillable = ['name', 'image'];
    public $timestamps = false;
    public function animal(){
        return $this->hasMany(AnimalStat::class,"animals_id",);
    }
}
