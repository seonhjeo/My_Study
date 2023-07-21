# 14-1 13챕터의 확장

## 클래스 템플릿과 배열 템플릿
 - 배열 템플릿에 int형 정수를 넣는 배열 템플릿 클래스를 만들기 위해서는 `BoundCheckArray<int> iarr(50);`과 같이 선언해주면 된다.
 - 배열 템플릿에 Point<int>형의 클래스 템플릿을 넣는 배열 템플릿 클래스를 만들기 위해서는 `BoundCheckArray<Point<int>> iaar(50);`과 같이 선언해주면 된다.
 - 포인터 형일 때에는 `BoundCheckArray<Point<int> *> iaar(50);`과 같이 선언해주면 된다.
 - 다음과 같이 선언도 가능하다.
```
typedef Point<int>* POINT_PTR;
BoundCheckArray<POINT_PTR> oparr(50);
```

## 특정 템플릿 클래스의 객체를 인자로 받는 일반함수의 정의와 friend 선언
 - 템플릿 클래스의 자료형을 대상으로도 템플릿이 아닌 일반함수의 정의가 가능하고 클래스 템플릿 내에서 이러한 함수를 대상으로 friend 선언도 가능하다.
```
template <typename T>
class Point 
{
private:
	T xpos, ypos;
public:
	Point(T x=0, T y=0): xpos(x), ypos(y)
	{  }
	void ShowPosition() const
	{
		cout<<'['<<xpos<<", "<<ypos<<']'<<endl; 
	}

	friend Point<int> operator+(const Point<int>&, const Point<int>&);
	friend ostream& operator<<(ostream& os, const Point<int>& pos);
};

Point<int> operator+(const Point<int>& pos1, const Point<int>& pos2)    // +연산자를 오버로딩하는 일반함수
{
	return Point<int>(pos1.xpos+pos2.xpos, pos1.ypos+pos2.ypos);
}


ostream& operator<<(ostream& os, const Point<int>& pos)
{
	os<<'['<<pos.xpos<<", "<<pos.ypos<<']'<<endl;
	return os;
}

int main(void)
{	
	Point<int> pos1(2, 4);
	Point<int> pos2(4, 8);
	Point<int> pos3=pos1+pos2;
	cout<<pos1<<pos2<<pos3;
	return 0;
}
```


# 14-2 클래스 템플릿 특수화

## 클래스 템플릿 특수화
 - 특정 자료형을 기반으로 생성된 객체에 대해 구분이 되는 다른 행동양식을 적용하기 위해서이다.
 - 클래스 템플릿을 특수화하면 템플릿을 구성하는 멤버함수의 일부 또는 전부를 다르게 행동하도록 정의할 수 있다.
 - 클래스 템플릿을 특수화하는 방법은 다음과 같다.
```
// 특수화 이전
template<typename T>
class SoSimple
{
    public:
        T SimpleFunc(T num) { ... }
}

// 특수화 이후
template<>
class SoSimple<int>
{
    public:
        int SimpleFunc(int num) { ... }
}
```

## 클래스 템플릿의 부분 특수화
 - 여러 타입을 동시에 사용하는 클래스 템플릿의 자료형 중 일부만을 특수화할 수 있다.
```
// 특수화 이전
template <typename T1, typename T2>
class MySimple { .... }

// 전체 특수화
template <>
class MySimple<char, int> { .... }

// 부분 특수화
template <typename T1>
class MySimple<T1, int> { .... }
```


# 14-3 템플릿 인자
 - 템플릿을 정의할 때 결정되지 않은 자료형을 의미하는 용도로 사용되는 T 또는 T1, T2와 같은 문자를 가리켜 '템플릿 매개변수'라 한다.
 - 템플릿 매개변수에 전달되는 자료형 정보를 가리켜 '템플릿 인자'라고 한다.

## 템플릿 매개변수에는 변수의 전언이 올 수 있다.
 - 템플릿 매개변수의 선언에는 함수처럼 변수의 선언이 나올 수 있다.
```
template <typename T, int len>
class SimpleArray
{
private:
    T arr[len];
}
```
 - 이를 기반으로 다음의 형태로 객체생성이 가능하다.
```
SimpleArray <int, 5> i5arr;
SimpleArray <double, 7> d7arr;
```
 - 객체생성시 컴파일러에 의해 다음과 같은 템플릿 클래스가 생성된다.
```
class SimpleArray<int, 5>
{
private:
    int arr[5];
}

class SimpleArray<double, 7>
{
private:
    double arr[7];
}
```
 - 템플릿 매개변수에 값을 전달받을 수 있는 변수를 선언하면, 변수에 전달되는 상수를 통해 서로 다른 형의 클래스가 생성되게 할 수 있다.
   - `SimpleArray <int, 5>`와 `SimpleArray <int, 7>`는 각각 다른 형이다. 따라서 두 객체간의 대입은 허용되지 않는다.
   - 자료형과 길이가 같은 객체에 대해서만 대입과 복사가 허용되므로 길이가 다른 배열 객체간의 대입 및 복사에 대한 부분을 신경쓸 필요가 없다.

## 템플릿 매개변수는 디폴트 값 지정도 가능하다.
 - 함수의 매개변수에 디폴트 값의 지정이 가능하듯이, 템플릿 매개변수에도 디폴트 값의 지정이 가능하다.
   - 디폴트 값을 이용해 템플릿 클래스를 선언하더라도 템플릿 클래스의 객체생성을 의미하는 <>기호는 반드시 추가해야 한다.
```
using namespace std;

template <typename T=int, int len=7>
class SimpleArray
{
private:
	T arr[len];
public:
	
	T& operator[] (int idx)
	{
		return arr[idx];
	}
	T& operator=(const T&ref)
	{
		for(int i=0; i<len; i++)
			arr[i]=ref.arr[i];
	}
};

int main(void)
{
	SimpleArray<> arr;      // 디폴트 값을 이용한 객체생성
	for(int i=0; i<7; i++)
		arr[i]=i+1;
	for(int i=0; i<7; i++)
		cout<<arr[i]<<" ";
	cout<<endl;
	return 0;
}
```


# 14-4 템플릿과 static
 - static 변수는 딱 한번 초기화된 상태에서 그 값을 계속 유지한다.

## 함수 템플릿과 static 지역변수
 - 다음과 같이 함수 탬플릿 내에 static 지역변수를 추가할 수 있다.
```
// 템플릿 인자 없음
template <typename T>
void ShowStaticValue(void)
{
    static T num = 0;
    num += 1;
    cout << num << endl;
}

// int형 템플릿 인자 삽입
void ShowStaticValue(void)
{
    static int num = 0;
    num += 1;
    cout << num << endl;
}
```
 - static 지역변수는 템플릿 함수별로 각각 존재하게 된다.(int형 따로, double형 따로 등등...)

## 클래스 템플릿과 static 멤버변수
 - static멤버변수는 변수가 선언된 클래스의 객체간 공유가 가능한 변수이다.
 - 위의 함수 템플릿과 마찬가지로 static 멤버변수는 같은 템플릿 클래스 내에서 공유된다.

## `template<typename T>`와 `template<>`
 - 템플릿 관련 정의에는 `template<typename T>`와 `template<>`같은 선언을 두어 템플릿의 일부 또는 전부를 정의하고 있다는 사실을 컴파일러에 알려야 한다.
 - 기본적으로 클래스 템플릿을 선언할 때에는 `template<typename T>`를, 특수화된 템플릿을 선언할 때는 `template<>`를 사용한다.

## 템플릿 static 멤버변수 초기ㅗ하의 특수화
 - 템플릿 static멤버변수는 다음과 같이 초기화한다.
```
template <typename T>
T SimpleClass<T>::mem = 0;
```
 - 특수화된 템플릿 static멤버변수는 다음과 같이 초기화한다.
```
template <>
long SimpleClass<long>::mem = 5;
```