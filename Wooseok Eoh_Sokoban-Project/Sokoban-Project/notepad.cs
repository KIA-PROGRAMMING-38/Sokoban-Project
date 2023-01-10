

int save = 0;
int[] saveset = new int[10]; // 나머지값을 저장하기 위한 배열을 만듦


int count = 0;

for (int i = 0; i < 10; ++i)
{
    string input = Console.ReadLine();

    int value = int.Parse(input);

    // 나머지값 구하기

    save = value % 42; // 1 2 3 4 5~~10을 42로 나머지를 구했다
    saveset[i] = save; // 나머지를 배열로 만들었다(LUT). saveset[0] = 1, saveset[1] = 2, saveset[2] = 3 , saveset[3] = 4 ~~


}  // lookup table을 만들었음


// 나머지값이 서로 다른지 비교해야한다
// 나머지값이 서로 다르다 = 서로 같은 나머지값이 없다
for (int k = 0; k < 10; ++k)
{

    if (saveset[k] = )
    {

    }

}


Console.WriteLine(count);















