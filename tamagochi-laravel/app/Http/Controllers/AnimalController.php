<?php

namespace App\Http\Controllers;

use App\Http\Requests\StoreAnimalRequest;
use App\Http\Resources\AnimalResource;
use App\Models\Animal;

class AnimalController extends Controller
{
    public function index(): \Illuminate\Http\Resources\Json\AnonymousResourceCollection
    {
        return AnimalResource::collection(Animal::all());
    }

    public function store(StoreAnimalRequest $request){
        $validated = $request->validated();
        return new AnimalResource(Animal::create($validated));
    }

    public function show($id){
        return new AnimalResource(Animal::findOrFail($id));
    }
}
