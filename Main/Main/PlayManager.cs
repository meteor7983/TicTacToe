using System;

namespace Main
{
    internal class PlayManager
    {
        private static bool isModeSelected = false;
        private static bool isXTurn = true;
        private static bool isComputerBattle;
        private static int level;
        private ComputerPlayer computer;
        private int lastComputerSelected = -1;

        public bool Play(ref int winner)
        {
            if(!isModeSelected)
            {
                while (!TryModeSelect(out string reason))
                {
                    Console.WriteLine(reason);
                }
            }

            MapData.DisplayMap();

            while(!TrySet(out string reason))
            {
                MapData.DisplayMap();

                Console.WriteLine(reason);
            }

            if(EndCheck(out winner))
            {
                ShowWinner(winner);

                Console.WriteLine();
                //Console.WriteLine("재시작 하시겠습니까? (Y/N)");
                //string restart = Console.ReadLine().Trim();
                bool reset = RestartCheck();
                if (reset)
                {
                    isModeSelected = false;
                    isXTurn = true;
                    isComputerBattle = false;
                    computer = null;
                    lastComputerSelected = -1;
                    MapData.MapReset();
                }
                else
                {
                    return false;
                }
            }

            return true;
        }

        private bool TryModeSelect(out string reason)
        {
            reason = string.Empty;
            Console.Clear();
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

            if (isComputerBattle)
            {
                MapData.SetModeLevel(2);
                computer = new ComputerPlayer();
            }

            isModeSelected = true;
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

        private bool EndCheck(out int winer)
        {
            int[] currentMap = MapData.GetPlayMap();
            winer = 0;

            if (currentMap[0] != 0 && currentMap[0] == currentMap[4] && currentMap[0] == currentMap[8])
            {
                winer = currentMap[0];
                return true;
            }
            else if(currentMap[2] != 0 && currentMap[2] == currentMap[4] && currentMap[2] == currentMap[6])
            {
                winer = currentMap[2];
                return true;
            }

            for (int i = 0; i < 3; i++)
            {
                if (currentMap[i * 3] == 0)
                {
                    continue;
                }

                if (currentMap[i * 3] == currentMap[i * 3 + 1] &&
                   currentMap[i * 3 + 1] == currentMap[i * 3 + 2])
                {
                    winer = currentMap[i * 3];
                    return true;
                }
            }

            for (int i = 0; i < 3; i++)
            {
                if (currentMap[i] == 0)
                {
                    continue;
                }

                if (currentMap[i] == currentMap[i + 3] &&
                   currentMap[i + 3] == currentMap[i + 6])
                {
                    winer = currentMap[i];
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
            MapData.DisplayMap();
            if (winner == 0)
            {
                Console.WriteLine($"무승부입니다!");
            }
            else
            {
                Console.WriteLine($"플레이어 {(winner == 1 ? "X" : "O")} 승리!");
            }
        }
    }
}
