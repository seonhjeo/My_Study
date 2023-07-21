# 11-1 반드시 해야 하는 대입 연산자의 오버로딩
 - 대입 연산자의 오버로딩은 그 성격이 복사 생성자와 매우 유사하다.

## 객체간 대입연산의 비밀 : 디폴트 대입 연산자
 - 복사 생성자의 대표적인 특성(챕터 5)
   - 정의하지 않으면 디폴트 복사 생성자가 삽입된다.
   - 디폴트 복사 생성자는 멤버 대 멤버의 복사(얕은 복사)를 진행한다.
   - 생성자 내에서 동적 할당을 한다면, 그리고 깊은 복사가 필요하다면 직접 정의해야 한다.
 - 대입 연산자의 대표적인 특성
   - 정의하지 않으면 디폴트 대입 연산자가 삽입된다.
   - 디폴트 대입 연산자는 멤버 대 멤버의 복사(얕은 복사)를 진행한다.
   - 연산자 내에서 동적 할당을 한다면, 그리고 깊은 복사가 필요하다면 직접 정의해야 한다.
 - 복사 생성자가 호출되는 대표적인 상황  
`Point pos1(5, 7); Point pos2 = pos1;`
 - 대입 연산자가 호출되는 대표적인 상황  
`Point pos1(5, 7); Point pos2(3, 4); pos2 = pos1;`
   - pos1과 pos2 둘 다 생성 및 초기화가 된 객체이다.
   - 즉 기존에 생성된 두 객체간의 대입연산 시에 대입 연산자가 호출되는 것이다.
 - 대입 연산자는 클래스 내에 정의하지 않아도, 대입 연산시 디폴트 대입 연산자가 삽입되 멤버간의 복사가 일어난다.
 - 객체간의 대입연산은 c언어의 구조체 대입 연산과 본질적으로 다르다. 단순한 대입연산이 아닌 대입 연산자를 오버로딩한 함수의 호출이기 때문이다.

## 디폴트 대입 연산자의 문제점
 - 디폴트 대입 연산자의 문제점은 복사 생성자에서 보인 문제와 유사하다.
   - 대입 연산 이전에 사용하고 있던 문자열 멤버에 대한 주소 값을 잃어 메모리 누수가 나게 된다.
   - 얕은 복사로 인해 객체 소멸과정에서 지워진 문자열을 중복 소멸하는 문제가 발생한다.
 - 따라서 다음과 같이 해결하면 된다.
   - 깊은 복사를 진행하도록 한다.
   - 메모리 누수가 나지 않도록 깊은 복사에 앞서 메모리 해체 과정을 거친다.
```
Person& operator=(const Person &ref)
{
    delete []name;
    int len = strlen(ref.name) + 1;
    name = new char[len];
    strcpy(name, ref.name);
    age = ref.age;
    return *this;
}
```

## 상속 구조에서의 대입 연산자 호출
 - 대입 연산자는 생성자가 아니다.
   - 유도 클래스의 생성자에는 아무런 명시를 하지 않아도 기초 클래스의 생성자가 호출된다.
   - 유도 클래스의 대입 연산자에는 아무런 명시를 하지 않으면 기초 클래스의 대입 연산자가 호출되지 않는다.
 - 유도 클래스의 대입 연산자 정의에서, 명시적으로 기초 클래스의 대입 연산자 호출문을 삽입하지 않으면 기초 클래스의 대입 연산자는 호출되지 않아서 기초 클래스 멤버변수는 멤버 대 멤버의 복사대상에서 제외된다.
 - 때문에 유도 클래스의 대입 연산자를 정의해야 하는 상황에 놓이게 되면, 기초 클래스의 대입 연산자를 명시적으로 호출해야 한다.
```
Second& operator=(const Second &ref)
{
	cout<<"Second& operator=()"<<endl;
	First::operator=(ref);      // 기초 클래스의 대입 연산자 호출
	num3=ref.num3;
	num4=ref.num4;
	return *this;
}
```
 - 위의 예시에서 'ref'는 Second형 참조자이지만, First형 참조자로 매개변수를 선언한 operator=함수의 인자로 전달이 가능하다.
   - c++에서는 AAA형 참조자는 AAA객체 또는 AAA를 직간접적으로 상속하는 모든 객체를 참조할 수 있다.

## 이니셜라이저와 대입 연산자
 - 이니셜라이저 대입 연산자를 이용한객체 초기화는 다음과 같다.
```
BBB(const AAA &ref) : mem(ref) { }  // 이니셜라이저
CCC(const AAA &ref) { mem = ref; }  // 대입 연산자
```
 - 이니셜라이저를 사용한 초기화는 선언과 동시에 초기화가 이루어지는 형태의 바이너리 코드가 생긴다.
 - 대입 연산자를 사용한 초기화는 선언과 초기화를 각각 별도의 문장에서 진행하는 형태의 바이너리 코드가 생겨난다.
 - 이러한 이유로 이니셜라이저를 이용하는 것이 대입 연산자를 이용하는 것 보다 성능상의 이점이 있다.


# 11-2 배열의 인덱스 연산자 오버로딩
 - 배열요소에 접근할 때 사용하는 []연산자는 연산의 기본 특성상 멤버함수 기반으로만 오버로딩할 수 있다.

## 배열보다 나은 배열 클래스
 - c, c++의 기본 배열은 경계 검사를 하지 않는 단점이 있다. 따라서 배열의 범위가 넘어가도 컴파일 에러가 나지 않는다.
 - `arrObject[2];`의 해석은 다음과 같다.
   - 객체 arrObject의 멤버함수 호출로 이어진다.
   - 연산자가 []이므로 멤버함수의 이름은 `operator[]`이다.
   - 함수호출 시 전달되는 인자의 값은 정수 2이다.
   - 따라서 `arrObject.operator[](2);`와 동일하다.
 - 배열 클래스는 다음과 같이 정의내릴 수 있다.
```
#include <iostream>
#include <cstdlib>
using namespace std;

class BoundCheckIntArray 
{
private:
	int * arr;
	int arrlen;
    // (1)
	BoundCheckIntArray(const BoundCheckIntArray& arr) { }
	BoundCheckIntArray& operator=(const BoundCheckIntArray& arr) { }

public:
	BoundCheckIntArray(int len) :arrlen(len)
	{
		arr=new int[len];
	}
	int& operator[] (int idx)   // (2)
	{
		if(idx<0 || idx>=arrlen)
		{
			cout<<"Array index out of bound exception"<<endl;
			exit(1);
		}
		
		return arr[idx];
	}
	~BoundCheckIntArray()
	{
		delete []arr;
	}
};

int main(void)
{
	BoundCheckIntArray arr(5);

	for(int i=0; i<5; i++)
		arr[i]=(i+1)*11;

	BoundCheckIntArray copy1(5);
	// copy1=arr;                       // (3)
	// BoundCheckIntArray copy=arr;     // (3)
	return 0;
}
```
 - 위의 예시에 대한 주석 설명은 다음과 같다.
   - (1) : 주석 (3)의 안전하지 않은 접근을 방지하기 위해 private으로 선언해주었다.
   - (2) : 인자로 전달된 인덱스에 해당하는 배열요소를 반환하는데, 반환형이 참조형이다. 때문에 배열요소의 참조값이 반환되고 이 값을 이용해 배열요소에 저장된 값의 참조뿐만 아니라 변경도 가능하다.
   - (3) : 배열은 저장소의 일종이고, 저장소에 저장된 데이터는 유일성이 보장되어야 되기 때문에, 대부분의 경우 저장소의 복사는 불필요하거나 잘못될 일로 간주된다. 따라서 깊은 복사가 진행되도록 클래스를 정의할 것이 아니라, 주석 (1)에서 진행한 것처럼 빈 상태로 정의된 복사 생성자와 대입 연산자를 private 멤버로 두어 복사와 대입을 원천적으로 막아야 한다.

## const 함수를 이용한 오버로딩의 활용
```
class BoundCheckIntArray 
{
private:
	int * arr;
	int arrlen;

	BoundCheckIntArray(const BoundCheckIntArray& arr) { }
	BoundCheckIntArray& operator=(const BoundCheckIntArray& arr) { }

public:
	BoundCheckIntArray(int len) :arrlen(len)
	{
		arr=new int[len];
	}
	int& operator[] (int idx)
	{
		if(idx<0 || idx>=arrlen)
		{
			cout<<"Array index out of bound exception"<<endl;
			exit(1);
		}
		return arr[idx];
	}
	int operator[] (int idx) const 
	{
		if(idx<0 || idx>=arrlen)
		{
			cout<<"Array index out of bound exception"<<endl;
			exit(1);
		}
		return arr[idx];
	}
	int GetArrLen() const { return arrlen; }
	~BoundCheckIntArray() { delete []arr; }
};

void ShowAllData(const BoundCheckIntArray& ref)
{
	int len=ref.GetArrLen();

	for(int idx=0; idx<len; idx++)
		cout<<ref[idx]<<endl;
}
```
 - `ShowAllData`함수에서 사용되는 `ref[idx]`는 `ref.operator[] (idx)`로 변환된다. 이 때 함수의 매개변수는 const형이므로 const형의 `operator[]`함수가 필요하게 된다.
 - `int& operator[] (int idx)` 와 `int operator[] (int idx) const`을 둘 다 선언하였다. 따라서 const형을 인자로 받는 함수 `ShowAllData`에서 무리 없이 const형의 `operator[]`함수를 이용해 연산을 처리할 수 있게 된다. const선언이 되지 않은 인자를 사용하는 함수들은 `int& operator[] (int idx)`를 사용하게 된다.
 - **const의 선언유무도 함수 오버로딩의 조건에 해당된다.**

## 객체의 저장을 위한 배열 클래스의 정의
 - 객체를 저장할 수 있는 배열 클래스로는 두 가지 형태로 고려할 수 있다.
   - Point 객체를 저장하는 배열 기반의 클래스
   - Point 객체의 주소값을 저장하는 배열 기반의 클래스
 - 객체 저장 기반 클래스의 예시는 다음과 같다.
   - 객체 저장 기반 배열을 생성할 때 배열의 요소에 대입 연산자를 이용해 데이터를 저장하게 된다.  
`arr[0] = Point(3, 5);`
```
class BoundCheckPointArray 
{
private:
	Point * arr;
	int arrlen;

	BoundCheckPointArray(const BoundCheckPointArray& arr) { }
	BoundCheckPointArray& operator=(const BoundCheckPointArray& arr) { }
public:
	BoundCheckPointArray(int len) :arrlen(len) { arr=new Point[len]; }
	Point& operator[] (int idx)
	{
		if(idx<0 || idx>=arrlen)
		{
			cout<<"Array index out of bound exception"<<endl;
			exit(1);
		}
		return arr[idx];
	}
	Point operator[] (int idx) const 
	{
		if(idx<0 || idx>=arrlen)
		{
			cout<<"Array index out of bound exception"<<endl;
			exit(1);
		}
		return arr[idx];
	}
	int GetArrLen() const { return arrlen; }
	~BoundCheckPointArray() { delete []arr; }
};
```
 - 객체 주소값 저장 기반 클래스의 예시는 다음과 같다.
   - 객체 주소값 저장 기반 배열을 생성할 때 배열의 요소는 new 연산자를 이용해 메모리에 새 객체를 할당해주게 된다.  
`arr[0] = new Point(3, 5);`
```
typedef Point * POINT_PTR;

class BoundCheckPointPtrArray 
{
private:
	POINT_PTR * arr;
	int arrlen;

	BoundCheckPointPtrArray(const BoundCheckPointPtrArray& arr) { }
	BoundCheckPointPtrArray& operator=(const BoundCheckPointPtrArray& arr) { }
public:
	BoundCheckPointPtrArray(int len) :arrlen(len) { arr=new POINT_PTR[len]; }
	POINT_PTR& operator[] (int idx)
	{
		if(idx<0 || idx>=arrlen)
		{
			cout<<"Array index out of bound exception"<<endl;
			exit(1);
		}
		return arr[idx];
	}
	POINT_PTR operator[] (int idx) const 
	{
		if(idx<0 || idx>=arrlen)
		{
			cout<<"Array index out of bound exception"<<endl;
			exit(1);
		}
		return arr[idx];
    }
    int GetArrLen() const { return arrlen; }
	~BoundCheckPointPtrArray() { delete []arr; }
};
```
 - 주소값을 저장하는 경우, 각 배열의 요소마다 new와 delete로 생성과 삭제를 해주어야 하지만, 대입 연산에 의한 깊은/얕은 복사를 신경 쓸 필요가 없어지기 때문에 주소값 저장 기반 클래스를 많이 쓰게 된다.


# 11-3 그 이외의 연산자 오버로딩
 - 책 참고 (p.469~)