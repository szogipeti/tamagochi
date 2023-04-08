<?php

use Illuminate\Database\Migrations\Migration;
use Illuminate\Database\Schema\Blueprint;
use Illuminate\Support\Facades\Schema;
use Illuminate\Support\Facades\DB;
use Carbon\Carbon;

return new class extends Migration
{
    /**
     * Run the migrations.
     *
     * @return void
     */
    public function up()
    {
        Schema::create('animal_stats', function (Blueprint $table) {
            $table->id();
            $table->foreignId('user_id')->constrained('users');
            $table->foreignId("animal_id")->constrained("animals","id");
            $table->string("name",25);
            $table->integer("hunger");
            $table->integer("thirst");
            $table->integer("happiness");
            $table->integer("activity");
            $table->integer("health");
            $table->integer("dexterity");
            $table->timestamp('last_hunger');
            $table->timestamp('last_thirst');
            $table->timestamp('last_happiness');
            $table->timestamp('last_activity');
            $table->timestamp('last_health');
            $table->timestamp('last_dexterity');
            $table->timestamp('created_at');
        });
    }

    /**
     * Reverse the migrations.
     *
     * @return void
     */
    public function down()
    {
        Schema::dropIfExists('animal_stats');
    }
};
