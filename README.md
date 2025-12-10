# SpaceShooter Prototype 🚀

## Overview
This repository contains a scalable **SpaceShooter** prototype developed in **Unity**. While the initial foundation follows standard course tutorials, this project significantly evolves the codebase to focus on **gameplay design scalability**, modular architecture, and reduced dependency on hard-coded Blueprint logic.

## 🛠 Key Features & Improvements

### 1. Spline-Based Enemy Movement
The enemy movement system was redesigned from simple forward/right vectors to a **Spline Blueprint System**.
- **Pathing:** Enemies follow pre-designed spline curves, allowing for complex movement patterns.
- **Scalability:** New levels can be created simply by adding spline actors to the map; no modifications to the enemy code are required.
- **Logic:** Movement is driven by a **Timeline**, updating the actor's position along the curve via an Alpha value.

### 2. Modular Shooting System
Enemy shooting mechanics are now dynamic and visual-based.
- **Component Tags:** Projectiles spawn from `Arrow` components tagged within the Blueprint.
- **Customization:** Designers can alter shot patterns (angle, quantity) simply by moving arrows in the Viewport.
- **Boss Mechanics:** Bosses include logic for **Burst Fire** sequences and adjustable **Rate of Fire**.

### 3. Wave Spawner & Game Mode
The Spawner and GameMode work together to manage game flow.
- **Wave System:** Enemies spawn in progressive sets (e.g., 5 sets of 6 enemies), increasing in intensity.
- **Boss Trigger:** The GameMode tracks enemy kills; once a level is cleared, the Boss (with unique stats and patterns) is spawned.
- **Win/Loss States:** Handles global conditions and level transitions.

### 4. User Interface (UI)
The Game Widget has been updated to display real-time data:
- **Lives:** Generated dynamically via iteration.
- **Timer:** Time elapsed in seconds.
- **Kill Counter & Wave Progress.**

### 5. Visuals
- 2D sprites were sourced to replace placeholders and improve the game's aesthetic.

---

## ⚡ Power-Up System

A robust interaction system was added to enhance gameplay depth.

### Architecture
- **Base Class:** All power-ups inherit from `BP_PowerUpItem` for easy maintenance.
- **Interfaces:** Uses `BPI_Interactable` to decouple Player logic from Item logic.

### How it Works
1.  **Collision:** The player overlaps with an item.

### Types of Power-Ups
| Type | Behavior | Examples |
| :--- | :--- | :--- |
| **Instant** | Executes immediately, then destroys item. | Extra Life (+1), Nuke (Kill All). |
| **Temporary** | Modifies player stats for a set time (via Timers). | Speed Boost, Fire Rate Up, Invincibility. |

---

## 🎮 Controls

| Key | Action |
| :--- | :--- |
| **W / A / S / D** | Ship Movement |
| **Spacebar** | Shoot |

---

## 📝 Credits
- Developed in **Unity**.