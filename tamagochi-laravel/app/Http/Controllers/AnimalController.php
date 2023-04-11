<?php

namespace App\Http\Controllers;

use App\Http\Requests\StoreAnimalRequest;
use App\Http\Resources\AnimalResource;
use App\Models\Animal;

class AnimalController extends Controller
{
    public function index(): \Illuminate\Http\Resources\Json\AnonymousResourceCollection
    {
        header("info: hello2");
        return AnimalResource::collection(Animal::all());
    }

    public function store(StoreAnimalRequest $request){
        header("info: hello");
        $validated = $request->validated();
        return new AnimalResource(Animal::create($validated));
    }
}
