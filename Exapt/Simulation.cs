// Copyright (C) 2024 negative_seven
// This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. If a copy of the MPL was not
// distributed with this file, You can obtain one at https://mozilla.org/MPL/2.0/.

namespace Exapt;

public class Simulation
{
    private readonly Solution solution;

    public bool Completed => inner.Completed;
    public int Cycles => inner.Cycles;
    public int CodeSize => solution.Exas.Select(e => Wrappers.Simulation.CountCodeSize(e.Code)).Sum();
    public int Activity => inner.Activity;

    private readonly Wrappers.Simulation inner;

    internal Simulation(Solution solution, int testIndex)
    {
        this.solution = solution;

        Dictionary<Wrappers.Team, IEnumerable<Wrappers.SolutionExa>> solutionExas =
            new() { { Wrappers.Team.Player, solution.Exas } };
        inner = Wrappers.Simulation.Create(
            Wrappers.Puzzles.FromId(new Wrappers.PuzzleId(solution.PuzzleId)),
            testIndex,
            Wrappers.Team.Player,
            solutionExas
        );
    }

    public void Step()
    {
        inner.Step();
    }
}
