class Player {
  String name;
  int xp;
  String team;

  Player({required this.name, required this.xp, required this.team});

  void sayHello() {
    print("hi my name is $name");
  }
}

void main() {
  // var nico = Player(name: 'jiuen', xp: 0, team: 'red');
  // nico.name = 'jiwoo';
  // nico.xp = 80000000;
  // nico.team = 'redd';

  // ** cascad operator ** //
  var nico = Player(name: 'jiuen', xp: 0, team: 'red')
    ..name = 'jiwoo'
    ..xp = 100000000
    ..team = 'red';
}
