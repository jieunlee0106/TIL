class Player {
  final String name;
  int xp;
  String team;

  Player({required this.name, required this.xp, required this.team});

  void sayHello() {
    print("hi my name is $name");
  }
}

void main() {
  var apiData = [
    {"name": "jieun", "xp": 0, "team": "blue"},
    {"name": "jiwoo", "xp": 0, "team": "red"}
  ];
}
