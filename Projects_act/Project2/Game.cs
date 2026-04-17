namespace textquest;

public class Game {
    private GameState _state;
    private List<GameEventBase> _events;
    private Dictionary<string, Location> _map;

    public Game(GameState state, List<GameEventBase> events, Dictionary<string, Location> map) {
        _state = state;
        _events = events;
        _map = map;
    }

    public void Run() {
        while (_state.Player.Health > 0 && !_state.flags.ContainsKey("Win")) {
            Location loc = _map[_state.CurrentLocation];
            Console.WriteLine("\nHP: " + _state.Player.Health + " | Локация: " + loc.Name);
            foreach(string s in loc.description) Console.WriteLine(s);

            Console.Write("> ");
            string input = Console.ReadLine().ToLower();
            string[] parts = input.Split(' ');
            string verb = parts[0];

            if (verb == "go" && parts.Length > 1) {
                if (_map.ContainsKey(parts[1])) new GoToCommand(parts[1]).executeCommand(_state);
                else Console.WriteLine("Пути туда нет.");
            }
            else if (verb == "interact" && parts.Length > 1) {
                new InteractCommand(parts[1], loc.objects).executeCommand(_state);
            }

            foreach (GameEventBase ev in _events) ev.tick(_state);
        }
        if (_state.Player.Health <= 0) Console.WriteLine("\nВы погибли...");
    }
}
