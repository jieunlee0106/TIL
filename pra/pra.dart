class Player {
  final String name;

  Player.fromJson(Map<String, dynamic> playerJson) : name = playerJson['name'];

  void sayHello() {
    print("hi my name is $name");
  }
}

void main() {
  var apiData = [
    {"name": "jieun"},
    {"name": "jiwoo"}
  ];
  apiData.forEach((playerJson) {
    var player = Player.fromJson(playerJson);
    player.sayHello();
  });
}
