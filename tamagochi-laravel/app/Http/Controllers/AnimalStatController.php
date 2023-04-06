<?php

namespace App\Http\Controllers;

use App\Http\Resources\AnimalStatResource;
use App\Models\Animal_stat;
use Illuminate\Http\Request;
use App\Http\Requests\StoreAnimalRequest;

class AnimalStatController extends Controller
{
    public function show($id){
        return new AnimalStatResource(Animal_stat::findOrFail($id));
    }

    public function store(StoreAnimalRequest $request){
        $validated = $request->validated();
        return new AnimalStatResource(Animal_stat::create($validated));
    }
}
