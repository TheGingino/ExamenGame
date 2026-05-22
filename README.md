### Beschrijving
We hebben de opdracht gekregen van de klant om een Match 3 te maken waar complete artistieke creativiteit aan ons is gegeven buiten de Match 3, 
waarbij we ervoor gekozen hebben om een Space Themed Dungeon ish Crawler .te maken

# Geproduceerde Game Onderdelen

Gino Schaap:
  * [Grid System](https://github.com/TheGingino/ExamenGame/blob/Develop/Match-3-Team2/Assets/Scripts/Match3/TileS/GridSystem.cs)
  * [Tiles](https://github.com/TheGingino/ExamenGame/blob/Develop/Match-3-Team2/Assets/Scripts/Match3/TileS/Tile.cs)
  * [Swap Tiles](https://github.com/TheGingino/ExamenGame/blob/Develop/Match-3-Team2/Assets/Scripts/Match3/TileS/SpawnTiles.cs)
  * [MatchTiles](https://github.com/TheGingino/ExamenGame/blob/Develop/Match-3-Team2/Assets/Scripts/Match3/TileS/MatchTiles.cs)
  * [Tile Gravity](https://github.com/TheGingino/ExamenGame/blob/Develop/Match-3-Team2/Assets/Scripts/Match3/TileS/TileGravity.cs)
  * [Bomb](https://github.com/TheGingino/ExamenGame/blob/Develop/Match-3-Team2/Assets/Scripts/PowerUp/Bomb.cs)
  * [Save System](https://github.com/TheGingino/ExamenGame/blob/Develop/Match-3-Team2/Assets/Scripts/SaveSystem/SaveSystem.cs)
  * [Level Selection Scroll]()
  * [Framerate Display](https://github.com/TheGingino/ExamenGame/blob/Develop/Match-3-Team2/Assets/Scripts/FrameCheck.cs)
  * [Level Creator]()


Julie Jaasma:
  * [Start Screen](https://github.com/TheGingino/ExamenGame/blob/Develop/Match-3-Team2/Assets/Scripts/Menu/StartScreen.cs)
  * [Options Menu](https://github.com/TheGingino/ExamenGame/blob/Develop/Match-3-Team2/Assets/Scripts/Menu/MenuSlider.cs)
  * [Enemy Behaviour](https://github.com/TheGingino/ExamenGame/tree/Develop/Match-3-Team2/Assets/Scripts/Enemy)
  * [Enemy Health](https://github.com/TheGingino/ExamenGame/blob/Develop/Match-3-Team2/Assets/Scripts/Enemy/EnemyHealth.cs)
  * [Enemy Attack](http://github.com/TheGingino/ExamenGame/blob/Develop/Match-3-Team2/Assets/Scripts/Enemy/EnemyAttack.cs)
  * [Player Behaviour](https://github.com/TheGingino/ExamenGame/tree/Develop/Match-3-Team2/Assets/Scripts/Player)
  * [Player Health](https://github.com/TheGingino/ExamenGame/blob/Develop/Match-3-Team2/Assets/Scripts/Player/PlayerHealth.cs)
  * [Player Attack](https://github.com/TheGingino/ExamenGame/blob/Develop/Match-3-Team2/Assets/Scripts/Player/PlayerAttack.cs)
  * [Player Shield](https://github.com/TheGingino/ExamenGame/blob/Develop/Match-3-Team2/Assets/Scripts/Player/PlayerShield.cs)
  * [HealthBar](https://github.com/TheGingino/ExamenGame/blob/Develop/Match-3-Team2/Assets/Scripts/Player/HealthBar.cs)
  * [Combat Meter Bars](https://github.com/TheGingino/ExamenGame/blob/Develop/Match-3-Team2/Assets/Scripts/Abilities/CombatMeterBar.cs)
  * [Charge Visuals](https://github.com/TheGingino/ExamenGame/blob/Develop/Match-3-Team2/Assets/Scripts/UI/Charges.cs)
     
Nikki van Wijngaarden:
 * [TileSO](https://github.com/TheGingino/ExamenGame/blob/Develop/Match-3-Team2/Assets/Scripts/Match3/TileS/TileSO.cs)
 * [AbilitieCounterUI](https://github.com/TheGingino/ExamenGame/blob/Develop/Match-3-Team2/Assets/Scripts/Abilities/AbilitieCounterUI.cs)
 * [SwapTiles](https://github.com/TheGingino/ExamenGame/blob/Develop/Match-3-Team2/Assets/Scripts/Match3/TileS/SpawnTiles.cs)
 * [CombatMeter](https://github.com/TheGingino/ExamenGame/blob/Develop/Match-3-Team2/Assets/Scripts/Match3/TileS/CombatMeter.cs)
 * [PlayerAttack](https://github.com/TheGingino/ExamenGame/blob/Develop/Match-3-Team2/Assets/Scripts/Player/PlayerAttack.cs)
 * [PlayerHealth](https://github.com/TheGingino/ExamenGame/blob/Develop/Match-3-Team2/Assets/Scripts/Player/PlayerHealth.cs)
 * [PlayerShield](https://github.com/TheGingino/ExamenGame/blob/Develop/Match-3-Team2/Assets/Scripts/Player/PlayerShield.cs)


Kiana (Jasper) Hiemstra:
  * [Level Selection Screen (OLD)]()
  * [Turn Based System](https://github.com/TheGingino/ExamenGame/blob/Develop/Match-3-Team2/Assets/scripts/TurnSystem/TurnManager.cs)
  * [Win Lose Condition](https://github.com/TheGingino/ExamenGame/blob/Develop/Match-3-Team2/Assets/Scripts/Menu/GameEndManager.cs)
  * [Tutorial](https://github.com/TheGingino/ExamenGame/blob/Develop/Match-3-Team2/Assets/Scripts/Menu/TutorialManager.cs)
  * [Main Scene]()
    
Bo Bakker:
* enemy model
* boss enviroment
* tutorial
* health bar
* slash animation
* special attack animation
* game border
* damage rim
 
Robin van Wandelen:
*

 Guilherme Loures Oliveira
 * Grid visuals
 * Move counter
 * Energy counter
 * Turn indicator
 * Settings button
 * Ability button
 * Ability charges
 * Bomb

Min van der Veen:
 * Beginscherm
 * Level select scherm
 * Wapen
 * UI knoppen
 * Pause screen/ win-lose screen
 * Level 2 Environment
   
## 1. Grid System

Het spel speelt zich af op een rechthoekig grid. Het grid heeft een vaste breedte, hoogte en celgrootte die samen de speelruimte bepalen. Elke cel op het grid kan een Tile bevatten.
```mermaid
---
config:
  layout: elk
---
flowchart TD
    A([Start]) --> B[CreateGrid\nSpawn Quad per cell]
    B --> C[GetWorldPosition\nx,y → Vector2]
    B --> D[GetTile\nOverlapBox → Tile]
    B --> E[GetGridPosition\nworldPos → Vector2Int]
    D --> F[LayerMask Tile\nPhysics-based lookup]
```

---

## 2. Tile Types

Elke Tile heeft een type dat bepaalt hoe hij eruit ziet en hoe hij zich gedraagt. Het type wordt meegegeven als data aan de Tile zodat het spel altijd weet wat voor soort Tile er op een plek staat. Je hebt 5 soorten Tiles: Attack Tile, Shield Tile, Heal Tile, Special Tile en een Filler Tile. ze bouwen allemaal een bar op behalve de filler. Die is er puur om de grid te vullen met extra tiles.

```mermaid
---
config:
  layout: elk
---
classDiagram
    class Tile {
        -TileSO tileData
        -bool canBeSwapped
        +TileSO _tileData
        +SetType(TileSO) void
        +DestroyTile() void
        +Highlight() void
    }
    class TileSO {
        +int HealAmount
        +int DamageAmount
        +int ShieldAmount
        +int specialAttackAmount
        +Sprite tileSprite
        +TileType tileType
        -OnEnable() void
    }
    class TileType {
        <<enumeration>>
        Normal
        Heal
        Shield
        Damage
        Special
    }
    Tile --> TileSO : has
    TileSO --> TileType : uses
```


---

## 3. Tile Spawning

Tiles spawnen op lege plekken in het grid met een black hole-effect zodat het visueel aantrekkelijk oogt. Bij de start van een level wordt het volledige grid gevuld. Wanneer Tiles verdwijnen na een match, worden de vrijgekomen plekken direct weer opgevuld. Er spawnt nooit een Tile die direct een match vormt.

```mermaid
---
config:
  layout: elk
---
flowchart TD
    A([Awake]) --> B[Wire GridSystem\nTileGravity · Transform]
    C([Start]) --> D[SpawnInitialTiles\nfor every x,y]
    D --> E[SpawnDirect x,y\nGetNonMatchingTile → Instantiate → Register]
    E --> F[GetNonMatchingTile\nLoop until WouldMatch = false]
    G([FillColumn col, count\ncalled by MatchTiles or Bomb]) --> H[Spawn above grid top\nstacked positions]
    H --> I[EnqueueTile → TileGravity]
```

---

## 4. Tile Swapping

De speler verplaatst Tiles door ze te verwisselen met een aangrenzende Tile. Als de wissel geen match oplevert, schuift de Tile automatisch terug naar zijn originele positie. Alleen Tiles die grenzen aan de geselecteerde Tile kunnen gewisseld worden.

```mermaid
---
config:
  layout: elk
---
flowchart TD
    A([Update called every frame]) --> B[HandleClickSwap]
    A --> C[HandleDrag]

    B --> B1{isSwapping or\ninputDisabled or\nisDragging?}
    B1 -->|yes| B2([Skip])
    B1 -->|no| B3{MouseButtonDown?}
    B3 -->|no| B2
    B3 -->|yes| B4[Raycast → get clicked tile]
    B4 --> B5{firstTile == null?}
    B5 -->|yes| B6[Set firstTile]
    B5 -->|no| B7{secondTile == null\nand different tile?}
    B7 -->|no| B2
    B7 -->|yes| B8{AreAdjacent?}
    B8 -->|no| B9[ResetSelection]
    B8 -->|yes| B10[StartCoroutine Swap]

    C --> C1{isSwapping or\ninputDisabled?}
    C1 -->|yes| C2([Skip])
    C1 -->|no| C3{Touch input?}
    C3 -->|yes| C4[Handle TouchPhase\nBegan / Moved / Ended]
    C3 -->|no| C5[Handle Mouse\nDown / Hold / Up]
    C4 --> C6[TryBeginDrag or\nTryCompleteDrag]
    C5 --> C6

    C6 --> C7{AreAdjacent?}
    C7 -->|no| C8([Wait])
    C7 -->|yes| B10

    B10 --> D[Animate swap visually\nLerp positions]
    D --> E[Update _TileGrid]
    E --> F{HasMatches?}
    F -->|no| G[Reverse animation\nRestore grid]
    G --> H[ResetSelection]
    F -->|yes| I[TriggerMatchCheck\nRegisterSwap]
    I --> H
    H --> A
```

---

## 5. Tile Match Check

Zodra een swap is uitgevoerd, controleert het spel automatisch of er een match aanwezig is. Een match is een aaneengesloten reeks van drie of meer Tiles van hetzelfde type. De posities van gematchte Tiles worden opgeslagen, waarna die Tiles worden verwijderd en nieuwe Tiles spawnen op de vrijgekomen plekken.

<img src="https://github.com/TheGingino/ExamenGame/blob/Develop/WikiResources/Gino/match3.gif" width="70%">

---

## 6. Deadlock System

Als er geen geldige zetten meer mogelijk zijn op het board, shufflet het board automatisch. De speler kan dan niet meer zetten totdat de shuffle klaar is. De shuffle zorgt er altijd voor dat er daarna geldige zetten beschikbaar zijn. Een Tile Limiter houdt bij of er nog zetten mogelijk zijn.

```mermaid
---
config:
  layout: elk
---
flowchart TD
    A([CheckForMatches]) --> B[GetMatchingTiles\nScan horizontal + vertical triplets]
    B --> C{matches.Count >= 3?}
    C -->|no| Z([Done])
    C -->|yes| D[ResolveMatches coroutine\nPause gravity]
    D --> E[Tally per TileType\nHeal / Damage / Shield / Special]
    E --> F[CombatMeter.Instance.Add]
    F --> G[Destroy matched tiles]
    G --> H[ApplyGravityContinuously]
    H --> I[FillColumn per cleared column]
    I --> J[Resume gravity]
    J --> A
```
---
## Tile Gravity
Zodra er tiles gebroken worden spawnen er niet alleen nieuwe tiles in, ze vallen ook van boven naar beneden en de tiles die boven de gebroken tiles stonden vallen dan op de plek van de verwijderde tiles. Het spawnt van column naar column.

<img src="https://github.com/TheGingino/ExamenGame/blob/Develop/WikiResources/Gino/TIleGravity.png" width="75%">
---

## Bomb Powerup
De bomb doet een ding en dat is een 3x3 range van tiles op de grid kapot maken. Hij kan ook minder verwijderen als je hem ver boven zet. Je hebt een use per level tenzij je er meerdere zou kunnen unlocken. 

![Bomb][https://github.com/TheGingino/ExamenGame/blob/Develop/WikiResources/Gino/Bomb.gif]

<img src="https://github.com/TheGingino/ExamenGame/blob/Develop/WikiResources/Gino/BombLogic.png" width="75%">

---

## 7. Turn Based Rounds

Het spel verloopt in beurten. Per beurt heeft de speler vijf sets om zetten te doen en matches te maken. Na die vijf sets voert de vijand een aanval uit. De hoeveelheid schade die de vijand doet hangt af van of de Block Slider vol is. Daarna begint een nieuwe beurt.

---

### Flowchart

```mermaid
flowchart TD

    A[Game Starts] --> B["TurnManager.Start()"]
    B --> C[Find SwapTiles]
    B --> D["StartPlayerTurn()"]

    %% PLAYER TURN

    D --> E[Set playerTurn = true]
    E --> F[Reset Current Swaps = 0]
    F --> G[Enable Tile Input]
    G --> H[Invoke OnPlayerTurnStart]
    H --> I[Show Player Turn Indicator]
    I --> J[Update Swap UI]

    %% PLAYER ACTION LOOP

    J --> K[Player Swaps Tiles]
    K --> L["RegisterSwap()"]

    L --> M{Is Player Turn?}

    M -->|No| N[Ignore Swap]
    M -->|Yes| O[Increase Swap Count]

    O --> P[Update Remaining Swaps UI]

    P --> Q{Swaps >= Max Swaps?}

    Q -->|No| K[Player Swaps Tiles]
    Q -->|Yes| R["EndPlayerTurn()"]

    %% END PLAYER TURN

    R --> S[Set playerTurn = false]
    S --> T[Disable Tile Input]
    T --> U[Invoke OnEnemyTurnStart]
    U --> V[Show Enemy Turn Indicator]
    V --> W["Start EnemyTurnCoroutine()"]

    %% ENEMY TURN

    W --> X[Wait 0.5 Seconds]
    X --> Y[Invoke OnEnemyTurn Event]
    Y --> Z[Wait 2.5 Seconds]
    Z --> D

    %% TURN INDICATOR FLOW

    I --> AA["ShowTurnImage(true)"]
    V --> AB["ShowTurnImage(false)"]

    AA --> AC["FadeTurnIndicator()"]
    AB --> AC

    AC --> AD[Fade Out Current Indicator]
    AD --> AE[Fade In Next Indicator]
    AE --> AF[Fade Out Next Indicator]

    %% FADE SYSTEM

    AF --> AG[Fade Coroutine]
    AG --> AH[Get or Add CanvasGroup]
    AH --> AI[Lerp Alpha Over Time]
    AI --> AJ{Target Alpha = 0?}

    AJ -->|Yes| AK[Disable Indicator]
    AJ -->|No| AL[Keep Active]

    %% UI

    P --> AM[Update Counter Text]
    AM --> AN[Remaining Swaps = Max - Current]
```
---
## 8. Enemy Boss

## Enemy Attack
Na elke 5 matches van de speler start de beurt van de vijand. De vijand voert vervolgens een aanval uit met een willekeurig gekozen attack animation. Tijdens deze aanval wordt er ook een sound effect afgespeeld om de aanval meer feedback te geven aan de speler.

Wanneer de aanval is uitgevoerd, gebruikt het systeem een random roll om te bepalen hoeveel damage de speler ontvangt. Dit gebeurt met een gewogen random systeem. Eerst wordt er een willekeurig getal tussen de 0 en 100 gerold. Op basis van deze uitkomst wordt bepaald hoeveel damage de vijand doet.

De kansverdeling werkt als volgt:

* 1 damage → 50% kans
* 2 damage → 25% kans
* 3 damage → 15% kans
* 4 damage → 10% kans

Hierdoor komen lage damagewaarden vaker voor dan hoge damagewaarden. Dit zorgt ervoor dat de aanvallen van de vijand onvoorspelbaar blijven, terwijl de speler meestal geen extreem hoge damage ontvangt. Zodra de damage is berekend, verliest de speler health en gaat de healthbar omlaag.


### Flowchart
```mermaid
flowchart TD

    A["Game Starts"] --> B["Awake"]
    B --> C["Find PlayerHealth"]

    C --> D["DoDamage Called"]

    D --> E["Start AttackRoutine Coroutine"]

    E --> F["Generate Random Attack Index 1-3"]

    F --> G["Trigger Attack Animation"]

    G --> H["Play Attack SFX"]

    H --> I["Wait 1 Second"]

    I --> J["RollDamage"]

    J --> K["Generate Random Number 0-99"]

    K --> L{"Damage Roll Result"}

    L -->|0-49| M["Damage = 1"]
    L -->|50-74| N["Damage = 2"]
    L -->|75-89| O["Damage = 3"]
    L -->|90-99| P["Damage = 4"]

    M --> Q["Debug Log Damage"]
    N --> Q
    O --> Q
    P --> Q

    Q --> R["PlayerHealth TakeDamage"]
```
### Class Diagram

```mermaid
classDiagram

class EnemyAttack {
    - PlayerHealth playerHealth
    - bool enemyTurn
    - Animator animator
    - AudioSource attackSFX

    + Awake()
    + DoDamage()
    + RollDamage()
    + AttackRoutine()
}

class PlayerHealth {
    + TakeDamage(int damage)
}

class Animator {
    + SetTrigger(string triggerName)
}

class AudioSource {
    + Play()
}

class Random {
    + Range(int min, int max)
}

EnemyAttack --> PlayerHealth : damages player
EnemyAttack --> Animator : triggers animations
EnemyAttack --> AudioSource : plays audio
EnemyAttack --> Random : generates values
```

## Enemy Health
De enemy heeft een eigen health systeem dat gekoppeld is aan een healthbar. Wanneer de speler een attack of special attack gebruikt, ontvangt de enemy damage en gaat de healthbar omlaag. Hierdoor krijgt de speler direct visuele feedback over hoeveel health de enemy nog over heeft.

De damage wordt verwerkt via het enemy health script. Dit script houdt bij hoeveel health de enemy nog heeft en vermindert deze waarde wanneer de enemy geraakt wordt. Daarna wordt de healthbar automatisch geüpdatet zodat de nieuwe hoeveelheid health zichtbaar wordt op het scherm.

Als de health van de enemy op 0 komt, wordt de enemy verslagen. Er wordt dan een death state geactiveerd en de speler wint het level. Hierdoor vormt de healthbar niet alleen een visuele indicator, maar ook een belangrijk onderdeel van de gameplay loop.


### Flowchart

```mermaid
flowchart TD

    A["Game Starts"] --> B["Start"]

    B --> C["Set Current Health = Max Health"]

    C --> D["HealthBar SetMaxHealth"]

    D --> E["Find GameEndManager"]

    E --> F["TakeDamage Called"]

    F --> G{"Current Health <= 0?"}

    G -->|Yes| H["Return"]

    G -->|No| I["Subtract Damage"]

    I --> J["Clamp Health Minimum To 0"]

    J --> K["Debug Log Current Health"]

    K --> L["Update HealthBar"]

    L --> M["CheckState"]

    M --> N{"Current Health <= 0?"}

    N -->|No| O["Continue Game"]

    N -->|Yes| P["Trigger Boss_Death Animation"]

    P --> Q["Start AudioEffect Coroutine"]

    Q --> R["Debug Log Won"]

    R --> S{"GameEndManager Exists?"}

    S -->|No| T["End"]

    S -->|Yes| U["Start WinAfterAnim Coroutine"]

    U --> V["Wait 3 Seconds"]

    V --> W["Trigger Win"]
```

### Class Diagram

```mermaid
classDiagram

class EnemyHealth {
    - int maxHealth
    - int currentHealth
    - Animator animator
    - AudioSource deathSFX
    - HealthBar healthBar
    - GameEndManager gameEndManager

    + Start()
    + TakeDamage(int damage)
    + CheckState()
    + AudioEffect()
    + WinAfterAnim()
}

class Animator {
    + SetTrigger(string triggerName)
}

class AudioSource {
    + Play()
}

class HealthBar {
    + SetMaxHealth(int value)
    + SetHealth(int value)
}

class GameEndManager {
    + TriggerWin()
}

EnemyHealth --> Animator : triggers death animation
EnemyHealth --> AudioSource : plays death sound
EnemyHealth --> HealthBar : updates UI
EnemyHealth --> GameEndManager : triggers win state
```

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

### Tutorial Screen
Een apart scherm dat uitlegt hoe het spel werkt. De speler krijgt stap voor stap uitleg over de belangrijkste mechanics zoals Tile Swapping, matches maken en hoe de sliders werken. Er zijn geen chains of complexe mechanics actief tijdens de tutorial.
- **Terug** → [Start Screen](#start-screen)

---

### Save System
De game heeft een save system voor het geval er meerdere levels komen. Zodra je een level verslaat unlock je de volgende level en dan wordt de knop van de level klikbaar en er is ook een knop om het te resetten zodat je opnieuw kan spelen.

---

### Level Creator
Het doel was om meerdere levels te maken en daarvoor is een LevelCreator voor gemaakt dat een Tool Window is waarbij je met knoppen kan klikken welke prefabs je er in wilt hebben. Je kan ook de naam kiezen van de level en als je op Create klikt wordt de scene gemaakt in een folder genaamd Levels en dat is ook zichtbaar op de Tool Window.

<img src="https://github.com/TheGingino/ExamenGame/blob/Develop/WikiResources/Gino/LevelCreator.png" width="80%">

---
## 11. Player behaviour

De speler heeft meerdere abilities. Hier leggen we uit hoe deze allemaal in elkaar zitten.

## Player Attack
De speler kan de enemy aanvallen met twee verschillende aanvallen: een standaard aanval en een special attack. Beide abilities moeten eerst worden opgeladen door matches te maken met de juiste tiles. Zodra een ability volledig is opgeladen, kan de speler deze activeren via de bijbehorende knop in de UI.

Een standaard aanval doet 5 damage aan de enemy en kan meerdere keren gebruikt worden tijdens een level. De special attack is krachtiger en doet 10 damage, maar kan slechts één keer gebruikt worden. Wanneer een aanval wordt uitgevoerd, ontvangt de enemy damage en gaat de healthbar omlaag. Hierdoor krijgt de speler direct feedback op de impact van de aanval.



### Flowchart

```mermaid
flowchart TD

    A["Game Starts"] --> B["Awake"]

    B --> C["Find EnemyHealth"]

    C --> D{"Player Action"}

    D -->|Normal Attack| E["DoDamage"]

    E --> F["EnemyHealth TakeDamage attackDamage"]

    F --> G{"Enemy Current Health > 0"}

    G -->|Yes| H["Trigger Hit Small Animation"]

    H --> I["Play hitSFX"]

    I --> J["Debug Log Damage"]

    G -->|No| J

    D -->|Special Attack| K["SpecialAttack"]

    K --> L["EnemyHealth TakeDamage specialAttackDamage"]

    L --> M{"Enemy Current Health > 0"}

    M -->|Yes| N["Trigger Hit Big Animation"]

    N --> O["Play specialHitSFX"]

    O --> P["Debug Log Special Damage"]

    M -->|No| P
```

### Class Diagram

```mermaid
classDiagram

class PlayerAttack {
    - int _attackDamage
    - int _specialAttackDamage
    - Animator animator
    - AudioSource hitSFX
    - AudioSource specialHitSFX
    - EnemyHealth enemyHealth

    + Awake()
    + DoDamage()
    + SpecialAttack()
}

class EnemyHealth {
    + int _currentHealth
    + TakeDamage(int damage)
}

class Animator {
    + SetTrigger(string triggerName)
}

class AudioSource {
    + Play()
}

PlayerAttack --> EnemyHealth : damages enemy
PlayerAttack --> Animator : triggers animation
PlayerAttack --> AudioSource : plays audio
```

## Player health
De speler verliest health wanneer hij wordt aangevallen door de enemy. Iedere aanval vermindert de huidige health van de speler, waardoor de healthbar omlaag gaat. Hoeveel health de speler verliest, hangt af van de damage die de enemy tijdens zijn beurt rolt.

Om health terug te krijgen, kan de speler de Heal ability gebruiken. Deze ability wordt opgeladen door groene tiles met een hart erop te matchen. Elke match vult de ability verder op. Zodra de Heal ability volledig is opgeladen, wordt de heal move vrijgespeeld en kan de speler deze activeren via de bijbehorende knop in de UI.

Wanneer de speler de heal move gebruikt, krijgt de speler health terug en wordt de healthbar weer verhoogd. Hierdoor kan de speler langer overleven en strategisch omgaan met de aanvallen van de enemy.



<img width="1200" height="900" alt="HealVS" src="https://github.com/user-attachments/assets/8d415bcb-1b55-4c82-a933-41fb26774b22" />


### Flowchart

```mermaid
flowchart TD

    A["Game Starts"] --> B["Start"]

    B --> C["Set Current Health = Max Health"]

    C --> D["HealthBar SetMaxHealth"]

    D --> E["Find GameEndManager"]

    E --> F["Find ImageFader"]

    F --> G{"Action"}

    G -->|Take Damage| H["TakeDamage"]

    H --> I{"Current Health <= 0"}

    I -->|Yes| J["Return"]

    I -->|No| K{"Shield Amount > 0"}

    K -->|Yes| L["Shield Takes Damage"]

    L --> M{"Remaining Damage <= 0"}

    M -->|Yes| N["Return"]

    M -->|No| O["Apply Remaining Damage"]

    K -->|No| O

    O --> P["Reduce Current Health"]

    P --> Q["Clamp Health Minimum To 0"]

    Q --> R["Update Health Bar"]

    R --> S["Show Damage Effect"]

    S --> T["Debug Log Current Health"]

    T --> U["CheckState"]

    U --> V{"Current Health <= 0 AND Not Lost"}

    V -->|No| W["Continue Game"]

    V -->|Yes| X["Set hasLost = true"]

    X --> Y{"GameEndManager Exists"}

    Y -->|No| Z["End"]

    Y -->|Yes| AA["Start WaitForLos Coroutine"]

    AA --> AB["Wait 2 Seconds"]

    AB --> AC["Trigger Lose"]

    G -->|Heal| AD["Heal"]

    AD --> AE["Increase Current Health"]

    AE --> AF["Clamp To Max Health"]

    AF --> AG["Update Health Bar"]

    AG --> AH["Play Heal SFX"]

    AH --> AI["Debug Log Heal"]
```

### Class Diagram

```mermaid
classDiagram

class PlayerHealth {
    - int _maxHealth
    - int _healAmount
    - int _currentHealth
    - bool _hasLost

    - HealthBar _healthBar
    - PlayerShield _playerShield
    - AudioSource healSFX
    - GameEndManager _gameEndManager
    - ImageFader _imageFader

    + Start()
    + TakeDamage(int damage)
    + Heal()
    + CheckState()
    + WaitForLos()
}

class HealthBar {
    + SetMaxHealth(int value)
    + SetHealth(int value)
}

class PlayerShield {
    + int shieldAmmount
    + TakeDamage(int damage)
}

class AudioSource {
    + Play()
}

class GameEndManager {
    + TriggerLose()
}

class ImageFader {
    + ShowDamage()
}

PlayerHealth --> HealthBar : updates UI
PlayerHealth --> PlayerShield : absorbs damage
PlayerHealth --> AudioSource : plays heal sound
PlayerHealth --> GameEndManager : triggers lose state
PlayerHealth --> ImageFader : shows damage effect
```

## Player Shield
De speler kan een Shield ability gebruiken om zichzelf te beschermen tegen aanvallen van de enemy. De ability wordt opgeladen door blauwe tiles met een schild erop te matchen. Zodra de ability volledig is opgeladen, kan de speler de shield move activeren via de knop in de UI.

Wanneer het shield actief is, kan het maximaal 5 damage absorberen. Damage wordt eerst door het shield opgevangen voordat de speler health verliest.


### Flowchart

```mermaid
flowchart TD

    A["Game Starts"] --> B["Start"]

    B --> C["Set Shield Amount = 0"]

    C --> D{"Player Action"}

    D -->|Gain Shield| E["GainShield"]

    E --> F["Increase Shield Amount"]

    F --> G["Play Shield SFX"]

    G --> H["Debug Log Shield Amount"]

    H --> I["Show Shield Visual"]

    D -->|Take Damage| J["TakeDamage"]

    J --> K["Calculate Absorbed Damage"]

    K --> L["Reduce Shield Amount"]

    L --> M["Debug Log Remaining Shield"]

    M --> N{"Shield Amount <= 0"}

    N -->|Yes| O["Hide Shield Visual"]

    N -->|No| P["Keep Shield Visual"]

    O --> Q["Return Remaining Damage"]

    P --> Q

    D -->|Enemy Turn Starts| R["RemoveShieldVisual"]

    R --> S["Hide Shield Visual"]

    S --> T["Remove Listener From Enemy Turn Event"]
```

### Class Diagram

```mermaid
classDiagram

class PlayerShield {
    + int shieldAmmount

    - int _shieldToAdd
    - AudioSource shieldSFX
    - HealthBar playerHealth
    - TurnManager turnManager

    + Start()
    + GainShield()
    + TakeDamage(int damage)
    + RemoveShieldVisual()
}

class AudioSource {
    + Play()
}

class HealthBar {
    + ShowShieldVisual()
    + HideShieldVisual()
}

class TurnManager {
    + OnEnemyTurnStart
}

PlayerShield --> AudioSource : plays shield sound
PlayerShield --> HealthBar : updates shield UI
PlayerShield --> TurnManager : listens for enemy turn
```



---
## Tile grid visuals
De tile grid is natuurlijk waar de gameplay zelf speelt hiervoor hebben wij art nodig om de vershilende soorten tiles te onderschieden. Rood voor attack, Blauw voor shield, Groen voor healing, Wit als een filler tile en geel voor special attack.

<img width="125" height="125" alt="Tile_SPATK_BaseColor" src="https://github.com/user-attachments/assets/0dc1a137-ad0d-4cd5-8f98-733d9affcf3a" />
<img width="125" height="125" alt="Tile_SHD_BaseColor" src="https://github.com/user-attachments/assets/5652e3d8-137e-4f62-be4c-911254052f00" />
<img width="125" height="125" alt="Tile_Null_BaseColor" src="https://github.com/user-attachments/assets/b69dc797-06e7-49c4-9d3c-bae8527cd395" />
<img width="125" height="125" alt="Tile_HPL_BaseColor" src="https://github.com/user-attachments/assets/9977e03d-54ff-48d0-9df7-f91c688c11ae" />
<img width="125" height="125" alt="Tile_ATK_BaseColor" src="https://github.com/user-attachments/assets/b48c5d2f-1265-449a-b8be-4ca713ddf23c" />

## Move counter & Energy counter

Move Counter
De move counter laat zien hoeveel zetten de speler nog kan doen voordat de enemy aanvalt.

<img width="125" height="125" alt="TurnCounter" src="https://github.com/user-attachments/assets/11689393-9e81-4cfa-b033-992496309671" />


Energy Counter
De energy counter laat zien dat de speler 3 abilities kan gebruiken per beurt.
Als er een ability gebruikt is, word 1 van de buttons donker van kleur.

<img width="125" height="125" alt="EnergyCounter_On" src="https://github.com/user-attachments/assets/6e65f7b0-0c88-42e5-94bb-44f8669c6de6" />
<img width="125" height="125" alt="EnergyCounter_Off" src="https://github.com/user-attachments/assets/ed0ad02a-3320-4f6f-8a1c-93e906f50fca" />


## Turn indicator
De turn indicator geeft aan of de speler aan zet is of dat de enemy aan de beurt is.

<img width="549" height="285" alt="Player Turn" src="https://github.com/user-attachments/assets/754781a8-e2f6-4d7b-8a55-46d21c1661f9" />
<img width="549" height="285" alt="Enemy Turn" src="https://github.com/user-attachments/assets/13ef0a47-1e34-4ef3-a715-0e780d9879c7" />


## Setting button
Door op deze knop te drukken word het optie menu geopend

<img width="125" height="125" alt="Settings" src="https://github.com/user-attachments/assets/e362008f-05b0-489a-ae61-551ca92924a7" />


## Ability buttons & Ability charger
Door op 1 van deze knoppen te drukken word de bijbehorende ability geactiveerd. 
De charge visuals laten zien hoeveel charges de speler verzameld heeft.

<img width="218" height="251" alt="Screenshot 2026-05-21 135938" src="https://github.com/user-attachments/assets/63ebba9c-024c-447f-99c0-236cc62fd998" />

## Bomb Visual
De bomb kan op de grid gesleept worden door de speler. Dan worden er in 1 keer meerdere tiles gebroken.

<img width="250" height="250" alt="Dyamite_Outlined" src="https://github.com/user-attachments/assets/4d58862f-855c-4102-97b4-7f640448a70c" />
<img width="250" height="250" alt="Dyamite" src="https://github.com/user-attachments/assets/cca52e76-52e4-42be-839f-e8b0d5c13973" />

---

## Beginscherm 
Elke game heeft wel een beginscherm. Het is het eerste dat je ziet als je een game start. Het moet de sfeer vertellen van de game en waar het over gaat. Ik heb 6 concepten gemaakt voor het beginscherm. Na ee poll om te stemmen kwam er een uitslag. Na verschillende concepten van het ruimteschip en nog een poll verder is er een winner. 

<img width="250" height="500" alt="image" src="https://github.com/user-attachments/assets/32756ccb-b29d-4d53-8699-53c6ab46655b" /> 


## Level select scherm
Nadat je het beginscherm heb gehad en je wilt verder ga je naar een scherm waar je de levels kan zien. Het level select scherm laat je alle levels zien die bestaan/je kan spelen. Ik heb hier ook concepten voor gemaakt en een poll gecreeerd waar een winner op was besloten. Ook als game hadden we besloten dat je begint helemaal onderaan in het ruimteschip en jemoet door het ruimteschip gaan waar je in verschillende kamers komt met levels waar je dingen moet verslaan. 

<img width="600" height="210" alt="image" src="https://github.com/user-attachments/assets/067ec3f2-707d-44ee-8d5e-984cc4480bd5" />


## Wapen
In een eerdere versie van het concept was dat de speler een wapen heeft waarmee je je mee kan verdedigen. Als je attack balkje vol is door de match 3 de spelen dan kon je aanvallen met het wapen.

<img width="210" height="370" alt="image" src="https://github.com/user-attachments/assets/22158f73-45f3-4579-882a-a28349632f01" />


## UI knoppen
Voor het startscherm en level select scherm moeten er knoppen komen om op te klikken om verder te gaan in het spel. 
<img width="600" height="170" alt="image" src="https://github.com/user-attachments/assets/8095c17a-51ce-4f52-94b7-4cb7f1715f6d" />


## Pause screen
Als je het spel op pause zet dan krijg je een menu met opties. Dit is de UI.

<img width="454" height="554" alt="image" src="https://github.com/user-attachments/assets/f8afb320-b677-45fc-9641-46c5e5671e2c" />

## Enviroment Boss level
Sinds wij begonnen bij het boss level was daar ook meteen een environment voor nodoig. We hadden als idee voor de environment dat je door het schip heen loopt van begin tot eind. Daarom hebben we de boss level in het puntje van het schip gezet, vaak omdat dat de belangrijkste ruimte is ivm besturing. Hier zijn ook concepten voor gemaakt waaruit de favoriete is gekozen door ons team. Dit is het level waar onze boss in staat en welke de speler het meeste terugziet. 

<img width="611" height="450" alt="Screenshot 2026-05-21 141707" src="https://github.com/user-attachments/assets/f4c51fbe-90e9-43bb-b366-1ef18ccad158" />


## Environment level 2
We hadden best wel veel tijd over dus er zijn ideen voor een 2e level. Omdat we als idee had dat je door het schip moet lopen en de eindbaas in de top zit, was de environment best wel vast in welke kamer het is. Daarom zijn er weinig variatie met vorm. Ik heb er concepten voor gemaakt, er is weer gestemt op een eindresultaat. 

## Enemy model
Onze antagonisten in de game zjin aliens. Daarom hebben we voor de boss level een groot en sterke alien gekozen. Die alien kan jou als speler aanvallen en jij kan hem terug aanvallen. Er is veel gespeeld met lichaamshouding en met kleuren theorie. Hij staat met de borst naar voren wat hem intimiderend maakt en heeft koude kleuren wat hem er kil uit laat zien. 

<img width="480" height="270" alt="Texture alien" src="https://github.com/user-attachments/assets/50e52304-462e-4bbd-9524-bfab94e93bb0" />

Daarnaast heeft de alien ook een rig voor de animaties zodat hij kan bewegen. Dit maakt het model levendiger en interessanter voor de speler. 

<img width="480" height="270" alt="rig eneemy" src="https://github.com/user-attachments/assets/1ca8f9dc-df5f-46db-b808-af1a72687948" />

<img width="480" height="270" alt="poses enemy" src="https://github.com/user-attachments/assets/2c0130df-1043-444b-a3b8-ac48d6f5f24b" />

## Tutorial

Om het voor de speler zo duidelijk mogelijk te maken hoe de game werkt hebben wij een tutorial aan de game toegevoegd. De tutorial start aan het begin van het level en de speler kan zelf kiezen of die hem wilt skippen en hoe snel hij door de tutorial heen wilt gaan. 

<img width="480" height="270" alt="tutorial new" src="https://github.com/user-attachments/assets/3008bfc6-65a2-44ba-b00b-e1e68e26a801" />

## Slash animation
Wanneer de speler de enemy aanvalt, komt deze animatie in het scherm. Het zorgt ervoor dat de speler meer visual feedback heeft over het aanvallen en het ziet er interessanter uit.

<img width="480" height="270" alt="slash spritesheet" src="https://github.com/user-attachments/assets/a742867b-e8f1-4538-98c2-8babf864ca95" />

## Special attack animation
Wanneer de speler de enemy aanvalt met een special attack, komt deze animatie in het scherm. Het zorgt ervoor dat de speler meer visual feedback heeft over het aanvallen en het ziet er interessanter uit.

<img width="500" height="270" alt="Special attck beam" src="https://github.com/user-attachments/assets/adb78eb7-8d38-48bb-9551-fc1403fd8dd5" />

## health bar
Om duidelijk health van de speler en de enemy te kunnen aangeven, hebben wij gebruik gemaakt van health bars. Als de speler volle health heeft, dan is de bar vol. Wanneer de speler damage neemt loopt de bar een beetje leeg. Als de bar leeg is, dan is de speler dood. Dit geldt ook voor de enemy. Het verschil tussen de 2 is dat de enemy healthbar een icon heeft om aan te geven dat dat die van hem is voor duidelijkheid.

<img width="175" height="150" alt="Health Bar blue" src="https://github.com/user-attachments/assets/ff5ed4e0-011b-4dcc-95cf-4e015f434fea" />

Daarnaast is er ook een healthbar die blauw is die aangeeft wanneer de shield geactiveerd is

<img width="175" height="150" alt="Health Bar" src="https://github.com/user-attachments/assets/d2a09335-f4c5-410e-a8e3-eaa88d3f70df" />

## border
Om de artstyle en de game bij elkaar te brengen hebben wij gebruik gemaakt van een game border. Deze maakt het spel visueel interessanter en zorgd voor een afgemaakte look in het spel 

<img width="180" height="390" alt="S_Rim" src="https://github.com/user-attachments/assets/f2be57f4-a5d5-4db8-98ab-03b0400e48b1" />

## damage rim
Zodat de speler meteen en duidelijk weet dat hij damage heeft genomen, hebben wij een damage rim gemaakt. Deze komt snel te voorschijn wanneer de alien de speler aanvalt. De rim gaat over het hele scherm zodat de speler het niet kan missen.

<img width="180" height="390" alt="S_Damage Rim" src="https://github.com/user-attachments/assets/3df684f4-a070-4398-b8af-0e4a2eab4ec1" />
