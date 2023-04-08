<?php

namespace App\Http\Requests;

use Illuminate\Foundation\Http\FormRequest;

class UpdateAnimalRequest extends FormRequest
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
        ];
    }
}
