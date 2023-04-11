<?php

namespace App\Http\Requests;

use Illuminate\Foundation\Http\FormRequest;

class StoreAnimalRequest extends FormRequest
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
            'name' => 'required|string|max:25',
            'animal_id' => 'required|exists:animals,id',
            'user_id' => 'required|exists:users,id'
        ];
    }

    public function messages()
    {
        return [
            'name.required' => 'A név kitöltése kötelező!',
            'name.string' => 'A névnek szövegnek kell lennie!',
            'name.max' => 'A név maximum 25 karakter hosszú lehet!',

            'animal_id.required' => 'Az állat kiválasztása kötelező!',
            'animal_id.exists' => 'Az állatnak léteznie kell az adatbázisban!',

            'user_id.required' => 'Csak bejelentkezett felhasználók tudnak háziállatokat létrehozni',
            'user_id.exits' => 'Csak bejelentkezett felhasználók tudnak háziállatokat létrehozni'
        ];
    }
}
