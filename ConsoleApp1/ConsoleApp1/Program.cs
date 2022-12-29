

int save = 0;
int[] saveset = new int[10]; // 나머지값을 저장하기 위한 배열을 만듦
string test = "";
int[] testset = new int[10]; // 나머지값을 비교하기 위해 배열을 만듦
int count = 0;

for (int i = 0; i < 10; ++i)
{
    string input = Console.ReadLine();

    int value = int.Parse(input);

    // 나머지값 구하기

    save = value % 42; // 1 2 3 4 5~~10을 42로 나머지를 구했다
    saveset[i] = save; // 나머지를 배열로 만들었다. saveset[0] = 1, saveset[1] = 2, saveset[2] = 3 , saveset[3] = 4 ~~
    test += save + " "; // test = "1 2 3 4 5 6 7 8 9 10"
    testset[i] = save; // testset[0] = 1, testset[1] = 2, testset[2] = 3, testset[3] = 4 ~~~
} 

    for(int k = 0; k < 10 ;++k)
    {
        for (int m = 0; m < 9 ;++m)
        {
            if (saveset[k] != testset[m])
            {
            count++;
            }
        }
        

    }

    Console.WriteLine(count);
   








    





