// namespace textquest;
// // интерфейсы

// interface iCommand {
//     void executeCommand(GameState state);
// }
// interface iInteractable {
//     void interact(GameState state);
// }
// interface iCondition{ 
//     bool condition(GameState state);
// }
// interface iEffect {
//     void applyEffect(GameState state);
// }

// // абстрактные классы

// public abstract class CommandBase: iCommand {
//     string name;
//     string description;
//     public abstract void executeCommand(GameState state);
// }
// public abstract class ConditionBase: iCondition{
//     public abstract bool condition(GameState state);
// }
// public abstract class EffectBase: iEffect {
//     public abstract void ApplyEffect(GameState state);
// }
// public abstract class GameEventBase
// {
//     private string name;
//     private iCondition condition;
//     private iEffect effect;

//     public GameEventBase(string name, iCondition condition, iEffect effect) {
//         this.name = name;
//         this.condition = condition;
//         this.effect = effect;
//     }
//     public virtual void tick(GameState state) {
//         if (this.condition.condition(state)) {
//             this.run(state);}
//     }
// }
// public abstract class GameObject: iInteractable {
//     private string name;
//     private string description;
//     public GameObject(string name, string description){
//         this.name = name;
//         this.description = description;
//     }
//     public string Name {get{return this.name;}}
//     public string Description {get{return this.description;}}
//     public interaction(){}

// }
// // классы

// public class GameState {
//     private int turn;
//     private int day;
//     private int time;
//     private string currentLocation;
//     private string[] roomsAvailable;
//     private Dictionary<string, bool> flags;
//     private Gamer player;
//     private Entity entities;
    
//     public GameState(int turn, int day, int time, string[] roomsavailable, string currentlocation, Dictionary<string, bool> flags){
//         this.currentLocation = currentlocation;
//         this.day = day;
//         this.time = time;
//         this.roomsAvailable = roomsavailable;
//         this.flags = flags;
//     }

//     public Gamer Player
//     {
//         get{return this.player;}
//         set{this.player = value;}
//     }
//     public Entity Entities
//     {
//         get{return this.entities;}
//         set{this.entities = value;}
//     }
//     public List<string> roomsAvailable{get; set;}
//     public string CurrentLocation {get; set;}
// }

// public class Location {
//     private bool dark;
//     private int lightLevel;
//     private string[] description;
//     private Dictionary<string, Location> connections;
//     public Location(int light, string[] description, Dictionary<string, GameObject> connections, bool dark) {
//         this.lightLevel = light;
//         this.description = description;
//         this.connections = connections;
//         this.dark = dark;
//     }
// }

// public class Entity: GameObject {
//     private Dictionary<string, Item> inventory;
//     private string[] phrases;
//     public Entity(string[] inventory, string[] phrases) {
//         this.inventory = inventory;
//         this.phrases = phrases;
//     }
//     public Dictionary<string, iItem> Inventory{
//         get{return this.inventory;}
//         set{this.inventory = value;}
//     }
//     public void Speak(int lineNumber){
//         Console.WriteLine($"{this.name}: {this.phrases[lineNumber]}");
//     }
// }

// public class Item: GameObject {
//     private double weight;
//     public Item(int weight){
//         this.weight = weight;
//     }
//     public double Weight
//     {
//         get{return this.weight;}
//         set{this.weight = value;}
//     }
// }

// public class Obstacle: GameObject {
//     private int level;
//     public Obstacle(string name, string description, int level){
//         this.name = name;
//         this.description = description;
//         this.level = level;
//     }
// }

// public class Trap: GameObject {
//     private DamageEffect damage;
//     public Trap(string name, string description, DamageEffect damage){
//         this.name = name;
//         this.description = description;
//         this.damage = damage;
//     }
//     public void Damage(GameState state){
//         damage.ApplyEffect(state);
//     }
// }

// public class Gamer {
//     private Dictionary<string, Item> inventory;
//     private double inventoryCapacity;
//     private double health;
//     private double healthMax;
//     private double sanity;
//     public Gamer(Dictionary<string, Item> inventory, double inventorycapacity, double health, double healthmax, double sanity) {
//         this.inventory = inventory;
//         this.inventoryCapacity = inventorycapacity;
//         this.health = health;
//         this.healthMax = healthmax;
//         this.sanity = sanity;
//     }
//     public double InventoryWeight()
//     {
//         double weight = 0;
//         foreach (KeyValuePair elem in this.inventory)
//         {
//             weight += elem.value.weight;
//         }
//         return weight;
//     }
//     public double Health{
//         get{return this.health;}
//         set{if (value > this.healthMax){this.health = this.healthMax;}
//             else if (value < 0){this.health = 0;}
//             else {this.health = value;}}
//     }
//     public double Sanity{
//         get{return this.sanity;}
//         set{if (value < 0){this.sanity = 0;}
//             else {this.sanity = value;}}
//     }
//     public Dictionary<string, Item> Inventory
//     {
//         get{return this.inventory;}
//         set{this.inventory = value;}
//     }
// }

// //Команды
// public class GoToCommand: CommandBase {
//     private string direction;

//     public GoToCommand(string direction) {
//         this.direction = direction;
//     }
//     public override void executeCommand(GameState state) {
//         state.CurrentLocation = direction;
//     }
// }
// public class InteractCommand: CommandBase {
//     private GameObject target;

//     public InteractCommand(GameObject target) {
//         this.target = target;
//     }
//     public override void executeCommand(GameState state)
//     {}
// }
// // условия

// public class AndCondition: ConditionBase{
//     ConditionBase cond1;
//     ConditionBase cond2;
//     public AndCondition(iCondition cond1, iCondition cond2) {
//         this.cond1 = cond1;
//         this.cond2 = cond2;
//     }
//     public override bool Condition(GameState state){
//         return (cond1.Condition(state) && cond2.Condition(state));
//     }
// }
// public class OrCondition: ConditionBase
// {
//     ConditionBase cond1;
//     ConditionBase cond2;
//     public OrCondition(iCondition cond1, iCondition cond2) {
//         this.cond1 = cond1;
//         this.cond2 = cond2;
//     }
//     public override bool Condition(GameState state){
//         return (cond1.Condition(state) || cond2.Condition(state));
//     }
// }
// public class NotCondition: ConditionBase{    
//     ConditionBase cond1;
//     public NotCondition(iCondition cond1) {
//         this.cond1 = cond1;
//     }
//     public override bool Condition(GameState state){
//         return !(cond1.Condition(state));
//     }}
// public class HasItemCondition: ConditionBase{
//     private string item;
//     public HasItemCondition(string name){
//         this.item = name;
//     }
//     public override bool Condition(GameState state) {
//         return (state.Player.Inventory.ContainsKey(item));
//     }
// }

// public class IsSaneCondition: ConditionBase{
//     private double sanity;
//     public IsSaneCondition(double sanity){
//         this.sanity = sanity;
//     }
//     public override bool Condition(GameState state){
//         return (state.Player.Sanity > this.sanity); 
//     }
// }

// public class IsHealthyCondtion: ConditionBase{
//     private double health;
//     public IsHealthyCondtion(double health){
//         this.health = health;
//     }
//     public override bool Condition(GameState state){
//         return (state.Player.Health > 5);
//     }
// }

// public class IsFlagCondition: ConditionBase{
//     private string flag;
//     public IsFlagCondition(string flag){
//         this.flag = flag;
//     }
//     public override bool Condition(GameState state){
//         return state.Flags[this.flag]; 
//     }
// }
// public class CanEnterCondition: ConditionBase{
//     private string roomName;
//     public IsFlagCondition(string roomName){
//         this.roomName = roomName;
//     }
//     public override bool Condition(GameState state){
//         return (state.roomsAvailable.Contains(this.roomName) && state.CurrentLocation.Connections.Contains(this.roomName)); 
//     }
// }

// // эффекты
// public class AddItemEffect: EffectBase {
//     private string itemName;
//     private Item item;
//     public AddItemEffect(string name, Item item) {
//         this.item = item;
//         this.itemName = name;
//     }
//     public override void ApplyEffect(GameState state) {
//         state.Player.Inventory = state.Player.Inventory.Add(this.itemName, this.item);
//     }
//     public override void ApplyEffect(GameState state){
//         state.Entities.Inventory = state.Entities.Inventory(this.itemName, this.item);
//     }
// }

// public class RemoveItemEffect: EffectBase {
//     private string itemName;
//     public RemoveItemEffect(string name) {
//         this.itemName = name;
//     }
//     public override void ApplyEffect(GameState state) {
//         state.Player.inventory.Remove(this.itemName);
//     }
// }

// public class DamageEffect: EffectBase {
//     private double amount;
//     public DamageEffect(double amount){
//         this.amount = amount;
//     }
//     public override void ApplyEffect(GameState state)
//     {
//         state.Player.Health = state.Player.Health - this.amount;
//     }
//     public override void ApplyEffect(GameState state){
//         state.Entities.Health = state.Entities.Health - this.amount;
//     }
// }

// public class HealEffect: EffectBase{
//     private double amount;
//     public HealEffect(double amount){
//         this.amount = amount;
//     }
//     public override void ApplyEffect(GameState state)
//     {
//         state.Player.Health = state.Player.Health + this.amount;
//     }
// }

// public class TemporaryDamageEffect: EffectBase{
//     private double time;
//     private DamageEffect damage;
//     public TemporaryDamageEffect(double amount, double time){
//         this.damage = new DamageEffect(amount);
//         this.time = time;
//     }
//     public override ApplyEffect(GameState state){
//         //
//     }
// }

// public class SanityDownEffect: EffectBase {
//     private double amount;
//     public SanityDownEffect(double amount){
//         this.amount = amount;
//     }
//     public override void ApplyEffect(GameState state)
//     {
//         state.Player.Sanity = state.Player.Sanity - this.amount;
//     }
// }

// public class SanityUpEffect: EffectBase{
//     private double amount;
//     public SanityUpEffect(double amount){
//         this.amount = amount;
//     }
//     public override void ApplyEffect(GameState state)
//     {
//         state.Player.Sanity = state.Player.Sanity + this.amount;
//     }
// }
// // события
// public class RandomSanityEvent: GameEventBase {
//     private EffectBase effect;
//     private ConditionBase condition;
//     private string description;
//     public WorldEvent(string description, ConditionBase condition, EffectBase effect){
//         this.condition = condition;
//         this.effect = effect;
//         this.description = description;
//     }
//     public override void tick(GameState state) {
//         if (condition.condition(state)) {
//             effect.applyEffect(state);
//         }
//     }
// }

// // локации

// public class Corridor: Location{
//     private double difficulty;
//     public Corridor(double difficullty, int light, string[] description, Dictionary<string, Location> doors, bool dark)
//     {
//         this.difficullty = difficullty;
//         this.lightLevel = light;
//         this.description = description;
//         this.connections = connections;
//         this.dark = dark;
//     }
//     public double Difficullty{
//         get{return this.difficullty;}
//         set{this.difficullty = value;}
//     }
// }

// public class LivingRoom: Location{
//     private Dictionary<string, Entity> furniture;
//     public LivingRoom(Dictionary<string, Entity> furniture, int light, string[] description, Dictionary<string, Location> connections, bool dark){
//         this.furniture = furniture;
//         this.lightLevel = light;
//         this.description = description;
//         this.connections = connections;
//         this.dark = dark;
//     }
//     public Dictionary<string, GameObject> Furniture{
//         get{return this.furniture;}
//         set{this.furniture = value;}
//     }
// }

// public class Hall: Location{
//     private Dictionary<string, Entity> people;
//     public Hall(int light, string[] description, Dictionary<string,GameObject> connections, bool dark, Dictionary<string, Entity> people) {
//         this.lightLevel = light;
//         this.description = description;
//         this.connections = connections;
//         this.dark = dark;
//         this.people = people;
//     }
//     public Dictionary<string, Entity> People{
//         get{return this.people;}
//         set{this.people = value;}
//     }
// }

// // Дополнение к условиям
// public class IsAtLocationCondition : ConditionBase {
//     private string _locationName;
//     public IsAtLocationCondition(string locationName) { _locationName = locationName; }
//     public override bool condition(GameState state) { return state.CurrentLocation == _locationName; }
// }

// // Реализация события (исправлено: запуск эффекта через run)
// public class WorldEvent : GameEventBase {
//     private iEffect _effect;
//     public WorldEvent(string name, iCondition cond, iEffect eff) : base(name, cond, eff) {
//         _effect = eff;
//     }
//     // В базовом классе вызывается run, реализуем его
//     public void run(GameState state) {
//         _effect.applyEffect(state);
//     }
//     public override void tick(GameState state) {
//         // Если условие верно, применяем эффект
//         if (base.condition_ref.condition(state)) {
//             this.run(state);
//         }
//     }
// }

// // Дополнение к эффектам
// public class DamageEffect : EffectBase {
//     private double _amount;
//     public DamageEffect(double amount) { _amount = amount; }
//     public override void ApplyEffect(GameState state) {
//         state.Player.Health -= _amount;
//         Console.WriteLine("!!! Темнота пугает вас. Вы теряете " + _amount + " HP.");
//     }
// }

// public class WinEffect : EffectBase {
//     public override void ApplyEffect(GameState state) {
//         state.Flags["Escaped"] = true;
//         Console.WriteLine("ПОЗДРАВЛЯЕМ! Вы выбрались из отеля!");
//     }
// }
using System;
using System.Collections.Generic;
using System.Linq;

namespace textquest;

// ИНТЕРФЕЙСЫ
interface iCommand { void executeCommand(GameState state); }
interface iInteractable { void interact(GameState state); }
interface iCondition { bool condition(GameState state); }
interface iEffect { void applyEffect(GameState state); }

// АБСТРАКТНЫЕ КЛАССЫ
public abstract class CommandBase : iCommand {
    public string name;
    public string description;
    public abstract void executeCommand(GameState state);
}

public abstract class ConditionBase : iCondition {
    public abstract bool condition(GameState state);
}

public abstract class EffectBase : iEffect {
    public abstract void applyEffect(GameState state);
}

public abstract class GameEventBase {
    protected string name;
    protected iCondition condition;
    protected iEffect effect;

    public GameEventBase(string name, iCondition condition, iEffect effect) {
        this.name = name;
        this.condition = condition;
        this.effect = effect;
    }

    public virtual void tick(GameState state) {
        if (this.condition.condition(state)) {
            this.run(state);
        }
    }

    // Добавлена реализация метода run, который вызывается в tick
    public virtual void run(GameState state) {
        if (this.effect != null) this.effect.applyEffect(state);
    }
}

public abstract class GameObject : iInteractable {
    protected string name;
    protected string description;
    public List<iEffect> interactionEffects = new List<iEffect>(); // Список эффектов для взаимодействия

    public GameObject(string name, string description) {
        this.name = name;
        this.description = description;
    }

    public string Name { get { return this.name; } }
    public string Description { get { return this.description; } }

    public virtual void interact(GameState state) {
        foreach (iEffect eff in interactionEffects) {
            eff.applyEffect(state);
        }
    }
}

// КЛАССЫ СОСТОЯНИЯ
public class GameState {
    public int turn;
    public int day;
    public int time;
    public string currentLocation;
    public List<string> roomsAvailable; // Исправлено: только один список
    public Dictionary<string, bool> flags;
    public Gamer player;
    public Entity entities;

    public GameState(int turn, int day, int time, List<string> roomsavailable, string currentlocation, Dictionary<string, bool> flags) {
        this.currentLocation = currentlocation;
        this.day = day;
        this.time = time;
        this.roomsAvailable = roomsavailable;
        this.flags = flags;
    }

    public Gamer Player { get { return this.player; } set { this.player = value; } }
    public Entity Entities { get { return this.entities; } set { this.entities = value; } }
    public string CurrentLocation { get { return this.currentLocation; } set { this.currentLocation = value; } }
}

public class Location {
    public bool dark;
    public int lightLevel;
    public string[] description;
    public Dictionary<string, Location> connections;
    public List<GameObject> objects = new List<GameObject>(); // Список объектов в комнате

    public Location(int light, string[] description, Dictionary<string, Location> connections, bool dark) {
        this.lightLevel = light;
        this.description = description;
        this.connections = connections;
        this.dark = dark;
    }
}

public class Entity : GameObject {
    public Dictionary<string, Item> inventory;
    public string[] phrases;

    public Entity(string name, string description, Dictionary<string, Item> inventory, string[] phrases) : base(name, description) {
        this.inventory = inventory;
        this.phrases = phrases;
    }

    public void Speak(int lineNumber) {
        Console.WriteLine(this.name + ": " + this.phrases[lineNumber]);
    }
}

public class Item : GameObject {
    public double weight;
    public Item(string name, string description, double weight) : base(name, description) {
        this.weight = weight;
    }
}

public class Obstacle : GameObject {
    public int level;
    public Obstacle(string name, string description, int level) : base(name, description) {
        this.level = level;
    }
}

public class Trap : GameObject {
    public DamageEffect damage;
    public Trap(string name, string description, DamageEffect damage) : base(name, description) {
        this.damage = damage;
    }
    public void Damage(GameState state) {
        damage.applyEffect(state);
    }
}

public class Gamer {
    public Dictionary<string, Item> inventory;
    public double inventoryCapacity;
    public double health;
    public double healthMax;
    public double sanity;

    public Gamer(Dictionary<string, Item> inventory, double inventorycapacity, double health, double healthmax, double sanity) {
        this.inventory = inventory;
        this.inventoryCapacity = inventorycapacity;
        this.health = health;
        this.healthMax = healthmax;
        this.sanity = sanity;
    }

    public double InventoryWeight() {
        double weight = 0;
        foreach (KeyValuePair<string, Item> elem in this.inventory) {
            weight += elem.Value.weight;
        }
        return weight;
    }

    public double Health {
        get { return this.health; }
        set {
            if (value > this.healthMax) this.health = this.healthMax;
            else if (value < 0) this.health = 0;
            else this.health = value;
        }
    }

    public double Sanity {
        get { return this.sanity; }
        set {
            if (value < 0) this.sanity = 0;
            else this.sanity = value;
        }
    }

    public Dictionary<string, Item> Inventory { get { return this.inventory; } set { this.inventory = value; } }
}

// КОМАНДЫ
public class GoToCommand : CommandBase {
    private string direction;
    public GoToCommand(string direction) { this.direction = direction; }
    public override void executeCommand(GameState state) {
        state.CurrentLocation = direction;
        Console.WriteLine("Вы перешли в " + direction);
    }
}

public class InteractCommand : CommandBase {
    private string targetName;
    private List<GameObject> currentObjects;

    public InteractCommand(string name, List<GameObject> objects) {
        this.targetName = name;
        this.currentObjects = objects;
    }

    public override void executeCommand(GameState state) {
        foreach (GameObject obj in currentObjects) {
            if (obj.Name.ToLower() == targetName.ToLower()) {
                obj.interact(state);
                return;
            }
        }
        Console.WriteLine("Я не вижу здесь " + targetName);
    }
}

// УСЛОВИЯ
public class AndCondition : ConditionBase {
    iCondition cond1; iCondition cond2;
    public AndCondition(iCondition cond1, iCondition cond2) { this.cond1 = cond1; this.cond2 = cond2; }
    public override bool condition(GameState state) { return (cond1.condition(state) && cond2.condition(state)); }
}

public class NotCondition : ConditionBase {
    iCondition cond1;
    public NotCondition(iCondition cond1) { this.cond1 = cond1; }
    public override bool condition(GameState state) { return !(cond1.condition(state)); }
}

public class HasItemCondition : ConditionBase {
    private string item;
    public HasItemCondition(string name) { this.item = name; }
    public override bool condition(GameState state) { return (state.Player.Inventory.ContainsKey(item)); }
}

public class IsAtLocationCondition : ConditionBase {
    private string loc;
    public IsAtLocationCondition(string loc) { this.loc = loc; }
    public override bool condition(GameState state) { return state.CurrentLocation == loc; }
}

// ЭФФЕКТЫ
public class AddItemEffect : EffectBase {
    private string itemName; private Item item;
    public AddItemEffect(string name, Item item) { this.itemName = name; this.item = item; }
    public override void applyEffect(GameState state) {
        if (!state.Player.Inventory.ContainsKey(itemName)) {
            state.Player.Inventory.Add(itemName, item);
            Console.WriteLine("Вы получили: " + itemName);
        }
    }
}

public class DamageEffect : EffectBase {
    private double amount;
    public DamageEffect(double amount) { this.amount = amount; }
    public override void applyEffect(GameState state) {
        state.Player.Health -= amount;
        Console.WriteLine("ТЕМНОТА! Вы теряете " + amount + " ОЗ.");
    }
}

public class WinEffect : EffectBase {
    public override void applyEffect(GameState state) {
        state.flags["Win"] = true;
        Console.WriteLine("\nВы нашли выход!");
    }
}
