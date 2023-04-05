<?php

use Illuminate\Http\Request;
use Illuminate\Support\Facades\Route;

/*
|--------------------------------------------------------------------------
| API Routes
|--------------------------------------------------------------------------
|
| Here is where you can register API routes for your application. These
| routes are loaded by the RouteServiceProvider within a group which
| is assigned the "api" middleware group. Enjoy building your API!
|
*/

Route::middleware('auth:sanctum')->get('/user', function (Request $request) {
    return $request->user();
});

Route::controller(\App\Http\Controllers\AuthController::class)->group(function (){
    Route::post('/login', 'login')->name('auth.login');
    Route::post('/register', 'register')->name('auth.register');
    Route::middleware(["auth:sanctum"])->get('/profile', 'profile')->name('auth.profile');
    Route::middleware(["auth:sanctum"])->post('/logout','logout')->name('auth.logout');
});

Route::get('/users/{id}', [\App\Http\Controllers\UserController::class, 'show'])->name('users.show');
