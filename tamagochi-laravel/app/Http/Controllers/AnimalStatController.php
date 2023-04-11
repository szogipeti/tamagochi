<?php

namespace App\Http\Controllers;

use App\Http\Resources\AnimalStatResource;
use App\Models\AnimalStat;
use Illuminate\Http\Request;
use App\Http\Requests\StoreAnimalStatRequest;
use App\Http\Requests\UpdateAnimalStatRequest;

class AnimalStatController extends Controller
{
    public function show($id){
        return new AnimalStatResource(AnimalStat::findOrFail($id));
    }

    public function store(StoreAnimalStatRequest $request){
        $validated = $request->validated();
        return new AnimalStatResource(AnimalStat::create($validated));
    }

    public function update(UpdateAnimalStatRequest $request, $id){
        $validated = $request->validated();


        $animal = AnimalStat::findOrFail($id);
        $animal->update($validated);

        $animal->save();

        return new AnimalStatResource($animal);
    }

    public function destroy($id){
        AnimalStat::findOrFail($id)->delete();
    }
}
