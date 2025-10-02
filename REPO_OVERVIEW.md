# Repository Overview

## Top-level layout
- `README.md` – short description of the archive as a collection of work from university projects, personal prototypes, and game jams.【F:README.md†L1-L2】
- `GGJ 2024/` – Unity project left in the state it was at the end of Global Game Jam 2024, containing gameplay, UI, and interaction systems for a puzzle adventure prototype.【F:'GGJ 2024/Readme.txt'†L1-L4】
- `Pre 2024/` – mixed directory of older Unity projects, ASP.NET MVC coursework, utilities, and archived jam builds, added over several years.【F:'Pre 2024/README.md'†L1-L9】
- `Uni/` – coursework projects from university spanning Unity simulations, console apps, C++ assignments, and database tables.

## GGJ 2024 Unity project (`GGJ 2024/Code`)
The `Code` folder is a Unity assembly defining the 2024 game jam prototype. Major systems include:

### Game and progression
- `GameManager/GameManager.cs` maintains a persistent singleton that exposes `GameData` and tracks the main `Player` instance across scenes.【F:'GGJ 2024/Code/GameManager/GameManager.cs'†L1-L24】
- `Transition/Illusion Manager.cs` (with `IllusionObject` and `illusionSlave`) implements a five-step level state machine, raises `StateChanged` events, and swaps illusionary scene elements or loads an end scene when the final state is reached.【F:'GGJ 2024/Code/Transition/Illusion Manager.cs'†L1-L41】【F:'GGJ 2024/Code/Transition/IllusionObject.cs'†L1-L77】
- `Transition/IllusionObject.cs` lets master objects toggle visibility for themselves and their registered slave objects when the illusion state changes.【F:'GGJ 2024/Code/Transition/IllusionObject.cs'†L1-L77】

### Player and interaction framework
- `player/Player.cs` wires user input to interactions (ladder handling, journal/menu toggles) and manages the list of nearby `Iinteractable` implementations and carried `IStorable` items.【F:'GGJ 2024/Code/player/Player.cs'†L1-L98】
- `Interactable/Interactable.cs` defines the base interactable contract, automatically adds trigger colliders when needed, plays optional audio clips, and coordinates one-shot usage and prompt removal with the player controller.【F:'GGJ 2024/Code/Interactable/Interactable.cs'†L1-L86】
- `Interactable/Inventory/Storable.cs` extends `Interactable` so items such as `Key` can be picked up, hidden, dropped, or consumed while notifying listeners via the `PickedUp` callback.【F:'GGJ 2024/Code/Interactable/Inventory/Storable.cs'†L1-L48】【F:'GGJ 2024/Code/Interactable/Inventory/Key.cs'†L1-L6】
- `Interactable/Padlock.cs` blocks other interactables until a keypad mini-game succeeds; it coordinates with `PadlockUI` and unblocks referenced objects once the code is solved.【F:'GGJ 2024/Code/Interactable/Padlock.cs'†L1-L48】
- `Interactable/Crank.cs` moves a target transform toward a line anchor over time, optionally triggering another interactable once the crank completes.【F:'GGJ 2024/Code/Interactable/Crank.cs'†L1-L54】
- `Interactable/SpotLight.cs` advances the illusion state when activated, showcasing how interactables feed progression.【F:'GGJ 2024/Code/Interactable/SpotLight.cs'†L1-L18】
- `Interactable/InteractionPompt.cs` renders the floating interaction prompt, interpolating its position between player and target and hiding it when the target disappears.【F:'GGJ 2024/Code/Interactable/InteractionPompt.cs'†L1-L56】

### UI and audio
- `UI/UIManager.cs` acts as a persistent HUD singleton, toggling the journal, escape menu, and delegating to `Lottie` for flavour text popups.【F:'GGJ 2024/Code/UI/UIManager.cs'†L1-L56】
- `UI/PadlockUI.cs` contains the keypad mini-game that reads numeric key presses, displays the rolling code, and unlocks the `Padlock` when the configured combination is entered.【F:'GGJ 2024/Code/UI/PadlockUI.cs'†L1-L71】
- `UI/Lottie.cs` periodically shows random dialogue lines for a limited duration via a sprite/text pair.【F:'GGJ 2024/Code/UI/Lottie.cs'†L1-L46】
- `Audio/AudioManager.cs` centralises transient audio playback, instantiating a pool of sources and seeding music on start.【F:'GGJ 2024/Code/Audio/AudioManager.cs'†L1-L47】
- `Audio/MusicPlayer.cs` listens for illusion state changes and swaps background music tracks accordingly.【F:'GGJ 2024/Code/Audio/MusicPlayer.cs'†L1-L27】

### Data and narrative assets
- `Data/GameData.cs` is a `ScriptableObject` storing shared configuration such as default trigger radius, enter sound, and padlock code.【F:'GGJ 2024/Code/Data/GameData.cs'†L1-L17】
- `Narrative/DialogueObject.cs` stores dialogue text and a `randomQue` flag for designers via another `ScriptableObject` asset type.【F:'GGJ 2024/Code/Narrative/DialogueObject.cs'†L1-L12】

## Pre-2024 projects (`Pre 2024/`)
This folder mixes Unity prototypes, coursework, and archives predating 2024.

### Drivehack code (Unity endless-runner prototype)
- `Controllers/GameMaster.cs` implements the core singleton wiring UI, level controller, dragon character, and persistence. It saves stats on level exit and supports scene resets.【F:'Pre 2024/Drivehack code/Controllers/GameMaster.cs'†L1-L75】
- `Controllers/LevelController.cs` (long file) tracks run metrics (food, distance, streaks), schedules slate/row spawns, and supports both deterministic level play and procedural spawning.【F:'Pre 2024/Drivehack code/Controllers/LevelController.cs'†L1-L140】
- `Controllers/InputController.cs` manages swipe inputs, queued lane changes, jumping, and blocking, translating gestures into dragon movement within bounds.【F:'Pre 2024/Drivehack code/Controllers/InputController.cs'†L1-L120】
- `Controllers/TerrainController.cs` streams ground and scenery prefabs ahead of the player, recycling segments when they move behind the camera.【F:'Pre 2024/Drivehack code/Controllers/TerrainController.cs'†L1-L101】
- `Controllers/UIController.cs` drives HUD state (notifications, lives, combo/shield toggles, panel switching) and routes touch events via Unity’s event system.【F:'Pre 2024/Drivehack code/Controllers/UIController.cs'†L1-L108】
- `Controllers/Stats.cs` serialises cumulative run stats (distance, food, level completions) using `BinaryFormatter` at `Application.persistentDataPath`.【F:'Pre 2024/Drivehack code/Controllers/Stats.cs'†L1-L79】
- `ChainManager.cs` awards bonus rewards for chained pickups, resetting UI state when the chain breaks.【F:'Pre 2024/Drivehack code/ChainManager.cs'†L1-L85】
- `Dragon/Dragon.cs` governs the player avatar: shielding, energy management, slide animations, collision handling, and timed invulnerability effects.【F:'Pre 2024/Drivehack code/Dragon/Dragon.cs'†L1-L140】
- `Dragon/Ability.cs` and `FlameBreath.cs` define ScriptableObject abilities, exemplifying an energy-gated flame breath attack prefab.【F:'Pre 2024/Drivehack code/Dragon/Ability.cs'†L1-L14】【F:'Pre 2024/Drivehack code/Dragon/FlameBreath.cs'†L1-L28】
- `CollideOBjects/` contains hazard, power-up, and spawn logic scripts referenced by level slates (e.g., buffs, random spawners, lane locks).【F:'Pre 2024/Drivehack code/CollideOBjects/RandomSpawner.cs'†L1-L30】

### Space Miner (Unity arcade prototype)
- `Master.cs` is the singleton orchestrating hook types (grapple, tractor beam, teleport), level loading, UI updates, resource tracking, and hook reuse flags.【F:'Pre 2024/Space Miner/Master.cs'†L1-L115】
- `MainChain.cs`, `Targeter.cs`, `Upgrades.cs`, `UI/` (not shown) provide supporting systems for aiming the hook, presenting UI, and managing upgrades.

### Yogsjam 48 hours (Unity jam build)
- `Game.cs` coordinates resource delivery, navigation pointers, unlock spawns, and win/loss conditions while staying persistent between scene loads.【F:'Pre 2024/Yogsjam 48 hours/Game.cs'†L1-L112】
- `WaveController.cs` sequences enemy waves, spawning mobs with random offsets until quotas are satisfied.【F:'Pre 2024/Yogsjam 48 hours/WaveController.cs'†L1-L74】
- Additional scripts (e.g., `Defenses`, `ElfController`, `Weapons`) cover combat models, interactables, and enemy behaviour as part of a holiday-themed defense game.

### MVC_NET/Vidly (ASP.NET MVC sample)
- `Controllers/` contains `Home`, `Movies`, and `Customers` controllers with scaffolded list/detail actions populated by hard-coded sample data.【F:'Pre 2024/MVC_NET/Vidly/Vidly/Controllers/CustomersController.cs'†L1-L33】
- `Models/AppDbContext.cs` defines an EF Core `DbContext` exposing `Customers` and `Movies` sets, while `Models/Customer.cs` and `Movie.cs` model the domain.【F:'Pre 2024/MVC_NET/Vidly/Vidly/Models/AppDbContext.cs'†L1-L17】【F:'Pre 2024/MVC_NET/Vidly/Vidly/Models/Movie.cs'†L1-L12】
- `ViewModels/` (e.g., `CustomersViewModel`, `RandomMovieViewModel`) provide typed payloads for Razor views.
- `packages/` and `packages.config` capture NuGet dependencies; `Migrations/` holds EF migration history.

### Utility snippets and archives
- `code snips 2022/Beam.cs` implements a damage-dealing line renderer that sweeps across an arc, raycasts for hits, and self-destructs after a lifetime.【F:'Pre 2024/code snips 2022/Beam.cs'†L1-L79】
- `code snips 2022/Tools.cs` exposes helpers such as `Randsign`, `Player()` lookup, and a 2D interception solver (dependent on an external `CMath.QuadraticSolver`).【F:'Pre 2024/code snips 2022/Tools.cs'†L1-L71】
- `Dragon Run.zip` and `Yogs Jam 2019.zip` are archived builds not unpacked here.

## University projects (`Uni/`)
Coursework ranges from Unity scripts to C++ assignments.

### Boids (Unity flocking exercise)
- `boid_script.cs` simulates fish with separation/cohesion behaviours, fear responses to sharks, and an FSM controlling wander, flee, and dead states.【F:'Uni/Boids/boid_script.cs'†L1-L120】
- `sharkboid_script.cs` makes predator agents hunt the nearest fish, digest food over time, and switch between wander and hunt states via another FSM.【F:'Uni/Boids/sharkboid_script.cs'†L1-L120】
- `deployer.cs` spawns fish and shark prefabs within a configurable range on start.【F:'Uni/Boids/deployer.cs'†L1-L27】

### Simple ATM Simulation (C# console)
- `ConsoleApplication2/Atmachine.cs` provides balance display, withdrawal, and transfer workflows with fee calculations, denomination breakdown, and simple input loops.【F:'Uni/Simple ATM Simulation/ConsoleApplication2/Atmachine.cs'†L1-L120】
- `ATM_INB270.csproj` and generated `bin/`/`obj/` directories hold the Visual Studio project scaffolding.

### Travelling Salesman (C++)
- `TSP.cpp` is a driver that generates or reads city coordinates, builds a complete graph, computes optimal tours for small problem sizes, and always evaluates an MST-based approximation.【F:'Uni/Traveling salesmen(C++)/TSP.cpp'†L1-L105】
- Supporting classes include `graph.h/cpp`, `vertex.h/cpp`, `edge.h/cpp`, `disjointset` (for MST), `edgecomparer`, and `random.{h,cpp}`.
- Sample coordinate files (`hexagon.txt`, `square.txt`, etc.) allow reproducible runs.

### Poker hand evaluator (C++)
- `main.cpp` sketches a 10-player deal loop using `Deck`, `Hand`, and comparator classes, alongside `shuffle.cpp` and `random.cpp` utilities.【F:'Uni/poker(C++)/main.cpp'†L1-L46】
- `card.h/cpp`, `hand.h/cpp`, and `handcomparer.h/cpp` implement card representations and ranking comparisons.

### University databases (MySQL)
- Binary table definitions (`student.frm/.ibd`, `grade_report.frm/.ibd`, etc.) capture schema and storage for coursework database assignments. They require MySQL to inspect.

### Additional items
- `Simple ATM Simulation` under `Uni/` duplicates the console ATM coursework described above.
- `poker(C++)`, `Traveling salesmen(C++)`, and `Boids` illustrate different language/lab exercises, highlighting the breadth of the author’s early academic projects.

---
This document summarises the structure and key components to help future contributors locate relevant code across the historical projects collected in this repository.
