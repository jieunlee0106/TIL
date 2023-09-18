### pages → controller → service 로 처리되도록 작성

- 리그 오브 레전드 공연 목록을 불러와보자

### service

```dart
import 'package:flutter_dotenv/flutter_dotenv.dart';
import 'package:get/get.dart';

class CategoryService extends GetConnect {
  @override
  void onInit() async {
    await dotenv.load();
    httpClient.baseUrl = "https://k8b207.p.ssafy.io/api ";
    httpClient.timeout = const Duration(microseconds: 5000);
    super.onInit();
  }

  //카테고리 목록 조회 api
  Future<Response> getCategoryList({required String categoryName}) async {
    final response = await get("https://k8b207.p.ssafy.io/api/shows/",
        query: {"category": categoryName});
    return response;
  }
}
```

### controller

```dart
import 'package:get/get.dart';
import 'package:nnz/src/services/category_service.dart';

class CategoryController extends GetxController {
  final CategoryService _categoryService = Get.put(CategoryService());

  final RxList<String> categories = RxList<String>([]);

  Future<void> getCategoryList() async {
    final response =
        await _categoryService.getCategoryList(categoryName: "리그 오브 레전드");
    if (response.status.hasError) {
      // 에러 처리
      print("errorAAAA");
      print(response.request);
    } else {
      categories.value = response.body;
      // 응답 받은 데이터 처리
      print(response.body);
    }
  }
}
```

### pages

```dart
import 'package:flutter/material.dart';
import 'package:get/get.dart';
import 'package:nnz/src/controller/category_controller.dart';

class NotificationPage extends StatelessWidget {
  final categoryController = Get.put(CategoryController());

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: Text("Category List"),
      ),
      body: Center(
        child: ElevatedButton(
          child: Text("Fetch Categories"),
          onPressed: () {
            categoryController.getCategoryList();
          },
        ),
      ),
    );
  }
}
```

![Untitled](https://s3-us-west-2.amazonaws.com/secure.notion-static.com/0b8b08ae-3feb-497b-bfbf-ccaebad1d187/Untitled.png)

→ 페이지에서 버튼을 누르면 카테고리 목록이 불러와지도록 했다. queryString 로 category를 받기에 `query: {"category": categoryName})` 을 작성해주었다

![Untitled](https://s3-us-west-2.amazonaws.com/secure.notion-static.com/a1d80afb-4f32-4bd2-a625-2a1037e5b328/Untitled.png)

![Untitled](https://s3-us-west-2.amazonaws.com/secure.notion-static.com/9474f70f-e7d9-4180-b80f-3d27d07202f7/Untitled.png)

음…error가 뜬다 

```dart
final response = await get(
      "https://k8b207.p.ssafy.io/api/show-service/shows/?category=리그 오브 레전드",
    );
```

url을 하드코딩해보고 

![Untitled](https://s3-us-west-2.amazonaws.com/secure.notion-static.com/109865a5-305f-435a-82ac-36f4e26762db/Untitled.png)

데이터는 Map으로 오는데 현재는 List로 받기로 되어있어서 에러가 발생한 것 같다

```dart
final RxList<String> categories = RxList<String>([]);을

// 수정 
final RxList<String> categories = RxList<_Map<String, dynamic>>([])
```

---

## 에러

```dart
import 'package:get/get.dart';
import 'package:dio/dio.dart';

class CategoryController extends GetxController {
  var categoryList = {}.obs;

  void getCategoryList() async {
    try {
      final response = await Dio().get(
        'https://k8b207.p.ssafy.io/api/show-service/shows',
        queryParameters: {'category': '야구'},
      );

      if (response.statusCode == 200) {
        print(response.data);
        categoryList.value = Map<String, dynamic>.from(response.data);
      }
    } catch (error) {
      print('Error while getting category list: $error');
    }
  }
}
```

`categoryList` 에 값이 할당되지 않는 에러가 계속 발생한다. 

```dart
ErrorLog 

'List<dynamic>' is not a subtype of type 'RxMap<dynamic, dynamic>' of 'function result’
```

---

```dart
import 'package:get/get.dart';
import 'package:dio/dio.dart';

class CategoryController extends GetxController {
  var categoryList = [];

  void getCategoryList() async {
    try {
      final response = await Dio().get(
        'https://k8b207.p.ssafy.io/api/show-service/shows',
        queryParameters: {'category': '축구'},
      );

      if (response.statusCode == 200) {
        categoryList = response.data['content'];
        print('성공');
      }
    } catch (error) {
      print('Error while getting category list: $error');
    }
  }
}

```

- RX를 삭제 했더니 해결 되었다 → 근데 RX를 삭제할 시 바뀐 값을 페이지에 바로 가지고오지 못 할 것 같다..

---

- 코드 리펙토링을 해보자

### service

```dart
import 'package:flutter_dotenv/flutter_dotenv.dart';
import "package:get/get_connect/connect.dart";
import 'package:dio/dio.dart';

class CategoryService extends GetConnect {

  @override
  void onInit() async {
    await dotenv.load();
    // httpClient.baseUrl = dotenv.env['BASE_URL'];
    httpClient.timeout = const Duration(milliseconds: 5000); 
    super.onInit();
  }

  Future<dynamic> getCategoryList({required String categoryName}) async {
    final response = await Dio().get(
      'https://k8b207.p.ssafy.io/api/show-service/shows',
      queryParameters: {'category': categoryName},
    );

    return response; // resposne가 아닌 response로 수정
  }
}
```

### controller

```dart
import 'package:get/get.dart';

import 'package:nnz/src/services/category_service.dart';

class CategoryController extends GetxController {
  var categoryList =[];

  void getCategoryList() async {
    try {
      final data = await CategoryService().getCategoryList(categoryName: '야구');
      // categoryList.assignAll(data.date['content']);
      print(data.data['content']);
      categoryList = data.data['content'];
      // assignAll 메소드는 RxList에 새로운 요소를 추가하고, 리스트를 업데이트합니다.
    } catch (e) {
      print(e);
    }
  }
}
```

### Page

```
import 'package:flutter/material.dart';
import 'package:get/get.dart';
import 'package:nnz/src/controller/category_controller.dart';

class NotificationPage extends StatelessWidget {
  final categoryController = Get.put(CategoryController());

  void test() {
    final ll = categoryController.categoryList;
    print(ll);
    Get.to(() => CategoryListScreen(categoryList: ll));
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: Text("Category List"),
      ),
      body: Center(
        child: ElevatedButton(
          child: Text("Fetch Categories"),
          onPressed: () async {
            await categoryController.getCategoryList();
            test();
          },
        ),
      ),
    );
  }
}

class CategoryListScreen extends StatelessWidget {
  final List<dynamic> categoryList;

  const CategoryListScreen({Key? key, required this.categoryList})
      : super(key: key);

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: Text("Category List"),
      ),
      body: ListView.builder(
        itemCount: categoryList.length,
        itemBuilder: (BuildContext context, int index) {
          final category = categoryList[index];
          return Card(
            child: ListTile(
              title: Text(category['date']),
              subtitle: Text(category['rightTeam'] + category['location']),
            ),
          );
        },
      ),
    );
  }
}
```

=⇒ categoryController.getCategoryList(); 와 test()을 호출 할 때  categoryController.getCategoryList(); 이 비동기라 test가 먼저 호출 되는 경우가 있다.

그렇게되면 전에 할당된 categoryList가 화면에 나타나는 치명적인 오류가있다.

그래서 일단 categoryList값이 바뀐뒤 test()가 호출 되도록 async, await를 주었다…

딜레이가 발생한다 다른 좋은 방법을 생각해보자