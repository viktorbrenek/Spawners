using HarmonyLib;
using JetBrains.Annotations;
using System;

namespace Sporelings
{
  [UsedImplicitly]
  public class Patch
  {
    [HarmonyPatch(typeof(SpawnArea), nameof(SpawnArea.Awake))]
    public class PatchSpawnAreaAwake
    {
      [UsedImplicitly]
      [HarmonyPrefix]
      [HarmonyPriority(Priority.Normal)]
      // ReSharper disable once InconsistentNaming
      public static void Prefix([NotNull] ref SpawnArea __instance)
      {
        try
        {
          Shroomer.Instance.OnPatchSpawnAreaAwake(ref __instance);
        }
        catch (Exception e)
        {
          ZLog.LogError(e.Message);
          ZLog.LogError(e);
        }
      }

      [UsedImplicitly]
      [HarmonyPostfix]
      [HarmonyPriority(Priority.Normal)]
      // ReSharper disable once InconsistentNaming
      public static void Postfix([NotNull] ref SpawnArea __instance)
      {
        try
        {
          Shroomer.Instance.OnPatchSpawnAreaAwake(ref __instance);
        }
        catch (Exception e)
        {
          ZLog.LogError(e.Message);
          ZLog.LogError(e);
        }
      }
    }
  }
}
