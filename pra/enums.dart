enum Team { red, blue }

class Player {
  String name;
  int xp;
  // String team;
  Team team;

  Player({required this.name, required this.xp, required this.team});

  void sayHello() {
    print("hi my name is $name");
  }
}

void main() {
  // ** enums  => Team.red ** //
  var nico = Player(name: 'jiuen', xp: 0, team: Team.red)
    ..name = 'jiwoo'
    ..xp = 100000000
    ..team = Team.blue;
}
