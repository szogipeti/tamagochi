<?php

namespace App\Http\Resources;

use Illuminate\Http\Resources\Json\JsonResource;

class AnimalStatResource extends JsonResource
{
    /**
     * Transform the resource into an array.
     *
     * @param  \Illuminate\Http\Request  $request
     * @return array|\Illuminate\Contracts\Support\Arrayable|\JsonSerializable
     */
    public function toArray($request)
    {
        return [
            'id' => $this->id,
            'user_id' => $this->user_id,
            'hunger' => $this->hunger,
            'thirst' => $this->thirst,
            'happiness' => $this->happiness,
            'activity' => $this->activity,
            'health' => $this->health,
            'dexterity' => $this->dexterity
        ];
    }
}
