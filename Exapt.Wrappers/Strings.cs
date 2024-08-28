// Copyright (C) 2024 negative_seven
// This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. If a copy of the MPL was not
// distributed with this file, You can obtain one at https://mozilla.org/MPL/2.0/.

namespace Exapt.Wrappers;

public class Strings : StaticWrapper<Strings>
{
    private static bool initialized = false;

    static Strings()
    {
        SetWrappedType("#=q$q_5RIWNSfdbsTA5NIdJHA==");
    }

    public static void Initialize(string exapunksDirectory)
    {
        if (!initialized)
        {
            Utils.WithWorkingDirectory(exapunksDirectory, () => CallStatic("#=q2IcpkAiRPnhyxP4lHls68w=="));
            initialized = true;
        }
    }
}
