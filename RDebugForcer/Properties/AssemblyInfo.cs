using MelonLoader;
using RDebugForcer; // The namespace of your mod class
// ...
[assembly: MelonInfo(typeof(RDebugForceMod), "Rhythm Doctor Debug Forcer", "0.0.2", "lightfall")]
[assembly: MelonColor(System.ConsoleColor.DarkYellow)]

// Create and Setup a MelonGame Attribute to mark a Melon as Universal or Compatible with specific Games.
// If no MelonGame Attribute is found or any of the Values for any MelonGame Attribute on the Melon is null or empty it will be assumed the Melon is Universal.
// Values for MelonGame Attribute can be found in the Game's app.info file or printed at the top of every log directly beneath the Unity version.
[assembly: MelonGame("7th Beat Games", "Rhythm Doctor")]