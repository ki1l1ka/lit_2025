using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Security.Cryptography.X509Certificates;

namespace textquest;

class Program {
    static void Main()
    {
        // Создание мира
            // создание локаций
            Location hall = new Location(10, "Вы оказались в освещённом холле. В холле вы видите ресепшен и одиноко стоящего уборщика.", new Dictionary<string, Location> {{"Дверь 1", room_1}, {"Дверь 2", room_2}, {"Дверь 3", room_3}, {"Дверь 4", room_4}}, false);
            // Location reception 
            Location room_1 = new Location(10, "Вы оказались в освещённом холле. В холле вы видите ресепшен и одиноко стоящего уборщика.", new Dictionary<string, Location> {{"Дверь 1", room_1}}, false);
            Location room_2 = new Location(10, "Вы оказались в освещённом холле. В холле вы видите ресепшен и одиноко стоящего уборщика.", new Dictionary<string, Location> {{"Дверь 1", room_1}}, false);
            Location room_3 = new Location(10, "Вы оказались в освещённом холле. В холле вы видите ресепшен и одиноко стоящего уборщика.", new Dictionary<string, Location> {{"Дверь 1", room_1}}, false);
            Location room_4 = new Location(10, "Вы оказались в освещённом холле. В холле вы видите ресепшен и одиноко стоящего уборщика.", new Dictionary<string, Location> {{"Дверь 1", room_1}}, false); 
            Location basement = new Location(10, "Вы оказались в освещённом холле. В холле вы видите ресепшен и одиноко стоящего уборщика.", new Dictionary<string, Location> {{"Дверь 1", room_1}}, false);
            Location corridor = new Location(10, "Вы оказались в освещённом холле. В холле вы видите ресепшен и одиноко стоящего уборщика.", new Dictionary<string, Location> {{"Дверь 1", room_1}}, false);
            // создание предметов и npc
            Entity hostess = new Entity("Хостесс", "...", new Dictionary<string, Item> {}, new string[] {"реплика 1", "реплика 2"});
            Entity guest_1 = new Entity("Хостесс", "...", new Dictionary<string, Item> {}, new string[] {"реплика 1", "реплика 2"});
            Entity guest_2 = new Entity("Хостесс", "...", new Dictionary<string, Item> {}, new string[] {"реплика 1", "реплика 2"});
            Entity guest_3 = new Entity("Хостесс", "...", new Dictionary<string, Item> {}, new string[] {"реплика 1", "реплика 2"});
            Entity guest_4 = new Entity("Хостесс", "...", new Dictionary<string, Item> {}, new string[] {"реплика 1", "реплика 2"});
            Entity guest_5 = new Entity("Хостесс", "...", new Dictionary<string, Item> {}, new string[] {"реплика 1", "реплика 2"});
            
            Entity generator = new Entity("Хостесс", "...", new Dictionary<string, Item> {}, new string[] {"реплика 1", "реплика 2"});
            Entity chest = new Entity("Хостесс", "...", new Dictionary<string, Item> {}, new string[] {"реплика 1", "реплика 2"});

            ActionObject torch = new ActionObject("Фонарик", "...");
            ActionObject key = new ActionObject("Фонарик", "...");
            ActionObject fuse = new ActionObject("Фонарик", "...");

            Item artefact_1 = new Item();
            Item artefact_2 = new Item();
            Item artefact_3 = new Item();
            // создание эффектов
            // Эффекты для предметов
            var getTorchEffect = new AddItemEffect("фонарик", new Item(2));
            var getKeyEffect = new AddItemEffect("ключ", new Item(1));
            var getFuseEffect = new AddItemEffect("предохранитель", new Item(1));

            // Сюжетные эффекты
            var repairGeneratorEffect = new SetFlagEffect("generator_fixed", true); // Нужно создать этот класс или менять флаг напрямую
            var winEffect = new WinEffect();

            // Создание условий
            var hasKey = new HasItemCondition("ключ");
            var hasFuse = new HasItemCondition("предохранитель");
            var generatorFixed = new IsFlagCondition("generator_fixed");
            var isDark = new NotCondition(generatorFixed);

        string input = Console.ReadLine().ToLower().Split(' ');
        CommandBase command = input[0] switch {
        "go" => new MoveCommand(input[1]),
        "take" or "interact" => new InteractCommand(input[1]),
        "use" => new UseItemCommand(input[1]),
        "inv" => new InventoryCommand(),
        _ => null
};

command?.executeCommand(state);

    }
}


public static void Main()
{
    while (true) // Цикл перезапуска игры
    {
        // 1. ИНИЦИАЛИЗАЦИЯ СОСТОЯНИЯ
        var flags = new Dictionary<string, bool> {
            { "quest_clean", false }, { "quest_seals", false },
            { "room1_clean", false }, { "room2_clean", false }, { "room3_clean", false },
            { "generator_on", false }, { "electricity", true }, { "luck", false }, { "restart", false }
        };
        var player = new Gamer(new Dictionary<string, Item>(), 100, 100, 100, 100);
        var map = new Dictionary<string, Location>();

        // 2. СОЗДАНИЕ ЭФФЕКТОВ
        var getSealEffect = new AddItemEffect("печать", new Item(1));
        var getToolsEffect = new AddItemEffect("инструменты", new Item(2));
        var getLeverEffect = new AddItemEffect("рычаг", new Item(1));
        
        // Эффект хостесс: выключает свет и включает урон по рассудку
        var darknessSequence = new CombinedEffect(new List<iEffect> {
            new SetFlagEffect("electricity", false),
            new SanityDownEffect(10) 
        });

        // 3. СОЗДАНИЕ ЛОКАЦИЙ
        Location hall = new Location(10, new[] { "Вы в холле. Здесь Хостесс и Уборщик." }, new Dictionary<string, GameObject>(), false);
        Location r1 = new Location(10, new[] { "Комната 1. Пыльное кресло и сундук." }, new Dictionary<string, GameObject>(), false);
        Location r2 = new Location(10, new[] { "Комната 2. Шкаф и сундук." }, new Dictionary<string, GameObject>(), false);
        Location r3 = new Location(10, new[] { "Комната 3. Зеркало и сундук." }, new Dictionary<string, GameObject>(), false);
        Location basement = new Location(5, new[] { "Сырой подвал с генератором." }, new Dictionary<string, GameObject>(), true);
        Location corridor = new Location(0, new[] { "Бесконечный темный коридор." }, new Dictionary<string, GameObject>(), true);

        // 4. НАПОЛНЕНИЕ ОБЪЕКТАМИ
        // Комната 1: Кресло дает инструменты и флаг чистки
        r1.Objects.Add(new ActionObject("кресло", "Убрать кресло", "Вы убрали кресло и нашли инструменты!", 
            new CombinedEffect(new List<iEffect> { getToolsEffect, new SetFlagEffect("room1_clean", true) })));
        r1.Objects.Add(new ActionObject("сундук", "Открыть сундук", "Вы нашли первую печать.", getSealEffect));

        // Комната 3: Сундук дает печать и рычаг
        r3.Objects.Add(new ActionObject("сундук", "Открыть сундук", "Вы нашли печать и рычаг!", 
            new CombinedEffect(new List<iEffect> { getSealEffect, getLeverEffect })));

        // Подвал: Генератор
        var genLogic = new GeneratorObject("генератор", "Старый агрегат", new AndCondition(new HasItemCondition("рычаг"), new HasItemCondition("инструменты")));
        basement.Objects.Add(genLogic);

        // 5. СВЯЗИ И КАРТА
        hall.Exits = new Dictionary<string, string> { {"1", "r1"}, {"2", "r2"}, {"3", "r3"}, {"вниз", "basement"}, {"выход", "corridor"} };
        r1.Exits.Add("назад", "hall"); r2.Exits.Add("назад", "hall"); r3.Exits.Add("назад", "hall");
        basement.Exits.Add("вверх", "hall");

        map.Add("hall", hall); map.Add("r1", r1); map.Add("r2", r2); map.Add("r3", r3); 
        map.Add("basement", basement); map.Add("corridor", corridor);

        var state = new GameState(0, 1, 12, new string[]{"hall"}, hall, flags) { Player = player, Map = map };

        // 6. ИГРОВОЙ ЦИКЛ
        while (state.Player.Health > 0 && state.Player.Sanity > 0 && !state.Flags["restart"])
        {
            Console.WriteLine($"\nHP: {state.Player.Health} | Sanity: {state.Player.Sanity} | Локация: {state.CurrentLocation.Name}");
            Console.WriteLine(string.Join(" ", state.CurrentLocation.Description));

            // Проверка условий квеста уборщика (Luck)
            if (state.Flags["room1_clean"] && state.Flags["room2_clean"] && state.Flags["room3_clean"])
                state.Flags["luck"] = true;

            // Логика коридора (Урон)
            if (state.CurrentLocation == corridor) {
                double damage = state.Flags["luck"] ? 10 : 20;
                if (!state.Player.Inventory.ContainsKey("фонарик")) {
                    state.Player.Health -= damage;
                    Console.WriteLine($"Тьма ранит вас! -{damage} HP");
                }
            }

            // Ввод
            Console.Write("> ");
            var input = Console.ReadLine().ToLower().Split(' ');
            if (input[0] == "exit") break;

            // Выполнение команд
            CommandBase command = input[0] switch {
                "go" => new MoveCommand(input[1]),
                "take" or "use" => new InteractCommand(input[1]),
                "inv" => new InventoryCommand(),
                _ => null
            };
            command?.executeCommand(state);

            // Проверка конца коридора
            if (state.CurrentLocation == corridor && input[0] == "open") state.Flags["restart"] = true;
        }

        Console.WriteLine("\n--- ИГРА ПЕРЕЗАГРУЖАЕТСЯ ---");
    }
}
