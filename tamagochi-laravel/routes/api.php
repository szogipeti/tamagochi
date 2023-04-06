<?php

use Illuminate\Http\Request;
use Illuminate\Support\Facades\Route;
use App\Http\Controllers\AuthController;
use App\Http\Controllers\UserController;
use App\Http\Controllers\AnimalController;

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

Route::controller(AuthController::class)->group(function (){
    Route::post('/login', 'login')->name('auth.login');
    Route::post('/register', 'register')->name('auth.register');
    Route::middleware(["auth:sanctum"])->get('/profile', 'profile')->name('auth.profile');
    Route::middleware(["auth:sanctum"])->post('/logout','logout')->name('auth.logout');
});

Route::get('/animals', [AnimalController::class, 'index'])->name('animals.index');

Route::get('/users/{id}', [UserController::class, 'show'])->name('users.show');
