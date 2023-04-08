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
        $animal['name'] = $validated['name']??$animal['name'];
        $animal['hunger'] = $validated['hunger']??$animal['hunger'];
        $animal['thirst'] = $validated['thirst']??$animal['thirst'];
        $animal['happiness'] = $validated['happiness']??$animal['happiness'];
        $animal['activity'] = $validated['activity']??$animal['activity'];
        $animal['health'] = $validated['health']??$animal['health'];
        $animal['dexterity'] = $validated['dexterity']??$animal['dexterity'];

        $animal->save();

        return new AnimalStatResource($animal);
    }
}
