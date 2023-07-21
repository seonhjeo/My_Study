# 9-1 멤버함수와 가상함수의 동작원리

## 객체 안에 멤버함수가 존재하는가?
 - 개념적으로는 객체 내에 멤버함수가 존재하지만, 실질적으로는 그렇지 않다.
 - 객체가 생성되면 멤버변수는 객체 내에 존재하지만, 멤버함수는 메모리의 한 공간에 별도로 위치하고 이 함수가 정의된 클래스의 모든 객체가 이를 공유하게 된다.

## 가상함수의 동작원리와 가상함수 테이블
 - 한 개 이상의 가상함수를 포함하는 클래스에 대해서는 컴파일러가 가상함수 테이블을 만들게 된다.
   - 가상함수 테이블은 호출하고자 하는 함수를 구분지어주는 key와 실제 함수가 저장되어 있는 주소인 value로 구성되어 있다.
   - 오버라이딩된 가상함수의 주소정보는 유도 클래스의 가상함수 테이블에 포함되지 않는다. 때문에 오버라이딩 된 가상함수를 호출하면 무조건 가장 마지막에 오버라이딩한 유도 클래스의 멤버함수가 호출되게 된다.

## 가상함수 테이블의 참조방식
 - 프로그램이 실행되면 메인함수가 호출되기 전에 메모리에 각 클래스별 가상함수 테이블이 할당된다.
 - 메인 함수가 호출되어 객체가 생성되게 되면, 위의 멤버함수처럼 별도의 공간에 위치한 가상함수 테이블을 동일한 클래스의 모든 객체가 공유하게 된다.

## private 가상함수
 - 상속받은 private 가상 함수를 파생 클래스가 재정의 할 수 있다.
   - 가상 함수를 재정의하는 일은 어떤 동작을 어떻게 구현할 것인가를 지정하는 것이고, 가상 함수를 호출하는 일은 그 동작이 수횅될 시점을 지정하는 것이다.
   - 따라서 이 경우에, 어떤 기능을 어떻게 구현할지를 조정하는 권한은 파생 클래스가 갖게 되지만, 함수를 언제 호출할지를 결정하는 것은 기본 클래스만의 고유 권한이다.
 - private 가상함수를 사용하는 이유는 상속받은 자식 클래스에서 해당 함수 구현을 필수로 하고 (사용을 원한다면) 현재 구현부는 본인만 사용하기 위해서이다.
   - private으로 선언된 함수는 본인만 호출할 수 있다. 자식 클래스에서 재정의한 virtual function은 자식 클래스 내에서만 호출이 가능하다. 자식 클래스에서는 부모 클래스의 private virtual function을 호출하여 사용할 수 없다.


# 9-2 다중상속(Multiple Inheritance)에 대한 이해
 - 다중상속은 둘 이상의 클래스를 동시에 상속하는 것을 말한다.
 - c++은 다중상속을 지원하나 다양한 문제를 동반해 가급적 사용하지 않는 것을 권장한다.

## 다중상속의 기본방법
 - 다중상속은 다음과 같이 선언할 수 있다.
```
class MultiDerived : public BaseOne, protected BaseTwo
{
    ...
}
```

## 다중상속의 모호성
 - 다중상속의 대상이 되는 두 기초 클래스에 동일한 이름의 멤버가 존재하는 경우 유도 클래스 내에서 멤버의 이름만으로 접근이 불가능해 문제가 발생한다.
 - 이럴 때 이름공간을 사용해 두 함수의 정확한 위치를 구분지어주면 해결이 가능하다.
```
class MultiDerived : public BaseOne, protected BaseTwo
{
public:
    void Func()
    {
        BaseOne::SimpleFunc();      // BaseOne 클래스의 SimpleFunc함수 호출
        BaseTwo::SimpleFunc();      // BaseTwo 클래스의 SimpleFunc함수 호출
    }
}
```

## 가상 상속
 - 함수 호출관계의 모호함은 다른 상황에서도 발생할 수 있다.
```
class Base
{
public:
	Base() { cout<<"Base Constructor"<<endl; }
	void SimpleFunc() { cout<<"BaseOne"<<endl; }
};

class MiddleDrivedOne : virtual public Base     // Base를 가상상속하는 MiddleDrivedOne
{
public:
	MiddleDrivedOne() : Base()
	{
		cout<<"MiddleDrivedOne Constructor"<<endl;
	}
	void MiddleFuncOne()
	{
		SimpleFunc();
		cout<<"MiddleDrivedOne"<<endl;
	}
};

class MiddleDrivedTwo : virtual public Base     // Base를 가상상속하는 MiddleDrivedTwo
{
public:
	MiddleDrivedTwo() : Base()
	{
		cout<<"MiddleDrivedTwo Constructor"<<endl;
	}
	void MiddleFuncTwo()
	{
		SimpleFunc();
		cout<<"MiddleDrivedTwo"<<endl;
	}
};

class LastDerived : public MiddleDrivedOne, public MiddleDrivedTwo     // 다중상속
{
public:
	LastDerived() : MiddleDrivedOne(), MiddleDrivedTwo()
	{
		cout<<"LastDerived Constructor"<<endl;
	}
	void ComplexFunc()
	{
		MiddleFuncOne();
		MiddleFuncTwo();
		SimpleFunc();
	}
};
```
 - 위의 상황에서 LastDerived 클래스는 Base클래스를 간접적으로 두 번 상속한다.
 - 만일 중간의 유도 클래스들이 가상 상속을 하지 않은 상태이면 LastDerived클래스는 Base클래스의 데이터를 두 개를 갖게 된다.
   - 이 때 최종 유도 클래스에서 간접 상속한 기초 클래스의 멤버함수를 호출 할 때 어느 중간 유도 클래스를 통해 호출되는지 이름공간을 명시해주어야 한다.
   - 위와 같은 번거로움을 해결하기 위해 LastDerived 클래스에 Base클래스를 한 번만 상속하게끔 하는 문법이 가상 상속이다.
 - 가상 상속시에는 중간 유도 클래스들이 기초 클래스 하나를 공유하게 되므로 최종 유도 클래스에서도 기초 클래스가 한 번만 상속이 되게 된다.
