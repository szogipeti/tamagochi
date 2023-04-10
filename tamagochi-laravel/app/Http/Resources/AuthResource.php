<?php

namespace App\Http\Resources;

use Illuminate\Http\Resources\Json\JsonResource;

class AuthResource extends JsonResource
{
    /**
     * Transform the resource into an array.
     *
     * @param  \Illuminate\Http\Request  $request
     * @return array|\Illuminate\Contracts\Support\Arrayable|\JsonSerializable
     */
    public function toArray($request)
    {
        $body = [
            'id' => $this->id,
            'username' => $this->username,
            'token' => $this->createToken('authToken')->plainTextToken,
        ];
        if($this->pet !== null){
            $body['animal_id'] = $this->pet->id;
        }
        return $body;
    }
}
