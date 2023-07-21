# 추가자료
 - https://velog.io/@hey-chocopie/C-Module-07-9dmf3a5a#%ED%85%9C%ED%94%8C%EB%A6%BF

# 13-1 템플릿에 대한 이해와 함수 템플릿

## 함수를 대상으로 템플릿 이해하기
 - 함수 템플릿은 함수를 만들어낸다. 함수의 기능은 결정되어 있지만, 자료형은 결정되어 있지 않아서 결정해야 한다.
 - 함수 템플릿은 함수를 만드는 도구가 된다. 함수 템플릿도 다양한 자료형의 함수를 만들어낼 수 있다.
```
template <typename T>       // 템플릿 선언
T Add (T num1, T num2)
{
    return num1 + num2;
}

cout << Add<int>(15, 20) << endl;       // 템플릿 사용
cout << Add<double>(3.2, 3.2) << endl;
```
 - 위의 예시는 템플릿 선언의 예시이다.
   - T는 자료형을 결정짓기 않겠다는 의미로 사용한 것임을 컴파일러에게 알리기 위해 `template <typename T>`를 맨 위에 선언한다.
   - `template <typename T>`는 `template <class T>`와 동일하고, T대신 다른 문자를 사용할 수도 있다.
 - `Add<int>(15, 20)`같은 구문을 이용해 템플릿을 사용한다.
   - `<int>`의 의미는 'T를 int로 해서 만들어진 Add함수를 호출한다'는 의미이다.
   - T를 int로 바꾼 int형 함수를 컴파일러가 새로 정의하여 실행한다. 한번 정의된 함수는 메모리 공간에 남아있어 대기하게 된다.
 - 위의 템플릿 사용 방법에서 자료형 표현(`<int>`, `<double>`)을 생략할 수 있다. 컴파일러가 매개변수의 자료형을 참조해 호출될 함수의 유형을 컴파일러가 결정하기 때문이다.

## 함수 템플릿과 템플릿 함수
 - 다음의 정의를 가리켜 함수 템플릿(function template)이라 한다
```
template <typename T>
T Add (T num1, T num2)
{
    return num1 + num2;
}
```
 - 위의 템플릿을 기반으로 컴파일러가 만들어내는 다음 유형의 함수들을 가리켜 템플릿 함수(template function)이라 한다.
```
int Add<int> (int num1, iunt num2) { return num1 + num2; }
double Add<double>(double num1, double num2) { return num1 + num2; }
```
 - 템플릿 함수는 컴파일러에 의해 생성된 함수이기 때문에 '생성된 함수(Generated Function)'라 불린다.

## 둘 이상의 형(Type)에 대해 템플릿 선언하기
 - 함수 템플릿을 정의할 때에는 기본 자료형 선언을 못하는 것으로 오해하는 경우가 있는데, 템플릿의 정의에도 다양한 자료형의 선언이 가능할 뿐만 아니라, 둘 이상의 형(type)에 대해서 템플릿을 선언할 수도 있다.
 - 예시는 다음과 같다.
```
template <class T1, class T2>
void ShowData(double num)
{
	cout<<(T1)num<<", "<<(T2)num<<endl;
}

int main(void)
{
	ShowData<char, int>(65);
	ShowData<char, int>(67);
	ShowData<char, double>(68.9);
	ShowData<short, double>(69.2);
	ShowData<short, double>(70.4);
	return 0;
}
```
 - 위에서 템플릿의 키워드로 class가 쓰였는데, c++초창기에는 키워드를 class로만 선언할 수 있었다. 이후에 typename이 추가된 것이다.

## 함수 템플릿의 특수화
 - 상황에 따라서 템플릿 함수의 구성방법에 예위를 둘 필요가 있는데(max함수에 문자열을 집어넣는 경우) 이 때 사용되는 것이 함수 템플릿의 특수화이다.
```
template <typename T>
T Max(T a, T b)
{
	return a > b ? a : b ;
}

template <>
char* Max(char* a, char* b)     // char *형 함수에 대한 특수 템플릿
{
	cout<<"char* Max<char*>(char* a, char* b)"<<endl;
	return strlen(a) > strlen(b) ? a : b ;
}

template <>
const char* Max(const char* a, const char* b)   // const char *형 함수에 대한 특수 템플릿
{
	cout<<"const char* Max<const char*>(const char* a, const char* b)"<<endl;
	return strcmp(a, b) > 0 ? a : b ;
}

int main(void)
{	cout<< Max(11, 15)				<<endl;
	cout<< Max('T', 'Q')			<<endl;
	cout<< Max(3.5, 7.5)			<<endl;
	cout<< Max<char *>("Simple", "Best")	<<endl;

	char str1[]="Simple";
	char str2[]="Best";
	cout<< Max(str1, str2)			<<endl;
	return 0;
}
```
 - 특수 템플릿을 만드는 것은 컴파일러에게 특정한 자료형의 함수는 프로그래머가 직접 제시하니 별도로 만들지 말고 제시한 것을 사용하라는 의미이다.
 - 특수화하는 자료형 정보의 기입여부는 의미하는 바는 차이가 없지만, 가급적이면 가독성을 위해 자료형 정보를 명시해주는 것이 좋다.


# 13-2 클래스 템플릿
 - 클래스 또한 템플릿으로 정의가 가능하다. 이를 클래스 템플릿이라 하며, 클래스 템플릿을 기반으로 컴파일러가 만들어 낸 클래스를 템플릿 클래스라 한다.

## 클래스 템플릿의 정의
 - 다음은 int형 좌표정수를 표현하는 클래스이다.
```
#include <iostream>
using namespace std;

class Point
{
private:
	int xpos, ypos;
public:
	Point(int x=0, int y=0) : xpos(x), ypos(y)
	{  }
	void ShowPosition() const
	{
		cout<<'['<<xpos<<", "<<ypos<<']'<<endl;
	}
};
```
 - 이를 템플릿으로 변환하여 다양한 자료형을 위한 클래스 템플릿을 만들 수 있다.
```
#include <iostream>
using namespace std;

template <typename T>
class Point
{
private:
	T xpos, ypos;
public:
	Point(T x=0, T y=0) : xpos(x), ypos(y)
	{  }
	void ShowPosition() const
	{
		cout<<'['<<xpos<<", "<<ypos<<']'<<endl;
	}
};
```
 - 함수 템플릿과 마찬가지로 자료형을 결정짓지 않겠다는 의미로 문자T가 사용되고, T를 이용해 아래의 클래스를 템플릿으로 정의한다는 의미로 `template<typename T>`가 선언되었다.
 - 함수 템플릿과 마찬가지로 `Point<int>`, `Point<double>`, `Point<char>`처럼 각자 자료형에 따라 템플릿 클래스가 만들어지며 뒤의 '<자료형>'을 이용해 일반 클래스와 구분짓는다.
 - 클래스 템플릿은 함수 템플릿과는 다르게 자료형의 생략이 불가능하다.

## 클래스 템플릿의 선언과 정의의 분리
 - 클래스 템플릿도 멤버함수를 클래스 외부에 정의하는 것이 가능하다.
 - 외부함수의 정의해도 `template`키워드를 이용해 문자의 의미를 컴파일러에 알려야 한다.
```
template <typename T>
class Point
{
private:
	T xpos, ypos;
public:
	Point(T x=0, T y=0);
	void ShowPosition() const;
};

template <typename T>
Point<T>::Point(T x, T y) : xpos(x), ypos(y)
{  }

template <typename T>
void Point<T>::ShowPosition() const
{
	cout<<'['<<xpos<<", "<<ypos<<']'<<endl;
}
```
 - 위의 예시를 클래스 선언과 멤버함수 정의 부분으로 나누면 컴파일 오류가 발생한다. 메인에서 클래스 템플릿에 대한 정보가 부족하지 않기 때문이다.
   - 헤더 파일과 소스 파일을 동시에 컴파일하지 않기 때문에 나오는 에러이다.
   - 해결책은 두 가지가 있다. 헤더파일에 소스파일의 내용을 모두 넣는 것, 그리고 메인 파일에 소스파일을 인클루드 해주는 것
