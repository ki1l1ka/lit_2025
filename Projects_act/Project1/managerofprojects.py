def create():
    with open("notes.txt", "a") as notes:
        print("Введите название заметки")
        name = input()
        print("Напишите заметку")
        body = input()
        notes.write(name + ": " + body + "\n")
        
        
def delete(name):
    n = open("notes.txt")
    app = n.readlines()
    f = True
    for i in range(len(app)):
      if app[i].split(':')[0] == name:
            f = False
            app.pop(i)
            break

    n.close()
    if f:
        print("Таких заметок нет")
        return
    new_n = open("notes.txt", "w")
    for i in range(len(app)):

        new_n.write(app[i])
    print("Заметка удалена")
    
    
def search(name):
    with open("notes.txt") as notes:
        f = True
        app = notes.readlines()
        for i in app:
            if name == i.split(':')[0]:
                print(i[len(i.split(':')[0])+1:])
                return
        print("Таких заметок нет")
                
                
def closing():
    return True
def show(): 
    with open("notes.txt") as notes:
        print(*notes.readlines())
def clear_all():
    with open("notes.txt", 'w') as notes:
        return
def interface():
    print('''Привет, пользователь, это программа Manager of projects. Здесь можно писать заметки''')
    while True:
        print('''Что ты хочешь сделать? 
        1. Создать заметку 
        2. Удалить заметку. 
        3. Найти заметку 
        4. Закрыть заметки 
        5. Показать заметки
        6. Очистить всё
        Для выбора команды, напишите номер команды.''')
        answer = input()
        match answer:
            case "1":
                create()
            case "2":
                print("введите имя заметки")
                n = input()
                delete(n)
            case "3":
                print("введите имя заметки")
                n = input()
                search(n)
            case "4":
                if(closing()):
                    break
            case "5":
                show()
            case "6":
                print("Напишите да, если уверены")
                response = input()
                if response.lower() == "да":
                    clear_all()
                else:
                    continue
            case _:
                print('''Выбери команды от 1 до 6''')
                continue
interface()
#addition
