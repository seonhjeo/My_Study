# 10-1 연산자 오버로딩의 이해와 유형
 - 함수를 오버로딩하여 다양한 기능을 제공하는 것처럼 연산자도 오버로딩하여 다른 기능을 추가할 수 있다.

## 연산자 오버로딩
 - c++은 객체 또한 기본 자료형처럼 덧셈과 같은 연산을 가능하게 하려 한다.
   - 'operator'키워드와 연산자를 묶어 함수의 이름을 정의하면 함수의 이름을 이용한 함수의 호출뿐만 아니라 연산자를 이용한 함수의 호출도 허용해준다.
   - 컴파일러는 연산자로 입력된 구문을 정의된 함수로 해석하여 컴파일한다.
 - 연산자 오버로딩 방법에는 멤버함수에 의한 방법과 전역함수에 의한 방법이 있다.
 - 연산자를 오버로딩한 함수도 const선언이 가능하다. 값을 변경하지 않는 연산자 오버로딩 함수에는 const를 추가해주는 것이 좋다.
 - 연산자 오버로딩의 예시는 다음과 같다.
```
class Point 
{
private:
	int xpos, ypos;
public:
	Point(int x=0, int y=0) : xpos(x), ypos(y)
	{  }
	Point operator+(const Point &ref)    // operator+라는 이름의 함수
	{
		Point pos(xpos+ref.xpos, ypos+ref.ypos);
		return pos;
	}
};

int main(void)
{
	Point pos1(3, 4);
	Point pos2(10, 20);
	Point pos3=pos1.operator+(pos2);    // Point pos3=pos1+pos2; 와 동일한 구문이다.
	return 0;
}
```

## 연산자를 오버로딩하는 두 가지 방법
 - 연산자는 멤버함수에 의해, 혹은 전역함수에 의해 오버로딩될 수 있다.
 - `pos1 + pos2`에 대한 해석은 다음과 같다.
   - 멤버함수는 `pos1.operator+(pos2);`로 해석된다.
   - 전역함수는 `operator+(pos1, pos2);`로 해석된다.
 - 사실 객체지향에는 전역에 대한 개념이 없지만 c++은 c스타일의 코드구현이 가능해 전역에 대한 개념이 여전히 존재한다.
   - 특별한 경우가 아니면 멤버함수를 기반으로 연산자를 로딩하는 것이 낫다.
 - 전역함수를 이용한 연산자 오버로딩의 일반적인 모델은 다음과 같다.
```
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
	friend Point operator+(const Point &pos1, const Point &pos2);
};

Point operator+(const Point &pos1, const Point &pos2)
{
	Point pos(pos1.xpos+pos2.xpos, pos1.ypos+pos2.ypos);

	return pos;
} 

int main(void)
{
	Point pos1(3, 4);
	Point pos2(10, 20);	
	Point pos3=pos1+pos2;

	pos1.ShowPosition();
	pos2.ShowPosition();
	pos3.ShowPosition();
	return 0;
}
```

## 오버로딩이 불가능한 연산자의 종류
 - 오버로딩이 불가능한 연산자의 종류는 다음과 같다.
   - .                  멤버 접근 연산자
   - .*                 멤버 포인터 연산자
   - ::                 범위 지정 연산자
   - ?:                 조건 연산자(3항 연산자)
   - sizeof             바이트 단위 크기 계산
   - typeid             RTTI 관련 연산자(프로그램 실행중 실시간으로 데이터 타입을 얻어오는 연산)
   - static_cast        형변환 연산자
   - dynamic_cast       형변환 연산자
   - const_cast         형변환 연산자
   - reinterpret_cast   형변환 연산자
   - 이들 연산자들의 오버로딩을 허용해주면 c++의 문법규칙에 어긋나는 문장 구성이 가능해지기 때문에 제한한다.
 - 멤버함수를 기반으로만 오버로딩이 가능한 연산자의 종류는 다음과 같다.
   - =                  대입 연산자
   - ()                 함수 호출 연산자
   - []                 배열 접근 연산자(인덱스 연산자)
   - ->                 멤버 접근용 포인터 연산자
   - 이들은 객체를 대상으로 진행해야 의미가 통하는 연산자들이기 때문에 멤버함수 기반으로만 오버로딩이 가능하다.

## 연산자 오버로딩의 주의사항
 - 본래의 의도를 벗어난 형태의 연산자 오버로딩은 좋지 않다.
    - +는 두 입력값을 더해 새로운 출력값을 만들어 주는 것이지만 연산결과를 입력값 주소에 저장하는 등의 코드는 옳지 않다.
 - 연산자의 우선순위와 결합성은 바뀌지 않는다.
 - 배개변수의 디폴트 값 설정이 불가능하다.
 - 연산자의 순수 기능까지 뺏을 수 없다.
   - +선언을 하고 함수는 -로 작성하는 것은 허용되지 않는다.


# 10-2 단항 연산자의 오버로딩
 - 피연산자가 두 개인 이항 연산자와 피연산자가 한 개인 단항 연산자의 가장 큰 차이점은 피연산자의 개수이다.
   - 이에 따른 연산자 오버로딩의 차이점은 매개변수의 개수에서 발견된다.

## 증가, 감소 연산자의 오버로딩
 - `++pos;`는 다음과 같이 선언할 수 있다.
    - 멤버함수로 오버로딩 `pos.operator++();`
    - 전역함수로 오버로딩 `operator++(pos);`
 - `operator++()`함수는 다음과 같이 정의된다.
   - 멤버변수들에 대한 계산을 한 후 자기 자신을 반환한다.
```
Point& operator++()     // 멤버 변수
{
    xpos += 1;
    ypos += 1;
    return *this;
}

Point &operator--(Point &ref)   // 전역 변수
{
    ref.xpos -= 1;
    ref.ypos -= 1;

    return ref;
}
```

## 전위증가와 후위증가의 구분
 - ++연산자와 --연산자는 피연산자의 위치에 따라 의미가 달라진다. 그래서 c++에서는 전위/후위에 대해 다음과 같은 규칙을 정했다.
   - ++pos      ->      pos.operator++();
   - pos++      ->      pos.operator++(int);
   - 함수의 매개변수의 마지막에 int를 추가하여 구분짓는다.
 - 전위 연산자는 증감된 값을 참조형으로 반환하지만, 후위 연산자는 값은 증가시키지만 증감 전의 값을 임시 객체로 만들어 반환한다.
   - 따라서 전위 연산자가 속도 면에서 더 이점이 있다.

## 반환형에서의 const선언과 const 객체
 - 연산자를 오버로딩한 함수의 반환형을 const로 선언한다는 것은 함수의 반환으로 인해 생성되는 임시객체를 const객체로 생성하겠다는 의미이다.
 - const 객체를 대상으로 참조자를 선언할 때에는 참조자도 const로 선언해야 한다. `const Point pos(3); const Point &ref = pos;`
 - `pos.operator(int)`함수, 즉 후위 연산자 함수들은 증감 전의 값을 임시 객체로 만들어 반환하는데, 이 함수에 const를 붙이면 반환되는 임시객체가 상수 객체가 된다.
   - 따라서 값의 변경이 불가능해지므로 `(pos++)++;`와 같은 연속적인 후위 연산이 불가능해진다. 이는 c++문법 규약을 따른 것이다.


# 10-3 교환법칙 문제의 해결
 - 교환법칙이란 'A + B의 결과는 B + A의 결과와 같음'을 뜻한다. 즉 연산자를 중심으로 한 피연산자의 위치는 연산의 결과에 아무런 영향을 미치지 않는다는 법칙이다.
 - 교환법칙이 성립하는 대표적인 연산으로 덧셈과 곱셈이 있다.

## 자료형이 다른 두 피연산자를 대상으로 하는 연산
 - 기본적으로 연산에 사용되는 두 피연산자의 자료형은 일지해야 한다. 그리고 일치하지 않으면 형변환의 규칙에 따라서 변환이 진행된 다음에 연산이 이뤄져야 한다.
 - 하지만 연산자 오버로딩을 이용하면 이러한 연산규칙에 예외를 둘 수 있다.
 - 교환법칙이 비성립하는 연산자 오버로딩의 대표적인 예시는 다음과 같다.  
 `Point operator*(int times) { Point pos(xpos*times, ypos*times); return pos }`
   - 위 함수는 `cpy = pos * 3;`의 형태는 가능하나 `cpy = 3 * pos;`의 형태는 불가능하다.

## 교환법칙 성립을 위한 구현
 - 위의 예시에서 `cpy = 3 * pos;`의 형태가 가능하려면 `cpy = operator*(3, pos);`와 같이 전역함수의 형태로 오버로딩을 하는 수밖에 없다.
 - 전역함수로 정의한 `operator*`함수는 다음과 같다.
```
Point operator*(int times, Point &ref)
{
    Point pos(ref.xpos * times, ref.ypos * times);
    return pos;
}
```
 - 결과적으로는 다음과 같은 클래스가 생기게 된다.
```
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
	Point operator*(int times) 
	{
		Point pos(xpos*times, ypos*times);
		return pos;
	}
	friend Point operator*(int times, Point & ref);
};

Point operator*(int times, Point & ref)
{
	return ref*times;
}
```


# cin, cout, endl의 정체
 - 1장에서 처음 사용했던 cin, cout, endl의 구조를 설명한다.
```
#include <iostream>	

namespace mystd                 // 실제 cin, cout과 구분하기 위한 이름공간
{
	using namespace std;

	class ostream
	{
	public:
		ostream& operator<< (char * str)
		{
			printf("%s", str);
			return *this;       // 자기 자신 반환
		}
		ostream& operator<< (char str) 
		{
			printf("%c", str);
			return *this;
		}
		ostream& operator<< (int num) 
		{
			printf("%d", num);
			return *this;
		}	
		ostream& operator<< (double e) 
		{
			printf("%g", e);
			return *this;
		}
		ostream& operator<< (ostream& (*fp)(ostream &ostm))
		{
			return fp(*this);
		}
	};

	ostream& endl(ostream &ostm)
	{
		ostm<<'\n';
		fflush(stdout);
		return ostm;
	}
	ostream cout;
}

int main(void)
{
	using mystd::cout;
	using mystd::endl;
	cout<<3.14<<endl<<123<<endl;
	return 0;
}
```
 - 즉 연산자 오버로딩을 이용해 `cout << "123";`을 `cout.operator<<("123");`으로 변환하여 해당 함수로 출력한 것이다.
 - 모든 함수가 자기자신을 반환하여 연속해서 생기는 `<<` 연산자에 대한 cout 내의 함수를 호출할 수 있게끔 해준다.