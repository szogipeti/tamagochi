<?php

namespace Database\Seeders;

use Illuminate\Database\Console\Seeds\WithoutModelEvents;
use Illuminate\Database\Seeder;
use Illuminate\Support\Facades\DB;

class AnimalSeeder extends Seeder
{
    /**
     * Run the database seeds.
     *
     * @return void
     */
    public function run()
    {
        DB::table("animals")->insert(
            [
                ['id' => 1,'name' => 'Tigris','image' => 'img/tigris.png'],
                ['id' => 2,'name' => 'Poro','image' => 'img/poro.png'],
                ['id' => 3,'name' => 'Anivia','image' => 'img/anivia.png'],
            ]);
    }
}
