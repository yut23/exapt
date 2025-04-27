// Copyright (C) 2024 negative_seven
// This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. If a copy of the MPL was not
// distributed with this file, You can obtain one at https://mozilla.org/MPL/2.0/.

using Exapt.Wrappers.Meta;

namespace Exapt.Wrappers;

[ClassWrapper("#=q2aH5$ZKtSXtKEmmCMXr5b9pWEMtpEOdvcXOTcJQZaGc=")]
public class Config : NonStaticWrapper<Config>
{
    internal Config(object inner)
        : base(inner) { }

    public Config(string configPath)
        : base(CallConstructor(configPath)!) { }
}
