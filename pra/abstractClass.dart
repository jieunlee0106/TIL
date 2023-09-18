abstract class Human {
  // walk 메소드의 시그니처가 무엇인지 정의 => 이 메소드가 반환하는 값잉 뭐냐
  // walk라는 메서드는 void 를 반환해야
  void walk();
}

enum Team { red, blue }

class Player extends Human {
  String name;
  int xp;
  // String team;
  Team team;

  Player({required this.name, required this.xp, required this.team});

  void walk() {
    print('im walk');
  }

  void sayHello() {
    print("hi my name is $name");
  }
}

void main() {}
