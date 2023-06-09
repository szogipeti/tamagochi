# Tamagochi - Felhasználói dokumentáció

A Tamagochi egy weboldal, melynek segítségével könnyen ki tudjuk magunkat kapcsolni. Létre tudunk hozni háziállatokat, melyeknek a szükségleteiről folyamatosan gondoskodnunk kell. Minden nap tudunk műveletekt végrehajtani, viszont figyelni kell arra, hogy minden szükséglet megfelelő állapotban van-e, hiszen a lépések száma korlátozott. A cél az, hogy minél tovább életben tartsuk a mi kis Tamagochinkat.

A projekt tartalmaz egy asztali alkalmazást is, amely segítségével adminisztrációs műveleteket tudunk végrehajtani.

## Weboldal

### Rendszerkövetelmények

#### Szükséges alkalmazások, beállítások:

- Docker Desktop, amely letölthető a `https://docs.docker.com/desktop/`
- WSL2 használatának engedélyezése
- A számítógép BIOS-ában engedélyezni kell a virtualizációt

Mivel a weboldal dockerizáltan működik, ezért a rendszerkövetelmémyek megfelelnek a Docker Desktop használatának minimális követelményeinek:
- Windows 10 vagy 11 operációs rendszer
- 64 bites processzor
- Legalább 4 GB RAM

### Telepítés

1. Klónozzuk a projektet a `git clone https://github.com/szogipeti/tamagochi.git` paranccsal
2. Futtassuk a Docker Desktop alkalmazást
3. Futtassuk a `start.sh` shell script-et (Minden szükséges beállítást elvégez)

**Amennyiben szükséges, a parancsokat manuálisan is ki lehet adni**

1. Másoljuk le a `.env.example` fájlt `.env` néven
2. Készítsük el build-et a `docker compose build` parancs segítségével
3. Futtassuk a konténert a `docker compose up` paranccsal
   - A `-f` kapcsolóval meg tudjuk adni a konténer compose fájlának a nevét, melyek különböző felüldefiniálásokat tartalmaznak:
     - `docker-compose.yml` - Alapértelmezett compose fájl
     - `docker-compose.dev.yml` - Fejlesztői felüldefiniálások
     - `docker-compose.prod.yml` - Kész prokelt bemutatásához szükséges felüldefiniálások
     - `docker-compose.user.yml` - Akkor lehet szükséges, ha a felhasználói jogosultságok az adott gépen nem megfelelően lennének beállítva
   - A `-d` kapcsolóval a háttérben tudjuk elindítani a konténert, így továbbra is elérhető lesz a parancssoros felület.
   - Példa a konténer indítására:<br> 
   `docker-compose -f docker-compose.yml -f docker compose.dev.yml up -d`
4. Lépjünk be a konténerbe a `docker-compose exec app fish` paranccsal; A további parancsokat mind itt kell kiadni:
   - Telepítsük a Laravel-hez tartozó függőségeket a `composer install` paranccsal.
   - Generáljuk le az API-hoz szükséges kulcsot a `php artisan key:generate` paranccsal.
   - Telepítsük a Vue.js-hez szükséges függőségeket az `npm install` paranccsal.
   - Futtassuk a migrációs és az adatok betöltésére szolgáló fájlokat a `php artisan migrate --seed` paranccsal.
   - Készítsük el a hivatkozást a képek számára a `php artisan storage:link` paranccsal.

**A `start.sh` manuális végrehajtásához szükséges utasítások vége**

Ezeket elvégezve minden szükséges függőséget telepítettünk. Utolsó lépés a Vite élő szerver elindítása, amit az `npm run dev` konténeren belül való kiadásával tudunk megtenni. Ezután elérhetjük a weboldalt a `http://localhost:8881` címen.

## Asztali alkalmazás