using System;
using System.Collections.Generic;

namespace textquest;

class Program {
    static void Main() {
        Dictionary<string, Location> map = new Dictionary<string, Location>();
        Location hall = new Location(10, new string[]{"Холл"}, false);
        hall.Name = "Холл";
        map.Add("hall", hall);

        List<string> rooms = new List<string> { "hall" };
        Dictionary<string, bool> flags = new Dictionary<string, bool>();
        Gamer player = new Gamer(new Dictionary<string, Item>(), 20, 100, 100, 100);
        
        GameState state = new GameState(0, 1, 12, rooms, "hall", flags, player);
        List<GameEventBase> events = new List<GameEventBase>();

        Game game = new Game(state, events, map);
        game.Run();
    }
}
