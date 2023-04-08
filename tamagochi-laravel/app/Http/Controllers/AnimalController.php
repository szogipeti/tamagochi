<?php

namespace App\Http\Controllers;

use App\Http\Resources\AnimalResource;
use Illuminate\Http\Request;
use App\Models\Animal;

class AnimalController extends Controller
{
    public function index(): \Illuminate\Http\Resources\Json\AnonymousResourceCollection
    {
        return AnimalResource::collection(Animal::all());
    }
}
