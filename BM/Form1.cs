using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace BM
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void BtnSolve_Click(object sender, EventArgs e)
        {
            Globals.numberOfRots = 0;
            Globals.rotList.Clear();
            Globals.totalElapsedTime = 0;
            RubiksCube.Start();
            rots.Text = String.Empty;
            List<string> sublist = Globals.rotList.GetRange(19, Globals.rotList.Count - 20);
            rots.Text = string.Join(" ", sublist);
            numOfRots.Text = "Number of Rotations: " + Globals.numberOfRots.ToString();
            execTime.Text = "Elapsed Time: " + Globals.totalElapsedTime.ToString();
            fCrossTime.Text = "Front Cross Time: " + Globals.solveCrossTime.ToString();
            fCornerTime.Text = "Front Corner Time: " + Globals.solveCornersTime.ToString();
            middleTime.Text = "Middle Layer Time: " + Globals.solveMiddleTime.ToString();
            bCrossTime.Text = "Back Cross Formation Time: " + Globals.solveBackCrossTime.ToString();
            bCross2Time.Text = "Back Cross Solution Time: " + Globals.allignBackCrossTime.ToString();
            bCornersTime.Text = "Back Corner Placement Time: " + Globals.solveBackCrornerTime.ToString();
            bCorners2Time.Text = "Back Corner Solution Time: " + Globals.allignCornersTime.ToString();
            TextBox[,] fF = new TextBox[,]
            {
                {f00, f01, f02 },
                {f10, f11, f12 },
                {f20, f21, f22}};
            TextBox[,] fL = new TextBox[,]
            {
                {l00, l01, l02 },
                {l10, l11, l12 },
                {l20, l21, l22}};
            TextBox[,] fR = new TextBox[,]
            {
                {r00, r01, r02 },
                {r10, r11, r12 },
                {r20, r21, r22}};
            TextBox[,] fB = new TextBox[,]
            {
                {b00, b01, b02 },
                {b10, b11, b12 },
                {b20, b21, b22}};
            TextBox[,] fU = new TextBox[,]
            {
                {u00, u01, u02 },
                {u10, u11, u12 },
                {u20, u21, u22}};
            TextBox[,] fD = new TextBox[,]
            {
                {d00, d01, d02 },
                {d10, d11, d12 },
                {d20, d21, d22}};
            TextBox[][,] textBoxes = new TextBox[6][,]
            {
                fF, fL, fR, fB, fU, fD
            };
            for (int i = 0; i<6; i++)
            {
                for (int j=0; j<3; j++)
                {
                    for (int k=0; k<3; k++)
                    {
                        if (Globals.unsolvedCube[i][j,k] == 'O')
                        {
                            textBoxes[i][j, k].BackColor = Color.Orange;
                        }
                        else if (Globals.unsolvedCube[i][j, k] == 'G')
                        {
                            textBoxes[i][j, k].BackColor = Color.Green;
                        }
                        else if (Globals.unsolvedCube[i][j, k] == 'B')
                        {
                            textBoxes[i][j, k].BackColor = Color.Blue;
                        }
                        else if (Globals.unsolvedCube[i][j, k] == 'R')
                        {
                            textBoxes[i][j, k].BackColor = Color.Red;
                        }
                        else if (Globals.unsolvedCube[i][j,k] == 'Y')
                        {
                            textBoxes[i][j, k].BackColor = Color.Yellow;
                        }
                        else if(Globals.unsolvedCube[i][j, k] == 'W')
                        {
                            textBoxes[i][j, k].BackColor = Color.White;
                        }
                    }
                }
            }

        }
    }
}



public static class Globals
{
    public static List<string> rotList = new List<string>();
    public static int numberOfRots = 0;
    public static long totalElapsedTime, solveCrossTime, solveCornersTime,
        solveMiddleTime, solveBackCrossTime, allignBackCrossTime, solveBackCrornerTime, allignCornersTime;
    public static char[][,] unsolvedCube = new char[6][,];

}


public class RubiksCube
{
    public static void Start()
    {
        var totalStopwatch = new Stopwatch();
        var solveCrossStopwatch = new Stopwatch();
        var solveCornersStopwatch = new Stopwatch();
        var solveMiddleStopwatch = new Stopwatch();
        var solveBackCrossStopwatch = new Stopwatch();
        var allignBackCrossStopwatch = new Stopwatch();
        var solveBackCornerStopwatch = new Stopwatch();
        var allignCornersStopwatch = new Stopwatch();
        List<string> possibleMoves = new List<string>
        {"LA","RC","LC","RA","TC","TA","DC","DA","BA","FC","BC","FA"};
        Random random = new Random();
        char[,] F = new char[,]{
            {'O','O','O'},
            {'O','O','O'},
            {'O','O','O'}};
        char[,] L = new char[,]{
            {'G','G','G'},
            {'G','G','G'},
            {'G','G','G'}};
        char[,] R = new char[,]{
            {'B','B','B'},
            {'B','B','B'},
            {'B','B','B'}};
        char[,] B = new char[,]{
            {'R','R','R'},
            {'R','R','R'},
            {'R','R','R'}};
        char[,] U = new char[,]{
            {'Y','Y','Y'},
            {'Y','Y','Y'},
            {'Y','Y','Y'}};
        char[,] D = new char[,]{
            {'W','W','W'},
            {'W','W','W'},
            {'W','W','W'}};

        /*rotation(ref F, ref B, ref U, ref D, ref L, ref R, "LC");
        foreach (char el in F)
        {
            Console.Write(el + " ");
        }

        Console.Write('\n');
        foreach (char el in L)
        {
            Console.Write(el + " ");
        }

        Console.Write('\n');
        foreach (char el in R)
        {
            Console.Write(el + " ");
        }

        Console.Write('\n');
        foreach (char el in B)
        {
            Console.Write(el + " ");
        }

        Console.Write('\n');
        foreach (char el in U)
        {
            Console.Write(el + " ");
        }

        Console.Write('\n');
        foreach (char el in D)
        {
            Console.Write(el + " ");
        }

        Console.Write('\n'); */



        for (int i = 0; i < 20; i++)
        {
            int index = random.Next(possibleMoves.Count);
            if (possibleMoves[index] == "LA")
            {
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "LA");
                checkNumber(ref F, ref B, ref U, ref D, ref L, ref R, "LA");
            }
            else if (possibleMoves[index] == "LC")
            {
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "LC");
                checkNumber(ref F, ref B, ref U, ref D, ref L, ref R, "LC");
            }
            else if (possibleMoves[index] == "RC")
            {
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "RC");
                checkNumber(ref F, ref B, ref U, ref D, ref L, ref R, "RC");
            }
            else if (possibleMoves[index] == "RA")
            {
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "RA");
                checkNumber(ref F, ref B, ref U, ref D, ref L, ref R, "RA");
            }
            else if (possibleMoves[index] == "TC")
            {
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "TC");
                checkNumber(ref F, ref B, ref U, ref D, ref L, ref R, "TC");
            }
            else if (possibleMoves[index] == "TA")
            {
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "TA");
                checkNumber(ref F, ref B, ref U, ref D, ref L, ref R, "TA");
            }
            else if (possibleMoves[index] == "DC")
            {
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "DC");
                checkNumber(ref F, ref B, ref U, ref D, ref L, ref R, "DC");
            }
            else if (possibleMoves[index] == "DA")
            {
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "DA");
                checkNumber(ref F, ref B, ref U, ref D, ref L, ref R, "DA");
            }
            else if (possibleMoves[index] == "BA")
            {
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BA");
                checkNumber(ref F, ref B, ref U, ref D, ref L, ref R, "BA");
            }
            else if (possibleMoves[index] == "BC")
            {
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
                checkNumber(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
            }
            else if (possibleMoves[index] == "FC")
            {
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "FC");
                checkNumber(ref F, ref B, ref U, ref D, ref L, ref R, "FC");
            }
            else if (possibleMoves[index] == "FA")
            {
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "FA");
                checkNumber(ref F, ref B, ref U, ref D, ref L, ref R, "FA");
            }
            else { Console.WriteLine("Unkown command"); }
        }

        Globals.unsolvedCube[0] = (char[,])F.Clone();
        Globals.unsolvedCube[1] = (char[,])L.Clone();
        Globals.unsolvedCube[2] = (char[,])R.Clone();
        Globals.unsolvedCube[3] = (char[,])B.Clone();
        Globals.unsolvedCube[4] = (char[,])U.Clone();
        Globals.unsolvedCube[5] = (char[,])D.Clone();

        totalStopwatch.Restart();
        solveCrossStopwatch.Restart();
        do
        {
            SolveCross(ref F, ref B, ref U, ref D, ref L, ref R);
        } while (!(F[0, 1] == 'O' & U[2, 1] == 'Y' & F[1, 2] == 'O' & R[1, 0] == 'B' & F[2, 1] == 'O' & D[0, 1] == 'W' & F[1, 0] == 'O' & L[1, 2] == 'G'));
        solveCrossStopwatch.Stop();
        Globals.solveCrossTime = solveCrossStopwatch.ElapsedMilliseconds;
        solveCornersStopwatch.Restart();
        do
        {
            SolveCorners(ref F, ref B, ref U, ref D, ref L, ref R);
        } while (!(F[0, 0] == 'O' & U[2, 0] == 'Y' & F[0, 2] == 'O' & U[2, 2] == 'Y' & F[2, 0] == 'O' & D[0, 0] == 'W' & F[2, 2] == 'O' & D[0, 2] == 'W'));
        solveCornersStopwatch.Stop();
        Globals.solveCornersTime = solveCornersStopwatch.ElapsedMilliseconds;
        solveMiddleStopwatch.Restart();
        do
        {
            SolveMiddle(ref F, ref B, ref U, ref D, ref L, ref R);
        } while (!(L[0, 1] == 'G' & U[1, 0] == 'Y' & U[1, 2] == 'Y' & R[0, 1] == 'B' & R[2, 1] == 'B' & D[1, 2] == 'W' & D[1, 0] == 'W' & L[2, 1] == 'G'));
        solveMiddleStopwatch.Stop();
        Globals.solveMiddleTime = solveMiddleStopwatch.ElapsedMilliseconds;
        solveBackCrossStopwatch.Restart();
        do
        {
            SolveBackCross(ref F, ref B, ref U, ref D, ref L, ref R);
        } while (!(B[0, 1] == 'R' & B[1, 0] == 'R' & B[1, 2] == 'R' & B[2, 1] == 'R'));
        solveBackCrossStopwatch.Stop();
        Globals.solveBackCrossTime = solveBackCrossStopwatch.ElapsedMilliseconds;
        allignBackCrossStopwatch.Restart();
        do
        {
            AllignBackCross(ref F, ref B, ref U, ref D, ref L, ref R);
        } while (!(L[1, 0] == 'G' & U[0, 1] == 'Y' & R[1, 2] == 'B' & D[2, 1] == 'W'));
        allignBackCrossStopwatch.Stop();
        Globals.allignBackCrossTime = allignBackCrossStopwatch.ElapsedMilliseconds;
        solveBackCornerStopwatch.Restart();
        do
        {
            SolveBackCorner(ref F, ref B, ref U, ref D, ref L, ref R);
        } while (!((B[0, 2] == 'R' & U[0, 0] == 'Y' & L[0, 0] == 'G') | (B[0, 2] == 'G' & U[0, 0] == 'R' & L[0, 0] == 'Y') | (B[0, 2] == 'Y' & U[0, 0] == 'G' & L[0, 0] == 'R') &
                ((B[0, 0] == 'R' & U[0, 2] == 'Y' & R[0, 2] == 'B') | (B[0, 0] == 'B' & U[0, 2] == 'R' & R[0, 2] == 'Y') | (B[0, 0] == 'Y' & U[0, 2] == 'B' & R[0, 2] == 'R')) &
                ((B[2, 2] == 'R' & L[2, 0] == 'G' & D[2, 0] == 'W') | (B[2, 2] == 'W' & L[2, 0] == 'R' & D[2, 0] == 'G') | (B[2, 2] == 'G' & L[2, 0] == 'W' & D[2, 0] == 'R')) &
                ((B[2, 0] == 'R' & R[2, 2] == 'B' & D[2, 2] == 'W') | (B[2, 0] == 'W' & R[2, 2] == 'R' & D[2, 2] == 'B') | (B[2, 0] == 'B' & R[2, 2] == 'W' & D[2, 2] == 'R'))));
        solveBackCornerStopwatch.Stop();
        Globals.solveCornersTime = solveBackCornerStopwatch.ElapsedMilliseconds;
        allignCornersStopwatch.Restart();
        AllignCorners(ref F, ref B, ref U, ref D, ref L, ref R);
        allignCornersStopwatch.Stop();
        Globals.allignCornersTime = allignCornersStopwatch.ElapsedMilliseconds;
        totalStopwatch.Stop();
        Globals.totalElapsedTime = totalStopwatch.ElapsedMilliseconds;
    }

    public static void checkNumber(ref char[,] F, ref char[,] B, ref char[,] U, ref char[,] D, ref char[,] L, ref char[,] R, string rot)
    {
        char[][,] maxArray = new char[][,] { F, B, U, D, L, R };
        int countO = 0;
        int countG = 0;
        int countB = 0;
        int countY = 0;
        int countW = 0;
        int countR = 0;
        for (int i = 0; i < 6; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                for (int k = 0; k < 3; k++)
                {
                    if (maxArray[i][j, k] == 'O')
                    {
                        countO++;
                    }
                    else if (maxArray[i][j, k] == 'G')
                    {
                        countG++;
                    }
                    else if (maxArray[i][j, k] == 'B')
                    {
                        countB++;
                    }
                    else if (maxArray[i][j, k] == 'Y')
                    {
                        countY++;
                    }
                    else if (maxArray[i][j, k] == 'W')
                    {
                        countW++;
                    }
                    else if (maxArray[i][j, k] == 'R')
                    {
                        countR++;
                    }
                }
            }
        }
        if (countO != 9 | countG != 9 | countB != 9 | countY != 9 | countW != 9 | countR != 9)
        {
            Console.WriteLine(rot);
        }
    }
    public static void rotation(ref char[,] F, ref char[,] B, ref char[,] U, ref char[,] D, ref char[,] L, ref char[,] R, string rot)
    {
        char[,] tempF = (char[,])F.Clone();
        char[,] tempB = (char[,])B.Clone();
        char[,] tempU = (char[,])U.Clone();
        char[,] tempD = (char[,])D.Clone();
        char[,] tempL = (char[,])L.Clone();
        char[,] tempR = (char[,])R.Clone();
        if (rot == "LA")
        {
            U[0, 0] = tempF[0, 0]; U[1, 0] = tempF[1, 0]; U[2, 0] = tempF[2, 0];
            B[0, 2] = tempU[2, 0]; B[1, 2] = tempU[1, 0]; B[2, 2] = tempU[0, 0];
            D[0, 0] = tempB[2, 2]; D[1, 0] = tempB[1, 2]; D[2, 0] = tempB[0, 2];
            F[0, 0] = tempD[0, 0]; F[1, 0] = tempD[1, 0]; F[2, 0] = tempD[2, 0];
            L[0, 0] = tempL[0, 2]; L[0, 2] = tempL[2, 2]; L[2, 2] = tempL[2, 0]; L[2, 0] = tempL[0, 0];
            L[0, 1] = tempL[1, 2]; L[1, 2] = tempL[2, 1]; L[2, 1] = tempL[1, 0]; L[1, 0] = tempL[0, 1];
            Globals.rotList.Add("LA");
        }
        else if (rot == "LC")
        {
            U[0, 0] = tempB[2, 2]; U[1, 0] = tempB[1, 2]; U[2, 0] = tempB[0, 2];
            B[0, 2] = tempD[2, 0]; B[1, 2] = tempD[1, 0]; B[2, 2] = tempD[0, 0];
            D[0, 0] = tempF[0, 0]; D[1, 0] = tempF[1, 0]; D[2, 0] = tempF[2, 0];
            F[0, 0] = tempU[0, 0]; F[1, 0] = tempU[1, 0]; F[2, 0] = tempU[2, 0];
            L[0, 0] = tempL[2, 0]; L[0, 2] = tempL[0, 0]; L[2, 2] = tempL[0, 2]; L[2, 0] = tempL[2, 2];
            L[0, 1] = tempL[1, 0]; L[1, 2] = tempL[0, 1]; L[2, 1] = tempL[1, 2]; L[1, 0] = tempL[2, 1];
            Globals.rotList.Add("LC");
        }
        else if (rot == "RC")
        {
            U[0, 2] = tempF[0, 2]; U[1, 2] = tempF[1, 2]; U[2, 2] = tempF[2, 2];
            B[2, 0] = tempU[0, 2]; B[1, 0] = tempU[1, 2]; B[0, 0] = tempU[2, 2];
            D[0, 2] = tempB[2, 0]; D[1, 2] = tempB[1, 0]; D[2, 2] = tempB[0, 0];
            F[0, 2] = tempD[0, 2]; F[1, 2] = tempD[1, 2]; F[2, 2] = tempD[2, 2];
            R[0, 0] = tempR[2, 0]; R[0, 2] = tempR[0, 0]; R[2, 2] = tempR[0, 2]; R[2, 0] = tempR[2, 2];
            R[0, 1] = tempR[1, 0]; R[1, 2] = tempR[0, 1]; R[2, 1] = tempR[1, 2]; R[1, 0] = tempR[2, 1];
            Globals.rotList.Add("RC");
        }
        else if (rot == "RA")
        {
            U[0, 2] = tempB[2, 0]; U[1, 2] = tempB[1, 0]; U[2, 2] = tempB[0, 0];
            B[0, 0] = tempD[2, 2]; B[1, 0] = tempD[1, 2]; B[2, 0] = tempD[0, 2];
            D[0, 2] = tempF[0, 2]; D[1, 2] = tempF[1, 2]; D[2, 2] = tempF[2, 2];
            F[0, 2] = tempU[0, 2]; F[1, 2] = tempU[1, 2]; F[2, 2] = tempU[2, 2];
            R[0, 0] = tempR[0, 2]; R[0, 2] = tempR[2, 2]; R[2, 2] = tempR[2, 0]; R[2, 0] = tempR[0, 0];
            R[0, 1] = tempR[1, 2]; R[1, 2] = tempR[2, 1]; R[2, 1] = tempR[1, 0]; R[1, 0] = tempR[0, 1];
            Globals.rotList.Add("RA");
        }
        else if (rot == "TC")
        {
            L[0, 0] = tempF[0, 0]; L[0, 1] = tempF[0, 1]; L[0, 2] = tempF[0, 2];
            B[0, 0] = tempL[0, 0]; B[0, 1] = tempL[0, 1]; B[0, 2] = tempL[0, 2];
            R[0, 0] = tempB[0, 0]; R[0, 1] = tempB[0, 1]; R[0, 2] = tempB[0, 2];
            F[0, 0] = tempR[0, 0]; F[0, 1] = tempR[0, 1]; F[0, 2] = tempR[0, 2];
            U[0, 0] = tempU[2, 0]; U[0, 2] = tempU[0, 0]; U[2, 2] = tempU[0, 2]; U[2, 0] = tempU[2, 2];
            U[0, 1] = tempU[1, 0]; U[1, 2] = tempU[0, 1]; U[2, 1] = tempU[1, 2]; U[1, 0] = tempU[2, 1];
            Globals.rotList.Add("TC");
        }
        else if (rot == "TA")
        {
            L[0, 0] = tempB[0, 0]; L[0, 1] = tempB[0, 1]; L[0, 2] = tempB[0, 2];
            B[0, 0] = tempR[0, 0]; B[0, 1] = tempR[0, 1]; B[0, 2] = tempR[0, 2];
            R[0, 0] = tempF[0, 0]; R[0, 1] = tempF[0, 1]; R[0, 2] = tempF[0, 2];
            F[0, 0] = tempL[0, 0]; F[0, 1] = tempL[0, 1]; F[0, 2] = tempL[0, 2];
            U[0, 0] = tempU[0, 2]; U[0, 2] = tempU[2, 2]; U[2, 2] = tempU[2, 0]; U[2, 0] = tempU[0, 0];
            U[0, 1] = tempU[1, 2]; U[1, 2] = tempU[2, 1]; U[2, 1] = tempU[1, 0]; U[1, 0] = tempU[0, 1];
            Globals.rotList.Add("TA");
        }
        else if (rot == "DC")
        {
            L[2, 0] = tempB[2, 0]; L[2, 1] = tempB[2, 1]; L[2, 2] = tempB[2, 2];
            B[2, 0] = tempR[2, 0]; B[2, 1] = tempR[2, 1]; B[2, 2] = tempR[2, 2];
            R[2, 0] = tempF[2, 0]; R[2, 1] = tempF[2, 1]; R[2, 2] = tempF[2, 2];
            F[2, 0] = tempL[2, 0]; F[2, 1] = tempL[2, 1]; F[2, 2] = tempL[2, 2];
            D[0, 0] = tempD[2, 0]; D[0, 2] = tempD[0, 0]; D[2, 2] = tempD[0, 2]; D[2, 0] = tempD[2, 2];
            D[0, 1] = tempD[1, 0]; D[1, 2] = tempD[0, 1]; D[2, 1] = tempD[1, 2]; D[1, 0] = tempD[2, 1];
            Globals.rotList.Add("DC");
        }
        else if (rot == "DA")
        {
            L[2, 0] = tempF[2, 0]; L[2, 1] = tempF[2, 1]; L[2, 2] = tempF[2, 2];
            B[2, 0] = tempL[2, 0]; B[2, 1] = tempL[2, 1]; B[2, 2] = tempL[2, 2];
            R[2, 0] = tempB[2, 0]; R[2, 1] = tempB[2, 1]; R[2, 2] = tempB[2, 2];
            F[2, 0] = tempR[2, 0]; F[2, 1] = tempR[2, 1]; F[2, 2] = tempR[2, 2];
            D[0, 0] = tempD[0, 2]; D[0, 2] = tempD[2, 2]; D[2, 2] = tempD[2, 0]; D[2, 0] = tempD[0, 0];
            D[0, 1] = tempD[1, 2]; D[1, 2] = tempD[2, 1]; D[2, 1] = tempD[1, 0]; D[1, 0] = tempD[0, 1];
            Globals.rotList.Add("DA");
        }
        else if (rot == "BA")
        {
            L[0, 0] = tempD[2, 0]; L[1, 0] = tempD[2, 1]; L[2, 0] = tempD[2, 2];
            U[0, 0] = tempL[2, 0]; U[0, 1] = tempL[1, 0]; U[0, 2] = tempL[0, 0];
            R[0, 2] = tempU[0, 0]; R[1, 2] = tempU[0, 1]; R[2, 2] = tempU[0, 2];
            D[2, 2] = tempR[0, 2]; D[2, 1] = tempR[1, 2]; D[2, 0] = tempR[2, 2];
            B[0, 0] = tempB[0, 2]; B[0, 2] = tempB[2, 2]; B[2, 2] = tempB[2, 0]; B[2, 0] = tempB[0, 0];
            B[0, 1] = tempB[1, 2]; B[1, 2] = tempB[2, 1]; B[2, 1] = tempB[1, 0]; B[1, 0] = tempB[0, 1];
            Globals.rotList.Add("BA");
        }
        else if (rot == "BC")
        {
            L[0, 0] = tempU[0, 2]; L[1, 0] = tempU[0, 1]; L[2, 0] = tempU[0, 0];
            U[0, 0] = tempR[0, 2]; U[0, 1] = tempR[1, 2]; U[0, 2] = tempR[2, 2];
            R[0, 2] = tempD[2, 2]; R[1, 2] = tempD[2, 1]; R[2, 2] = tempD[2, 0];
            D[2, 2] = tempL[2, 0]; D[2, 1] = tempL[1, 0]; D[2, 0] = tempL[0, 0];
            B[0, 0] = tempB[2, 0]; B[0, 2] = tempB[0, 0]; B[2, 2] = tempB[0, 2]; B[2, 0] = tempB[2, 2];
            B[0, 1] = tempB[1, 0]; B[1, 2] = tempB[0, 1]; B[2, 1] = tempB[1, 2]; B[1, 0] = tempB[2, 1];
            Globals.rotList.Add("BC");
        }
        else if (rot == "FC")
        {
            L[0, 2] = tempD[0, 0]; L[1, 2] = tempD[0, 1]; L[2, 2] = tempD[0, 2];
            U[2, 0] = tempL[2, 2]; U[2, 1] = tempL[1, 2]; U[2, 2] = tempL[0, 2];
            R[0, 0] = tempU[2, 0]; R[1, 0] = tempU[2, 1]; R[2, 0] = tempU[2, 2];
            D[0, 2] = tempR[0, 0]; D[0, 1] = tempR[1, 0]; D[0, 0] = tempR[2, 0];
            F[0, 0] = tempF[2, 0]; F[0, 2] = tempF[0, 0]; F[2, 2] = tempF[0, 2]; F[2, 0] = tempF[2, 2];
            F[0, 1] = tempF[1, 0]; F[1, 2] = tempF[0, 1]; F[2, 1] = tempF[1, 2]; F[1, 0] = tempF[2, 1];
            Globals.rotList.Add("FC");
        }
        else if (rot == "FA")
        {
            L[0, 2] = tempU[2, 2]; L[1, 2] = tempU[2, 1]; L[2, 2] = tempU[2, 0];
            U[2, 0] = tempR[0, 0]; U[2, 1] = tempR[1, 0]; U[2, 2] = tempR[2, 0];
            R[0, 0] = tempD[0, 2]; R[1, 0] = tempD[0, 1]; R[2, 0] = tempD[0, 0];
            D[0, 2] = tempL[2, 2]; D[0, 1] = tempL[1, 2]; D[0, 0] = tempL[0, 2];
            F[0, 0] = tempF[0, 2]; F[0, 2] = tempF[2, 2]; F[2, 2] = tempF[2, 0]; F[2, 0] = tempF[0, 0];
            F[0, 1] = tempF[1, 2]; F[1, 2] = tempF[2, 1]; F[2, 1] = tempF[1, 0]; F[1, 0] = tempF[0, 1];
            Globals.rotList.Add("FA");
        }
    }


    private static void SolveCross(ref char[,] F, ref char[,] B, ref char[,] U, ref char[,] D, ref char[,] L, ref char[,] R)
    {
        if (B[0, 1] == 'O')
        {
            if (U[0, 1] == 'Y')
            {
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "TC");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "TC");
                Globals.numberOfRots = Globals.numberOfRots + 1;
                return;
            }
            else if (U[0, 1] == 'G')
            {
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "LC");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "LC");
                Globals.numberOfRots = Globals.numberOfRots + 2;
                return;
            }
            else if (U[0, 1] == 'B')
            {
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BA");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "RA");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "RA");
                Globals.numberOfRots = Globals.numberOfRots + 2;
                return;
            }
            else if (U[0, 1] == 'W')
            {
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "DC");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "DC");
                Globals.numberOfRots = Globals.numberOfRots + 2;
                return;
            }
        }
        if (B[1, 0] == 'O')
        {
            if (R[1, 2] == 'B')
            {
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "RA");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "RA");
                Globals.numberOfRots = Globals.numberOfRots + 1;
                return;
            }
            else if (R[1, 2] == 'Y')
            {
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "TC");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "TC");
                Globals.numberOfRots = Globals.numberOfRots + 2;
                return;
            }
            else if (R[1, 2] == 'W')
            {
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BA");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "DC");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "DC");
                Globals.numberOfRots = Globals.numberOfRots + 2;
                return;
            }
            else if (R[1, 2] == 'G')
            {
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "LC");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "LC");
                Globals.numberOfRots = Globals.numberOfRots + 2;
                return;
            }
        }
        if (B[2, 1] == 'O')
        {
            if (D[2, 1] == 'W')
            {
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "DC");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "DC");
                Globals.numberOfRots = Globals.numberOfRots + 1;
                return;
            }
            else if (D[2, 1] == 'G')
            {
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BA");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "LC");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "LC");
                Globals.numberOfRots = Globals.numberOfRots + 2;
                return;
            }
            else if (D[2, 1] == 'B')
            {
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "RC");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "RC");
                Globals.numberOfRots = Globals.numberOfRots + 2;
                return;
            }
            else if (D[2, 1] == 'Y')
            {
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "TC");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "TC");
                Globals.numberOfRots = Globals.numberOfRots + 2;
                return;
            }
        }
        if (B[1, 2] == 'O')
        {
            if (L[1, 0] == 'G')
            {
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "LC");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "LC");
                Globals.numberOfRots = Globals.numberOfRots + 1;
                return;
            }
            else if (L[1, 0] == 'Y')
            {
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BA");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "TC");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "TC");
                Globals.numberOfRots = Globals.numberOfRots + 2;
                return;
            }
            else if (L[1, 0] == 'W')
            {
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "DC");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "DC");
                Globals.numberOfRots = Globals.numberOfRots + 2;
                return;
            }
            else if (L[1, 0] == 'B')
            {
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "RC");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "RC");
                Globals.numberOfRots = Globals.numberOfRots + 2;
                return;
            }
        }
        if (D[1, 2] == 'O')
        {
            if (R[2, 1] == 'B')
            {
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "RC");
                Globals.numberOfRots = Globals.numberOfRots + 1;
                return;
            }
            else { rotation(ref F, ref B, ref U, ref D, ref L, ref R, "RA"); Globals.numberOfRots = Globals.numberOfRots + 1; return; }
        }
        if (D[1, 0] == 'O')
        {
            if (L[2, 1] == 'G')
            {
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "LA");
                Globals.numberOfRots = Globals.numberOfRots + 1;
                return;
            }
            else { rotation(ref F, ref B, ref U, ref D, ref L, ref R, "LC"); Globals.numberOfRots = Globals.numberOfRots + 1; return; }
        }
        if (L[2, 1] == 'O')
        {
            if (D[1, 0] == 'W')
            {
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "DC");
                Globals.numberOfRots = Globals.numberOfRots + 1;
                return;
            }
            else { rotation(ref F, ref B, ref U, ref D, ref L, ref R, "DA"); Globals.numberOfRots = Globals.numberOfRots + 1; return; }
        }
        if (L[0, 1] == 'O')
        {
            if (U[1, 0] == 'Y')
            {
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "TA");
                Globals.numberOfRots = Globals.numberOfRots + 1;
                return;
            }
            else { rotation(ref F, ref B, ref U, ref D, ref L, ref R, "TC"); Globals.numberOfRots = Globals.numberOfRots + 1; return; }
        }
        if (U[1, 0] == 'O')
        {
            if (L[0, 1] == 'G')
            {
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "LC");
                Globals.numberOfRots = Globals.numberOfRots + 1;
                return;
            }
            else { rotation(ref F, ref B, ref U, ref D, ref L, ref R, "LA"); Globals.numberOfRots = Globals.numberOfRots + 1; return; }
        }
        if (U[1, 2] == 'O')
        {
            if (R[0, 1] == 'B')
            {
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "RA");
                Globals.numberOfRots = Globals.numberOfRots + 1;
                return;
            }
            else { rotation(ref F, ref B, ref U, ref D, ref L, ref R, "RC"); Globals.numberOfRots = Globals.numberOfRots + 1; return; }
        }
        if (R[0, 1] == 'O')
        {
            if (U[1, 2] == 'Y')
            {
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "TC");
                Globals.numberOfRots = Globals.numberOfRots + 1;
                return;
            }
            else { rotation(ref F, ref B, ref U, ref D, ref L, ref R, "TA"); Globals.numberOfRots = Globals.numberOfRots + 1; return; }
        }
        if (R[2, 1] == 'O')
        {
            if (D[1, 2] == 'W')
            {
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "DA");
                Globals.numberOfRots = Globals.numberOfRots + 1;
                return;
            }
            else { rotation(ref F, ref B, ref U, ref D, ref L, ref R, "DC"); Globals.numberOfRots = Globals.numberOfRots + 1; return; }
        }
        if (R[1, 2] == 'O')
        {
            if (B[1, 0] == 'B')
            {
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "TC");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "RA");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "TA");
                Globals.numberOfRots = Globals.numberOfRots + 4;
                return;
            }
            else if (B[1, 0] == 'Y')
            {
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "RA");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "TC");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "RC");
                Globals.numberOfRots = Globals.numberOfRots + 3;
                return;
            }
            else if (B[1, 0] == 'W')
            {
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "RC");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "DA");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "RA");
                Globals.numberOfRots = Globals.numberOfRots + 3;
                return;
            }
            else if (B[1, 0] == 'G')
            {
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "TA");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "LC");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "TC");
                Globals.numberOfRots = Globals.numberOfRots + 3;
                return;
            }
        }
        if (U[0, 1] == 'O')
        {
            if (B[0, 1] == 'Y')
            {
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "LC");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "TA");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "LA");
                Globals.numberOfRots = Globals.numberOfRots + 4;
                return;
            }
            else if (B[0, 1] == 'G')
            {
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "TA");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "LC");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "TC");
                Globals.numberOfRots = Globals.numberOfRots + 3;
                return;
            }
            else if (B[0, 1] == 'B')
            {
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "TC");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "RA");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "TA");
                Globals.numberOfRots = Globals.numberOfRots + 3;
                return;
            }
            else if (B[0, 1] == 'W')
            {
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "LA");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "DC");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "LC");
                Globals.numberOfRots = Globals.numberOfRots + 4;
                return;
            }
        }
        if (L[1, 0] == 'O')
        {
            if (B[1, 2] == 'G')
            {
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "DC");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "LA");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "DA");
                Globals.numberOfRots = Globals.numberOfRots + 4;
                return;
            }
            else if (B[1, 2] == 'W')
            {
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "LA");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "DC");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "LC");
                Globals.numberOfRots = Globals.numberOfRots + 3;
                return;
            }
            else if (B[1, 2] == 'Y')
            {
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "LC");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "TA");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "LA");
                Globals.numberOfRots = Globals.numberOfRots + 3;
                return;
            }
            else if (B[1, 2] == 'B')
            {
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "DA");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "RC");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "DC");
                Globals.numberOfRots = Globals.numberOfRots + 4;
                return;
            }
        }
        if (D[2, 1] == 'O')
        {
            if (B[2, 1] == 'W')
            {
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "RC");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "DA");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "RA");
                Globals.numberOfRots = Globals.numberOfRots + 4;
                return;
            }
            else if (B[2, 1] == 'B')
            {
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "DA");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "RC");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "DC");
                Globals.numberOfRots = Globals.numberOfRots + 3;
                return;
            }
            else if (B[2, 1] == 'G')
            {
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "DC");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "LA");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "DA");
                Globals.numberOfRots = Globals.numberOfRots + 3;
                return;
            }
            else if (B[2, 1] == 'Y')
            {
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "RA");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "TC");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "RC");
                Globals.numberOfRots = Globals.numberOfRots + 4;
                return;
            }
        }
        if (F[0, 1] == 'O' & U[2, 1] != 'Y')
        {
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "TC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "TC");
            Globals.numberOfRots = Globals.numberOfRots + 1;
            return;
        }
        if (F[1, 2] == 'O' & R[1, 0] != 'B')
        {
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "RC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "RC");
            Globals.numberOfRots = Globals.numberOfRots + 1;
            return;
        }
        if (F[2, 1] == 'O' & D[0, 1] != 'W')
        {
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "DC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "DC");
            Globals.numberOfRots = Globals.numberOfRots + 1;
            return;
        }
        if (F[1, 0] == 'O' & L[1, 2] != 'G')
        {
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "LC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "LC");
            Globals.numberOfRots = Globals.numberOfRots + 1;
            return;
        }
        if (D[0, 1] == 'O')
        {
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "DC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "DC");
        }
        if (L[1, 2] == 'O')
        {
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "LC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "LC");
        }
        if (U[0, 1] == 'O')
        {
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "TC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "TC");
        }
        if (R[1, 0] == 'O')
        {
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "RC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "RC");
        }
    }

    private static void SolveCorners(ref char[,] F, ref char[,] B, ref char[,] U, ref char[,] D, ref char[,] L, ref char[,] R)
    {
        if (L[2, 0] == 'O')
        {
            if (B[2, 2] == 'G' & D[2, 0] == 'W')
            {
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BA");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "DA");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "DC");
                Globals.numberOfRots = Globals.numberOfRots + 4;
                return;
            }
            else if (B[2, 2] == 'Y' & D[2, 0] == 'G')
            {
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BA");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BA");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "LA");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "LC");
                Globals.numberOfRots = Globals.numberOfRots + 4;
                return;
            }
            else if (B[2, 2] == 'W' & D[2, 0] == 'B')
            {
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "RA");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "RC");
                Globals.numberOfRots = Globals.numberOfRots + 3;
                return;
            }
            else if (B[2, 2] == 'B' & D[2, 0] == 'Y')
            {
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "TA");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "TC");
                Globals.numberOfRots = Globals.numberOfRots + 4;
                return;
            }
        }
        if (L[0, 0] == 'O')
        {
            if (B[0, 2] == 'G' & U[0, 0] == 'Y')
            {
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "TC");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BA");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "TA");
                Globals.numberOfRots = Globals.numberOfRots + 4;
                return;
            }
            else if (B[0, 2] == 'Y' & U[0, 0] == 'B')
            {
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "RC");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BA");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "RA");
                Globals.numberOfRots = Globals.numberOfRots + 3;
                return;
            }
            else if (B[0, 2] == 'B' & U[0, 0] == 'W')
            {
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BA");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "DC");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BA");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "DA");
                Globals.numberOfRots = Globals.numberOfRots + 4;
                return;
            }
            else if (B[0, 2] == 'W' & U[0, 0] == 'G')
            {
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "LC");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BA");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "LA");
                Globals.numberOfRots = Globals.numberOfRots + 4;
                return;
            }
        }
        if (D[2, 0] == 'O')
        {
            if (L[2, 0] == 'G')
            {
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "LC");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BA");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "LA");
                Globals.numberOfRots = Globals.numberOfRots + 4;
                return;
            }
            else if (L[2, 0] == 'W')
            {
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "DC");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BA");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "DA");
                Globals.numberOfRots = Globals.numberOfRots + 4;
                return;
            }
            else if (L[2, 0] == 'B')
            {
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BA");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "RC");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BA");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "RA");
                Globals.numberOfRots = Globals.numberOfRots + 4;
                return;
            }
            else
            {
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "TC");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BA");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "TA");
                Globals.numberOfRots = Globals.numberOfRots + 3;
                return;
            }
        }
        if (U[0, 2] == 'O')
        {
            if (R[0, 2] == 'B')
            {
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "RC");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BA");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "RA");
                Globals.numberOfRots = Globals.numberOfRots + 4;
                return;
            }
            else if (R[0, 2] == 'Y')
            {
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "TC");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BA");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "TA");
                Globals.numberOfRots = Globals.numberOfRots + 4;
                return;
            }
            else if (R[0, 2] == 'G')
            {
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BA");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "LC");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BA");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "LA");
                Globals.numberOfRots = Globals.numberOfRots + 4;
                return;
            }
            else
            {
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "DC");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BA");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "DA");
                Globals.numberOfRots = Globals.numberOfRots + 3;
                return;
            }
        }
        if (R[2, 2] == 'O')
        {
            if (D[2, 2] == 'W')
            {
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "DC");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BA");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "DA");
                Globals.numberOfRots = Globals.numberOfRots + 4;
                return;
            }
            else if (D[2, 2] == 'B')
            {
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "RC");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BA");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "RA");
                Globals.numberOfRots = Globals.numberOfRots + 4;
                return;
            }
            else if (D[2, 2] == 'Y')
            {
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BA");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "TC");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BA");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "TA");
                Globals.numberOfRots = Globals.numberOfRots + 4;
                return;
            }
            else
            {
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "LC");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BA");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "LA");
                Globals.numberOfRots = Globals.numberOfRots + 3;
                return;
            }
        }
        if (L[2, 0] == 'O')
        {
            if (D[2, 0] == 'W')
            {
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BA");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "DA");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "DC");
                Globals.numberOfRots = Globals.numberOfRots + 4;
                return;
            }
            else if (D[2, 0] == 'G')
            {
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BA");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BA");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "LA");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "LC");
                Globals.numberOfRots = Globals.numberOfRots + 4;
                return;
            }
            else if (D[2, 0] == 'Y')
            {
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "TA");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "TC");
                Globals.numberOfRots = Globals.numberOfRots + 4;
                return;
            }
            else
            {
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "RA");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "RC");
                Globals.numberOfRots = Globals.numberOfRots + 3;
                return;
            }
        }
        if (R[0, 2] == 'O')
        {
            if (U[0, 2] == 'Y')
            {
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BA");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "TA");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "TC");
                Globals.numberOfRots = Globals.numberOfRots + 4;
                return;
            }
            else if (U[0, 2] == 'B')
            {
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BA");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BA");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "RA");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "RC");
                Globals.numberOfRots = Globals.numberOfRots + 4;
                return;
            }
            else if (U[0, 2] == 'W')
            {
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "DA");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "DC");
                Globals.numberOfRots = Globals.numberOfRots + 4;
                return;
            }
            else
            {
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "LA");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "LC");
                Globals.numberOfRots = Globals.numberOfRots + 3;
                return;
                //UP TO HERE
            }
        }
        if (D[2, 2] == 'O')
        {
            if (R[2, 2] == 'B')
            {
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BA");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "RA");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "RC");
                Globals.numberOfRots = Globals.numberOfRots + 4;
                return;
            }
            else if (R[2, 2] == 'W')
            {
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BA");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BA");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "DA");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "DC");
                Globals.numberOfRots = Globals.numberOfRots + 4;
                return;
            }
            else if (R[2, 2] == 'G')
            {
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "LA");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "LC");
                Globals.numberOfRots = Globals.numberOfRots + 4;
                return;
            }
            else
            {
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "TA");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "TC");
                Globals.numberOfRots = Globals.numberOfRots + 3;
                return;
            }
        }
        if (U[0, 0] == 'O')
        {
            if (L[0, 0] == 'G')
            {
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BA");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "LA");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "LC");
                Globals.numberOfRots = Globals.numberOfRots + 4;
                return;
            }
            else if (L[0, 0] == 'Y')
            {
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BA");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BA");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "TA");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "TC");
                Globals.numberOfRots = Globals.numberOfRots + 4;
                return;
            }
            else if (L[0, 0] == 'B')
            {
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "RA");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "RC");
                Globals.numberOfRots = Globals.numberOfRots + 4;
                return;
            }
            else
            {
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "DA");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "DC");
                Globals.numberOfRots = Globals.numberOfRots + 3;
                return;
            }
        }
        if (B[0, 2] == 'O')
        {
            if (F[0, 0] != 'O')
            {
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "LA");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "LC");
                Globals.numberOfRots = Globals.numberOfRots + 3;
                return;
            }
            else if (F[2, 0] != 'O')
            {
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "DA");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "DC");
                Globals.numberOfRots = Globals.numberOfRots + 4;
                return;
            }
            else if (F[0, 2] != 'O')
            {
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BA");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "TA");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "TC");
                Globals.numberOfRots = Globals.numberOfRots + 4;
                return;
            }
            else
            {
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "RA");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "RC");
                Globals.numberOfRots = Globals.numberOfRots + 4;
                return;
            }
        }
        if (B[0, 0] == 'O')
        {
            if (F[0, 2] != 'O')
            {
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "TA");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "TC");
                Globals.numberOfRots = Globals.numberOfRots + 3;
                return;
            }
            else if (F[0, 2] != 'O')
            {
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "LA");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "LC");
                Globals.numberOfRots = Globals.numberOfRots + 4;
                return;
            }
            else if (F[0, 2] != 'O')
            {
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BA");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "RA");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "RC");
                Globals.numberOfRots = Globals.numberOfRots + 4;
                return;
            }
            else
            {
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "DA");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "DC");
                Globals.numberOfRots = Globals.numberOfRots + 4;
                return;
            }
        }
        if (B[2, 2] == 'O')
        {
            if (F[2, 0] != 'O')
            {
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "DA");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "DC");
                Globals.numberOfRots = Globals.numberOfRots + 3;
                return;
            }
            else if (F[2, 2] != 'O')
            {
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "RA");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "RC");
                Globals.numberOfRots = Globals.numberOfRots + 4;
                return;
            }
            else if (F[0, 0] != 'O')
            {
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BA");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "LA");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "LC");
                Globals.numberOfRots = Globals.numberOfRots + 4;
                return;
            }
            else
            {
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "TA");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "TC");
                Globals.numberOfRots = Globals.numberOfRots + 4;
                return;
            }
        }
        if (B[2, 0] == 'O')
        {
            if (F[2, 2] != 'O')
            {
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "RA");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "RC");
                Globals.numberOfRots = Globals.numberOfRots + 3;
                return;
            }
            else if (F[0, 2] != 'O')
            {
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "TA");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "TC");
                Globals.numberOfRots = Globals.numberOfRots + 4;
                return;
            }
            else if (F[2, 0] != 'O')
            {
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BA");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "DA");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "DC");
                Globals.numberOfRots = Globals.numberOfRots + 4;
                return;
            }
            else
            {
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "LA");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "LC");
                Globals.numberOfRots = Globals.numberOfRots + 4;
                return;
            }
        }
        if (L[0, 2] == 'O')
        {
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "LA");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BA");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "LC");
            Globals.numberOfRots = Globals.numberOfRots + 3;
            return;
        }
        if (L[2, 2] == 'O')
        {
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "LC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "LA");
            Globals.numberOfRots = Globals.numberOfRots + 3;
            return;
        }
        if (D[0, 0] == 'O')
        {
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "DA");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BA");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "DC");
            Globals.numberOfRots = Globals.numberOfRots + 3;
            return;
        }
        if (D[0, 2] == 'O')
        {
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "DC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "DA");
            Globals.numberOfRots = Globals.numberOfRots + 3;
            return;
        }
        if (R[2, 0] == 'O')
        {
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "RA");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BA");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "RC");
            Globals.numberOfRots = Globals.numberOfRots + 3;
            return;
        }
        if (R[0, 0] == 'O')
        {
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "RC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "RA");
            Globals.numberOfRots = Globals.numberOfRots + 3;
            return;
        }
        if (U[2, 2] == 'O')
        {
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "TA");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BA");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "TC");
            Globals.numberOfRots = Globals.numberOfRots + 3;
            return;
        }
        if (U[2, 0] == 'O')
        {
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "TC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "TA");
            Globals.numberOfRots = Globals.numberOfRots + 3;
            return;
        }
        if (F[0, 0] == 'O' & U[2, 0] != 'Y')
        {
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "LA");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BA");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "LC");
            Globals.numberOfRots = Globals.numberOfRots + 3;
            return;
        }
        if (F[0, 2] == 'O' & U[2, 2] != 'Y')
        {
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "RC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "RA");
            Globals.numberOfRots = Globals.numberOfRots + 3;
            return;
        }
        if (F[2, 0] == 'O' & D[0, 0] != 'W')
        {
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "LC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "LA");
            Globals.numberOfRots = Globals.numberOfRots + 3;
            return;
        }
        if (F[2, 2] == 'O' & D[0, 2] != 'W')
        {
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "RA");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BA");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "RC");
            Globals.numberOfRots = Globals.numberOfRots + 3;
            return;
        }
    }

    public static void SolveMiddle(ref char[,] F, ref char[,] B, ref char[,] U, ref char[,] D, ref char[,] L, ref char[,] R)
    {
        if (U[0, 1] == 'G' & B[0, 1] == 'Y')
        {
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "TC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BA");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "TA");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BA");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "LA");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "LC");
            Globals.numberOfRots = Globals.numberOfRots + 8;
            return;
        }
        if (R[1, 2] == 'G' & B[1, 0] == 'Y')
        {
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BA");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "TC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BA");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "TA");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BA");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "LA");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "LC");
            Globals.numberOfRots = Globals.numberOfRots + 8;
            return;
        }
        if (D[2, 1] == 'G' & B[2, 1] == 'Y')
        {
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "TC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BA");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "TA");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BA");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "LA");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "LC");
            Globals.numberOfRots = Globals.numberOfRots + 7;
            return;
        }
        if (L[1, 0] == 'G' & B[1, 2] == 'Y')
        {
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "TC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BA");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "TA");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BA");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "LA");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "LC");
            Globals.numberOfRots = Globals.numberOfRots + 8;
            return;
        }
        if (U[0, 1] == 'Y' & B[0, 1] == 'G')
        {
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BA");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "LA");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "LC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "TC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BA");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "TA");
            Globals.numberOfRots = Globals.numberOfRots + 8;
            return;
        }
        if (R[1, 2] == 'Y' & B[0, 1] == 'G')
        {
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "LA");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "LC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "TC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BA");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "TA");
            Globals.numberOfRots = Globals.numberOfRots + 7;
            return;
        }
        if (D[2, 1] == 'Y' & B[2, 1] == 'G')
        {
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "LA");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "LC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "TC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BA");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "TA");
            Globals.numberOfRots = Globals.numberOfRots + 8;
            return;
        }
        if (L[1, 0] == 'Y' & B[1, 2] == 'G')
        {
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BA");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BA");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "LA");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "LC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "TC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BA");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "TA");
            Globals.numberOfRots = Globals.numberOfRots + 8;
            return;
        }
        if (U[0, 1] == 'G' & B[0, 1] == 'W')
        {
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "DA");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "DC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "LC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BA");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "LA");
            Globals.numberOfRots = Globals.numberOfRots + 7;
            return;
        }
        if (R[1, 2] == 'G' & B[0, 1] == 'W')
        {
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "DA");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "DC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "LC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BA");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "LA");
            Globals.numberOfRots = Globals.numberOfRots + 8;
            return;
        }
        if (D[1, 2] == 'G' & B[1, 0] == 'W')
        {
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "DA");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "DC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "LC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BA");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "LA");
            Globals.numberOfRots = Globals.numberOfRots + 8;
        }
        if (L[1, 0] == 'G' & B[1, 2] == 'W')
        {
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BA");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "DA");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "DC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "LC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BA");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "LA");
            Globals.numberOfRots = Globals.numberOfRots + 8;
            return;
        }
        if (U[0, 1] == 'W' & B[0, 1] == 'G')
        {
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BA");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "LC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BA");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "LA");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BA");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "DA");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "DC");
            Globals.numberOfRots = Globals.numberOfRots + 8;
            return;
        }
        if (R[1, 2] == 'W' & B[1, 0] == 'G')
        {
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "LC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BA");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "LA");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BA");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "DA");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "DC");
            Globals.numberOfRots = Globals.numberOfRots + 7;
            return;
        }
        if (D[2, 1] == 'W' & B[2, 1] == 'G')
        {
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "LC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BA");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "LA");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BA");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "DA");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "DC");
            Globals.numberOfRots = Globals.numberOfRots + 8;
            return;
        }
        if (L[1, 0] == 'W' & B[1, 2] == 'G')
        {
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "LC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BA");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "LA");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BA");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "DA");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "DC");
            Globals.numberOfRots = Globals.numberOfRots + 8;
            return;
        }
        if (U[0, 1] == 'B' & B[0, 1] == 'W')
        {
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "DC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BA");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "DA");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BA");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "RA");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "RC");
            Globals.numberOfRots = Globals.numberOfRots + 7;
            return;
        }
        if (R[1, 2] == 'B' & B[1, 0] == 'W')
        {
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "DC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BA");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "DA");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BA");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "RA");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "RC");
            Globals.numberOfRots = Globals.numberOfRots + 8;
            return;
        }
        if (D[2, 1] == 'B' & B[2, 1] == 'W')
        {
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "DC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BA");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "DA");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BA");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "RA");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "RC");
            Globals.numberOfRots = Globals.numberOfRots + 8;
            return;
        }
        if (L[1, 0] == 'B' & B[1, 2] == 'W')
        {
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BA");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "DC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BA");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "DA");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BA");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "RA");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "RC");
            Globals.numberOfRots = Globals.numberOfRots + 8;
            return;
        }
        if (L[1, 0] == 'W' & B[1, 2] == 'B')
        {
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "RA");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "RC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "DC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BA");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "DA");
            Globals.numberOfRots = Globals.numberOfRots + 7;
            return;
        }
        if (U[0, 1] == 'W' & B[0, 1] == 'B')
        {
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "RA");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "RC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "DC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BA");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "DA");
            Globals.numberOfRots = Globals.numberOfRots + 8;
            return;
        }
        if (R[1, 2] == 'W' & B[1, 0] == 'B')
        {
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "RA");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "RC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "DC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BA");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "DA");
            Globals.numberOfRots = Globals.numberOfRots + 8;
            return;
        }
        if (D[2, 1] == 'W' & B[2, 1] == 'B')
        {
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BA");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "RA");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "RC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "DC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BA");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "DA");
            Globals.numberOfRots = Globals.numberOfRots + 8;
            return;
        }
        if (D[2, 1] == 'B' & B[2, 1] == 'Y')
        {
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "TA");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "TC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "RC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BA");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "RA");
            Globals.numberOfRots = Globals.numberOfRots + 7;
            return;
        }
        if (L[1, 0] == 'B' & B[1, 2] == 'Y')
        {
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "TA");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "TC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "RC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BA");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "RA");
            Globals.numberOfRots = Globals.numberOfRots + 8;
            return;
        }
        if (U[0, 1] == 'B' & B[0, 1] == 'Y')
        {
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "TA");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "TC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "RC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BA");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "RA");
            Globals.numberOfRots = Globals.numberOfRots + 8;
            return;
        }
        if (R[1, 2] == 'B' & B[1, 0] == 'Y')
        {
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BA");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "TA");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "TC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "RC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BA");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "RA");
            Globals.numberOfRots = Globals.numberOfRots + 8;
            return;
        }
        if (L[1, 0] == 'Y' & B[1, 2] == 'B')
        {
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "RC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BA");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "RA");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BA");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "TA");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "TC");
            Globals.numberOfRots = Globals.numberOfRots + 7;
            return;
        }
        if (U[0, 1] == 'Y' & B[0, 1] == 'B')
        {
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "RC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BA");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "RA");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BA");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "TA");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "TC");
            Globals.numberOfRots = Globals.numberOfRots + 7;
            return;
        }
        if (R[1, 2] == 'Y' & B[1, 0] == 'B')
        {
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "RC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BA");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "RA");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BA");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "TA");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "TC");
            Globals.numberOfRots = Globals.numberOfRots + 8;
            return;
        }
        if (D[2, 1] == 'Y' & B[2, 1] == 'B')
        {
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BA");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "RC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BA");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "RA");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BA");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "TA");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "TC");
            Globals.numberOfRots = Globals.numberOfRots + 8;
            return;
        }
        if (L[0, 1] == 'Y' & U[1, 0] == 'G')
        {
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "LA");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "LC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "TC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BA");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "TA");
            Globals.numberOfRots = Globals.numberOfRots + 7;
            return;
        }
        if (U[1, 2] == 'B' & R[0, 1] == 'Y')
        {
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "RC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BA");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "RA");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BA");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "TA");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "TC");
            Globals.numberOfRots = Globals.numberOfRots + 7;
            return;
        }
        if (R[2, 1] == 'W' & D[1, 2] == 'B')
        {
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "DC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BA");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "DA");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BA");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "RA");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "RC");
            Globals.numberOfRots = Globals.numberOfRots + 7;
            return;
        }
        if (D[1, 0] == 'G' & L[2, 1] == 'W')
        {
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "LC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BA");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "LA");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BA");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "DA");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "DC");
            Globals.numberOfRots = Globals.numberOfRots + 7;
            return;
        }
        if (U[1, 0] != 'Y' || L[0,1] != 'G')
        {
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "LA");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "LC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "TC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BA");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "TA");
            Globals.numberOfRots = Globals.numberOfRots + 7;
            return;
        }
        if (U[1,2] != 'Y' || R[0,1] != 'B')
        {
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "RC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BA");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "RA");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BA");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "TA");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "TC");
            Globals.numberOfRots = Globals.numberOfRots + 7;
        }
        if (D[1,0] != 'W' || L[2,1] != 'G')
        {
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "LC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BA");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "LA");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BA");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "DA");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "DC");
            Globals.numberOfRots = Globals.numberOfRots + 7;
        }
        if (D[1,2] != 'W' || R[2,1] != 'B')
        {
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "DC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BA");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "DA");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BA");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "RA");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "RC");
            Globals.numberOfRots = Globals.numberOfRots + 7;
        }
    }

    private static void SolveBackCross(ref char[,] F, ref char[,] B, ref char[,] U, ref char[,] D, ref char[,] L, ref char[,] R)
    {
        if (B[0, 1] == 'R' & B[2, 1] == 'R')
        {
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "RC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "DC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "DA");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BA");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "RA");
            Globals.numberOfRots = Globals.numberOfRots + 6;
        }
        else if (B[1, 2] == 'R' & B[1, 0] == 'R')
        {
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "DC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "LC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "LA");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BA");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "DA");
            Globals.numberOfRots = Globals.numberOfRots + 6;
        }
        else if (B[0, 1] == 'R')
        {
            if (B[1, 2] == 'R')
            {
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "RC");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "DC");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "DA");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BA");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "DC");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "DA");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BA");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "RA");
                Globals.numberOfRots = Globals.numberOfRots + 10;
            }
            if (B[1, 0] == 'R')
            {
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "DC");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "LC");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "LA");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BA");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "LC");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "LA");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BA");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "DA");
                Globals.numberOfRots = Globals.numberOfRots + 10;
            }
        }
        else if (B[2, 1] == 'R')
        {
            if (B[1, 0] == 'R')
            {
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "LC");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "TC");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "TA");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BA");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "TC");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "TA");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BA");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "LA");
                Globals.numberOfRots = Globals.numberOfRots + 10;
            }
            if (B[1, 2] == 'R')
            {
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "TC");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "RC");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "RA");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BA");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "RC");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "RA");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BA");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "TA");
                Globals.numberOfRots = Globals.numberOfRots + 10;
            }
        }
        else
        {
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "RC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "DC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "DA");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BA");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "RA");
            Globals.numberOfRots = Globals.numberOfRots + 6;
        }
    }

    private static void AllignBackCross(ref char[,] F, ref char[,] B, ref char[,] U, ref char[,] D, ref char[,] L, ref char[,] R)
    {
        if (U[0, 1] == 'Y' & R[1, 2] == 'B')
        {
            if (L[1, 0] == 'G')
            {
                return;
            }
            else
            {
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "TC");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "TA");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "TC");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "TA");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
                Globals.numberOfRots = Globals.numberOfRots + 8;
            }
        }
        else if (U[0, 1] == 'Y' & L[1, 0] == 'G')
        {
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "LC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "LA");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "LC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "LA");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
            Globals.numberOfRots = Globals.numberOfRots + 8;
        }
        else if (D[2, 1] == 'W' & R[1, 2] == 'B')
        {
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "RC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "RA");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "RC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "RA");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
            Globals.numberOfRots = Globals.numberOfRots + 8;
        }
        else if (D[2, 1] == 'W' & L[1, 0] == 'G')
        {
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "DC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "DA");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "DC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "DA");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
            Globals.numberOfRots = Globals.numberOfRots + 8;
        }
        else if (U[0, 1] == 'B' & R[1, 2] == 'W')
        {
            if (L[1, 0] == 'Y')
            {
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BA");
                Globals.numberOfRots = Globals.numberOfRots + 1;
            }
            else
            {
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BA");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "RC");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "RA");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "RC");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "RA");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
                Globals.numberOfRots = Globals.numberOfRots + 9;
            }
        }
        else if (U[0, 1] == 'B' & L[1, 0] == 'Y')
        {
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BA");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "TC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "TA");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "TC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "TA");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
            Globals.numberOfRots = Globals.numberOfRots + 9;
        }
        else if (D[2, 1] == 'G' & R[1, 2] == 'W')
        {
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BA");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "DC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "DA");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "DC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "DA");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
            Globals.numberOfRots = Globals.numberOfRots + 9;
        }
        else if (D[2, 1] == 'G' & L[1, 0] == 'Y')
        {
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BA");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "LC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "LA");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "LC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "LA");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
            Globals.numberOfRots = Globals.numberOfRots + 9;
        }
        else if (U[0, 1] == 'W' & R[1, 2] == 'G')
        {
            if (L[1, 0] == 'B')
            {
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BA");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BA");
                Globals.numberOfRots = Globals.numberOfRots + 1;
            }
            else
            {
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BA");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BA");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "DC");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "DA");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "DC");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "DA");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
                Globals.numberOfRots = Globals.numberOfRots + 9;
            }
        }
        else if (U[0, 1] == 'W' & L[1, 0] == 'B')
        {
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BA");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BA");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "RC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "RA");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "RC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "RA");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
            Globals.numberOfRots = Globals.numberOfRots + 9;
        }
        else if (D[2, 1] == 'Y' & R[1, 2] == 'G')
        {
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BA");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BA");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "LC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "LA");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "LC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "LA");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
            Globals.numberOfRots = Globals.numberOfRots + 9;
        }
        else if (D[2, 1] == 'Y' & L[1, 0] == 'B')
        {
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BA");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BA");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "TC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "TA");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "TC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "TA");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
            Globals.numberOfRots = Globals.numberOfRots + 9;
        }
        else if (U[0, 1] == 'G' & R[1, 2] == 'Y')
        {
            if (L[1, 0] == 'W')
            {
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
                Globals.numberOfRots = Globals.numberOfRots + 1;
            }
            else
            {
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "LC");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "LA");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "LC");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "LA");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
                Globals.numberOfRots = Globals.numberOfRots + 9;
            }
        }
        else if (U[0, 1] == 'G' & L[1, 0] == 'W')
        {
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "DC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "DA");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "DC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "DA");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
            Globals.numberOfRots = Globals.numberOfRots +9;
        }
        else if (D[2, 1] == 'B' & R[1, 2] == 'Y')
        {
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "TC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "TA");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "TC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "TA");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
            Globals.numberOfRots = Globals.numberOfRots + 9;
        }
        else if (D[2, 1] == 'B' & L[1, 0] == 'W')
        {
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "RC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "RA");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "RC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "RA");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
            Globals.numberOfRots = Globals.numberOfRots + 9;
        }
        else
        {
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "TC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "TA");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "TC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "TA");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
            Globals.numberOfRots = Globals.numberOfRots +8;
        }
    }

    private static bool CheckCornerPosition(ref char[,] F, ref char[,] B, ref char[,] U, ref char[,] D, ref char[,] L, ref char[,] R, string cor)
    {
        if (cor == "TL")
        {
            if ((U[0, 2] == 'Y' & R[0, 2] == 'B') | (U[0, 2] == 'B' & B[0, 0] == 'Y') | (B[0, 0] == 'B' & R[0, 2] == 'Y'))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        if (cor == "TR")
        {
            if ((U[0, 0] == 'Y' & L[0, 0] == 'G') | (U[0, 0] == 'G' & B[0, 2] == 'Y') | (B[0, 2] == 'G' & L[0, 0] == 'Y'))
            { return true; }
            else { return false; }
        }
        if (cor == "BL")
        {
            if ((R[2, 2] == 'B' & D[2, 2] == 'W') | (B[2, 0] == 'B' & R[2, 2] == 'W') | (D[2, 2] == 'B' & B[2, 0] == 'W'))
            {
                return true;
            }
            else { return false; }
        }
        if (cor == "BR")
        {
            if ((L[2, 0] == 'G' & D[2, 0] == 'W') | (B[2, 2] == 'G' & L[2, 0] == 'W') | (D[2, 0] == 'G' & B[2, 2] == 'W'))
            {
                return true;
            }
            else { return false; }
        }
        return false;
    }

    private static void SolveBackCorner(ref char[,] F, ref char[,] B, ref char[,] U, ref char[,] D, ref char[,] L, ref char[,] R)
    {
        bool topL = CheckCornerPosition(ref F, ref B, ref U, ref D, ref L, ref R, "TL");
        bool topR = CheckCornerPosition(ref F, ref B, ref U, ref D, ref L, ref R, "TR");
        bool botL = CheckCornerPosition(ref F, ref B, ref U, ref D, ref L, ref R, "BL");
        bool botR = CheckCornerPosition(ref F, ref B, ref U, ref D, ref L, ref R, "BR");
        if (topL == true & topR == true & botL & true & botR == true)
        {
            return;
        }
        else if (topL)
        {
            do
            {
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "RC");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BA");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "LA");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "RA");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BA");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "LC");
                Globals.numberOfRots = Globals.numberOfRots + 8;
            } while (!(CheckCornerPosition(ref F, ref B, ref U, ref D, ref L, ref R, "TL") & CheckCornerPosition(ref F, ref B, ref U, ref D, ref L, ref R, "TR") &
                    CheckCornerPosition(ref F, ref B, ref U, ref D, ref L, ref R, "BL") & CheckCornerPosition(ref F, ref B, ref U, ref D, ref L, ref R, "BR")));
            return;
        }
        else if (topR)
        {
            do
            {
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "TC");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BA");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "DA");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "TA");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BA");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "DC");
                Globals.numberOfRots = Globals.numberOfRots + 8;
            } while (!(CheckCornerPosition(ref F, ref B, ref U, ref D, ref L, ref R, "TL") & CheckCornerPosition(ref F, ref B, ref U, ref D, ref L, ref R, "TR") &
                    CheckCornerPosition(ref F, ref B, ref U, ref D, ref L, ref R, "BL") & CheckCornerPosition(ref F, ref B, ref U, ref D, ref L, ref R, "BR")));
            return;

        }
        else if (botR)
        {
            do
            {
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "LC");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BA");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "RA");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "LA");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BA");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "RC");
                Globals.numberOfRots = Globals.numberOfRots + 8;
            } while (!(CheckCornerPosition(ref F, ref B, ref U, ref D, ref L, ref R, "TL") & CheckCornerPosition(ref F, ref B, ref U, ref D, ref L, ref R, "TR") &
                    CheckCornerPosition(ref F, ref B, ref U, ref D, ref L, ref R, "BL") & CheckCornerPosition(ref F, ref B, ref U, ref D, ref L, ref R, "BR")));
            return;
        }
        else if (botL)
        {
            do
            {
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "DC");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BA");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "TA");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "DA");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BA");
                rotation(ref F, ref B, ref U, ref D, ref L, ref R, "TC");
                Globals.numberOfRots = Globals.numberOfRots + 8;
            } while (!(CheckCornerPosition(ref F, ref B, ref U, ref D, ref L, ref R, "TL") & CheckCornerPosition(ref F, ref B, ref U, ref D, ref L, ref R, "TR") &
                    CheckCornerPosition(ref F, ref B, ref U, ref D, ref L, ref R, "BL") & CheckCornerPosition(ref F, ref B, ref U, ref D, ref L, ref R, "BR")));
            return;
        }
        else
        {
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "TC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BA");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "DA");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "TA");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BA");
            rotation(ref F, ref B, ref U, ref D, ref L, ref R, "DC");
            Globals.numberOfRots = Globals.numberOfRots + 8;
            return;

        }
    }

    private static bool CheckCornerAllignment(ref char[,] F, ref char[,] B, ref char[,] U, ref char[,] D, ref char[,] L, ref char[,] R, string cor)
    {
        if (cor == "TL")
        {
            if (B[0, 0] == 'R') { return true; }
            else { return false; }
        }
        else if (cor == "TR")
        {
            if (B[0, 2] == 'R') { return true; }
            else { return false; }
        }
        else if (cor == "BL")
        {
            if (B[2, 0] == 'R') { return true; }
            else { return false; }
        }
        else if (cor == "BR")
        {
            if (B[2, 2] == 'R') { return true; }
            else { return false; }
        }
        return false;
    }

    private static void AllignCorners(ref char[,] F, ref char[,] B, ref char[,] U, ref char[,] D, ref char[,] L, ref char[,] R)
    {
        bool topL = CheckCornerAllignment(ref F, ref B, ref U, ref D, ref L, ref R, "TL");
        bool topR = CheckCornerAllignment(ref F, ref B, ref U, ref D, ref L, ref R, "TR");
        bool botL = CheckCornerAllignment(ref F, ref B, ref U, ref D, ref L, ref R, "BL");
        bool botR = CheckCornerAllignment(ref F, ref B, ref U, ref D, ref L, ref R, "BR");
        if (topL & topR & botL & botR) { return; }
        else if (topL == false)
        {
            for (int i = 1; i < 5; i++)
            {
                if (B[0, 0] != 'R')
                {
                    do
                    {
                        rotation(ref F, ref B, ref U, ref D, ref L, ref R, "RA");
                        rotation(ref F, ref B, ref U, ref D, ref L, ref R, "FA");
                        rotation(ref F, ref B, ref U, ref D, ref L, ref R, "RC");
                        rotation(ref F, ref B, ref U, ref D, ref L, ref R, "FC");
                        Globals.numberOfRots = Globals.numberOfRots + 4;
                    } while (B[0, 0] != 'R');
                    rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
                    Globals.numberOfRots = Globals.numberOfRots + 1;
                }
                else { rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC"); Globals.numberOfRots = Globals.numberOfRots + 1; }
            }
        }
        else if (topR == false)
        {
            for (int i = 1; i < 5; i++)
            {
                if (B[0, 2] != 'R')
                {
                    do
                    {
                        rotation(ref F, ref B, ref U, ref D, ref L, ref R, "TA");
                        rotation(ref F, ref B, ref U, ref D, ref L, ref R, "FA");
                        rotation(ref F, ref B, ref U, ref D, ref L, ref R, "TC");
                        rotation(ref F, ref B, ref U, ref D, ref L, ref R, "FC");
                        Globals.numberOfRots = Globals.numberOfRots + 4;
                    } while (B[0, 2] != 'R');
                    rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
                    Globals.numberOfRots = Globals.numberOfRots + 1;
                }
                else { rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC"); Globals.numberOfRots = Globals.numberOfRots + 1; }
            }
        }
        else if (botL == false)
        {
            for (int i = 1; i < 5; i++)
            {
                if (B[2, 0] != 'R')
                {
                    do
                    {
                        rotation(ref F, ref B, ref U, ref D, ref L, ref R, "DA");
                        rotation(ref F, ref B, ref U, ref D, ref L, ref R, "FA");
                        rotation(ref F, ref B, ref U, ref D, ref L, ref R, "DC");
                        rotation(ref F, ref B, ref U, ref D, ref L, ref R, "FC");
                        Globals.numberOfRots = Globals.numberOfRots + 4;
                    } while (R[2, 0] != 'R');
                    rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
                    Globals.numberOfRots = Globals.numberOfRots + 1;
                }
                else { rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC"); Globals.numberOfRots = Globals.numberOfRots + 1; }
            }
        }
        else if (botR == false)
        {
            for (int i = 1; 1 < 5; i++)
            {
                if (R[2, 2] != 'R')
                {
                    do
                    {
                        rotation(ref F, ref B, ref U, ref D, ref L, ref R, "LA");
                        rotation(ref F, ref B, ref U, ref D, ref L, ref R, "FA");
                        rotation(ref F, ref B, ref U, ref D, ref L, ref R, "LC");
                        rotation(ref F, ref B, ref U, ref D, ref L, ref R, "FC");
                        Globals.numberOfRots = Globals.numberOfRots + 4;
                    } while (R[2, 2] != 'R');
                    rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC");
                    Globals.numberOfRots = Globals.numberOfRots + 1;
                }
                else { rotation(ref F, ref B, ref U, ref D, ref L, ref R, "BC"); Globals.numberOfRots = Globals.numberOfRots + 1; }
            }
        }
    }

    

}
