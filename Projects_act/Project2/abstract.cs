# Интерфейсы

interface ICommand {
    void ExecuteCommand(GameState state);
}
interface IInteractable {
    void Interact(GameState state);
}
interface ICondition{ 
    bool Condition(GameState state);
}
interface IEffect {
    void ApplyEffect(GameState state);
}

# Абстрактные классы

abstract class CommandBase: ICommand {
    public abstract void ExecuteCommand(GameState state);
}
abstract class ConditionBase: ICondition{
    public abstract bool Condition(GameState state);
}
abstract class EffectBase: IEffect {
    public abstract void ApplyEffect(GameState state);
}
abstract class GameEventBase {}
abstract class Object {
    private string Name;
    private string Description;

}
# Классы

class Game {
}

class GameState {
    private int Turn;
    private int Day;
    private int Time;
    private string CurrentLocation;
    private string[] RoomsAvailable;
    private bool Power;
    private bool Exit;
    public GameState(turn, day, time, roomsavailable, currentlocation, power, exit){
        this.CurrentLocation = currentlocation;
        this.Day = day;
        this.Time = time;
        this.RoomsAvailable = roomsavailable;
        this.Power = power;
        this.Exit = exit;
    }
    public void AddItem(string item){
        this.Player.InventoryAdd(item);
    }
    public void RemoveItem(string item){
        this.Player.InventoryRemove(item);
    }

    // Методы
    public DealDamage(double damage){
        Player.DealDamage(damage);
    }
    public Heal(double amount)
    public bool HasItem(strin Item){
        return Player.Inventory.Contains(Item);
    }
}

class Location {
    private string[] Furniture;
    private bool Dark;
    private int LightLevel;
    private string[] Description;
    private string[] Doors;
    private Location(furniture, light, description, doors, dark) {
        this.Furniture = furniture;
        this.LightLevel = light;
        this.Descripton = description;
        this.Doors = doors;
    }
}

class Furniture: Object {}

class NPC: Object {
    private string[] Inventory;
    private string[] Phrases;
    private NPC(inventory, phrases) {
        this.Inventory = inventory;
        this.Phrases = phrases;
    }
}

class Item: Object {}

class Tool: Item {
    private int Durability;
    private Tool(durability) {
        this.Durability = durability;
    }
}

class Obstacle: Object {}

class Trap: Object {}

class Player {
    private string[] Inventory;
    private int Health;
    private int Sanity;
    private Player(inventory, health, sanity) {
        this.Inventory = inventory;
        this.Health = health;
        this.Sanity = sanity;
    }
    public int Health{
        get;
        set;
    }
    public int Sanity{
        get;
        set;
    }
    public void InventoryAdd(string item){
        this.Inventory.Add(item)
    }
    public void InventoryRemove(string item){
        this.Inventory.Remove(item)
    }
    public void DealDamage(double amount){
        this.Health -= amount
    }
    public void Heal(double amount){
        this.Health += amount
    }
}

// Условия

class HasItemCondition: ConditionBase{
    private string Item;
    public HasItemCondition(string name){
        this.Item = name;
    }
    public override bool Condition(GameState state) {
        return state.HasItem(this.Item);
    }
}

// Эффекты
public class AddItemEffect: EffectBase {
    private string Item;
    public AddItemEffect(string name) {
        this.Item = name;
    }
    public override void ApplyEffect(GameState state) {
        state.AddItem(itemName);
    }
}

public class DamageEffect: EffectBase {
    private double Amount;
    public DamageEffect(double amount){
        this.Amount = amount;
    }
    public override void ApplyEffect(GameState state) {
        state.DealDamage(this.Amount);
    }
}