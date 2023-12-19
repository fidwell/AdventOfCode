using AdventOfCode.Core.PuzzleSolvers;
using AdventOfCode.Core.PuzzleSolvers._2023;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode.Tests.SolutionVerifiers;

[TestClass]
public class SolutionVerifier2023 : SolutionVerifier
{
    private readonly IPuzzleSolver[] _solvers;

    public SolutionVerifier2023()
    {
        _solvers = [
            new Puzzle01Solver(),
            new Puzzle02Solver(),
            new Puzzle03Solver(),
            new Puzzle04Solver(),
            new Puzzle05Solver(),
            new Puzzle06Solver(),
            new Puzzle07Solver(),
            new Puzzle08Solver(),
            new Puzzle09Solver(),
            new Puzzle10Solver(),
            new Puzzle11Solver(),
            new Puzzle12Solver(),
            new Puzzle13Solver(),
            new Puzzle14Solver(),
            new Puzzle15Solver(),
            new Puzzle16Solver(),
            new Puzzle17Solver(),
            new Puzzle18Solver(),
            new Puzzle19Solver(),
            new Puzzle20Solver(),
            new Puzzle21Solver(),
            new Puzzle22Solver(),
            new Puzzle23Solver(),
            new Puzzle24Solver(),
            new Puzzle25Solver()
        ];
    }

    [DataTestMethod, Timeout(Timeout)]
    [DataRow(1, 1, true, "142", DisplayName = "2023.01.1-s")]
    [DataRow(1, 1, false, "54632", DisplayName = "2023.01.1")]
    [DataRow(1, 2, true, "281", DisplayName = "2023.01.2-s")]
    [DataRow(1, 2, false, "54019", DisplayName = "2023.01.2")]

    [DataRow(2, 1, true, "8", DisplayName = "2023.02.1-s")]
    [DataRow(2, 1, false, "2348", DisplayName = "2023.02.1")]
    [DataRow(2, 2, true, "2286", DisplayName = "2023.02.2-s")]
    [DataRow(2, 2, false, "76008", DisplayName = "2023.02.2")]

    [DataRow(3, 1, true, "4361", DisplayName = "2023.03.1-s")]
    [DataRow(3, 1, false, "540131", DisplayName = "2023.03.1")]
    [DataRow(3, 2, true, "467835", DisplayName = "2023.03.2-s")]
    [DataRow(3, 2, false, "86879020", DisplayName = "2023.03.2")]

    [DataRow(4, 1, true, "13", DisplayName = "2023.04.1-s")]
    [DataRow(4, 1, false, "26218", DisplayName = "2023.04.1")]
    [DataRow(4, 2, true, "30", DisplayName = "2023.04.2-s")]
    [DataRow(4, 2, false, "9997537", DisplayName = "2023.04.2")]

    [DataRow(5, 1, true, "35", DisplayName = "2023.05.1-s")]
    [DataRow(5, 1, false, "323142486", DisplayName = "2023.05.1")]
    [DataRow(5, 2, true, "46", DisplayName = "2023.05.2-s")]
    [DataRow(5, 2, false, "79874951", DisplayName = "2023.05.2")]

    [DataRow(6, 1, true, "288", DisplayName = "2023.06.1-s")]
    [DataRow(6, 1, false, "512295", DisplayName = "2023.06.1")]
    [DataRow(6, 2, true, "71503", DisplayName = "2023.06.2-s")]
    [DataRow(6, 2, false, "36530883", DisplayName = "2023.06.2")]

    [DataRow(7, 1, true, "6440", DisplayName = "2023.07.1-s")]
    [DataRow(7, 1, false, "254024898", DisplayName = "2023.07.1")]
    [DataRow(7, 2, true, "5905", DisplayName = "2023.07.2-s")]
    [DataRow(7, 2, false, "254115617", DisplayName = "2023.07.2")]

    [DataRow(8, 1, true, "6", DisplayName = "2023.08.1-s")]
    [DataRow(8, 1, false, "12737", DisplayName = "2023.08.1")]
    [DataRow(8, 2, true, "6", DisplayName = "2023.08.2-s")]
    [DataRow(8, 2, false, "9064949303801", DisplayName = "2023.08.2")]

    [DataRow(9, 1, true, "114", DisplayName = "2023.09.1-s")]
    [DataRow(9, 1, false, "1992273652", DisplayName = "2023.09.1")]
    [DataRow(9, 2, true, "2", DisplayName = "2023.09.2-s")]
    [DataRow(9, 2, false, "1012", DisplayName = "2023.09.2")]

    [DataRow(10, 1, true, "8", DisplayName = "2023.10.1-s")]
    [DataRow(10, 1, false, "6773", DisplayName = "2023.10.1")]
    [DataRow(10, 2, true, "10", DisplayName = "2023.10.2-s")]
    [DataRow(10, 2, false, "493", DisplayName = "2023.10.2")]

    [DataRow(11, 1, true, "374", DisplayName = "2023.11.1-s")]
    [DataRow(11, 1, false, "9599070", DisplayName = "2023.11.1")]
    [DataRow(11, 2, true, "82000210", DisplayName = "2023.11.2-s")]
    [DataRow(11, 2, false, "842645913794", DisplayName = "2023.11.2")]

    [DataRow(12, 1, true, "21", DisplayName = "2023.12.1-s")]
    [DataRow(12, 1, false, "7017", DisplayName = "2023.12.1")]
    [DataRow(12, 2, true, "525152", DisplayName = "2023.12.2-s")]
    [DataRow(12, 2, false, "527570479489", DisplayName = "2023.12.2")]

    [DataRow(13, 1, true, "405", DisplayName = "2023.13.1-s")]
    [DataRow(13, 1, false, "26957", DisplayName = "2023.13.1")]
    [DataRow(13, 2, true, "400", DisplayName = "2023.13.2-s")]
    [DataRow(13, 2, false, "42695", DisplayName = "2023.13.2")]

    [DataRow(14, 1, true, "136", DisplayName = "2023.14.1-s")]
    [DataRow(14, 1, false, "113525", DisplayName = "2023.14.1")]
    [DataRow(14, 2, true, "64", DisplayName = "2023.14.2-s")]
    [DataRow(14, 2, false, "101292", DisplayName = "2023.14.2")]

    [DataRow(15, 1, true, "1320", DisplayName = "2023.15.1-s")]
    [DataRow(15, 1, false, "516657", DisplayName = "2023.15.1")]
    [DataRow(15, 2, true, "145", DisplayName = "2023.15.2-s")]
    [DataRow(15, 2, false, "210906", DisplayName = "2023.15.2")]

    [DataRow(16, 1, true, "46", DisplayName = "2023.16.1-s")]
    [DataRow(16, 1, false, "7185", DisplayName = "2023.16.1")]
    [DataRow(16, 2, true, "51", DisplayName = "2023.16.2-s")]
    [DataRow(16, 2, false, "7616", DisplayName = "2023.16.2")]

    [DataRow(17, 1, true, "102", DisplayName = "2023.17.1-s")]
    [DataRow(17, 1, false, "785", DisplayName = "2023.17.1")]
    [DataRow(17, 2, true, "94", DisplayName = "2023.17.2-s")]
    [DataRow(17, 2, false, "922", DisplayName = "2023.17.2")]

    [DataRow(18, 1, true, "62", DisplayName = "2023.18.1-s")]
    [DataRow(18, 1, false, "58550", DisplayName = "2023.18.1")]
    [DataRow(18, 2, true, "952408144115", DisplayName = "2023.18.2-s")]
    [DataRow(18, 2, false, "47452118468566", DisplayName = "2023.18.2")]

    [DataRow(19, 1, true, "19114", DisplayName = "2023.19.1-s")]
    [DataRow(19, 1, false, "432434", DisplayName = "2023.19.1")]
    [DataRow(19, 2, true, "167409079868000", DisplayName = "2023.19.2-s")]
    [DataRow(19, 2, false, "132557544578569", DisplayName = "2023.19.2")]

    [DataRow(20, 1, true, "_", DisplayName = "2023.20.1-s")]
    [DataRow(20, 1, false, "_", DisplayName = "2023.20.1")]
    [DataRow(20, 2, true, "_", DisplayName = "2023.20.2-s")]
    [DataRow(20, 2, false, "_", DisplayName = "2023.20.2")]

    [DataRow(21, 1, true, "_", DisplayName = "2023.21.1-s")]
    [DataRow(21, 1, false, "_", DisplayName = "2023.21.1")]
    [DataRow(21, 2, true, "_", DisplayName = "2023.21.2-s")]
    [DataRow(21, 2, false, "_", DisplayName = "2023.21.2")]

    [DataRow(22, 1, true, "_", DisplayName = "2023.22.1-s")]
    [DataRow(22, 1, false, "_", DisplayName = "2023.22.1")]
    [DataRow(22, 2, true, "_", DisplayName = "2023.22.2-s")]
    [DataRow(22, 2, false, "_", DisplayName = "2023.22.2")]

    [DataRow(23, 1, true, "_", DisplayName = "2023.23.1-s")]
    [DataRow(23, 1, false, "_", DisplayName = "2023.23.1")]
    [DataRow(23, 2, true, "_", DisplayName = "2023.23.2-s")]
    [DataRow(23, 2, false, "_", DisplayName = "2023.23.2")]

    [DataRow(24, 1, true, "_", DisplayName = "2023.24.1-s")]
    [DataRow(24, 1, false, "_", DisplayName = "2023.24.1")]
    [DataRow(24, 2, true, "_", DisplayName = "2023.24.2-s")]
    [DataRow(24, 2, false, "_", DisplayName = "2023.24.2")]

    [DataRow(25, 1, true, "_", DisplayName = "2023.25.1-s")]
    [DataRow(25, 1, false, "_", DisplayName = "2023.25.1")]
    [DataRow(25, 2, true, "_", DisplayName = "2023.25.2-s")]
    [DataRow(25, 2, false, "_", DisplayName = "2023.25.2")]
    public void Solve(int puzzle, int part, bool useSample, string expected) =>
        Solve(_solvers[puzzle - 1], 2023, puzzle, part, useSample, expected);
}
