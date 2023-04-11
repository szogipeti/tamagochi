<?php

namespace App\Http\Controllers;

use App\Http\Resources\AnimalStatResource;
use App\Models\Animal_stat;
use Illuminate\Http\Request;
use App\Http\Requests\StoreAnimalRequest;
use App\Http\Requests\UpdateAnimalRequest;

class AnimalStatController extends Controller
{
    public function show($id){
        return new AnimalStatResource(Animal_stat::findOrFail($id));
    }

    public function store(StoreAnimalRequest $request){
        $validated = $request->validated();
        return new AnimalStatResource(Animal_stat::create($validated));
    }

    public function update(UpdateAnimalRequest $request, $id){
        $validated = $request->validated();


        $animal = Animal_stat::findOrFail($id);
        $animal->update($validated);

        $animal->save();

        return new AnimalStatResource($animal);
    }
}
