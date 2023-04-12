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
            'animal_id' => $this->animal_id,
            'name' => $this->name,
            'hunger' => $this->hunger,
            'thirst' => $this->thirst,
            'happiness' => $this->happiness,
            'activity' => $this->activity,
            'health' => $this->health,
            'dexterity' => $this->dexterity,
            'action_count' => $this->action_count,
            'last_hunger' => date('Y-m-d H:i:s', strtotime($this->last_hunger)),
            'last_thirst' => date('Y-m-d H:i:s', strtotime($this->last_thirst)),
            'last_happiness' => date('Y-m-d H:i:s', strtotime($this->last_happiness)),
            'last_activity' => date('Y-m-d H:i:s', strtotime($this->last_activity)),
            'last_health' => date('Y-m-d H:i:s', strtotime($this->last_health)),
            'last_dexterity' => date('Y-m-d H:i:s', strtotime($this->last_dexterity)),
            'last_action' => date('Y-m-d H:i:s', strtotime($this->last_action)),
            'created_at' => date('Y-m-d H:i:s', strtotime($this->created_at)),
        ];
    }
}
