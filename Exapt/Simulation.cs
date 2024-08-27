namespace Exapt;

public class Simulation
{
    public bool Completed => inner.Completed;
    public int Cycles => inner.Cycles;
    public int Activity => inner.Activity;

    private readonly Wrappers.Simulation inner;

    internal Simulation(Solution solution, int testIndex)
    {
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
