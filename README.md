# ExamenGame

### Beschrijving
We hebben de opdracht gekregen van de klant om een Match 3 te maken waar complete artistieke creativiteit aan ons is gegeven buiten de Match 3, 
waarbij we ervoor gekozen hebben om een Space Themed Dungeon ish Crawler .te maken

# Geproduceerde Game Onderdelen

Gino Schaap:
  * [Grid System]()
  * [TileSO]()
  * [Tiles]()
  * [Swap Tiles]()
  * [MatchTiles]()
  * [Tile Dropping]()


Julie Jaasma:
  * [Start Screen]()
  * [Options Menu]()
     
Nikki van Wijngaarden:
 * [DeadLock]()

Kiana (Jasper) Hiemstra:
  * [Level Selection Screen]()
  * [Turn Based System]()

Bo Bakker:
*
 
Robin van Wandelen:
*

 Guilherme Loures Oliveira
 * 

Min van der Veen:
 * 
   
## 1. Grid System

Het spel speelt zich af op een rechthoekig grid. Het grid heeft een vaste breedte, hoogte en celgrootte die samen de speelruimte bepalen. Elke cel op het grid kan een Tile bevatten.

---

## 2. Tile Types

Elke Tile heeft een type dat bepaalt hoe hij eruit ziet en hoe hij zich gedraagt. Het type wordt meegegeven als data aan de Tile zodat het spel altijd weet wat voor soort Tile er op een plek staat.

---

## 3. Tile Spawning

Tiles spawnen op lege plekken in het grid met een black hole-effect zodat het visueel aantrekkelijk oogt. Bij de start van een level wordt het volledige grid gevuld. Wanneer Tiles verdwijnen na een match, worden de vrijgekomen plekken direct weer opgevuld. Er spawnt nooit een Tile die direct een match vormt.

---

## 4. Tile Swapping

De speler verplaatst Tiles door ze te verwisselen met een aangrenzende Tile. Als de wissel geen match oplevert, schuift de Tile automatisch terug naar zijn originele positie. Alleen Tiles die grenzen aan de geselecteerde Tile kunnen gewisseld worden.

---

## 5. Tile Match Check

Zodra een swap is uitgevoerd, controleert het spel automatisch of er een match aanwezig is. Een match is een aaneengesloten reeks van drie of meer Tiles van hetzelfde type. De posities van gematchte Tiles worden opgeslagen, waarna die Tiles worden verwijderd en nieuwe Tiles spawnen op de vrijgekomen plekken.

---

## 6. Deadlock System

Als er geen geldige zetten meer mogelijk zijn op het board, shufflet het board automatisch. De speler kan dan niet meer zetten totdat de shuffle klaar is. De shuffle zorgt er altijd voor dat er daarna geldige zetten beschikbaar zijn. Een Tile Limiter houdt bij of er nog zetten mogelijk zijn.

---

## 7. Turn Based Rounds

Het spel verloopt in beurten. Per beurt heeft de speler vijf sets om zetten te doen en matches te maken. Na die vijf sets voert de vijand een aanval uit. De hoeveelheid schade die de vijand doet hangt af van of de Block Slider vol is. Daarna begint een nieuwe beurt.

---

## 8. Enemy Boss

De vijand is een boss met een vaste set aan aanvallen. Elke beurt kiest de boss willekeurig één aanval uit zijn movepool. De schade van die aanval wordt beïnvloed door de Block Slider van de speler.

---

## 9. Tile PowerUp Slider

De speler heeft een PowerUp Slider die oplaadt naarmate er matches worden gemaakt. Wanneer de slider vol is, kan de speler een speciale move activeren via een knop of slider in de HUD. De Block Slider werkt op dezelfde manier en vermindert de schade van de boss wanneer hij vol is.

---

## 10. Screens

### Start Screen
Het eerste scherm dat de speler ziet. Bevat drie knoppen:
- **Play** → [Level Selection Screen](#level-selection-screen)
- **Options** → Opent het instellingenmenu
- **Quit** → Sluit het spel af

---

### Level Selection Screen
Scherm waar de speler een level kiest. Het aantal level-knoppen past zich automatisch aan op het totale aantal beschikbare levels.
- **Level [X]** → Start het gekozen level
- **Back** → [Start Screen](#start-screen)

---

### Death Screen
Wordt getoond wanneer de speler het level verliest. Er staat een tekst die aangeeft dat de speler heeft verloren.
- **Retry** → Herstart het huidige level
- **Menu** → [Start Screen](#start-screen)

---

### Win Screen
Wordt getoond wanneer de speler het level wint. Er staat een tekst die aangeeft dat de speler heeft gewonnen.
- **Retry** → Herstart het huidige level
- **Menu** → [Start Screen](#start-screen)

---

### Tutorial Screen *(optioneel)*
Een apart scherm dat uitlegt hoe het spel werkt. De speler krijgt stap voor stap uitleg over de belangrijkste mechanics zoals Tile Swapping, matches maken en hoe de sliders werken. Er zijn geen chains of complexe mechanics actief tijdens de tutorial.
- **Terug** → [Start Screen](#start-screen)
