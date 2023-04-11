<?php

namespace App\Http\Controllers;

use App\Http\Requests\LoginRequest;
use App\Http\Requests\RegisterRequest;
use App\Http\Resources\AuthResource;
use App\Models\User;
use Illuminate\Http\JsonResponse;
use Illuminate\Http\Request;
use Illuminate\Support\Facades\Auth;
use Illuminate\Support\Facades\Hash;

class AuthController extends Controller
{
    public function login(LoginRequest $request)
    {
        $data = $request->only('email', 'password');
        if (Auth::attempt($data)) {

            return response()->json(new AuthResource(Auth::user(), 200));
        }
        return response()->json(["data" => ["message" => "Hibás felhasználónév és/vagy jelszó!"]],401);
    }


    public function register(RegisterRequest $request):JsonResponse
    {
        $data = $request->validated();
        $data["password"] = Hash::make($data["password"]);
        $user = User::create($data);

        return response()->json(["data" => ["message" => "Sikeres regisztráció!"]],201);


    }
    public function profile()
    {
        return response()->json(Auth::user(),200);
    }

    public function logout()
    {
        Auth::user()->tokens()->delete();
        auth()->logout();

        return response()->json(['message' => 'Sikeresen kijelentkezett.'], 200);
    }
    public function resetPassword(Request $request)
    {
        # Validation
        $request->validate([
            'id' => 'required',
            'new_password' => 'required',
        ]);

        #Update the new Password
        User::whereId($request->id)->update([
            'password' => Hash::make($request->new_password)
        ]);

        return back()->with("status", "Password changed successfully!");
    }

}
