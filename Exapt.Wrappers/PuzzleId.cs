// Copyright (C) 2024 negative_seven
// This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. If a copy of the MPL was not
// distributed with this file, You can obtain one at https://mozilla.org/MPL/2.0/.

namespace Exapt.Wrappers;

[Meta.ClassWrapper("Puzzle")]
public class PuzzleId : Meta.NonStaticWrapper<PuzzleId>
{
    public string Id => (string)Get("ID")!;

    internal PuzzleId(object inner)
        : base(inner) { }

    public PuzzleId(string id)
        : base(CallConstructor(id)!) { }
}
