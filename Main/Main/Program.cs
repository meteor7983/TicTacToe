using System;
using Main;

bool isPlay = true;
int winer = 0;

while (true)
{
    isPlay = PlayManager.Play(ref winer);

    if(isPlay == false)
    {
        MapData.DisplayMap();
        if(winer == 0)
        {
            Console.WriteLine($"무승부입니다!");
        }
        else
        {
            Console.WriteLine($"플레이어 {(winer == 1 ? "X" : "O")} 승리!");
        }

        Console.Write("종료하려면 Enter를 누르세요");
        Console.ReadLine();
        return;
    }
}