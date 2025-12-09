CST320FoodFiles

A Unity 6.2 Project

CST320FoodFiles is a Unity 6.2 project designed for coursework and experimentation in Unity development. It demonstrates proper project organization, data handling, UI workflows, and standardized Unity practices suitable for CST320 or similar game-development classes. This repository provides a solid baseline for expanding project functionality or using it as a foundation for assignments.

Project Overview

CST320FoodFiles focuses on food-related content and systems, typically involving data structures for food items, UI interfaces for displaying information, and fundamental Unity interactions. While the exact project content varies depending on your implementation, the project is structured to highlight:

Clean use of prefabs and reusable components

Organized asset pipelines

Data-driven objects (e.g., ScriptableObjects or other data structures)

Unity 6.2 workflows and editor conventions

A stable foundation for additional development

This project is suitable for anyone learning intermediate Unity development or needing a structured template for coursework.

Installation & Setup
1. Requirements

Unity 6.2.x

Git (optional)

Standard Unity dependencies (installed automatically)

2. Cloning the Project
git clone https://github.com/<your-repo>/CST320FoodFiles.git

3. Opening the Project

Launch Unity Hub

Click Open

Select the project folder

Choose Unity 6.2.x when prompted

Unity may reimport assets on first launch; this is normal for new checkouts.

Asset Workflow
Adding Food Items

Navigate to Assets/_Project/ScriptableObjects/Food/

Right-click → Create → Food Item (or your custom SO type)

Fill in the fields such as:

Name

Icon

Stats (e.g., calories, category, cost)

Save the asset and open the main scene to preview the update.

Adding UI Elements

Duplicate UI prefabs to maintain consistency

Update UIManager references if required

Test in Play Mode to verify proper interaction

Adding Scenes

Store new scenes in Assets/_Project/Scenes/

Add required scenes to Build Settings to ensure they compile correctly

Build Instructions
Windows / macOS / Linux

Open File → Build Settings

Select the target platform

Add scenes to the "Scenes in Build" list

Choose Build

Select an output folder

WebGL (if supported)

Switch build target to WebGL

Adjust memory and compression settings as needed

Build and deploy via your web host, GitHub Pages, itch.io, etc.

Troubleshooting
Problem	Possible Fix
Missing scripts in scene	Reassign C# components manually after pulling changes
Pink materials	Check rendering pipeline settings (URP/HDRP)
UI invisible or unresponsive	Ensure an EventSystem exists and Canvas Raycasters are active
ScriptableObjects not loading	Check file paths, Resources folder usage, or manager scripts
Build fails	Inspect Console for compiler errors or missing assemblies
Version Information

Unity Version: 6.2.x

API Compatibility: .NET Standard 2.1 / C# 9

Platforms: Editor, Windows, macOS, Linux, WebGL (depending on configuration)

License

Add a license depending on your use case:

MIT License — recommended for open academic or personal projects

GPL v3 — if you want strict open-source requirements

Proprietary / Educational Use — if this is for coursework only
