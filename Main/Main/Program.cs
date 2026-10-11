using System;
using TicTacToe;

PlayManager playManager = new PlayManager();
bool isPlay = true;

while (isPlay)
{
    isPlay = playManager.Play();
}