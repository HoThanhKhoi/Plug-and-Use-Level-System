# Plug-and-Use-Level-System
An all-in-one plug &amp; use for all leveling system. 

1. Overview
This package provides a drop‐and‐use way to handle XP, levels, UI displays, and progression data in your Unity project. It’s designed to be modular, data‐driven, and easy to extend.

2. Key Features
XP & Level Tracking: Separate components for experience (ExperienceTracker) and level progression (LevelTracker).
Flexible Strategies: Plug in ScriptableObject strategies (like ManualLevelingStrategySO) for different progression curves.
UI Integration: Out‐of‐the‐box scripts for displaying level text, XP bars, and floating level text above characters.
Event System: Raise LevelUpEventSO on level‐up, letting you easily trigger reward logic or UI popups.

3. Setup Steps
Add LevelingService to Your Scene
Create an empty GameObject named “LevelingService” and attach LevelingService.cs.
This is a singleton that orchestrates the entire system.
Create a “Player” Object (Example)
Add ExperienceTracker + LevelTracker to the same GameObject.
In ExperienceTracker, set the LevelingCategory to Player.
In LevelTracker, assign a LevelDataListSO (manual XP table) and a LevelingStrategySO (e.g., ManualLevelingStrategySO).
Test by Adding XP
Call LevelingService.Instance.AddExperience(LevelingCategory.Player, 50); from any script.
If 50 crosses the threshold for level 2, you’ll see a console log “Player leveled from 1 to 2.”
Optional UI
LevelTextUI: Attach to a TextMeshProUGUI object, set category = Player. It shows “Level: X.”
ExpBarUI: Attach to a Slider, set category = Player. It fills based on CurrentExperience / RequiredExperience.
FloatingLevelUI: If you want a floating label above an enemy’s head, place it in a child object with a world‐space canvas, referencing a local LevelTracker.

4. Common Use Cases
Player Leveling: Single “Player” category. You add XP from kills, quests, etc. The system auto‐increments level.
Weapon or Skill Leveling: Create new categories (LevelingCategory.Weapon, LevelingCategory.Skill). Attach separate trackers to your weapon or skill object.
Multiple Enemies: Either give each enemy a unique category (not typical) or skip the facade dictionary for them, referencing local LevelTracker directly. The system can still manage XP thresholds, but you must be mindful of collisions if you rely on Enemy as a single category.

5. What It Can & Can’t Do
Can Do:
Data‐driven XP thresholds (via LevelDataListSO).
Raise events on level‐up (LevelUpEventSO).
Display XP/level in various UI forms.
Handle single instance per LevelingCategory easily.
Limitations:
Single Key: If you register multiple objects under the same LevelingCategory, the last one overwrites the first in the facade dictionary.
Global Singleton: If you prefer no singletons, you’ll have to refactor or wrap the facade in your own DI approach.
Initialization Order: Relies on [DefaultExecutionOrder]. If you override it, you might see “No leveling system found” errors.

6. Advanced Tips
Unique IDs: If you want multiple instances in the same category, consider switching to a unique ID approach or local references.
Formula Strategies: Create a FormulaLevelingStrategySO with an [CreateAssetMenu], implement your math in CalculateRequiredExperience(...).
Saving/Loading: Store CurrentExperience and Level in your save file. On load, set them back, or repeatedly call PerformLevelUp() until the level matches.

7. Installation & Import
Unity Version: Tested with Unity 2020+
Import: Drag the /Scripts folder into your project or use a Unity package.
Setup: Add a “LevelingService” object, attach the script, assign the references.

8. Support & Contributing
Issues: If you find a bug, open an issue in our repository or contact the dev team.
Contributions: For new features (like a different leveling strategy or advanced UI), add a script under /Strategies or /LevelUI and reference it in your scene.
