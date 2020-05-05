# 🐶Animal Swipe Defence🐱

![0](https://user-images.githubusercontent.com/31693348/81091380-1f0c5b00-8f3a-11ea-9ef4-57be6bfa1ccc.png)

### 게임소개

Animal Swipe Defence(애니멀 스와이프 디펜스)는 간단한 스와이프 동작을 이용하여 동물들을 생산, 합체 시켜나가는 퍼즐 디펜스 게임입니다.



### 게임 구조

**Managers**

![1](https://user-images.githubusercontent.com/31693348/81091324-08660400-8f3a-11ea-9f4c-0d1987a90b1e.png)

[delegate Action을 이용한 Manager간 의존성 완화](https://github.com/JuicyPark/ASD_Refactoring/tree/develop/Assets/Scripts/Manager)

> GameManager가 다양한 Manager에서 발생하는 이벤트들을 서로 매핑시켜주어 Manager간 의존을 하지 않습니다.



**Stage Data**

![2](https://user-images.githubusercontent.com/31693348/81091360-14ea5c80-8f3a-11ea-933b-19aa11757878.png)

[ScriptableObject를 이용하여 Stage 생성](https://github.com/JuicyPark/ASD_Refactoring/blob/develop/Assets/Scripts/Data/EnemyData.cs)

> 스테이지들을  ScriptableObject로 만들어 두어 수정 및 관리가 용이합니다. 이 데이터들은 StageManager에서 해당 레벨에 맞는 Stage Enemy들을 불러와줍니다.



**Unit, Enemy**

![6](https://user-images.githubusercontent.com/31693348/81091402-27649600-8f3a-11ea-9bd5-34b3e390a029.png)

[플레이어가 생성하는 Unit](https://github.com/JuicyPark/ASD_Refactoring/blob/develop/Assets/Scripts/Unit.cs)

[스테이지에 등장하는 Enemy](https://github.com/JuicyPark/ASD_Refactoring/blob/develop/Assets/Scripts/Enemy.cs)



### Refactoring ISSUE

- Lobby
  - UI
  - Sound
- Field
  - UI
  - Input
    - Swipe
  - InGame
    - Spawn
    - Resources
    - Fight
  - Sound
- Result
  - UI
  - Sound



### Improvements

- Firebase Ranking
- Stage Buffer