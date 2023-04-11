<?php

namespace Database\Seeders;

use Illuminate\Database\Console\Seeds\WithoutModelEvents;
use Illuminate\Database\Seeder;

class MedicationSeeder extends Seeder
{
    /**
     * Run the database seeds.
     *
     * @return void
     */
    public function run()
    {
        DB::table("medications")->insert(
            [
                ['id' => 1,'name' => 'Aspirin'],
                ['id' => 2,'name' => 'Benuron'],
                ['id' => 3,'name' => 'Cataflam'],
                ['id' => 4,'name' => 'ACC'],
                ['id' => 5,'name' => 'Bronchostop'],
                ['id' => 6,'name' => 'Cavinton'],
                ['id' => 7,'name' => 'Paretin'],
                ['id' => 8,'name' => 'Yarocen'],
                ['id' => 9,'name' => 'Velaxin'],
                ['id' => 10,'name' => 'Zoloft'],
            ]);
    }
}
