class Student
{
    protected string name;
    protected int age;
    protected string group;
    public Student()
    {
        this.name = "Иван";
        this.age = 18;
        this.group = "1";
    }
    public Student(string name, int age, string group)
    {
        this.name = name;
        this.age = age;
        this.group = gtoup;
    }
    public string name{get{return name;} set{this.name = value;}}
    public string age{get{return age;} set{this.age = value;}}
    public string group{get{return group;} set{this.group = value;}}
    public void Study()
    {Console.WriteLine($"Студент по имени {name}, которому {age} лет, учится в группе {group}");}
}
class Master: Student
{
    public void Defend_Diploma(){Console.WriteLine(Console.ReadLine() >= 3? "Защитил успешно": "Не защитил");}
}
class Bachelor: Student
{
    public void Take_Exams(){Console.WriteLine(Console.ReadLine() >= 3? "Сдал": "Не сдал");}
}
class Program{
    static void Main(){
        Student Ivan = new Student();
        Ivan.Study();
        Bachelor Vanya = new Bachelor();
        Vanya.Take_Exams();
        Master Vanka = new Master();
        Vanka.Defend_Diploma();
    }
}