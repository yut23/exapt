// Copyright (C) 2024 negative_seven
// This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. If a copy of the MPL was not
// distributed with this file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace Exapt.Wrappers;

[Meta.ClassWrapper("Sim")]
public class Simulation : Meta.NonStaticWrapper<Simulation>
{
    public bool Completed => (bool)Call("#=q9jlSbij7xzD7a5JTreHwgSlOVHw2c6NutHpXBargYEs=")!;
    public int Cycles => (int)Call("#=q6Z2p0iddfrFXKSNmGS9UBQ==")!;
    public int Activity => (int)Call("#=q24Y2a5fbidtcHWquAdDEIBgp$0kfXMpkH3qbYxIvP8g=")!;

    private Simulation(object inner)
        : base(inner) { }

    public static Simulation Create(
        [NotNull] Puzzle puzzle,
        int testRunNumber,
        Team team,
        [NotNull] Dictionary<Team, IEnumerable<SolutionExa>> solutionExas
    )
    {
        Type dictionaryKeyType = Type.GetType("Team, Burbank")!;
        Type dictionaryValueType = typeof(IEnumerable<>).MakeGenericType(Type.GetType("SolutionExa, Burbank")!);
        Type dictionaryType = typeof(Dictionary<,>).MakeGenericType(dictionaryKeyType, dictionaryValueType);

        MethodInfo castToDictionaryValueMethod = typeof(Enumerable)
            .GetMethod("Cast")!
            .MakeGenericMethod(Type.GetType("SolutionExa, Burbank")!);
        object convertedSolutionExas = Meta.Utils.CallConstructor(dictionaryType)!;
        foreach (KeyValuePair<Team, IEnumerable<SolutionExa>> pair in solutionExas)
        {
            object key = pair.Key;
            object value = castToDictionaryValueMethod.Invoke(null, [pair.Value.Select(e => e.Inner)])!;
            _ = Meta.Utils.CallNonStatic(convertedSolutionExas, "Add", key, value);
        }

        object inner = CallStatic(
            "#=qCZfLbsGT8TxGPNVcp52ThA==",
            puzzle.Inner,
            testRunNumber,
            team,
            convertedSolutionExas
        )!;
        return new Simulation(inner);
    }

    public void Step()
    {
        _ = Call("#=qajowlg6dle2da5dexjWRrQ==");
    }

    public static int CountCodeSize([NotNull] Code code)
    {
        return (int)CallStatic("#=q$bzjuqpJ4$1ZnJopcCnGHikwx30NyLQHmmR$fALl5MA=", code.Inner)!;
    }
}
