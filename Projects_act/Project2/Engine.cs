// интерфейсы

interface iCommand {
    void executeCommand(GameState state);
}
interface iInteractable {
    void interact(GameState state);
}
interface iCondition{ 
    bool condition(GameState state);
}
interface iEffect {
    void applyEffect(GameState state);
}

// абстрактные классы

public abstract class CommandBase: iCommand {
    string name;
    string description;
    public abstract void executeCommand(GameState state);
}
public abstract class ConditionBase: iCondition{
    public abstract bool condition(GameState state);
}
public abstract class EffectBase: iEffect {
    public abstract void applyEffect(GameState state);
}
public abstract class GameEventBase
{
    private string name;
    private iCondition condition;
    private iEffect effect;

    public GameEventBase(string name, iCondition condition, iEffect effect) {
        this.name = name;
        this.condition = condition;
        this.effect = effect;
    }
    public virtual void tick(GameState state) {
        if (this.condition.condition(state)) {
            this.run(state);}
    }
}
public abstract class Object: iInteractable {
    private string name;
    private string description;
    public string name { get; set; }
    public string description { get; set; }
    public interaction(){}

}
// классы

public class Game {
}

public class GameState {
    private int turn;
    private int day;
    private int time;
    private string currentLocation;
    private string[] roomsAvailable;
    private Dictionary<string, bool> flags;
    private Gamer player;
    
    public GameState(int turn, int day, int time, string[] roomsavailable, string currentlocation, Dictionary<string, bool> flags){
        this.currentLocation = currentlocation;
        this.day = day;
        this.time = time;
        this.roomsAvailable = roomsavailable;
        this.flags = flags;
    }
    // свойства
}

public class Location {
    private string[] furniture;
    private bool dark;
    private int lightLevel;
    private string[] description;
    private List<Object> doors;
    private Location(string[] furniture, int light, string[] description, List<Object> doors, bool dark) {
        this.furniture = furniture;
        this.lightLevel = light;
        this.description = description;
        this.doors = doors;
    }
}

public class Furniture: Object {}

public class NPC: Object {
    private string[] inventory;
    private string[] phrases;
    private NPC(string[] inventory, string[] phrases) {
        this.inventory = inventory;
        this.phrases = phrases;
    }
    public void speak(int lineNumber){
        Console.WriteLine($"{this.name}: {this.phrases[lineNumber]}");
    }
}

public class Item: Object {
    private double weight;
    public Item(int weight){
        this.weight = weight;
    }
    public double Weight
    {
        get{return this.weight;}
        set{this.weight = value;}
    }
}

public class Tool: Item {
    private int durability;
    private Tool(int durability) {
        this.durability = durability;
    }
}

public class Obstacle: Object {}

public class Trap: Object {}

public class Gamer {
    private Dictionary<string, Item> inventory;
    private double inventoryCapacity;
    private double health;
    private double healthMax;
    private double sanity;
    private Player(Dictionary<string, Item> inventory, double inventorycapacity, double health, double healthmax, double sanity) {
        this.inventory = inventory;
        this.inventoryCapacity = inventorycapacity;
        this.health = health;
        this.healthMax = healthmax;
        this.sanity = sanity;
    }
    public double inventoryWeight()
    {
        double weight = 0;
        foreach (KeyValuePair elem in this.inventory)
        {
            weight += elem.value.weight;
        }
        return weight;
    }
    public double Health{
        get{return this.health;} // Ошибка: рекурсия
        set{if (value > this.healthMax){this.health = this.healthMax;}
            else if (value < 0){this.health = 0;}
            else {this.health = value;}}
    }
    public double Sanity{
        get{return this.sanity;}
        set{if (value < 0){this.sanity = 0;}
            else {this.sanity = value;}}
    }
    public Dictionary<string, Item> Inventory
    {
        get{return this.inventory;}
        set{this.inventory = value;}
    }
    public Gamer Player
    {
        get{return this.player;}
        set{this.player = value;}
    }
}

// условия

public class HasItemCondition: ConditionBase{
    private string item;
    public HasItemCondition(string name){
        this.item = name;
    }
    public override bool condition(GameState state) {
        return state.hasItem(this.item);
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
        return (state.health > 5);
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

public class AndCondition: ConditionBase{
    ConditionBase cond1;
    ConditionBase cond2;
    public AndCondition(iCondition cond1, iCondition cond2) {
        this.cond1 = cond1;
        this.cond2 = cond2;
    }
    public override bool condition(GameState state){
        return (cond1.condition(state) && cond2.condition(state));
    }
}
public class OrCondition: ConditionBase
{
    ConditionBase cond1;
    ConditionBase cond2;
    public OrCondition(iCondition cond1, iCondition cond2) {
        this.cond1 = cond1;
        this.cond2 = cond2;
    }
    public override bool condition(GameState state){
        return (cond1.condition(state) || cond2.condition(state));
    }
}
public class NotCondition: ConditionBase{    
    ConditionBase cond1;
    public NotCondition(iCondition cond1) {
        this.cond1 = cond1;
    }
    public override bool condition(GameState state){
        return !(cond1.condition(state));
    }}

// эффекты
public class AddItemEffect: EffectBase {
    private string itemName;
    private Item item;
    public AddItemEffect(string name, Item item) {
        this.item = item;
        this.itemName = name;
    }
    public override void applyEffect(GameState state) {
        state.Player.inventory.Add(this.itemName, this.item);
    }
}

public class RemoveItemEffect: EffectBase {
    private string itemName;
    public RemoveItemEffect(string name) {
        this.itemName = name;
    }
    public override void applyEffect(GameState state) {
        state.Player.inventory.Remove(this.itemName);
    }
}

public class DamageEffect: EffectBase {
    private double amount;
    public DamageEffect(double amount){
        this.amount = amount;
    }
    public override void applyEffect(GameState state)
    {
        state.Health = state.Health - this.amount;
    }
}

public class HealEffect: EffectBase{
    private double amount;
    public HealEffect(double amount){
        this.amount = amount;
    }
    public override void applyEffect(GameState state)
    {
        state.Health = state.Health + this.amount;
    }
}

public class SanityDownEffect: EffectBase {
    private double amount;
    public SanityDownEffect(double amount){
        this.amount = amount;
    }
    public override void applyEffect(GameState state)
    {
        state.Player.Sanity = state.Player.Sanity - this.amount;
    }
}

public class SanityUpEffect: EffectBase{
    private double amount;
    public SanityUpEffect(double amount){
        this.amount = amount;
    }
    public override void applyEffect(GameState state)
    {
        state.Player.Sanity = state.Player.Sanity + this.amount;
    }
}
// события

public class SanityEvent: GameEventBase{
}

// локации

public class Room: Location{
    private string[] npcs;
    public Room(string[] npcs){ // Ошибка: у Location нет конструктора без параметров
        this.npcs = npcs;
    }
}

public class Corridor: Location{}
