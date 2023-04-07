<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Factories\HasFactory;
use Illuminate\Database\Eloquent\Model;

class Animal_stat extends Model
{
    protected $fillable = ['user_id', 'animal_id', 'name', 'hunger', 'thirst', 'happiness','activity','health','dexterity'];
    protected $attributes = [
        'hunger' => 100,
        'thirst' => 100,
        'happiness' => 100,
        'activity' => 100,
        'health' => 100,
        'dexterity' => 100
    ];

    public $timestamps = false;

    public static function boot(){
        parent::boot();

        static::creating(function ($model) {
            $model->created_at = $model->freshTimestamp();
        });
    }

    public function animal(){
        return $this->hasOne(Animal::class, 'animal_id',"id",);
    }
}
