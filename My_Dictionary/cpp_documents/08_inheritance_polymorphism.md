# 8-1 객체 포인터의 참조관계
 - 챕터 7에서 배운 IS-A관계에 초점을 맞춰 내용을 전개한다.

## 객체 포인터의 참조관계
```
Person *ptr;        // 포인터 변수 선언
ptr = new Person(); // 포인터 변수의 객체 참조
```
 - 위의 예시처럼 클래스를 기반으로도 포인터 변수를 선언할 수 있다. 그런데 위의 Person형 포인터는 Person객체뿐만 아니라 Person을 상속하는 유도 클래스의 객체도 가리킬 수 있다.
 - **c++에서 AAA형 포인터 변수는 AAA객체 또는 AAA를 직접 혹은 간접적으로 상속하는 모든 객체를 가리킬 수 있다.(객체의 주소값을 가리킬 수 있다.)**
   - 이는 IS-A 관계에 의해 '유도 클래스는 기초 클래스의 일종이다'로 간주할 수 있기 때문이다.
 - 유도 클래스에서 기초 클래스의 함수와 동일한 이름과 매개변수를 갖는 새 함수를 작성할 수 있다. 이를 함수 오버라이딩이라 한다.
   - 함수 오버라이딩 시 유도 클래스에서 실행되는 함수 중 기초 클래스의 함수는 유도 클래스의 오버라이딩 된 함수에 가려지게 된다.
   - 기초 클래스의 이름공간을 불러 사용하면 오버라이딩 되기 이전의 함수를 사용할 수 있다. (ex. `BaseClass::Function();`)
   - 이름이 같아도 매개변수가 다르면 오버라이딩이 아닌 오버로딩이 된다.
 - 기초 클래스 내의 함수와 비슷한 일을 하지만, 유도 클래스의 멤버를 사용해야 할 때 유도 클래스에 함수 오버라이딩을 실시한다.


# 가상함수(Virtual Function)

## 객체 포인터 참조 심화
 - 다음과 같은 상황에서는 컴파일 오류가 난다.
```
Base *ptr = new Derived();  // 기초 클래스 포인터에 유도 클래스 선언
ptr->DerivedFunc();         // 포인터를 이용해 유도 클래스 내의 함수 실행
```
 - **컴파일러는 포인터 연산의 가능성 여부를 판단할 때 실제 가리키는 객체의 자료형이 아닌 포인터의 자료형을 기준으로 판단한다.**
 - 위의 예시에서는 ptr이 Base형일 가능성이 있기 때문에 컴파일러에서 함수 실행을 사전 차단하는 것이다.
 - 다음과 같은 상황은 정상적으로 실행된다.
```
Derived *dptr = new Derived();  // 유도 클래스 선언
Base *bptr = dptr;              // 기초 클래스 포인터에 유도 클래스 주소 저장
```
 - 위의 예시에서는 dptr이 Base클래스를 직접 혹은 간접적으로 상속하는 객체이기 때문에 Base형으로 참조가 가능하다.
 - **결과적으로 객체 포인터에 대해 포인터 형에 해당하는 클래스에 정의된 멤버에만 접근이 가능하다.**

## 함수 오버라이딩과 포인터형
```
class First {
public: void MyFunc() { ... }; }

class Second : public First {
public: void MyFunc() { ... }; }

class Third : public Second {
public: void MyFunc() { ... }; }

int main()
{
    Third * tptr=new Third();
	Second * sptr=tptr;
	First * fptr=sptr;

	fptr->MyFunc();             // First클래스의 MyFunc() 함수 실행
	sptr->MyFunc();             // Second클래스의 MyFunc() 함수 실행
	tptr->MyFunc();             // Third클래스의 MyFunc() 함수 실행
}
```
 - 위의 예시 `fptr->MyFunc();`에서 fptr은 First클래스형이기 때문에 이 포인터가 가리키는 객체는 First클래스에 정의된 MyFunc()함수는 무조건 호출할 수 있다는 컴파일러의 판단에 의해 First클래스의 함수가 호출되게 된다.
 - 또한 위의 예시 `sptr->MyFunc();`에서 sptr은 Second형 포인터이니 이 포인터가 가리키는 객체는 First의 MyFunc함수와 Second의 MyFunc함수가 오버라이딩 관계로 존재하기 때문에 오버라이딩한 Second의 MyFunc함수를 호출하게 된다.

## 가상함수(Virtual Function)
 - 함수를 오버라이딩했다는 것은 해당 객체에서 호출되어야 할 함수를 바꾼다는 의미인데, 포인터 변수의 자료형에 따라 호출되는 함수의 종류가 달라지는 것은 문제가 있다.
   - 이러한 상황을 방지하기 위해 가상함수라는 것을 제공하게 된다.
 - 가상함수는 다음과 같이 선언된다.
```
class First {
public: virtual void MyFunc() { ... }; }    // virtual 키워드로 가상함수 선언
```
 - 가상함수가 선언되면 해당 함수를 오버라이딩하는 함수도 가상함수가 된다.
 - 함수가 가상함수로 선언되면 해당 함수호출시 포인터의 자료형을 기반으로 호출대상을 결정하지 않고, 포인터 변수가 실제로 가리키는 객체를 참조하여 호출의 대상을 결정한다.

## 상속의 이유
 - 상속을 통해 연관된 일련의 클래스에 대해 공통적인 규약을 정의할 수 있다.
   - 모든 유도 클래스는 기초 클래스의 자료형으로 바라볼 수 있다.

## 순수 가상함수와 추상 클래스, 그리고 인터페이스
 - 클래스 중에는 객체생성을 목적으로 정의되지 않는 클래스도 존재한다. 이러한 클래스의 객체가 생성되면 이는 프로그래머의 실수가 된다.
 - 따라서 위의 실수를 막기 위해 다음과 같이 가상함수를 '순수 가상함수'로 선언해 객체의 생성을 문법적으로 막는 것이 좋다.
```
class First {
public: virtual void MyFunc() = 0; }    // 순수 가상함수 선언
```
 - 순수 가상함수는 함수의 몸체가 정의되지 않은 함수를 의미한다.
   - 순수 가상함수 선언에서의 0의 대입(= 0)은 '명시적으로 몸체를 정의하지 않았음'을 컴파일러에 알리는 것이다.
   - 순수 가상함수를 선언함으로써 실재로 실행이 되지 않지만 가상 오버라이딩된 함수들을 호출하는 데에 도움을 주는 가상함수들을 명시하기 더 쉬워졌다.
 - 컴파일러는 가상함수에 대한 0의 대입은 오류를 일으키지 않지만, 순수 가상함수가 포함된 클래스를 객체생성하려 할 때에는 완전하지 않은 클래스를 생성하려 하는 것이므로 컴파일러가 오류를 일으킨다.
 - 하나 이상의 멤버함수를 순수 가상함수로 선언한 클래스를 가리켜 추상 클래스라 한다. 추상 클래스는 객체생성이 불가능하다.
   - 추상 클래스에 추상 클래스를 상속하는 자식 클래스에 대한 메모리 할당이 가능하다. 포인터 변수를 사용한다 해도 해당 클래스에 대한 메모리 레이아웃이 잡히는 것은 아니기 때문이다.
   - 하지만 추상 클래스 타입의 메모리를 할당할 순 없다. 정확히 말하면 추상 클래스의 메모리를 단독으로 생성 자체가 불가능하다.
 - 인터페이스라는 개념은 하위 클래스들에서 꼭 정의해야할 함수를 정해주는 의미상으로 특별한 클래스이다.
   - 멤버 변수와 일반 멤버 함수가 있든 없든 순수 가상함수를 1개 이상 가지는 클래스가 추상 클래스였다면, 오로지 순수 가상함수로만 이루어진 클래스를 인터페이스라고 한다.
   - 다중 상속이 가능한 c++에서 잘못하면 멤버 변수와 함수가 겹칠 수 있기 때문에 다중 상속을 피하는 것이 좋은데, 인터페이스는 이 걱정 없이 다중 상속을 가능하게 해준다.
   - C++에선 인터페이스를 따로 지원하지 않기 때문에 인터페이스를 흉내내어 인터페이스처럼 사용한다. 그래서 멤버 변수를 선언하지 못하게 강제하거나 할 수는 없다는 단점이 있다. 따라서 인터페이스를 작성할 때 해당 클래스가 인터페이스라는 것을 알리기 위해 이름 맨 앞에 IFlyable, IWalkable과 같이 'I'를 붙이는 습관을 들이는 것이 좋다.

## 다형성
 - 지금까지 설명한 가상함수의 호출 관계에서 보인 특성을 가리켜 '다형성'이라 한다. 그리고 이는 객체지향을 설명하는 데에 있어 매우 중요한 요소이다.
 - 다형성은 다음의 의미를 갖는다. `모습은 같은데 형태는 다르다.` 이를 c++에 적용하면 다음과 같다. `문장은 같은데 결과는 다르다.`
 - 객체지향 언어에서 의미하는 다형성은 다음과 같다 : 서로 다른 객체가 동일한 메세지에 대해서 서로 다른 방법으로 응답할 수 있는 기능
   - '서로 다른 객체'는 서로 다른 클래스를 의미한다. 상속의 경우에도 해당이 된다.
   - '동일한 메세지'는 서로 다른 객체에게 같은 메세지(함수호출 등)을 보낸다는 의미이다.
 - 서로 다른 방법으로 응답한다는 의미는 다음과 같다.
   - 예를 들어 어떤 도형 모형의 클래스와 이 클래스를 상속받는 상각형, 사각형, 원형 등의 특정 형태를 갖는 클라스가 있다고 하자.
   - 서로 다른 객체는 삼각형, 사각형에서 각자 생성한 객체가 될 것이고, 동일한 메시지는 도형을 그리라는 의미를 가진 메소드 함수가 될 것이다.
   - 서로 다른 방법은 동일한 메소드를 받아 삼각형 객체는 삼각형, 사각형 객체는 사각형을 그릴 텐데, 그리는 방법은 각 도형마다 다를 것이다. 즉 같은 메소드 호출에 대해 서로 다른 방법으로 응답을 하게 되는 것이다.
```
#include <iostream>
#include <string>
using namespace std;

class Figure
{
public:
	virtual string draw() = 0;	// 순수 가상함수(추상 메소드)
};

class Triangle : public Figure
{
public:
	virtual string draw() { return "Draw Triangle"; }
};

class Square : public Figure
{
public:
	virtual string draw() { return "Draw Square"; }
};

class Circle : public Figure
{
public:
	virtual string draw() { return "Draw Circle"; }
};

int main()
{
	Figure* F1 = new Triangle;
	Figure* F2 = new Square;
	Figure* F3 = new Circle;

	cout << F1->draw() <<endl;
	cout << F2->draw() <<endl;
	cout << F3->draw() <<endl;
	
	delete F1;
	delete F2;
	delete F3;
		
	return 0;
}
```

## c++의 4가지 다형성
 - 사람들이 주로 이야기하는 C++의 다형성이란 Base 클래스의 포인터 또는 참조형을 통해 Derived 클래스를 사용하는 것을 의미하는 경우가 많은데, 이를 서브 타입 다형성(Subtype Polymorphism)이라고 부른다. 그러나 매개 변수 다형성(Parametric Polymorphism), 임시 다형성(Ad-hoc Polymorphism), 강제 다형성(Coercion Polymorphism)과 같이 C++에 존재하는 다른 모든 종류의 다형성들을 잊어버리는 경우가 많다. 이러한 다형성들은 C++에서 다른 이름으로 알려져 있다.
   - 서브타입 다형성은 **런타임 다형성(Runtime Polymorphism)**으로 알려져 있다.
   - 매개 변수 다형성은 **컴파일 타임 다형성(Compile-Time Polymorphism)**으로 알려져 있다.
   - 임시 다형성은 **오버로딩(Overloading)**으로 알려져 있다.
   - 강제 다형성은 (암시적 또는 명시적) **캐스팅(Casting)**으로 알려져 있다.

### 서브타입 다형성 (런타임 다형성)
 - 서브타입 다형성은 C++에서 "다형성"을 이야기할 때 모든 사람들이 이해하고 있는 의미의 다형성이다. Base 클래스의 포인터 또는 참조형을 통해 Derived 클래스를 사용하는 기능을 말한다.
 - 여기에 예제가 있다. 고양이과에 속하는 다양한 종류의 고양이들을 가지고 있다고 가정해 보자.
   - 모두 생물학적으로 고양이과에 속하고 "야옹"이라고 할 수 있기 때문에, *Felid*라는 Base 클래스로부터 상속을 받고 *meow*라는 순수 가상 함수를 오버라이딩하는 클래스들로 나타낼 수 있다.
```   
	// file cats.h

    class Felid {
        public:
            virtual void meow() = 0;
    };

    class Cat : public Felid {
    public:
        void meow() { std::cout << "Meowing like a regular cat! meow!\n"; }
    };

    class Tiger : public Felid {
    public:
        void meow() { std::cout << "Meowing like a tiger! MREOWWW!\n"; }
    };

    class Ocelot : public Felid {
    public:
        void meow() { std::cout << "Meowing like an ocelot! mews!\n"; }
    };
```
 - 이제 메인 프로그램에서 *Felid* (Base 클래스) 포인터를 통해 *Cat*, *Tiger*, *Ocelot*를 번갈아 가며 사용할 수 있다.
```
    #include <iostream>
    #include "cats.h"

    void do_meowing(Felid *cat) {
        cat->meow();
    }

    int main() {
        Cat cat;
        Tiger tiger;
        Ocelot ocelot;

        do_meowing(&cat);
        do_meowing(&tiger);
        do_meowing(&ocelot);
    }
```
 - 메인 프로그램은 Cat, Tiger, Ocelet을 가리키는 포인터를 do_meowing 함수로 전달한다. 전달되는 포인터들은 모두 Felid이기 때문에, 프로그램은 객체마다 올바른 meow 함수를 호출하며 출력 결과는 다음과 같다.
```
    Meowing like a regular cat! meow!
    Meowing like a tiger! MREOWWW!
    Meowing like an ocelot! mews!
```
 - 서브타입 다형성은 런타임 다형성이라고 부르기도 한다. 다형성 함수의 호출 결정은 런타임에 가상 테이블을 통한 간접 참조를 통해 일어난다. 좀 더 쉽게 설명하자면, 컴파일러가 컴파일 타임 때 호출될 함수의 주소를 찾는 것이 아니라 프로그램을 실행할 때 가상 테이블에 있는 오른쪽 포인터를 역참조해 함수를 호출하는 것이다.
 - 타입 이론에서는 서브타입 다형성을 포함 다형성(Inclusion Polymorphism)이라고도 부른다.

### 매개 변수 다형성 (컴파일 타임 다형성)
 - 매개 변수 다형성은 어떤 타입에 대해 동일한 코드를 실행하기 위한 수단을 제공한다. C++에서 매개 변수 다형성은 템플릿을 통해 구현할 수 있다.
 - 가장 간단한 예제 중 하나는 두 개의 인수 중에서 큰 값을 찾는 일반화된 *max* 함수다.
```
    #include <iostream>
    #include <string>

    template <class T>
    T max(T a, T b) {
        return a > b ? a : b;
    }

    int main() {
        std::cout << ::max(9, 5) << std::endl;     // 9

        std::string foo("foo"), bar("bar");
        std::cout << ::max(foo, bar) << std::endl; // "foo"
    }
```
 - *max* 함수는 타입 *T*에 대해 다양한 형태가 될 수 있다. 하지만, 포인터를 통한 비교는 내용이 아닌 메모리 위치를 비교하기 때문에 포인터 타입에 대해서는 동작하지 않는다. 포인터 타입에 대해서 동작하게 만들고 싶다면 포인터 타입에 대해 템플릿 특수화해야 되는데, 그렇게 되면 더 이상 매개 변수 다형성이 아닌 임시 다형성이 될 것이다.
 - 매개 변수 다형성은 컴파일 타임에 일어나기 때문에, 컴파일 타임 다형성이라고도 부른다.
 - 자세한 것은 추후에 템플릿으로써 배우게 된다.

### 임시 다형성 (오버로딩)
- 임시 다형성은 같은 이름을 가진 함수가 각 타입에 따라 다르게 행동할 수 있도록 해준다. 예를 들어, *int* 타입의 변수 2개와 *+* 연산자가 주어졌다면, 두 변수를 더한다. 반면, *std::string* 타입의 변수 2개와 *+* 연산자가 주어졌다면, 두 변수를 연결하게 된다. 이를 오버로딩이라고도 부른다.

 - *int*와 *std::string*에 대해 *add* 함수를 구현한 구체적인 예제가 있다.
```
    #include <iostream>
    #include <string>

    int add(int a, int b) {
        return a + b;
    }

    std::string add(const char *a, const char *b) {
        std::string result(a);
        result += b;
        return result;
    }

    int main() {
        std::cout << add(5, 9) << std::endl;
        std::cout << add("hello ", "world") << std::endl;
    }
```
 - C++에서 임시 다형성은 템플릿 특수화를 할 때도 나타난다. *max* 함수를 다룬 예제로 돌아가, *char* 포인터 타입의 변수 2개에 대한 *max* 함수를 작성하는 방법을 살펴보자.
```
    template <>
    const char *max(const char *a, const char *b) {
        return strcmp(a, b) > 0 ? a : b;
    }
```
 - 이제 두 문자열 "foo"와 "bar" 중 큰 값을 찾기 위해 *::max("foo", "bar")*를 호출할 수 있다.

### 강제 다형성 (캐스팅)
 - 강제 다형성은 객체 또는 기본 타입이 다른 객체 또는 기본 타입으로 변환할 때 일어난다. 예를 들어,
```
    float b = 6; // int는 (암시적으로) float으로 승격된다.
    int a = 9.99 // float은 (암시적으로) int로 강등된다.
```
 - 명시적 캐스팅은 *(unsigned int**)*나 *(int)*와 같이 C 스타일의 타입 캐스팅 표현식을 사용하거나 C++의 *static_cast*, *const_cast*, *reinterpret_cast*, *dynamic_cast*를 사용할 때 일어난다.
 - 또한 강제 다형성은 클래스의 생성자가 *explicit*로 선언되어 있지 않을 때에도 일어나는데, 예를 들어,
```
    #include <iostream>

    class A {
        int foo;
    public:
        A(int ffoo) : foo(ffoo) {}
        void giggidy() { std::cout << foo << std::endl; }
    };

    void moo(A a) {
        a.giggidy();
    }

    int main() {
        moo(55);     // 55를 출력 
    }
```
 - 만약 A의 생성자를 *explicit*로 선언했다면, 위 예제는 더 이상 실행이 되지 않는다. 변환과 관련된 실수를 피하기 위해서는 생성자를 *explicit*로 선언하는 것이 좋다.
 - 또한 클래스에 타입 T에 대한 변환 연산자를 정의하는 경우, T 타입을 알 수 있는 곳이라면 어디든 사용할 수 있다.
예를 들어,
```
    class CrazyInt {
        int v;
    public:
        CrazyInt(int i) : v(i) {}
        operator int() const { return v; } // CrazyInt에서 int로 변환
    };
```
 - *CrazyInt*는 *int* 타입에 대한 변환 연산자를 정의했다. 이제 *int*를 인수로 받는 *print_int* 함수가 있다면, *CrazyInt* 타입의 객체도 전달할 수 있게 된다.
```
    #include <iostream>

    void print_int(int a) {
        std::cout << a << std::endl;
    }

    int main() {
        CrazyInt b = 55;
        print_int(999);    // 999를 출력
        print_int(b);      // 55를 출력
    }
```
 - 실제로 앞에서 설명한 서브타입 다형성은 Derived 클래스를 Base 클래스 타입으로 변환하기 때문에 강제 다형성이기도 하다.

# 8-3 가상 소멸자와 참조자의 참조 가능성
 - virtual 선언은 소멸자에도 올 수 있다.

## 가상 소멸자
 - virtual로 선언된 소멸자를 가리켜 가상 소멸자라고 한다.
 - 위의 언급된 상황처럼 기초 클래스의 포인터형에 저장된 유도 클래스가 소멸할 경우, 기초 클래스의 소멸자가 호출되며 메모리 누수가 생길 수 있다.
   - 따라서 다음과 같이 소멸자에도 virtual 선언을 추가하여 예방해야 한다.
```
virtual ~Base() { ... }     // Base클래스의 가상 소멸자 선언
```
 - 가상함수와 마찬가지로 소멸자도 상속의 계층구조상 맨 위에 존재하는 기초 클래스의 소멸자만 virtual로 선언하면 이를 상속하는 유도 클래스들의 소멸자들도 모두 가상 소멸자로 선언이 된다.
 - 가상 소멸자가 호출되면 상속의 계층구조상 맨 아래에 존재하는 유도 클래스의 소멸자가 대신 호출되며, 최하위 유도 클래스부터 최상위 기초 클래스의 순으로 소멸자가 순차적으로 호출된다.

## 참조자의 참조 가능성
 - c++에서 AAA형 참조자는 AAA 객체 또는 AAA를 직접 혹은 간접적으로 상속하는 모든 객체를 참조할 수 있다.
 - 가상함수의 개념도 포인터와 마찬가지로 참조자에도 그대로 적용된다.
   - 기초 클래스의 포인터형에 유도 클래스를 지정한 상태에서 포인터의 가상함수를 호출하면, 유도 클래스에 작성된 가상함수가 실행된다.