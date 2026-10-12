using System;

namespace TicTacToe
{
    internal class PlayManager
    {
        private bool isModeSelected = false;
        private bool isLevelSetected = false;
        private bool isXTurn = true;
        private bool isComputerBattle;
        private int level = 0;
        private ComputerPlayer computer = null;
        private int lastComputerSelected = -1;

        public bool Play()
        {
            ModeAndLevelSelect();

            MapData.DisplayMap(level);

            while (!TrySet(out string failReason))
            {
                MapData.DisplayMap(level);

                Console.WriteLine(failReason);
            }

            int[] currentMap = MapData.GetPlayMap();
            if (EndCheck(ref currentMap, out int winner))
            {
                ShowWinner(winner);

                Console.WriteLine();
                bool reset = RestartCheck();
                if (reset)
                {
                    InitializeData();
                }
                else
                {
                    return false;
                }
            }

            return true;
        }

        private bool TryModeSelect(ref string reason)
        {
            Console.Clear();
            if(!string.IsNullOrEmpty(reason))
            {
                Console.WriteLine(reason);
            }
            Console.WriteLine("모드를 선택해 주세요");
            Console.WriteLine("1. 두 사람 대전");
            Console.WriteLine("2. 컴퓨터 대전");
            bool result = int.TryParse(Console.ReadLine(), out int selected);

            if (!result || selected < 0 || selected > 2)
            {
                reason = "잘못된 입력입니다. 1번 혹은 2번을 입력하세요";
                return false;
            }

            isComputerBattle = selected == 1 ? false : true;
            isModeSelected = true;
            return true;
        }
        private bool TryLevelSelect(ref string reason)
        {
            Console.Clear();
            if (!string.IsNullOrEmpty(reason))
            {
                Console.WriteLine(reason);
            }
            Console.WriteLine("난이도를 선택해 주세요");
            Console.WriteLine("1. 쉬움");
            Console.WriteLine("2. 어려움");
            bool result = int.TryParse(Console.ReadLine(), out int selected);

            if (!result || selected < 0 || selected > 2)
            {
                reason = "잘못된 입력입니다. 1번 혹은 2번을 입력하세요";
                return false;
            }

            level = selected;
            isLevelSetected = true;
            return true;
        }

        private bool TrySet(out string reason)
        {
            if(isComputerBattle && lastComputerSelected != -1)
            {
                Console.WriteLine($"컴퓨터의 선택번호는 {lastComputerSelected + 1}입니다.");
            }

            Console.WriteLine($"플레이어 {(isXTurn ? "X" : "O")}의 차례입니다.");
            reason = string.Empty;
            int index = 0;
            int[] currentMap = MapData.GetPlayMap();

            if (!isComputerBattle || isXTurn)
            {
                Console.Write("위치를 선택하세요(1~9): ");
                bool result = int.TryParse(Console.ReadLine(), out index);

                if (!result)
                {
                    reason = "잘못된 입력입니다. 1부터 9까지의 번호를 입력하세요";
                    return false;
                }

                index = index - 1;
                if (index < 0 || index > 9 || currentMap[index] != 0)
                {
                    reason = (index < 0 || index > 9) ? "1부터 9까지의 번호를 입력하세요" : "이미 선택한 칸입니다. 빈칸을 선택 하세요.";
                    return false;
                }
            }
            else
            {
                index = computer.SelectIndex();
                lastComputerSelected = index;
            }

            currentMap[index] = (isXTurn) ? 1 : 2;
            isXTurn = !isXTurn;
            return true;
        }

        public static bool EndCheck(ref int[] currentMap, out int winer)
        {
            winer = 0;
            int rowCount = MapData.GetRowCount();
            int colCount = MapData.GetColCount();

            if (currentMap[0] != 0 && currentMap[0] == currentMap[4] && currentMap[0] == currentMap[8])
            {
                winer = currentMap[0];
                return true;
            }
            else if (currentMap[2] != 0 && currentMap[2] == currentMap[4] && currentMap[2] == currentMap[6])
            {
                winer = currentMap[2];
                return true;
            }

            //bool isDiagonalWin = true;
            //int originIndex = 0;
            //for (int col = 0; col < colCount - 1; col++)
            //{
            //    originIndex = col + (col * colCount);
            //    int nextIndex = col + (col * colCount) + colCount + 1;

            //    if (currentMap[originIndex] == 0 || 
            //        currentMap[originIndex] != currentMap[nextIndex])
            //    {
            //        isDiagonalWin = false;
            //    }
            //}

            //if(isDiagonalWin)
            //{
            //    winer = currentMap[originIndex];
            //    return true;
            //}

            //for (int col = colCount; col > 1; col--)
            //{
            //    originIndex = (rowCount * col) - col;
            //    int nextIndex = (rowCount * (col - 1)) - (col-1);

            //    if (currentMap[originIndex] == 0 ||
            //        currentMap[originIndex] != currentMap[nextIndex])
            //    {
            //        isDiagonalWin = false;
            //    }
            //}

            //if (isDiagonalWin)
            //{
            //    winer = currentMap[originIndex];
            //    return true;
            //}

            for (int row = 0; row < rowCount; row++)
            {
                int colIndex = 0;
                bool isWin = true;
                for (int col = 0; col < colCount - 1; col++)
                {
                    colIndex = row * colCount + col;
                    if (currentMap[colIndex] == 0 ||
                        currentMap[colIndex] != currentMap[colIndex + 1])
                    {
                        isWin = false;
                        break;
                    }
                }

                if(isWin)
                {
                    winer = currentMap[colIndex];
                    return true;
                }
            }

            for (int col = 0; col < colCount; col++)
            {
                int rowIndex = 0;
                bool isWin = true;
                for (int row = 0; row < rowCount - 1; row++)
                {
                    rowIndex = col + (row * colCount);
                    if (currentMap[rowIndex] == 0 ||
                        currentMap[rowIndex] != currentMap[rowIndex + colCount])
                    {
                        isWin = false;
                        break;
                    }
                }

                if (isWin)
                {
                    winer = currentMap[rowIndex];
                    return true;
                }
            }

            bool isFull = true;
            for (int i = 0;i < currentMap.Length; i++)
            {
                if(currentMap[i] == 0)
                {
                    isFull = false;
                    break;
                }
            }

            if (isFull)
            {
                return true;
            }

            return false;
        }

        private bool RestartCheck()
        {
            string restart = string.Empty;
            bool result = true;
            while(result)
            {
                if (!result)
                {
                    Console.WriteLine("잘못 입력하셨습니다. Y 혹은 N중 선택해 주세요.");
                }
                Console.WriteLine("재시작 하시겠습니까? (Y/N)");
                restart = Console.ReadLine();

                if (restart.ToUpper() == "Y")
                {
                    result = true;
                    break;
                }
                else if(restart.ToUpper() == "N")
                {
                    result = false;
                    break;
                }
            }
            return result;
        }

        private void ShowWinner(int winner)
        {
            MapData.DisplayMap(level);
            if (winner == 0)
            {
                Console.WriteLine($"무승부입니다!");
            }
            else
            {
                Console.WriteLine($"플레이어 {(winner == 1 ? "X" : "O")} 승리!");
            }
        }

        private void ModeAndLevelSelect()
        {
            string selectFailReason = string.Empty;
            if (!isModeSelected)
            {
                while (!TryModeSelect(ref selectFailReason))
                {
                }
            }

            selectFailReason = string.Empty;
            if (!isLevelSetected && isComputerBattle)
            {
                while (!TryLevelSelect(ref selectFailReason))
                {
                }
            }

            if (isComputerBattle && isLevelSetected && computer == null)
            {
                computer = new ComputerPlayer(level);
            }
        }

        private void InitializeData()
        {
            isModeSelected = false;
            isLevelSetected = false;
            isXTurn = true;
            isComputerBattle = false;
            computer = null;
            level = 0;
            lastComputerSelected = -1;
            MapData.MapReset();
        }
    }
}
