# Codebase Overview

This repository is a scrapbook of projects ranging from university coursework to recent game jam prototypes. The `README` at the root frames it as a chronological archive of experiments and learning milestones.

## Repository layout

- **`GGJ 2024/`** – A Unity project from Global Game Jam 2024. The `Code/` folder contains the runtime scripts, grouped by domain (audio, data, gameplay managers, interactables, narrative, transitions, UI, and player controls).
- **`Pre 2024/`** – Older personal work and prototypes, including the more feature-complete “Space Miner” Unity game scripts and a `code snips 2022` folder with reusable utilities.
- **`Uni/`** – University assignments, such as a C# boids simulation and a C++ Travelling Salesman solver, kept for reference.

## Highlights from the Global Game Jam project

The GGJ project is the most recent and cohesive codebase; start here if you want to understand the author’s current patterns.

- **Game state and data** – `GameManager` is the singleton-style entry point that persists between scenes and exposes shared `GameData` (a `ScriptableObject` for interaction ranges, audio, and puzzle codes).
- **Player interactions** – `Core.player.Player` maintains trigger-based interaction lists, handles ladder movement state, and routes input to the UI and interaction prompts.
- **Progression system** – `IllusionManager` drives a five-step progression flow, broadcasting state changes to systems like music, UI, and interactables; when the final state is reached it triggers an end-scene load.
- **User interface hooks** – `UIManager` persists UI across scenes, toggling menus and a journal whose entries are keyed off the current illusion state and accompanied by audio feedback.

### Working with the Unity project

1. Open the `GGJ 2024` folder as a Unity project (the `Code/` directory sits alongside Unity scene/assets data). Unity’s assembly definition (`Core.asmdef`) keeps scripts under the `Core` namespace scoped for faster compilation.
2. Start by playing the main scene; use `Player.cs` to trace how inputs tie into interactions and UI prompts.
3. Follow `IllusionManager`’s `StateChanged` event to see how progression orchestrates feedback (music, journal entries, interactables).

## Legacy projects worth browsing

- **Space Miner (Unity, 2019-2022 era)** – A more complex management of hooks, resources, and UI in `Master.cs`, showcasing earlier patterns for singleton managers, resource loading, and UI coordination.
- **Code Snippets 2022** – Small utility scripts like `Tools` (random helpers, interception math) and `Beam` (a sweeping raycast beam effect) illustrate reusable gameplay code the author extracted from prototypes.
- **University assignments** – The boids simulation implements classic flocking rules and state handling, while the C++ TSP program demonstrates graph construction and approximation vs. optimal solvers on Euclidean graphs.

## Suggested next steps for newcomers

1. **Get Unity running and profile the latest project** – Build familiarity with the GGJ systems by stepping through play mode, inspecting `Player` interactions and `IllusionManager` events to see how state drives audio/UI feedback.
2. **Compare manager patterns across eras** – Contrast the modern `GameManager`/`UIManager` approach with the legacy `Master` controller in Space Miner to understand how architecture evolved toward leaner, event-driven scripts.
3. **Explore algorithmic foundations** – Review the boids and TSP coursework to refresh on classic AI/algorithm problems that underpin gameplay mechanics and could inform future systems work.
4. **Identify reusable utilities** – Consider how helper scripts like `Tools` and `Beam` might be refactored into packages or Unity scriptable tools for future prototypes.

By following this roadmap you can quickly orient yourself in the active GGJ project, appreciate historical context from earlier games, and spot opportunities to modernize or reuse legacy systems.
