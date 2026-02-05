abstract class Character: IDamagable{
    string Name;
    double Health;
    public abstract double Attack();
    public void Move()
    {
        Console.Writeline("Вы подвинулись")
    }
    public int TakeDamage(int damage){
        return damage;
    }
}
class Warrior: Character{
    public Warrior(){
        this.Name = "warrior";
        this.Health = 100
    }
    public Warrior(name){
        this.Name = name;
        this.Health = 100
    }
    public override double Attack(){
        Console.Writeline("Нанесён урон");
        return10;
    }
}
class Mage: Character{
    public Mage(){
        this.Name = "mage";
        this.Health = 80;
    }
    public Mage(name, health){
        this.Name = name;
        this.Health = health;
    }
    public override double Attack(){
        Console.Writeline("Нанесён магический урон");
        return 20;
    }
}
Main{
    Character[] chars = [new Warrior(); new Mage()]
    foreach (i in chars){
        Console.Writeline(chars[i].Attack());
    }
}
interface IDamageable {
    public Take Damage(int damage){}}
class Cleric: Character {
    double Healing;

    public Cleric(){
        this.Name = "Cleric";
        this.Health = 50;
        this.Healing = 10;
}

    public Cleric(name, health, Healing) {
        this.Name = name;
        this.Health = health;
        this.Healing = Healing; }


    public override double Attack() {
        this.Healing = this.Healing * 1.25
        return -10 - this.Healing
}}