<?php

namespace App\Http\Requests;

use Illuminate\Foundation\Http\FormRequest;

class RegisterRequest extends FormRequest
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
            'username' => 'required|string|min:5|max:100|unique:users,username',
            'email' => 'required|string|email|min:6|max:100|unique:users,email',
            'password' => 'required|string|min:5|max:100|confirmed',
            'password_confirmation' => 'required|string|min:5|max:100|same:password'
        ];
    }

    public function messages(){
        return [
            'username.required' => 'A felhasználónév megadása kötelező!',
            'username.string' => 'A felhasználónévnek szövegnek kell lennie!',
            'username.min' => 'A felhasználónévnek minimum :min karakternek kell lennie!',
            'username.max' => 'A felhasználónév maximum :max karakter hosszú lehet!',
            'username.unique' => 'A felhasználónév már használatban van!',

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
            'password.confirmed' => 'A jelszó nem egyezik meg a jelszó megerősítésével!',

            'password_confirmation.required' => 'A jelszó megerősítésének megadása kötelező!',
            'password_confirmation.string' => 'A jelszó megerősítésének szövegnek kell lennie!',
            'password_confirmation.min' => 'A jelszó megerősítésének minimum :min karakternek kell lennie!',
            'password_confirmation.max' => 'A jelszó megerősítésének maximum :max karakter hosszú lehet!',
            'password_confirmation.same' => 'A jelszó megerősítése nem egyezik meg a jelszóval!',
        ];
    }
}
