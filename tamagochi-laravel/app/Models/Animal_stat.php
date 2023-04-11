<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Factories\HasFactory;
use Illuminate\Database\Eloquent\Model;

class Animal_stat extends Model
{
    protected $fillable =
        [
            'user_id', 'animal_id', 'name', 'hunger', 'thirst', 'happiness',
            'activity', 'health', 'dexterity', 'action_count', 'last_hunger', 'last_thirst',
            'last_happiness', 'last_activity', 'last_health', 'last_dexterity', 'last_action'
        ];
    protected $attributes = [
        'hunger' => 100,
        'thirst' => 100,
        'happiness' => 100,
        'activity' => 100,
        'health' => 100,
        'dexterity' => 100,
        'action_count' => 10
    ];

    public $timestamps = false;

    public static function boot()
    {
        parent::boot();

        static::creating(function ($model) {
            $model->created_at = $model->freshTimestamp();
            $model->last_hunger = $model->created_at;
            $model->last_thirst = $model->created_at;
            $model->last_happiness = $model->created_at;
            $model->last_activity = $model->created_at;
            $model->last_health = $model->created_at;
            $model->last_dexterity = $model->created_at;
            $model->last_action = $model->created_at;
        });
    }

    public function animal()
    {
        return $this->hasOne(Animal::class, 'animal_id', "id",);
    }

    public function owner()
    {
        return $this->belongsTo(User::class, 'user_id', 'id');
    }
}
