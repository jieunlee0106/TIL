class Human {
  final String name;
  Human(this.name);
  void sayHello() {
    print('hi ma name is $name');
  }
}

enum Team { red, blue }

// human class 에 있는 모든 것 들을 player class에 넣고 싶어
class Player extends Human {
  final Team team;

  // named argument를 사용하는 생성자 함수 만들기
  Player({
    required this.team,
  })
};

void main() {
  var player = Player(team: Team.red);
}
