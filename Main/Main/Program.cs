using System;
using Main;

PlayManager playManager = new PlayManager();
bool isPlay = true;
int winner = 0;

while (true)
{
    isPlay = playManager.Play(ref winner);

    if(isPlay == false)
    {
        return;
    }
}