# 6-1 const에 관한 추가 사항

## const객체와 const객체의 특성
 - 변수를 상수화 하듯이 객체도 상수화할 수 있다.  
 `const SoSimple sim(20);`
   - 이 선언은 객체의 대ㅔ이터 변경을 허용하지 않겠다는 뜻이다.
   - `const`선언된 객체는 `const`가 선언된 멤버함수만 호출할 수 있다.

## const와 함수 오버로딩
 - const 키워드 또한 오버로딩이 가능하다.
```
void SimpleFunc() { ... }
void SimpleFunc() const { ... }
```
 - const로 오버로딩된 함수가 호출되는 시기는 다음과 같다.
   - 일반 객체를 대상으로 함수를 호출하면 일반 멤버함수가, const 객체를 대상으로 함수를 호출하면 const 멤버함수가 호출된다.
   - const 참조자를 통해 받은 객체를 대상으로 함수를 호출하면 const 멤버함수가 호출된다.


# 6-2 클래스와 함수에 대한 friend 선언

## 클래스의 friend 선언
 - A 클래스가 B 클래스를 대상으로 friend 선얼을 하면 B 클래스는 A클래스의 private 멤버에 직접 접근이 가능하다.
 - 단 A 클래스도 B 클래스의 private 멤버에 직접 접근이 가능하려면, B 클래스가 A 클래스를 대상으로 friend 선언을 해줘야 한다.
```
class Boy
{
    private:
        int height;         // private 변수 선언
        friend class Girl;  // Girl 클래스를 friend로 선언
}

class Girl
{
public:
    void ShowFriendInfo(Boy &frn)
    {
        cout << frn.height << endl; // 매개변수로 가져온 Boy 클래스의 private 매개변수 직접 접근
    }
}
```
 - friend 클래스 선언은 멤버변수를 선언함과 동시에 혹은 생성자 내에서 선언할 수 있다.
 - friend 선언은 필요한 상황에만 극히 소극적으로 사용해야 한다.

## 함수의 friend 선언
 - friend로 선언된 함수는 자신이 선언된 클래스의 private 영역에 접근이 가능하다.
```
class Point;

class PointOP
{
private:
    int opcnt;
public:
    Point PointAdd( ... );
}

class Point
{
private:
    int x;
public:
    friend Point PointOP::PointAdd(const Point &pnt)
    { return Point(pnt.x); }    // Point 클래스의 private 영역에 접근        
}

```
 - friend로 선언된 함수는 friend 선언 이외에 함수원형 선언이 포함되어 있기 때문에, 별도의 함수원형을 선언할 필요는 없다.


# 6-3 c++ 에서의 static
 - c++에서는 c언어에서의 static에 멤버변수와 멤버함수까지 static을 추가할 수 있다.

## c언어에서의 static
 - 전역변수에 선언된 static의 의미 : 선언된 파일 내에서만 참조를 허용하겠다.
 - 함수 내에 선언된 static의 의미 : 한번만 초기화되고, 지역변수와 달리 함수를 빠져나가도 소멸하지 않는다.
 - static 변수는 전역변수와 마찬가지로 초기화하지 않으면 0으로 초기화된다.

## static 멤버변수(클래스 변수)
 - 동일한 객체들이 공유하는 변수를 선언할 때 전역변수를 사용하면 접근을 제한할만한 수단이 없어 위험하다. 따라서 이러한 전역변수를 클래스 내에 static 멤버변수로 선언하여 문제의 소지를 없앨 수 있다.
 - static 멤버변수는 일반적인 멤버변수와 다르게 클래스당 하나씩만 생성되기 때문에 클래스 변수라고도 한다.
 - static 멤버변수의 선언 및 초기화는 다음과 같다.
```
class SoSimple
{
private:
    static int simObjCnt;       // static 멤버변수 선언
}
int SoSimple::simObjCnt = 0;    // static 멤버변수 초기화, 클래스를 이용해 접근하여 초기화하고 있다.
```
 - 이는 다음과 같은 특징을 가진다.
   - static 멤버변수는 객체를 몇 개를(0개 포함) 생성해도 메모리 공간에 단 하나만 생성되고, 생성된 모든 객체에게 공유된다.
   - 객체 외부의 메모리에 존재하며, 각각의 객체에게 접근할 수 있는 권한을 주는 것 뿐이다.
   - static 멤버변수를 생성자에서 초기화하면 객체를 생성할 때마다 초기화되기 때문에, static 멤버변수의 초기화는 위의 예시처럼 클래스 바깥에서 해야 한다.
 - static 멤버변수를 public으로 선언할 경우, 클래스의 이름 또는 객체의 이름을 통해서 어디에서든 접근이 가능하다.
   - 다만 객체의 이름을 통해서 접근할 경우 static이 아닌 일반 멤버변수에 접근하는 것과 같은 오해를 불러일으키기 때문에 클래스의 이름으로 접근하는 것을 권장한다.

## static 멤버함수
 - static 멤버함수 또한 static 멤버변수와 동일하게 다음과 같은 특징을 갖는다.
   - 선언된 클래스의 모든 객체가 공유한다.
   - public으로 선언되면 클래스의 이름을 이용해서 호출이 가능하다.
   - 객체의 멤버로 존재하는 것이 아니다.
 - 객체의 멤버로 존재하지 않기 때문에 일반적인 객체의 멤버변수에 접근이 불가능하다.
   - static 멤버함수 내에서는 static 멤버변수와 static 멤버함수만이 접근이 가능하다.

## const static 멤버
 - const static으로 선언되는 멤버변수(상수)는 다음과 같이 선언과 동시에 초기화가 가능하다.
   - 상수 취급을 받으며 값이 변하지 않기 때문에 가능하다.
```
class SoSimple
{
public:
    const static int num = 1;       static 멤버상수 초기화.
}
```

## 키워드 mutable
 - 키워드 mutable은 'const 함수 내에서의 값의 변경을 예외적으로 허용한다'는 의미이다.
```
class SoSimple
{
private:
    int num1;
    mutable int num2;       // const함수에 대한 예외 지정
public:
    void CopyToNum2() const { num2 = num1; }    // const 함수 내에서 mutable 선언된 변수의 값 변경
}
 - mutable은 제한적으로 매우 예외적인 경우에 한해서만 사용되는 키워드이다. mutable의 과도한 사용은 const의 선언을 의미 없게 만든다.
