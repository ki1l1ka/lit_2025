using System.Data;
using System.Security.Cryptography.X509Certificates;
using Microsoft.VisualBasic;
// интерфейсы
interface ICommand {
    void executeCommand(GameState state);
}
interface IInteractable {
    void Interact(GameState state);
}
interface ICondition{ 
    bool condition(GameState state);
}
interface IEffect {
    void ApplyEffect(GameState state);
}
// абстрактные классы
public abstract class CommandBase: ICommand {
    string name;
    string description;
    public abstract void executeCommand(GameState state);
}
public abstract class ConditionBase: ICondition{
    public abstract bool condition(GameState state);
}
public abstract class EffectBase: IEffect {
    public abstract void ApplyEffect(GameState state);
}
public abstract class GameEventBase
{
    private string name;
    private ICondition condition;
    private IEffect effect;

    public GameEventBase(string name, ICondition condition, IEffect effect) {
        this.name = name;
        this.condition = condition;
        this.effect = effect;
    }
    public virtual void tick(GameState state) {
    if (this.condition.condition(state)) {
        this.effect.ApplyEffect(state);}}
    public ICondition Condition{get{return this.condition;}}
}
public abstract class GameObject: IInteractable {
    protected string name;
    protected string description;
    public GameObject(string name, string description){
        this.name = name;
        this.description = description;
    }
    public virtual void update(GameState state) {}
    public string Name {get{return this.name;}}
    public string Description {get{return this.description;}}
    public virtual void Interact(GameState state) {
        // Если у объекта есть эффект — выполняем его
        if (interactionEffect != null) {
            interactionEffect.ApplyEffect(state);
        } else {
            Console.WriteLine($"Вы осматриваете {name}. {description}");
        }

}}
// классы

public class GameState {
    private int turn;
    private int day;
    private int time;
    private Location currentLocation;
    private Dictionary<string, Location> map;
    private Dictionary<string, bool> flags;
    private Gamer player;
    private Entity entities;
    
    public GameState(int turn, int day, int time, Location currentlocation, Dictionary<string, bool> flags){
        this.turn = turn;
        this.currentLocation = currentlocation;
        this.day = day;
        this.time = time;
        this.flags = flags;
        this.player = new Gamer(new Dictionary<string, ActionObject> {}, 100, 100, 10);
    }

    public Gamer Player
    {
        get{return this.player;}
        set{this.player = value;}
    }
    public Entity Entities
    {
        get{return this.entities;}
        set{this.entities = value;}
    }
    public List<string> Map{get {return this.map;} set{this.map = value;}}
    public string CurrentLocation {get {return this.currentLocation;} set{this.currentLocation = value;}}
}

public class Location {
    private bool dark;
    private int lightLevel;
    private string[] description;
    private Dictionary<string, GameObject> objects;
    private Dictionary<string, Location> connections;
    private Dictionary<string, Entity> entities;
    public Location(int light, string[] description, bool dark) {
        this.lightLevel = light;
        this.description = description;
        this.dark = dark;
    }
    public void processEvents(GameState state) {
    foreach (var ev in this.Events) {
        ev.tick(state);
    }}
    public bool Dark{get{return this.dark;} set{this.dark = value;}}
    public int LightLevel{get{return this.lightLevel;} set{this.lightLevel = value;}}
    public string[] Description{get{return this.description;} set{this.description = value;}}
    public Dictionary<string, GameObject> Objects{get{return this.objects;} set{this.objects = value;}}
    public Dictionary<string, Location> Connections{get{return this.connections;} set{this.connections = value;}}
    public Dictionary<string, Entity> Entities{get{return this.entities;} set{this.entities = value;}} 
    public List<GameEventBase> Events { get; set; } = new();
}

public class Entity: GameObject {
    private Dictionary<string, ActionObject> inventory;
    private string[] phrases;
    public Entity(string name, string description, Dictionary<string, ActionObject> inventory, string[] phrases) {
        this.name = name;
        this.description = description; 
        this.inventory = inventory;
        this.phrases = phrases;
    }
    public Dictionary<string, ActionObject> Inventory{
        get{return this.inventory;}
        set{this.inventory = value;}
    }
    public void Speak(int lineNumber){
        Console.WriteLine($"{this.name}: {this.phrases[lineNumber]}");
    }
}
public class ActionObject : GameObject {
    private IEffect _effect;
    private string _interactionText;

    public ActionObject(string name, string desc, string text, IEffect effect) : base(name, desc) {
        _effect = effect;
        _interactionText = text;
    }

    public override void Interact(GameState state) {
        Console.WriteLine(_interactionText);
        _effect.ApplyEffect(state);
    }
}
// public class ActionObject: GameObject {
//     public ActionObject(int weight){
//         this.weight = weight;
//     }
//     public double Weight
//     {
//         get{return this.weight;}
//         set{this.weight = value;}
//     }
// }

public class Obstacle: GameObject {
    private int level;
    public Obstacle(string name, string description, int level){
        this.name = name;
        this.description = description;
        this.level = level;
    }
}

public class Trap: GameObject {
    private DamageEffect damage;
    public Trap(string name, string description, DamageEffect damage){
        this.name = name;
        this.description = description;
        this.damage = damage;
    }
    public void Damage(GameState state){
        damage.ApplyEffect(state);
    }
}

public class Gamer {
    private Dictionary<string, ActionObject> inventory;
    private double health;
    private double healthMax;
    private double sanity;
    public Gamer(Dictionary<string, ActionObject> inventory, double health, double healthmax, double sanity) {
        this.inventory = inventory;
        this.health = health;
        this.healthMax = healthmax;
        this.sanity = sanity;
    }
    public double Health{
        get{return this.health;}
        set{if (value > this.healthMax){this.health = this.healthMax;}
            else if (value < 0){this.health = 0;}
            else {this.health = value;}}
    }
    public double Sanity{
        get{return this.sanity;}
        set{if (value < 0){this.sanity = 0;}
            else {this.sanity = value;}}
    }
    public Dictionary<string, ActionObject> Inventory
    {
        get{return this.inventory;}
        set{this.inventory = value;}
    }
}

//Команды
public class InteractCommand: CommandBase {
    private GameObject target;

    public InteractCommand(GameObject target) {
        this.target = target;
    }
    public override void executeCommand(GameState state)
    {}
}
// условия
public class AndCondition: ConditionBase{
    iCondition cond1;
    iCondition cond2;
    public AndCondition(iCondition cond1, iCondition cond2) {
        this.cond1 = cond1;
        this.cond2 = cond2;
    }
    public override bool condition(GameState state){
        return (cond1.Condition(state) && cond2.Condition(state));
    }
}
public class OrCondition: ConditionBase
{
    iCondition cond1;
    iCondition cond2;
    public OrCondition(iCondition cond1, iCondition cond2) {
        this.cond1 = cond1;
        this.cond2 = cond2;
    }
    public override bool condition(GameState state){
        return (cond1.Condition(state) || cond2.Condition(state));
    }
}
public class NotCondition: ConditionBase{    
    iCondition cond1;
    public NotCondition(iCondition cond1) {
        this.cond1 = cond1;
    }
    public override bool condition(GameState state){
        return !(cond1.Condition(state));
    }}
public class HasItemCondition: ConditionBase{
    private string item;
    public HasItemCondition(string name){
        this.item = name;
    }
    public override bool condition(GameState state) {
        return (state.Player.Inventory.ContainsKey(item));
    }
}

public class IsSaneCondition: ConditionBase{
    private double sanity;
    public IsSaneCondition(double sanity){
        this.sanity = sanity;
    }
    public override bool condition(GameState state){
        return (state.Player.Sanity > this.sanity); 
    }
}

public class IsHealthyCondtion: ConditionBase{
    private double health;
    public IsHealthyCondtion(double health){
        this.health = health;
    }
    public override bool condition(GameState state){
        return (state.Player.Health > 5);
    }
}

public class IsFlagCondition: ConditionBase{
    private string flag;
    public IsFlagCondition(string flag){
        this.flag = flag;
    }
    public override bool condition(GameState state){
        return state.Flags[this.flag]; 
    }
}
public class CanEnterCondition: ConditionBase{
    private string roomName;
    public CanEnterCondition(string roomName){
        this.roomName = roomName;
    }
    public override bool condition(GameState state){
        return (state.roomsAvailable.Contains(this.roomName) && state.CurrentLocation.Connections.Contains(this.roomName)); 
    }
}

// эффекты
public class AddItemEffect: EffectBase {
    private string itemName;
    private Item item;

    public AddItemEffect(string name, ActionObject item) {
        this.item = item;
        this.itemName = name;
    }
    public override void ApplyEffect(GameState state) {
        state.Player.Inventory.Add(this.itemName, this.item);
    }
}

public class RemoveItemEffect: EffectBase {
    private string itemName;
    private bool targetPlayer;
    public RemoveItemEffect(string name, bool target = true) {
        this.itemName = name;
        this.targetPlayer = target;
    }
    public override void ApplyEffect(GameState state) {
        if (this.targetPlayer) {
            state.Player.Inventory.Remove(this.itemName);}
        else
        {
            state.Entity.Inventory.Remove(this.itemName);
        }
    }
}

public class DamageEffect : EffectBase {
    private double amount;
    private bool targetPlayer; 

    public DamageEffect(double amount, bool targetPlayer = true) {
        this.amount = amount;
        this.targetPlayer = targetPlayer;
    }

    public override void ApplyEffect(GameState state) {
        if (targetPlayer) {
            state.Player.Health -= amount;
        } else {
            // Предположим, Entity — это текущий враг в комнате
            state.Entities.Health -= amount; 
        }
    }
}

public class HealEffect : EffectBase {
    private double amount;
    private bool targetPlayer; 

    public DamageEffect(double amount, bool targetPlayer = true) {
        this.amount = amount;
        this.targetPlayer = targetPlayer;
    }

    public override void ApplyEffect(GameState state) {
        if (targetPlayer) {
            state.Player.Health += amount;
        } else {
            // Предположим, Entity — это текущий враг в комнате
            state.Entities.Health += amount; 
        }
    }
}

public class SanityDownEffect: EffectBase {
    private double amount;
    public SanityDownEffect(double amount){
        this.amount = amount;
    }
    public override void ApplyEffect(GameState state)
    {
        state.Player.Sanity = state.Player.Sanity - this.amount;
    }
}

public class SanityUpEffect: EffectBase{
    private double amount;
    public SanityUpEffect(double amount){
        this.amount = amount;
    }
    public override void ApplyEffect(GameState state)
    {
        state.Player.Sanity = state.Player.Sanity + this.amount;
    }
}
public class SetFlagEffect : EffectBase {
    private string f; private bool v;
    public SetFlagEffect(string f, bool v) { 
        this.f = f; 
        this.v = v; }
    public override void ApplyEffect(GameState state) => state.Flags[f] = v;
}

public class CombinedEffect : iEffect {
    private List<iEffect> effects;
    public CombinedEffect(List<iEffect> e) => effects = e;
    public void ApplyEffect(GameState state) { 
        foreach(var e in effects) e.ApplyEffect(state);}
}
public class IsAtLocationCondition : ConditionBase {
    private string _locationName;
    public IsAtLocationCondition(string locationName) { _locationName = locationName; }
    public override bool condition(GameState state) { return state.CurrentLocation.Name == _locationName; }
}

public class WinEffect : EffectBase {
    public override void ApplyEffect(GameState state) {
        state.Flags["Escaped"] = true;
        Console.WriteLine("Вы снова здесь.");
    }
}
// команды
public class MoveCommand : CommandBase {
    private string destinationId;

    public MoveCommand(string destinationId) {this.destinationId = destinationId;}

    public override void executeCommand(GameState state)
    {
        if (state.CurrentLocation.Connections.ContainsKey(this.destinationId))
        {
            state.CurrentLocation = state.CurrentLocation.Connections[destinationId];
        }
        else {Console.WriteLine("Такой комнаты тут нет");}
    }
}

public class InteractCommand : CommandBase {
    private string targetName;

    public InteractCommand(string targetName) {this.targetName = targetName;}

    public override void executeCommand(GameState state) {
        if (state.Objects.ContainsKey(this.targetName)){state.Objects[targetName].Interact(state);}
        else {Console.WriteLine("Такого тут нет");}
    }
}

public class UseItemCommand : CommandBase {
    private string _itemName;

    public UseItemCommand(string itemName) => _itemName = itemName;

    public override void executeCommand(GameState state) {
        if (state.Player.Inventory.TryGetValue(_itemName, out ActionObjet item)) {
            item.Interact(state); 
        } else {
            Console.WriteLine("У вас нет этого предмета.");
        }
    }
}

public class InventoryCommand : CommandBase {
    public override void executeCommand(GameState state) {
        Console.WriteLine("--- Ваш инвентарь ---");
        if (state.Player.Inventory.Count == 0) {
            Console.WriteLine("Пусто.");
        } else {foreach (var item in state.Player.Inventory.Keys) {Console.WriteLine($"- {item}");}}
    }
}

// создание мира
public class Game{
    private GameState state;
    public void Init()
    {   
        // создание локаций и соединение
        Location hall = new Location(3, "Вы в ярко освещенном холле", false);
        Location storage = new Location(2, "Вы в хранилище", false);
        Location generatorRoom = new Location(1, "Вы в тускло освещенной комнате с генераторомшкатулка для бижутерии", false);
        Location darkcorridor = new Location(0, "Вы в тёмном коридоре", true);
        Location room_1 = new Location(3, "Вы в номере 1", false);
        Location room_2 = new Location(2, "Вы в номере 2", false);
        Location room_3 = new Location(1, "Вы в номере 3", true);
        Location exit = new Location(0, "Вы у выхода", false);
        var map = new Dictionary<string, Location> {{"hall", hall}, {"storage", storage}, {"generatorroom", generatorRoom}, {"darkcorridor", darkcorridor}, {"room_1", room_1}, {"room_2", room_2}, {"room_3", room_3}};
        hall.Connections = new Dictionary<string, Location> {{"storage", storage}, {"generatorroom", generatorRoom}, {"darkcorridor", darkcorridor}, {"room_1", room_1}, {"room_2", room_2}, {"room_3", room_3}};
        storage.Connections = new Dictionary<string, Location> {{"hall", hall}};
        generatorRoom.Connections = new Dictionary<string, Location> {{"hall", hall}};
        room_1.Connections = new Dictionary<string, Location> {{"hall", hall}};
        room_2.Connections = new Dictionary<string, Location> {{"hall", hall}};
        room_3.Connections = new Dictionary<string, Location> {{"hall", hall}};
        // Создание предметов
        ActionObject torch = new ActionObject("torch", "освещает тёмные места");
        ActionObject key = new ActionObject("key", "старый ключ. Должен что-то открывать");
        ActionObject fuse = new ActionObject("fuse", "Предохранитель для Генератора");
        ActionObject lever = new ActionObject("lever", "рычаг для генератора");
        ActionObject relic_1 = new ActionObject("clock", "Старинные часы. На дне, сквозь слой патины можно разглядеть необычную печать...");
        ActionObject relic_2 = new ActionObject("case", "Небольшая шкатулка. Внутри, под крышкой можно разглядеть необычную печать...");
        ActionObject relic_3 = new ActionObject("knife", "Изящный нож. У основания клинка можно разглядеть необычную печать...");
        // Расстановка предметов по комнатам
        room_1.Objects.Add("torch", torch); room_1.Objects.Add("clock", relic_1);
        room_2.Objects.Add("case", relic_2);
        storage.Objects.Add("fuse", fuse); storage.Objects.Add("lever", lever);
        // Создание сущностей
        Entity chest = new Entity("chest", "Старый деревянный сундук", new Dictionary<string, ActionObject> {"knife", relic_3}, new string[] {"Этот старый сундук можно открыть"});
        room_3.Entities.Add("chest", chest);
        var guidePhrases = new[] {
        "Добро пожаловать в отель.",
        "Будьте осторожны в темном коридоре — без света там опасно.",
        "Чтобы выйти, вам понадобится старый ключ, но он у Хранителя."};
        Entity guide = new Entity("guide", "Старый проводник в пыльном фраке.", new Dictionary<string, Item>(), guidePhrases);
        hall.Entities.Add("guide", guide);
        var questRewardEffect = new CombinedEffect(new List<IEffect> {
            new RemoveItemEffect("clock"),
            new RemoveItemEffect("case"),
            new RemoveItemEffect("knife"),
            new AddItemEffect("key", new Item(1) {Name = "key", Description = "Тот самый ключ от выхода."}),
            new SetFlagEffect("relics_collected", true)
        });
        var hasClock = new HasItemCondition("clock");
        var hasCase = new HasItemCondition("case");
        var hasKnife = new HasItemCondition("knife");
        var hasRelics = new AndCondition(hasClock, new AndCondition(hasCase, hasKnife));
        var keeperPhrases = new[] {
            "Принеси мне три реликвии с печатями, и я дам тебе кое-что",
            "Я вижу, ты собрал их все. Держи мой дар.",};

        Entity keeper = new Entity(
            "keeper", 
            "Таинственный Хранитель", 
            new Dictionary<string, Item>(), 
            keeperPhrases
        );
        hall.Entities.Add("keeper", keeper);
        // GameState
        int turn = 1; int day = 1; int time = 10; Location currentlocation = hall;
        Dictionary<string, bool> flags = new Dictionary<string, bool> {{ "relics_collected", false }, { "key_received", false }, { "generator_working", true }, { "light_on", false }, { "escaped", false }};
        GameState state = new GameState(turn, day, time, currentlocation, flags);
        }
    public void Run()
{
    Console.WriteLine("Вы оказались в холле таинственного отеля.");
    
    while (!state.Flags["escaped"])
    {
        Console.WriteLine("\n" + new string('=', 30));
        Console.WriteLine($"{state.CurrentLocation.Description}");
        
        // Список сущностей
        if (state.CurrentLocation.Entities.Count > 0) 
        {
            Console.WriteLine($"Здесь находятся: {string.Join(", ", state.CurrentLocation.Entities.Keys)}");
        }

        // Список предметов
        if (state.CurrentLocation.Objects.Count > 0)
        {
            Console.WriteLine($"На полу лежит: {string.Join(", ", state.CurrentLocation.Objects.Keys)}");
        }

        Console.WriteLine($"\nДоступные выходы: {string.Join(", ", state.CurrentLocation.Connections.Keys)}");
        Console.Write("\nВведите команду (идти [место], взять [предмет], говорить [кто], инвентарь): ");
        // парсер
        string input = Console.ReadLine()?.ToLower() ?? "";
        string[] parts = input.Split(' ', 2); // Разделяем на "команда" и "цель"
        string command = parts[0];
        string target = parts.Length > 1 ? parts[1] : "";

        switch (command)
        {
            case "идти":
                MoveTo(target);
                break;
            case "взять":
                PickUp(target);
                break;
            case "говорить":
                InteractWithEntity(target);
                break;
            case "инвентарь":
                ShowInventory();
                break;
            default:
                Console.WriteLine("Я вас не понял. Попробуйте: идти, взять, говорить или инвентарь.");
                break;
        }
    }
}
// Логика перемещения
private void MoveTo(string target)
{
    if (state.CurrentLocation.Connections.TryGetValue(target, out Location nextLocation))
    {
        // Проверка на темный коридор
        if (nextLocation.IsDark && !state.Flags["light_on"] && !playerInventory.ContainsKey("torch"))
        {
            Console.WriteLine("В коридоре слишком темно! Вы не решились войти без света.");
        }
        else
        {
            state.CurrentLocation = nextLocation;
            state.Turn++;
        }
    }
    else
    {
        Console.WriteLine("Вы не можете туда пойти.");
    }
}
// Логика взаимодействия
private void InteractWithEntity(string target)
{
    if (state.CurrentLocation.Entities.TryGetValue(target, out Entity entity))
    {
        Console.WriteLine($"[{entity.Name}]: {entity.Phrases[0]}"); 
        if (target == "keeper" && HasAllRelics()) 
        {
             Console.WriteLine("Хранитель забирает реликвии и даёт ключ.");
             state.Flags["key_received"] = true;
        }
    }
    else
    {
        Console.WriteLine("Здесь нет никого с таким именем.");
    }
}
private void PickUp(string target)
{
    if (state.CurrentLocation.Objects.TryGetValue(target, out ActionObject item))
    {
        playerInventory.Add(target, item);
        state.CurrentLocation.Objects.Remove(target);
        Console.WriteLine($"Вы подобрали {target}.");
    }
    else {Console.WriteLine("Такого предмета здесь нет.");}
}
}
class Program {
    static void Main() 
    {
        Game myGame = new Game();
        myGame.Init();
        myGame.Run();
    }
}