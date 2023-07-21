# 16-1 c++에서의 형변환 연산

## c의 형 변환
 - c언어의 형 변환 연산은 매우 강력해서 변환하지 못하는 대상이 없다. 따라서 실수를 해도 컴파일러가 잡아내지 못하는 경우가 생긴다.
```
#include <iostream>
using namespace std;

class Car
{
private:
	int fuelGauge;		
public:
	Car(int fuel) : fuelGauge(fuel)
	{  }
	void ShowCarState()
	{
		cout<<"잔여 연료량: "<<fuelGauge<<endl;
	}
};

class Truck : public Car
{
private:
	int freightWeight;

public:
	Truck(int fuel, int weight)
		: Car(fuel), freightWeight(weight)
	{  }
	void ShowTruckState()
	{
		ShowCarState();
		cout<<"화물의 무게: "<<freightWeight<<endl;
	}
};


int main(void)
{
	Car * pcar1=new Truck(80, 200);
	Truck * ptruck1=(Truck *)pcar1; 
	ptruck1->ShowTruckState();
	cout<<endl;
	Car * pcar2=new Car(120);
	Truck * ptruck2=(Truck *)pcar2; 
	ptruck2->ShowTruckState();
	return 0;
}
```
 - 메인 함수의 `Truck * ptruck1=(Truck *)pcar1;`에서 pcar1이 가리키는 대상이 실재로 Truck객체이기 때문에 형변한 연산이 문제가 되지 않을 수 있다.  
 하지만 기초 클래스의 포인터 형을 유도 클래스의 포인터 형으로 형 변환하는 것은 일반적인 경우의 형 변환이 아니다.
   - 프로그래머의 실수인지 의도인지 컴파일러가 파악하기 어렵다
 - 메인 함수의 `Truck * ptruck1=(Truck *)pcar2;`에서 pcar2가 가리키는 대상은 실제로 Car객체이기 때문에 해당 형변환은 문제가 생긴다.
 - 이와 같은 문제점들을 피하기 위해 c++에서는 다음과 같은 4개의 연산자를 추가로 제공하면서 용도에 맞는 형 변환 연산자의 사용을 유도하고 있다.
   - static_cast
   - const_cast
   - dynamic_cast
   - reinterpret_cast

## dynamic_cast : 상속관계에서의 안전한 형변환
 - dynamic_cast 형변환 연산자는 다음의 형태를 갖는다.
```
dynamic_cast<T>(expr)
```
 - <>사이에 변환하고자 하는 이름을 두되, 객체의 포인터 또는 참조형이 와야 하며, ()사이에는 변환의 대상이 와야 한다.
 - 상속관계에 놓여 있는 두 클래스 사이에서 유도 클래스의 포인터 및 참조형 데이터를 기초 클래스의 포인터 및 참조형 데이터로 형변환 하는 경우 형 변환 데이터를 반환한다.
 - 만일 형 변환이 적절하지 않은 경우에는 컴파일 에러를 일으킨다.
```
class Car
{
private:
	int fuelGauge;		
public:
	Car(int fuel) : fuelGauge(fuel)
	{  }
	void ShowCarState()
	{
		cout<<"잔여 연료량: "<<fuelGauge<<endl;
	}
};
class Truck : public Car
{
private:
	int freightWeight;
public:
	Truck(int fuel, int weight)
		: Car(fuel), freightWeight(weight)
	{  }
	void ShowTruckState()
	{
		ShowCarState();
		cout<<"화물의 무게: "<<freightWeight<<endl;
	}
}
int main(void)
{
	Car * pcar1=new Truck(80, 200);
	Truck * ptruck1=dynamic_cast<Truck*>(pcar1);     // 컴파일 에러 (기초를 유도로 형변환)

	Car * pcar2=new Car(120);
	Truck * ptruck2=dynamic_cast<Truck*>(pcar2);     // 컴파일 에러 (같은 형으로 변환)

	Truck * ptruck3=new Truck(70, 150);
	Car * pcar3=dynamic_cast<Car*>(ptruck3);     // 컴파일 OK! (유도를 기초로 형변환)
	return 0;
}
```

## static_cast : A타입에서 B타입으로
 - static_cast형 변환 연산자는 다음의 형태를 갖는다.
```
static_cast<T>(expr)
```
 - static_cast연산자를 사용하면 유도 클래스의 포인터 및 참조형 데이터를 기초 클래스의 포인터 및 참조형 데이터뿐만 아니라 기초 클래스의 포인터 및 참조형 데이터도 유도 클래스의 포인터 및 참조형 데이터로 아무 조건 없이 형변환시켜주지만, 이에 따른 책임은 프로그래머가 지게 된다.
 - static_cast연산자는 dynamic_cast연산자와 달리 보다 많은 형변환을 허용하지만, 그에 따른 책임도 프로그래머가 져야 하기 때문에 신중하게 선택해야 한다. dynamic_cast연산자를 사용할 수 있는 경우에는 dynamic_cast를 사용해 안전성을 높이고, 그 이외의 경우에는 정말 책임질 수 있는 상황에만 제한적으로 static_cast연산자를 사용해야 한다.
 - static_cast연산자는 기본 자료형 데이터간의 형변환에도 사용된다.
```
double result = (double)20/3;               // c언어 형식
double result = static_cast<double>(20/3);  // c++ 형식
```
 - 다음과 같은 형변환은 static_cast로 불가능하다.
```
const int num = 20;
int *ptr = (int *)&num;     // const 상수 포인터를 일반 변수형으로 변경
*ptr = 30;                  // const 상수로 선언된 값이 실제로 변경된다.

float *adr = (float*)ptr;   // int형 포인터를 float형 포인터로 변경
cout << *adr << endl;       // float형으로 출력됨
```

## const_cast : const성향 삭제
 - c++에서는 포인터와 참조자의 const성향을 제거하는 형 변환을 목적으로, 다음의 형 변환 연산자를 제공한다.
```
const_cast<T>(expr)
```
 - 주로 다음 예시와 같이 함수의 인자전달에 const선언으로 인한 형의 불일치를 해결할 때 유용하게 사용된다.
   - 다만 const의 의미를 반감시키므로 제한적인 경우에만 사용해야 한다.
```
#include <iostream>
using namespace std;

void ShowString(char* str)
{
	cout<<str<<endl;
}

void ShowAddResult(int& n1, int& n2)
{
	cout<<n1+n2<<endl;
}

int main(void)
{
	const char * name="Lee Sung Ju";
	ShowString(const_cast<char*>(name));    // 형 불일치 제거를 위한 const_cast 사용

	const int& num1=100;
	const int& num2=200;
	ShowAddResult(const_cast<int&>(num1), const_cast<int&>(num2));
	return 0;
}
```

## reinterpret_cast : 상관없는 자료형으로의 형변환
 - reinterpret_cast연산자는 전혀 상관이 없는 자료형으로의 형 변환에 사용이 되며, 기본적인 형태는 다음과 같다.
```
reinterpret_cast<T>(expr)
```
 - reinterpret_cast는 포인터를 대상으로 하는, 그리고 포인터와 관련이 있는 모든 유형의 형 변환을 허용한다.
   - 서로 다른 클래스끼리의 형변환도 가능케 하지만 그 결과는 어찌될지 모른다.
 - reinterpret_cast는 다음과 같이 사용될 수 있다.
```
int main(void)
{
	int num=0x010203;
	char * ptr=reinterpret_cast<char*>(&num);
    // int형 정수에 바이트 단위 접근을 위해 int형 포인터를 char형 포인터로 변환한다.

	for(int i=0; i<sizeof(num); i++)
		cout<<static_cast<int>(*(ptr+i))<<endl;

	return 0;
}
```
 - 또한 다음과 같은 문장 구성도 가능하다.
```
int main(void)
{
    int num = 72;
    int *ptr = &num;

    int adr = reinterpret_cast<int>(ptr);       // 주소 값을 정수로 변환
    cout << adr;                                // 주소 값 출력

    int *rptr = reinterpret_cast<int *>(adr);   // 정수를 다시 주소로 변환
    cout << *rptr;                              // 주소값에 저장된 정수 출력
}
```

## dynamic_cast 두 번째 : Polymorphic 클래스 기번의 형 변환
 - 상속관 관련된 형 변환은 다음과 같이 정리된다.
   - 상속관계에 놓여있는 두 클래스 사이에서, 유도 클래스의 포인터 및 참조형 데이터를 기초 클래스의 포인터 및 참조형 데이터로 형변환할 경우에는 dynamic_cast연산자를 사용한다.
   - 상속관계에 놓여있는 두 클래스 사이에서, 기초 클래스의 포인터 및 참조형 데이터를 유도 클래스의 포인터 및 참조형 데이터로 형변환할 경우에는 static_cast연산자를 사용한다.
 - dynamic_cast 연산자도 '기초 클래스가 Polymorphic 클래스'이기만 하면 기초 클래스에서 유도 클래스로의 형 변환은 허용한다.
   - Polymorphic 클래스란 하나 이상의 가상함수를 지니는 클래스를 뜻한다. 따라서 상속관계에 놓여있는 두 클래스 사이에서 기초 클래스에 가상함수가 하나 이상 존재하면 dynamic 연산자를 이용할 수 있다.
```
#include <iostream>
using namespace std;

class SoSimple  // 가상함수 ShowSimpleInfo()를 지닌 Polymorphic 클래스
{
public:
	virtual void ShowSimpleInfo()
	{
		cout<<"SoSimple Base Class"<<endl;
	}
};

class SoComplex : public SoSimple
{
public:
	void ShowSimpleInfo()
	{
		cout<<"SoComplex Derived Class"<<endl;
	}
};

int main(void)
{
	SoSimple * simPtr=new SoComplex;
	SoComplex * comPtr=dynamic_cast<SoComplex*>(simPtr);
	comPtr->ShowSimpleInfo();
	return 0;
}
```

## dynamic_cast와 static_cast의 차이점
 - dynamic_cast와 static_cast 둘 다 기초->유도로 형변환을 시도할 때 사용할 수 있다.
 - 하지만 dynamic_cast는 안정적인 형 변환을 보장한다.
   - 특히 컴파일 시간이 아닌 실행 시간에 안전성을 검사하도록 컴파일러가 바이너리 코드를 따로 생성한다.
   - 그만큼 실행속도는 늦어진다.
 - dynamic_cast를 이용한 포인터형의 형변환에 실패하면 포인터에 NULL값을 할당해주게 된다.
```
class SoSimple
{ ... };

class SoComplex : public SoSimple
{ ... };

int main(void)
{
	SoSimple * simPtr=new SoSimple;
	SoComplex * comPtr=dynamic_cast<SoComplex*>(simPtr);
    // 형변환에 실패하면서 comPtr에 NULL을 반환해준다.
	if(comPtr==NULL)
		cout<<"형 변환 실패"<<endl;
	else
		comPtr->ShowSimpleInfo();
	return 0;
}
```
 - dynamic_cast를 이용한 참조자형의 형변환에 실패하면 bas_cast에러를 throw하게 된다.
```
class SoSimple
{ ... };

class SoComplex : public SoSimple
{ ... };

int main(void)
{
	SoSimple simObj;
	SoSimple& ref=simObj;

	try
	{
		SoComplex& comRef=dynamic_cast<SoComplex&>(ref);
        // 참조자 형식에 대한 오류로 bad_cast를 throw
		comRef.ShowSimpleInfo();
	}
	catch(bad_cast expt)
	{
		cout<<expt.what()<<endl;
	}   // 오류 데이터를 받았으므로 실행된다.
	return 0;
}
```