# Casino

Ez a projekt egy C# WPF-ben készült kaszinó játékfelületet valósít meg, amely különböző klasszikus kaszinójátékokat tartalmaz. A cél egy felhasználóbarát és vizuálisan vonzó alkalmazás létrehozása, amely támogatja a felhasználói adatok és egyenleg kezelését.

## Főbb Funkciók

### Blackjack
- **Játékmenet**:
  - A játékos és az osztó egyaránt két-két lapot kap. A játékos lapjai láthatóak, az osztó egyik lapja rejtett.
  - A cél, hogy a játékos kártyáinak összértéke 21 legyen, vagy közelebb kerüljön a 21-hez, mint az osztó anélkül, hogy túllépné azt.
  - A játékos választhat: **Hit** (új lapot kér), **Stand** (megáll), **Double Down** (tét duplázása és egy lap kérése), vagy **Split** (ha két azonos értékű lapot kapott).
  - Az osztó kötelezően húz, amíg az értéke el nem éri vagy meg nem haladja a 17-et.
  - A győzelem a következő feltételek mellett történik:
    - A játékos értéke közelebb van a 21-hez, mint az osztóé.
    - Az osztó "Bust" (túllépi a 21-et), míg a játékos nem.

### Rulett
- **Játékmenet**:
  - A játékos fogadást helyezhet el különböző opciókra, például számokra, színekre (piros vagy fekete), vagy számcsoportokra (páros/páratlan, alacsony/magas).
  - A rulettkerék forgása után a golyó megáll egy adott számon.
  - A nyeremény a fogadási opciók szorzója szerint kerül kiszámításra, például:
    - Egyenes szám (Straight): 35:1
    - Szín (piros/fekete): 1:1
    - Számcsoport: 2:1
  - A fogadások érvényesítését a kerék forgásának véletlenszerű eredménye biztosítja.

### Slot Gépek
- **Játékmenet**:
  - A játékos kiválaszt egy tétösszeget, majd elindítja a slot gépet.
  - A slot gép három vagy több tárcsát tartalmaz, amelyek különböző szimbólumokat jelenítenek meg.
  - A győzelem akkor történik, ha a tárcsákon megjelenő szimbólumok egy előre meghatározott nyerővonalon helyezkednek el.
  - Példa nyerővonalak és szorzók:
    - Három azonos szimbólum: 10x tét
    - Két azonos és egy wild szimbólum: 5x tét
    - Egyéb kombinációk: különböző szorzók
  - A véletlenszerűséget programozott algoritmus biztosítja, például egy **Random Number Generator (RNG)** használatával.

### Chicken Cross
- **Játékmenet**:
  - A játékos célja, hogy egy "csirkét" vezessen át egy akadálypályán, miközben növeli a tétet minden lépéssel.
  - A pálya véletlenszerű akadályokat tartalmaz, és minden lépés előtt a játékos dönthet: **Továbbmegy** vagy **Kiszáll**.
  - Ha a játékos ütközik egy akadállyal, elveszíti az addigi nyereményt.
  - A játék vége akkor következik be, ha a játékos sikeresen átjut az akadálypályán, vagy úgy dönt, hogy kiszáll, és felveszi az addigi nyereményt.
  - A pálya generálása algoritmusokkal történik, hogy biztosítsa a dinamikus játékmenetet.

## Felhasználói Adatkezelés

- **Regisztráció és Bejelentkezés**: Minden felhasználónak saját fiókja van, amely biztosítja az egyéni játékélményt.
- **Egyenlegkezelés**: Az alkalmazás követi a felhasználó nyereményeit és veszteségeit, és lehetőséget biztosít egyenlegfeltöltésre a bankkártya és számlaadatok biztonságos kezelése mellett.

## Technológiai Háttere

- **Programozási nyelv**: C#
- **UI keretrendszer**: Windows Presentation Foundation (WPF)
- **Adatbázis**: UTF-8 formátumú CSV fájlok

## Használati Útmutató

1. Indítsa el az alkalmazást.
2. Hozzon létre egy új felhasználói fiókot, vagy jelentkezzen be egy meglévővel.
3. Válassza ki a kívánt játékot a főmenüből.
4. Kövesse a játékszabályokat, és élvezze a játékot!

## Fejlesztési Lehetőségek

- **További játékok**: Új kaszinójátékok integrálása a portfólió bővítésére.
- **Haladó grafikai elemek**: Animációk és interaktív dizájn elemek hozzáadása a felhasználói élmény javítása érdekében.

## Szerzők

- **Név**: Élő Zalán László, Reinhardt Benjámin, Roncz Olivér
- **GitHub**: https://github.com/Oliverpartequattro/casino
