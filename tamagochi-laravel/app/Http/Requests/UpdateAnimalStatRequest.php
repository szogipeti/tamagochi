<?php

namespace App\Http\Requests;

use Illuminate\Foundation\Http\FormRequest;

class UpdateAnimalStatRequest extends FormRequest
{
    /**
     * Determine if the user is authorized to make this request.
     *
     * @return bool
     */
    public function authorize()
    {
        return true;
    }

    /**
     * Get the validation rules that apply to the request.
     *
     * @return array<string, mixed>
     */
    public function rules()
    {
        return [
            'name' => 'string|max:25',
            'hunger' => 'integer|min:1|max:100',
            'thirst' => 'integer|min:1|max:100',
            'happiness' => 'integer|min:1|max:100',
            'activity' => 'integer|min:1|max:100',
            'health' => 'integer|min:1|max:100',
            'dexterity' => 'integer|min:1|max:100',
            'action_count' => 'integer|min:0|max:10',
            'last_hunger' => 'required_with:hunger|date',
            'last_thirst' => 'required_with:thirst|date',
            'last_happiness' => 'required_with:happiness|date',
            'last_activity' => 'required_with:activity|date',
            'last_health' => 'required_with:health|date',
            'last_dexterity' => 'required_with:dexterity|date',
            'last_action' => 'required_with:action_count|date'
        ];
    }
}
