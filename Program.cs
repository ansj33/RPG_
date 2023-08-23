namespace RPG1
{
    internal class Program
    {
        private static Character player;

        static void Main(string[] args)
        {
            GameDataSetting();
            DisplayGameIntro();
        }

        static void GameDataSetting()
        {
            // 캐릭터 정보 세팅
            player = new Character("쩝쩝쩝", "전사", 1, 10, 5, 100, 1500);

            // 아이템 정보 세팅
            player.Inventory.Add(new Item("무쇠갑옷", "무쇠로 만들어져 튼튼한 갑옷입니다.", 0, 5));
            player.Inventory.Add(new Item("낡은 검", "쉽게 볼 수 있는 낡은 검입니다.", 3, 0));
        }

        static void DisplayGameIntro()
        {
            Console.Clear();

            Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
            Console.WriteLine("이곳에서 던전으로 들어가기 전 활동을 할 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine("1. 상태보기");
            Console.WriteLine("2. 인벤토리");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");

            int input = CheckValidInput(1, 2);
            switch (input)
            {
                case 1:
                    DisplayMyInfo();
                    break;

                case 2:
                    DisplayInventory();
                    break;
            }
        }

        static void DisplayMyInfo()
        {
            Console.Clear();

            Console.WriteLine("상태보기");
            Console.WriteLine("캐릭터의 정보를 표시합니다.");
            Console.WriteLine();
            Console.WriteLine($"Lv.{player.Level}");
            Console.WriteLine($"{player.Name}({player.Job})");
            Console.WriteLine($"공격력: {player.Atk}");
            Console.WriteLine($"방어력: {player.Def}");
            Console.WriteLine($"체력: {player.Hp}");
            Console.WriteLine($"Gold: {player.Gold} G");
            Console.WriteLine();
            Console.WriteLine("0. 나가기");

            int input = CheckValidInput(0, 0);
            switch (input)
            {
                case 0:
                    DisplayGameIntro();
                    break;
            }
        }

        static void DisplayInventory()
        {
            Console.Clear();

            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine("1. 장착 관리");
            Console.WriteLine("0. 나가기");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");

            int input = CheckValidInput(0, 1);
            switch (input)
            {
                case 0:
                    DisplayGameIntro();
                    break;

                case 1:
                    ManageEquipments();
                    break;
            }
        }

        static void ManageEquipments()
        {
            Console.Clear();
            Console.WriteLine("장착 관리");
            Console.WriteLine();
            Console.WriteLine("아이템 목록");

            for (int i = 0; i < player.Inventory.Count; i++)
            {
                string equippedIndicator = player.Inventory[i].Equipped ? "E" : " ";
                Console.WriteLine($"{equippedIndicator} {i + 1}. {player.Inventory[i].ItemName} - ATK: {player.Inventory[i].ItemATK} - {player.Inventory[i].ItemEX}");
                //ATK DEF 넣기
            }

            Console.WriteLine("  0.나가기");
            Console.WriteLine();
            Console.WriteLine("장착할 아이템 번호를 입력해주세요.");

            int input = CheckValidInput(0, player.Inventory.Count);
            if (input == 0)
            {
                DisplayInventory();
                return;
            }

            Item selectedItem = player.Inventory[input - 1];

            if (selectedItem.Equipped)
            {
                selectedItem.Equipped = false;
                Console.WriteLine($"{selectedItem.ItemName} 아이템을 해제했습니다.");

                player.Atk -= selectedItem.ItemATK;
                player.Def -= selectedItem.ItemDEf;
            }
            else
            {
                selectedItem.Equipped = true;
                Console.WriteLine($"{selectedItem.ItemName} 아이템을 장착했습니다.");

                player.Atk += selectedItem.ItemATK;
                player.Def += selectedItem.ItemDEf;
            }

            Console.WriteLine();
            Console.WriteLine("enter");
            Console.ReadKey();
            DisplayInventory();
        }

        static void DisplayItemInfo(Item item)
        {
            Console.Clear();
            Console.WriteLine($"아이템 정보 - {item.ItemName}");
            Console.WriteLine();
            Console.WriteLine($"장착 상태: {(item.Equipped ? "장착 중" : "미장착")}");
            Console.WriteLine($"공격력: {item.ItemATK}");
            Console.WriteLine($"방어력: {item.ItemDEf}");

            Console.WriteLine();
            Console.WriteLine("1. 장착 / 해제");
            Console.WriteLine("0. 뒤로 가기");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");

            int input = CheckValidInput(0, 1);

            switch (input)
            {
                case 1:
                    item.Equipped = !item.Equipped;
                    Console.WriteLine(item.Equipped ? "아이템을 장착했습니다." : "아이템을 해제했습니다.");
                    Console.WriteLine();
                    Console.ReadKey();
                    DisplayInventory();
                    break;
                case 0:
                    DisplayInventory();
                    break;
            }
        }

        static int CheckValidInput(int min, int max)
        {
            while (true)
            {
                string input = Console.ReadLine();

                bool parseSuccess = int.TryParse(input, out int ret);
                if (parseSuccess)
                {
                    if (ret >= min && ret <= max)
                        return ret;
                }

                Console.WriteLine("잘못된 입력입니다.");
            }
        }
    }

    public class Character
    {
        public List<Item> Inventory { get; } = new List<Item>();
        public List<Item> EquippedItems { get; } = new List<Item>();


        public string Name { get; }
        public string Job { get; }
        public int Level { get; }
        public int Atk { get; set; }
        public int Def { get; set; }
        //set; 을 넣으니 오류는 없어졌지만 공격력 스탯은 변하지 않음
        public int Hp { get; }
        public int Gold { get; }

        public Character(string name, string job, int level, int atk, int def, int hp, int gold)
        {
            Name = name;
            Job = job;
            Level = level;
            Atk = atk;
            Def = def;
            Hp = hp;
            Gold = gold;
        }
    }

    public class Item
    {
        public string ItemName { get; set; }
        public string ItemEX { get; set; }
        public bool Equipped { get; set; }
        public int ItemATK { get; set; }
        public int ItemDEf { get; set; }

        public Item(string itemName, string itemEX, int itemATK, int itemDEF)
        {
            ItemName = itemName;
            ItemEX = itemEX;
            ItemATK = itemATK;
            ItemDEf = itemDEF;
        }
    }
}