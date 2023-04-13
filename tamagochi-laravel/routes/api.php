<?php

use Illuminate\Http\Request;
use Illuminate\Support\Facades\Route;
use App\Http\Controllers\AuthController;
use App\Http\Controllers\UserController;
use App\Http\Controllers\AnimalController;
use App\Http\Controllers\AnimalStatController;

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
    Route::post('/password', 'resetPassword')->name('auth.resetPassword');
    Route::middleware(["auth:sanctum"])->get('/profile', 'profile')->name('auth.profile');
    Route::middleware(["auth:sanctum"])->post('/logout','logout')->name('auth.logout');
});

Route::controller(AnimalController::class)->group(function (){
    Route::get('/animals', 'index')->name('animals.index');
    Route::post('/animals', 'store')->name('animals.store');
    Route::get('/animals/{id}', 'show')->whereNumber('id')->name('animals.show');
});

Route::controller(AnimalStatController::class)->group(function (){
    Route::get('/animals/stats/{id}', 'show')->whereNumber('id')->name('animals.stats.show');
    Route::post('/animals/stats', 'store')->name('animals.stats.store');
    Route::put('/animals/stats/{id}/update', 'update')->whereNumber('id')->name('animals.stats.update');
    Route::delete('/animals/stats/{id}', 'destroy')->whereNumber('id')->name('animals.stats.destroy');
});

Route::get('/users/{id}', [UserController::class, 'show'])->name('users.show');
