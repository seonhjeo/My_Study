# 클래스

### 클래스의 선언과 객체의 생성
 - 클래스의 선언은 다음과 같이 할 수 있다.
```
Cat cat = new Cat();
```
 - 이 때 `Cat()`은 생성자로, `new`키워드와 합쳐져 새로운 객체를 생성해 `Cat`형식의 `cat`이 참조할 수 있게 해준다.
 - 모든 클래스는 복합 데이터 형식이므로 참조 형식이다. 따라서 `Cat cat`의 `cat`은 값이 할당이 되지 않은 상태이므로 `null`을 가리키게 된다.

### 생성자와 종료자
 - C#의 생성자와 종료자는 C++의 생성자, 소멸자와 동일한 형식을 지닌다.
   - 생성자는 클래스 이름과 동일, 종료자는 클래스 이름 앞에 `~`
 - 생성자도 여느 메소드와 마찬가지로 오버로딩이 가능함.
 - 프로그래머가 생성자를 하나라도 정의하면 C#컴파일러는 매개변수 없는 기본 생성자를 제공하지 않는다.
 - 종료자는 프로그래머가 명시적으로 선언할 수 있지만, CLR의 가비지 컬렉터가 객체의 소멸을 주관하므로 구현 및 사용을 권장하지 않는다.

### 정적 필드와 메소드
 - 필드는 클래스 안에 선언된 변수를 뜻한다. 즉 정적 필드는 `static`키워드로 클래스 자체에 소속시킨 변수를 뜻한다.
 - 정적 필드는 프로그램 전체에 걸쳐 공유해야 하는 변수를 작성할 때 용이하다.
 - 정적 필드와 정적 메소드는 클래스의 인스턴스를 생성하지 않고도 호출이 가능하다.
```
class MyClass
{
  // 정적 필드와 정적 메소드
  public static int a;
  public static void MyFunc() { ... };
}

public static void Main()
{
  MyClass.a = 10;
  MyClass.MyFunc();
}
```

### 객체 복사하기
 - 클래스는 태생이 참조 형식이므로 단순한 등호 형식의 복사는 얕은 복사가 된다.
 - C#에서는 깊은 복사를 자동으로 해 주는 구문이 없으므로 프로그래머가 스스로 만들어야 한다.
 - 깊은 복사 메소드를 구현할거면 `System` 네임스페이스의 `ICloneable`인터페이스를 상속받는 것이 유용하다.

### this
 - 객체가 자신을 지칭할 때 사용하는 키워드로, 객체 내부에서 자신의 필드나 메소드에 접근할 때 this키워드를 사용한다.
 - `this()`생성자는 여러 생성자를 선언할 때 효율적으로 사용할 수 있다.
```
class MyClass
{
  int a, b, c;

  public MyClass() { this.a = 1; }

  public MyClass(int b) : this() { this.b = b; }

  public MyClass(int b, int c) : this(b) { this.c = c; }
}
```

### 상속
 - C#의 상속 또한 C++의 상속과 유사하다.
 - C#에서 파생 클래스의 생성자가 기반 클래스의 생성자에 값을 넘겨주고 싶으면 `base`키워드를 사용하면 된다.
```
class Base {
  public Base(string Name) { ... } // 생성자
  public void BaseMethod() { ... } // 메소드
}

class Derived : Base {
  public Derived(string Name) : base(Name) { ... }
  public void DerivedMethod() {
    base.BaseMethod();
    ...
  }
}
```

### 기반 클래스와 파생 클래스 사이의 형식 변환
 - 기반 클래스와 파생 클래스 사이에서는 족보를 오르내르는 형식 변환이 가능하며, 파생 클래스의 인스턴스는 기반 클래스의 인스턴스로서도 사용할 수 있다. 이는 코드의 생산성을 증가시킨다.
 - C#은 형식 변화를 위해 다음 연산자 두 개를 제공한다.
   - `is` : 객체가 해당 형식에 해당하는지 검사하여 그 결과를 bool값으로 반환한다.
   - `as` : 형식 변환 연산자와 같은 역할을 한다. 다만 형식 변환 연산자가 변환에 실패하는 경우 예외를 던지느 반면 as연산자는 객체 참조를 null로 만든다.
```
// is 사용 예시
Mammal mammal = new Dog();
Dog dog;

if (mammal is Dog) {
  dog = (Dog)mammal;
  dog.Bark();
}
```

```
// as 사용 예시
Mammal mammal = new Cat();
Cat cat = mammal as Cat;
if (cat != null) {
  cat.Mewo();
}
```
 - 일반적으로는 형변환에 실패하더라도 예외 점프가 없는 as연산자의 사용을 권장한다.

### 오버라이딩과 숨기기, 봉인하기
 - `virtual`과 `override`키워드를 이용해 상속된 클래스간의 메소드를 오버라이딩 할 수 있다.
   - `private`으로 선언한 메소드는 오버라이딩할 수 없다.
 - 자식 클래스에 부모 클래스와 이름은 같으나 기능이 다른 메소드를 만들고 싶으면 `new`키워드를 이용해 메소드 숨기기를 할 수 있다.
 - `sealed`키워드를 이용해 메소드가 오버라이딩되지 않도록 봉인할 수 있다. 이 때 봉인은 `virtual`로 선언된 가상 ㅁ소드를 오버라이딩한 버전의 메소드만 가능하다.(자식의 자식만)
```
class Base {
  public virtual void Method1() { ... }
  public void Method2() { ... }
}

class Derived : Base {
  public override void Method1() {
    base.Method1();
    ...
  }
  public new void Method2() { ... }
}
```

### 읽기 전용 필드
 - 상수는 `const`키워드로 선언한다.
 - 읽기 전용 필드는 말 그대로 읽기만 가능한 필드를 말하며, 클래스나 구조체의 멤버로만 존재할 수 있고 생성자 안에서 한 번 값을 지정하면, 그 후로는 값을 변경할 수 없는 것이 특징이다.
 - 읽기 전용 필드는 `readonly`키워드를 이용해 선언할 수 있다.

### 중첩 클래스
 - 클래스 안에 선언되어 있는 클래스를 의미한다. 다음과 같이 선언할 수 있다.
```
class OuterClass {
  class NestedClass { ... }
}
```
 - 중첩 클래스는 자신이 소속된 클래스의 모든 멤버에 자유롭게 접근할 수 있다.
 - 중첩 클래스는 다음과 같은 이유로 사용된다.
   - 클래스 외부에 공개하고 싶지 않은 형식을 만들고자 할 때
   - 현재 클래스의 일부분처럼 표현할 수 있는 클래스를 만들고자 할 때

### 분할 클래스
 - 분할 클래스는 여러번에 나누어 구현하는 클래스를 의미한다.
 - 특별한 기능을 하는 것이 아니며 클래스의 구현이 길어질 경우 여러 파일에 나눠서 구현할 수 있게 함으로서 소스 코드 관리의 편의성을 제공하는 데에 의미가 있다.
 - 컴파일러는 컴파일할 때 분할된 코드를 하나의 클래스로 묶어 컴파일하게 된다.
 - 분할 클래스는 `partial`키워드를 이용해 작성한다.
```
partial class Myclass{
  public void M1() { ... }
}

partial class MyClass {
  public void M2() { ... }
}
```

### 확장 메소드
 - 기존 클래스의 기능을 확장하는 기법이다.
 - 확장 메소드의 선언법은 다음과 같다.
   - 메소드를 선언하되 `static`한정자로 수식해야 한다.
   - 메소드의 첫 번째 매개변수는 반드시 `this`키워드와 함께 확장하고자 하는 클래스 또는 형식의 인스턴스여야 한다.
   - 이후의 매개변수 목록이 실제로 확장 메소드를 호출할 때 입력되는 매개변수이다.
   - 메소드를 가질 클래스를 선언한다. 이 때 선언하는 클래스도 `static`한정자로 수식해야 한다.
```
public static class IntExtention{
  public static int Square(this int myInt) { return myInt * myInt };
  public static int Power(this int myInt, int exponent) { ... return result; }
}

public static void Main() {
  3.Square();
  2.Power(10);
}
```

### 구조체
 - 구조체는 `struct`키워드로 선언하며 문법적으로 클래스와 거의 유사하다.
 - 클래스는 실세계의 객체를 추상화하려는 데 주로 사용되지만, 구조체는 데이터를 담기 위한 자료구조로 주로 사용되기에 은닉성을 비롯한 객체지향의 원칙에 강하게 적용받지 않는다.
 - 구조체와 클래스의 차이는 다음과 같다.
```
특징         클래스            구조체
키워드        class           sturct
형식         참조 형식(힙)      값 형식(스택)
복사         얕은 복사         깊은 복사
인스턴스 생성  new연산자와 생성자  선언만으로 생성
생성자        매개변수 없이 가능  매개변수 없이 불가능
상속         가능             불가능
```

### 구조체에서 상태 변화의 제한
 - 상태의 변화를 허용하는 객체를 변경가능 객체, 상태의 변화를 허용하지 않는 객체를 변경불가능 객체라고 한다.
 - 변경불가능 객체는 프로그램 성능 향상과 버그로 인한 상태의 오염을 막을 수 있는 이점이 있다.
 - 구조체의 모든 필드와 프로퍼티 값을 변경불가능 구조체로 선언하기 위해서는 `readonly`키워드를 사용한다. 이 때 해당 구조체가 속한 모든 필드의 값도 `readonly`로 선언해야 한다.
   - readonly구조체의 값을 변경하고 싶으면 새로운 객체를 만들어야 한다.
```
readonly struct temp {
  public readonly int num;
}
```
 - 또한 `readonly`키워드를 메소드의 형 앞에 추가하면, 해당 메소드가 객체의 필드값을 바꾸려 하는 것을 방지할 수 있다.
```
public readonly double Temp() {
  temp = temp * 1.8; // 컴파일 에러
  return temp;
}
```

### 튜플
 - 튜플도 여러 필드를 담을 수 있는 구조체이나 튜플은 형식 이름이 없다.
 - 따라서 응용 프로그램 전체에서 사용할 형식이 아닌, 즉석에서 사용할 복합 데이터 형식을 선언할 때 적합하다.
 - 튜플은 구조체이므로 값 형식이다.
 - 튜플은 다음과 같이 선언 및 사용한다.
   - 컴파일러가 튜플의 모양을 보고 직접 형식을 결정하도록 `var`키워드로 선언한다.
   - 튜플은 괄호 사이에 두 개 이상의 필드를 지정함으로써 만들어진다.
   - 필드의 이름을 지정하지 않은 튜플을 '명명되지 않은 튜플'이라 부르며 컴파일러가 자동적으로 `Item + 숫자`라는 필드에 담게 된다.
```
var tuple = (123, 789);

Console.WriteLine($"{tuple.Item1}, {tuple.Item2}");
```
 - 필드의 이름을 지정할 수 있는 '명명된 튜플을 선언할 수도 있다.
```
var tuple = (Name:"홍길동", Age:"20");

Console.WriteLine($"{tuple.Name}, {tuple.Age}");
```
 - 튜플을 분해할 수도 있다. 정의할 때와 반대의 형을 취한다.
```
var tuple = (Name:"홍길동", Age:"20");
var (name, age) = tuple;  // 튜플 분해
var (name, _) = tuple;    // 튜플 내의 age 필드 무시

Console.WriteLine($"{name}, {age}");
```
 - 튜플이 분해가 가능한 이유는 분해자를 구현하고 있기 때문이다. 분해자를 구현하고 있는 객체를 분해한 결과를 `switch`의 분기 조건에 활용할 수 있다.
```
var alice = (job: "학생", age: 17);

var discountRate = alice switch {
  ("학생", int n) when n < 18 => 0.2;
  ("학생", _)                 => 0.1;
  ("일반", int n) when n < 18 => 0.1;
  ("일반", _)                 => 0.0;
}
```

 - 명명되지 않은 튜플과 명명된 튜플끼리는 필드의 수와 형식이 같으면 할당이 가능하다.
