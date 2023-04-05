<?php

use Illuminate\Database\Migrations\Migration;
use Illuminate\Database\Schema\Blueprint;
use Illuminate\Support\Facades\Schema;

return new class extends Migration
{
    /**
     * Run the migrations.
     *
     * @return void
     */
    public function up()
    {
        Schema::create('animals', function (Blueprint $table) {
            $table->id();
            $table->string("Név",25);
            $table->integer("Éhség");
            $table->integer("Szomjúság");
            $table->integer("Jókedv");
            $table->integer("Mozgás");
            $table->integer("Egészség");
            $table->integer("Ügyesség");




        });
    }

    /**
     * Reverse the migrations.
     *
     * @return void
     */
    public function down()
    {
        Schema::dropIfExists('animals');
    }
};
