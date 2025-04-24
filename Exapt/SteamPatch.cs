using HarmonyLib;

namespace Exapt;

[HarmonyPatch("Steamworks.SteamUserStats, Steamworks.NET", "SetAchievement")]
internal static class PatchSetAchievement
{
    public static bool Prefix()
    {
        return false;
    }
}

[HarmonyPatch("Steamworks.SteamUserStats, Steamworks.NET", "StoreStats")]
internal static class PatchStoreStats
{
    public static bool Prefix()
    {
        return false;
    }
}
