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
            'name' => 'required|unique:animals|string|max:50',
            'image' => 'required|string|max:50'
        ];
    }

    public function messages()
    {
        return [
            'name.required' => 'A név kitöltése kötelező!',
            'name.string' => 'A névnek szövegnek kell lennie!',
            'name.max' => 'A név maximum :max karakter hosszú lehet!',
            'name.unique' => 'Már létezik ilyen nevű állat!' ,

            'image.required' => 'A kép nevének kitöltése kötelező!',
            'image.string' => 'A kép nevének szövegnek kell lennie!',
            'image.max' => 'A kép neve maximum :max karakter hosszú lehet!',
        ];
    }
}
