<?php

namespace App\Http\Controllers;

use App\Http\Resources\FelhasznaloResource;
use App\Models\User;
use Illuminate\Http\Request;

class FelhasznaloController extends Controller
{
    public function show($id){
        return new FelhasznaloResource(User::findOrFail($id));
    }
}
