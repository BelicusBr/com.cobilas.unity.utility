# Changelog
## [2.10.2] - 05/02/2024
- ### Changed
- - Updated dependency `com.cobilas.unity.core.net4x` to version `1.4.1`.
- - This update includes bug fixes and new features that do not directly impact this package.

## [2.10.1] - 25/01/2024
### Fixed
The dependency `com.cobilas.unity.core.net4x` was changed to version 1.2.2 because version 1.2.1 had compatibility issues with the C# 7.3 version.
### Removed
The `UnityTypeUtility` class was removed so that the `TypeUtilitarian` class already exists, which causes redundancy due to the fact that they have the same functions.
## [2.9.0-rc2] - 02/09/2023
### Removed
- Removed the `TypeUtilitarian` class and the classes from the `Threading` folder.
## [2.9.0-rc1] - 02/09/2023
### Added
- The packages <kbd>com.cobilas.core</kbd> and <kbd>com.cobilas.unity.extensions</kbd> were added to this package.
## [2.8.0] - 29/08/2023
### Changed
- Package dependencies have been changed.
## [2.7.2-ch1] - 28/08/2023
### Changed
- The package author was changed from `Cobilas CTB` to `BélicusBr`.
## [2.7.2] - 30/03/2023
###Fixed
When using `ReadOnlyVarAttribute` in a variable that has a `CustomPropertyDrawer` this `CustomPropertyDrawer` would be disabled.
## [2.7.1] - 25/03/2023
###Fixed
In `T:Luck.WhatProbability<T>(T[])` it could cause an `InvalidCastException`.
## [2.6.0] - 18/03/2023
### Added
The attributes `AButtonAttribute`, `ARepeatButtonAttribute` and `ReadOnlyVarAttribute` have been added.</br>
The `GarbageCollector` and `MonitorStatus` classes have been added.
#### AButtonAttribute & ARepeatButtonAttribute
The `ARepeatButtonAttribute` and `AButtonAttribute` attributes can be added to a `Boolean` field.
turned them into buttons.
#### ReadOnlyVarAttribute
The `ReadOnlyVarAttribute` attribute can be added to any field and it will disable it in the inspector</br>
making it read-only.
## [2.5.2] - 16/03/2023
###Fixed
In the `DecimalRange` method there was a possibility of StackOverflow occurring.
###Fixed
When canceling a `UnityTask` when entering editor mode, an `InvalidOperationalException` may occur.
## [2.3.2] - 12/02/2023
### Fixed (EditorUnityTaskPool)
When the editor entered `PlayModeStateChange.ExitingPlayMode` mode, the asynchronous action could have a new cancellation request
requested again which generates an error.
Now before canceling the asynchronous action, check that the action has not yet been canceled and discarded.
## [1.0.30] - 30/01/2023
### Changed
- Removal of unnecessary assignments.
- Transforming possible fields into `readonly`.
- In the `void:RepaintTexture2D.Paint()` method the `modifiedTexture.SetPixels` instruction was changed to `modifiedTexture.SetPixels32` for performance reasons.
###Fixed
- The `UnityTaskResult result` parameter of the `UnityTask` class construct will not be assigned.
## [1.0.29] 09/01/2023
###Add
Asynchronous classes have been added.
Coroutines with more functions added.
## [1.0.28] 23/11/2021
###Add
The methods `void:UnityFile.InitEditorCSharpFile()` and
`void:UnityFile.InitEditorWinCSharpFile()`
## [1.0.26] 11/11/2022
###ChangeVersion
ChangeVersion became com.cobilas.unity.changeversion@1.0.0
### DebugConsole
DebugConsole became com.cobilas.unity.debugconsole@1.0.0
## [1.0.24] 05/11/2022
### (Fix)ChangeVersionWin.cs
The `ChangeVersionWin.LoadConfig()` method did not have a check that determines<br/>
whether the Config.txt file exists.
## [1.0.23] 20/09/2022
### (Change) ChangeVersion
The ChangeVersion has been turned into a window now.
## [1.0.22] 13/09/2022
### (Fix)DebugConsole.cs
The `System.IO` namespace has been relocated out of the `#if UNITY_EDITOR` condition.
## [1.0.21] 05/09/2021
## Obsolete
- CobilasPaths.cs
- CobilasRandom.cs
- CobilasFile.cs
- CobilasFolder.cs
## [1.0.20] 04/09/2022
###Add
GameDataManager was added to store game data, in short the good old savegame.<br/>
The UnityPath class has been added.<br/>
The Randomico class has been added.<br/>
###Change
The classes `CobilasPaths` and `CobilasRandom` have been replaced by `UnityPath` and `Randomico`.
## [1.0.19] 03/09/2022
###Add
- Editor\Win\DebugConsoleWin.cs
- Runtime\DebugConsole\DebugConsole.cs
## [1.0.18] 27/08/2002
###Add
- Editor\ChangeVersion.cs
- Runtime\CompareObject.cs
- Runtime\RepaintTexture2D.cs
- Runtime\PurposefulErrors.cs
## [1.0.16] 15/08/2022
### Changed
The "BlankSpace" constant has been changed from `protected const float BlankSpace;` to `public const float BlankSpace;`.<br/>
The SingleLineHeight property has been changed from `protected float SingleLineHeight;` to `public static float SingleLineHeight;`.<br/>
The SingleRowHeightWithBlankSpace property has been changed from `protected float SingleRowHeightWithBlankSpace;` to `public static float SingleRowHeightWithBlankSpace;`.<br/>
## [1.0.15] 13/08/2022
- Change Editor\Cobilas.Unity.Editor.Utility.asmdef
- Change Editor\CobilasFile.cs
- Change Runtime\Cobilas.Unity.Utility.asmdef
## [1.0.14] 10/08/2022
- Merge com.cobilas.unity.editor.utility@1.0.7
## [0.0.1] 15/07/2022
### Repository com.cobilas.unity.utility started
- Released to GitHub