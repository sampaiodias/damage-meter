# Damage Meter

Simple damage meter UI to show how much DPS your player is doing.

![screenshot](https://i.imgur.com/phCRpVe.png)

## Installation

This is a Unity Package. To install, go to the Package Manager window, click on "Plus -> Add package from git URL...", paste the URL below and press Enter.
```
https://github.com/sampaiodias/damage-meter.git
```

_Requires Unity 2020.3+ and TextMeshPro._

## Features
- Show DPS numbers in a compact UI (similar to damage meter addons for World of Warcraft).
- Change how numbers are reported with a simple string layout.
- Pause, resume and reset current reports.
- Add hotkeys to toggle the visibility of the UI.
- Change the appearance of anything on the UI, such as fonts and sizes: everything is made with Unity's default GUI.

## How to use

- Add two prefabs to your scene: "DamageMeterManager" and "DamageMeter UI".
- _Optional_: Tweak the settings on the inspector of these two GameObjects.
- Call `damageMeterManager.Register(skill, amountOfDamage, subCategory)` during the gameplay.