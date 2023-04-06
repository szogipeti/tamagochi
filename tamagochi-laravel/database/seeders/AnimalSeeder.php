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
                ['id' => 1,'name' => 'Tigris'],
                ['id' => 2,'name' => 'Poro'],
                ['id' => 3,'name' => 'Anivia'],
            ]);
    }
}
