# Changelog
## [2.5.2] - 16/03/2023
### Fixed
No método `DecimalRange` Tinha a possibilidade de ocorrer StackOverflow.
### Fixed
Ao cancelar uma `UnityTask` ao entrar no modo editor pode ocorrer um `InvalidOperationalException`.
## [2.3.2] - 12/02/2023
### Fixed (EditorUnityTaskPool)
Quando o editor entrava no modo `PlayModeStateChange.ExitingPlayMode` poderia acontecer da ação assincrona ter uma nova requisição de cancelamento
solicitada novamente o que gera um erro.
Agora antes de cancelar a ação assincrona e verificado se á ação ainda não foi cancelada e descartada.
## [1.0.30] - 30/01/2023
### Changed
- Remoção de atribuições desnecessárias.
- Transformando possiveis campos em `readonly`.
- No método `void:RepaintTexture2D.Paint()` a instrução `modifiedTexture.SetPixels` foi alterada para `modifiedTexture.SetPixels32` por questões de desempenho.
### Fixed
- O parâmetro `UnityTaskResult result` do construdo da classe `UnityTask` não erá atribuido.
## [1.0.29] 09/01/2023
### Add
Classes asincronas foram adicionadas.
Corotinas com mais funções adicionadas.
## [1.0.28] 23/11/2022
### Add
Foi adicionado em `UnityFile` os métodos `void:UnityFile.InitEditorCSharpFile()` e 
`void:UnityFile.InitEditorWinCSharpFile()`
## [1.0.26] 11/11/2022
### ChangeVersion
ChangeVersion se tornou com.cobilas.unity.changeversion@1.0.0
### DebugConsole
DebugConsole se tornou com.cobilas.unity.debugconsole@1.0.0
## [1.0.24] 05/11/2022
### (Fix)ChangeVersionWin.cs
O método `ChangeVersionWin.LoadConfig()` não possuia uma verificação que determina<br/>
se o arquivo Config.txt existe.
## [1.0.23] 20/09/2022
### (Change) ChangeVersion
O ChangeVersion foi transformado em janela agora.
## [1.0.22] 13/09/2022
### (Fix)DebugConsole.cs
O namespace `System.IO` foi realocado pra fora da condição `#if UNITY_EDITOR`.
## [1.0.21] 05/09/2022
## Obsolete
- CobilasPaths.cs
- CobilasRandom.cs
- CobilasFile.cs
- CobilasFolder.cs
## [1.0.20] 04/09/2022
### Add
Foi adicionado GameDataManager para guarda dados do jogo, em suma o bom e velho savegame.<br/>
A classe UnityPath foi adicionado.<br/>
A classe Randomico foi adicionado.<br/>
### Change
As classes `CobilasPaths` e `CobilasRandom` foram substituidos por `UnityPath` e `Randomico`.
## [1.0.19] 03/09/2022
### Add
- Editor\Win\DebugConsoleWin.cs
- Runtime\DebugConsole\DebugConsole.cs
## [1.0.18] 27/08/2002
### Add
- Editor\ChangeVersion.cs
- Runtime\CompareObject.cs
- Runtime\RepaintTexture2D.cs
- Runtime\PurposefulErrors.cs
## [1.0.16] 15/08/2022
### Changed
A constante "BlankSpace" foi alterada de `protected const float BlankSpace;` para `public const float BlankSpace;`.<br/>
A propriedade SingleLineHeight foi alterada de `protected float SingleLineHeight;` para `public static float SingleLineHeight;`.<br/>
A propriedade SingleRowHeightWithBlankSpace foi alterada de `protected float SingleRowHeightWithBlankSpace;` para `public static float SingleRowHeightWithBlankSpace;`.<br/>
## [1.0.15] 13/08/2022
- Change Editor\Cobilas.Unity.Editor.Utility.asmdef
- Change Editor\CobilasFile.cs
- Change Runtime\Cobilas.Unity.Utility.asmdef
## [1.0.14] 10/08/2022
- Merge com.cobilas.unity.editor.utility@1.0.7
## [0.0.1] 15/07/2022
### Repositorio com.cobilas.unity.utility iniciado
- Lançado para o GitHub