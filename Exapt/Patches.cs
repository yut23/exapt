// Copyright (C) 2024 negative_seven
// This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. If a copy of the MPL was not
// distributed with this file, You can obtain one at https://mozilla.org/MPL/2.0/.

using HarmonyLib;

namespace Exapt;

// Steam achievement calls are used inside level code
[HarmonyPatch]
internal static class PatchSteamUserStats
{
    [HarmonyPatch("Steamworks.SteamUserStats, Steamworks.NET", "SetAchievement")]
    [HarmonyPrefix]
    public static bool SetAchievementPrefix()
    {
        return false;
    }

    [HarmonyPatch("Steamworks.SteamUserStats, Steamworks.NET", "StoreStats")]
    [HarmonyPrefix]
    public static bool StoreStatsPrefix()
    {
        return false;
    }
}

// Skip user input checks in sandbox
[HarmonyPatch("SpecialPuzzleLogics+#=qTeI8TrGQjU99Ov7BJNVX1w==", "#=qMcivoG7Mpioeq5qRq72aCA==")]
internal static class PatchSandboxKeyPressed
{
    public static bool Prefix(ref bool __result)
    {
        __result = false;
        return false;
    }
}
