namespace Test;
class Program
{
    static void Main(string[] args)
    {
        // 벽의 좌표
        int[] wall_X = new int[3];
        wall_X[0] = 3;
        wall_X[1] = 7;
        wall_X[2] = 6;

        int[] wall_Y = new int[3];
        wall_Y[0] = 3;
        wall_Y[1] = 7;
        wall_Y[2] = 6;

        Console.WriteLine(wall_X[2]);
    }
}

