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

## Források

- https://megaport.hu/media/25454/cartoon-tigers-transparent-png-tigrisek-meserajz
- https://www.deviantart.com/a1azne/art/Poro-906269915
- https://www.pinterest.es/pin/492088696755679523/