# Tamagochi - Fejlesztői dokumentáció

## Alkalmazott fejlesztői eszközök

- JavaScript
- PHP
- Laravel
- Vue.js
- MySQL
- phpMyAdmin
- C#
- GitHub

## Fájlok elérése

### Weboldal

*Mappa: `tamagochi-laravel`*

#### Backend

- API kontrollerek, kérések és erőforrások - `app/Http`
- Modellek - `app/Models`
- Konfigurációs fájlok - `config`
- Adatbázishoz kapcsolódó fájlok - `database`
- Képek - `storage/app/img`

#### Frontend

*Mappa: `resources`*

- Globális stílus - `css/app.scss`
- Html oldal - `views`
- Vue.js komponensek, oldalak, útvonalak - `js`

### Asztali alkalmazás

*Mappa: `Tamagochi`*

*Adatbázis-modell: `Tamagochi/Tamagochi/Model`*

### Tesztek

- Weboldal Selenium tesztjei - `TamagochiSeleniumTests`
- Asztali alkalmazás tesztjei - `Tamagochi/TamagochiTests`

## Táblák

### Adatbázis diagram

![Adatbázis diagram](/tamagochi_db.drawio.png)

### Állatok - animals

| Mező  | Adattípus       | Leírás                              | Tulajdonságok    |
|-------|-----------------|-------------------------------------|------------------|
| id    | Unsigned Bigint | Egyedi azonosító (elsődleges kulcs) | Egyedi           |
| name  | String          | Állatfajta neve                     | Max. 50 karakter |
| image | String          | Képfájl elérése                     | Max. 50 karakter |

### Állatok adatai - animal_stats

| Mező           | Adattípus       | Leírás                                                | Tulajdonságok                            |
|----------------|-----------------|-------------------------------------------------------|------------------------------------------|
| id             | Unsigned Bigint | Egyedi azonosító (elsődleges kulcs)                   | Egyedi                                   |
| user_id        | Unsigned Bigint | Állat azonosítója (idegen kulcs: users - id)          |                                          |
| animal_id      | Unsigned Bigint | Állat azonosítója (idegen kulcs: animals - id)        |                                          |
| name           | String          | Állat neve                                            | Max. 25 karakter                         |
| hunger         | Integer         | Az állat éhség adata                                  |                                          |
| thirst         | Integer         | Az állat szomjúság adata                              |                                          |
| happiness      | Integer         | Az állat boldogság adata                              |                                          |
| activity       | Integer         | Az állat mozgás adata                                 |                                          |
| health         | Integer         | Az állat egészség adata                               |                                          |
| dexterity      | Integer         | Az állat ügyesség adata                               |                                          |
| action_count   | Integer         | Az állathoz tartozó napi lépések száma                |                                          |
| last_hunger    | Timestamp       | Az állat éhségének utolsó módosításának időpontja     | Alapértelmezetten a létrehozás időpontja |
| last_thirst    | Timestamp       | Az állat szomjúságának utolsó módosításának időpontja | Alapértelmezetten a létrehozás időpontja |
| last_happiness | Timestamp       | Az állat boldogságának utolsó módosításának időpontja | Alapértelmezetten a létrehozás időpontja |
| last_action    | Timestamp       | Az állat mozgásának utolsó módosításának időpontja    | Alapértelmezetten a létrehozás időpontja |
| last_health    | Timestamp       | Az állat egészségének utolsó módosításának időpontja  | Alapértelmezetten a létrehozás időpontja |
| last_dexterity | Timestamp       | Az állat ügyességének utolsó módosításának időpontja  | Alapértelmezetten a létrehozás időpontja |
| created_at     | Timestamp       | Az állat létrehozásának időpontja                     |                                          |

### Gyógyszerek - medications

| Mező        | Adattípus       | Leírás                              | Tulajdonságok |
|-------------|-----------------|-------------------------------------|---------------|
| id          | Unsigned Bigint | Egyedi azonosító (elsődleges kulcs) | Egyedi        |
| medications | String          | Gyógyszer neve                      |               |

### Felhasználók - users

| Mező              | Adattípus       | Leírás                                | Tulajdonságok |
|-------------------|-----------------|---------------------------------------|---------------|
| id                | Unsigned Bigint | Egyedi azonosító (elsődleges kulcs)   | Egyedi        |
| username          | String          | Felhasználónév                        | Egyedi        |
| email             | String          | E-mail cím                            | Egyedi        |
| email_verified_at | Timestamp       | E-mail cím hitelesítésének időbélyege | Nullable      |
| password          | String          | Jelszo hashelve                       |               |
| remember_token    | String          | Biztonsági token                      |               |
| created_at        | Timestamp       | Létrehozás időbélyege                 |               |
| updated_at        | Timestamp       | Módosítás időbélyege                  |               |




Recipe

URL for model: /recipes Controller: RecipeController
Name 	HTTP method 	Parameter of URL 	Action 	Description 	Requirements
recipes.index 	GET 		index 	Gets all recipes 	
recipes.show 	GET 	/{id} - number 	show 	Gets one recipe 	
recipes.store 	POST 		store 	Creates new recipe 	Authenticated
recipes.update 	PUT 	/{id} - number 	update 	Updates one recipe 	Authenticated
recipes.destroy 	DELETE 	/{id} - number 	destroy 	Deletes one recipe 	Authenticated


### Rest API

Base URL: https://localhost:8881/api/

# Auth

Controller: AuthController

| Name              | HTTP method     | Parameter of URL                      | Action 		| Description 					   | Requirements |
|-------------------|-----------------|---------------------------------------|-------------| ---------------------------------|--------------|
| auth.login        | POST            |/login							      | index  		| Sends login request      		   |			  |
| auth.register     | POST            | /register                        	  | show   		| Sends register request      	   |		 	  |
| auth.profile      | GET             | /profile 	                          | show   		| Gets details of logged in profile|Authenticated |
| auth.logout       | POST            | /logout 							  | show   		| Sends logout request    		   |Authenticated | 
| auth.resetPassword| POST            |/password                      		  |resetPassword| Chnage user password             |Authenticated |

AuthResource

    username
    token

#User

URL for model: /useres Controller: UserController

| Name              | HTTP method     | Parameter of URL                      | Action 		| Description 					   |
|-------------------|-----------------|---------------------------------------|-------------| ---------------------------------|
| users.show        | GET             |/{id} - number					      | show  		| Gets one user		      		   |

UserResource

    id
    username

#Animal

URL for model: /animals Controller: AnimalController

| Name              | HTTP method     | Parameter of URL                      | Action 		| Description 					   |
|-------------------|-----------------|---------------------------------------|-------------| ---------------------------------|
| animals.index     | GET             | 								      | index  		| Gets all animals	    		   |
| animals.store     | POST            | 			                     	  | store   	| Sends register request      	   |
| animals.show      | GET             | /{id} - number                        | show   		| Gets one animal 	  			   |

AnimalResource

id
name
image

#AnimalStat

URL for model: /animals/stats Controller: AnimalStatController

| Name              	| HTTP method     | Parameter of URL                      | Action 		| Description 					   | Requirements |
|-----------------------|-----------------|---------------------------------------|-------------| ---------------------------------|--------------|
| animals.stats.show    | GET             |/{id}- number					      | show  		| Gets one animal	    		   | 			  |
| animals.stats.store   | POST            | 			                     	  | store   	| Creates new animal      	   	   |Authenticated |
| animals.stats.update  | PUT             | /{id}/update  - number                | update   	| Updates one animal 	  		   |Authenticated |
| animals.stats.destroy | DELETE          | /{id}	- number	                  | destroy   	| Deletes one animal 	  		   |Authenticated |

AnimalStatResource

id
user_id
animal_id
name
hunger
thirst
happiness
activity
health
dexterity
action_count
last_hunger
last_thirst
last_happiness
last_activity
last_health
last_dexterity
last_action
created_at

## Források

- https://megaport.hu/media/25454/cartoon-tigers-transparent-png-tigrisek-meserajz
- https://www.deviantart.com/a1azne/art/Poro-906269915
- https://www.pinterest.es/pin/492088696755679523/