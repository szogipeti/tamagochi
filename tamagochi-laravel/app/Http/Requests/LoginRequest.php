<?php

namespace App\Http\Requests;

use http\Message;
use Illuminate\Foundation\Http\FormRequest;

class LoginRequest extends FormRequest
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
            'email' => 'required|string|email|min:6|max:100|',
            'password' => 'required|string|min:5|max:100||'
        ];
    }

    public function messages(){
        return [
            'email.required' => 'Az e-mail megadása kötelező!',
            'email.string' => 'Az e-mailnek szövegnek kell lennie!',
            'email.email' => 'Az e-mailnek nem megfelelő a formátuma!',
            'email.min' => 'Az e-mailnek minimum :min karakternek kell lennie!',
            'email.max' => 'Az e-mail maximum :max karakter hosszú lehet!',
            'email.unique' => 'Az e-mail már használatban van!',

            'password.required' => 'A jelszó megadása kötelező!',
            'password.string' => 'A jelszónak szövegnek kell lennie!',
            'password.min' => 'A jelszónak minimum :min karakternek kell lennie!',
            'password.max' => 'A jelszónak maximum :max karakter hosszú lehet!',
        ];
    }
}
