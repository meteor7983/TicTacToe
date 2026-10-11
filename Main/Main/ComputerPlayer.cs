using System;

namespace TicTacToe
{
    internal class ComputerPlayer
    {
        private int level;
        private int playerNumber;
        private int computerNumber;
        public ComputerPlayer(int level, int playerNum, int computerNum)
        {
        }
        public ComputerPlayer(int level) : this(level, 1, 2)
        {
            this.level = level;
            playerNumber = 1;
            computerNumber = 2;
        }

        public int SelectIndex()
        {
            if (level == 2)
            {
                return HardSelect();
            }

            return EsaySelect();
        }

        private int EsaySelect()
        {
            int[] currentMap = MapData.GetPlayMap();
            Random random = new Random();
            int[] emptyPos = MapData.GetEmptyPositions();
            int index;
            while (true)
            {
                index = emptyPos[random.Next(emptyPos.Length)];
                if (currentMap[index] == 0)
                {
                    return index;
                }
            }
        }
        private int HardSelect()
        {
            int index = 0;
            if (TryWinPosCheck(computerNumber, out index))
            {
            }
            else if (TryWinPosCheck(playerNumber, out index))
            {
            }
            else
            {
                PriorityPosCheck(out index);
            }

            return index;
        }

        private bool TryWinPosCheck(int checkNubmer, out int index)
        {
            index = 0;
            int[] cloneMap = MapData.GetClonePlayMap();
            int[] emptyPos = MapData.GetEmptyPositions();
            int emptyPosIndex = 0;
            bool isFindWinPos = false;
            for (int i = 0; i < emptyPos.Length; i++)
            {
                emptyPosIndex = emptyPos[i];
                cloneMap[emptyPosIndex] = checkNubmer;
                if (PlayManager.EndCheck(ref cloneMap, out int winner))
                {
                    if (winner == checkNubmer)
                    {
                        isFindWinPos = true;
                        break;
                    }
                }
                cloneMap[emptyPosIndex] = 0;
            }

            if (isFindWinPos)
            {
                index = emptyPosIndex;
                return true;
            }

            return false;
        }

        private void PriorityPosCheck(out int Index)
        {
            int[] cloneMap = MapData.GetClonePlayMap();
            Random random = new Random();
            if (cloneMap[4] == 0)
            {
                Index = 4;
            }
            else if (cloneMap[0] == 0 || cloneMap[2] == 0 || cloneMap[6] == 0 || cloneMap[8] == 0)
            {
                int[] checkPos = new int[] { 0, 2, 6, 8};
                while (true)
                {
                    Index = checkPos[random.Next(checkPos.Length)];
                    if (cloneMap[Index] == 0)
                    {
                        break;
                    }
                }
            }
            else
            {
                int[] emptyPos = MapData.GetEmptyPositions();
                int pos = random.Next(emptyPos.Length);
                Index = pos;
            }
        }
    }
}
