// Copyright (C) 2024 negative_seven
// This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. If a copy of the MPL was not
// distributed with this file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System.Collections;
using Exapt.Wrappers.Meta;

namespace Exapt.Wrappers;

[ClassWrapper("Solution")]
public class Solution : NonStaticWrapper<Solution>
{
    public PuzzleId PuzzleId => new(Call("#=q0bXK6vTpnQXmi0XWqqn0yA==")!);

    public Dictionary<int, int> Score
    {
        get
        {
            object dict = Get("#=q9AyVqDGcPv_DK80o9gwBpA==")!;
            // use IDictionary so we don't have to deal with the obfuscated
            // Metric type. unfortunately, it doesn't support linq methods,
            // so we have to use a loop
            Dictionary<int, int> wrapped = [];
            foreach (DictionaryEntry kvp in (IDictionary)dict)
            {
                wrapped[(int)kvp.Key] = (int)kvp.Value!;
            }
            return wrapped;
        }
    }

    public IEnumerable<SolutionExa> SolutionExas
    {
        get
        {
            IEnumerable<object> inners = Enumerable.Cast<object>(
                (System.Collections.IEnumerable)Get("#=qzJMkmdeH1slKh8PKZTwTEA==")!
            );
            return inners.Select(e => new SolutionExa(e));
        }
    }

    internal Solution(object inner)
        : base(inner) { }

    public static Solution? FromFile(string filename)
    {
        Maybe solution = new(CallStatic("#=qrAtvddUJvCjJyuYaeXTtoA==", filename)!);
        return solution.IsSome() ? new Solution(solution.Unwrap()) : null;
    }
}
