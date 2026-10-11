using System;
using TicTacToe;

PlayManager playManager = new PlayManager();
bool isPlay = true;
int winner = 0;

while (isPlay)
{
    isPlay = playManager.Play(ref winner);
}