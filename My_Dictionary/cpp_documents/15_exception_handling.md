# 15-1 예외상황과 예외처리의 이해

## 예외상황을 처리하지 않았을 때의 결과
 - c++에서 말하는 예외(exception)는 프로그램의 실행 도중 발생하는 문제상황을 의미한다. 따라서 컴파일 시 발생하는 문법적인 에러는 예외의 범주에 포함되지 않는다.
 - 예외가 발생하면 그에 따른 처리가 이루어져야 된다.

## if문을 이용한 예외 처리
 - if문을 통해 예외상황의 발생유무를 확인한 다음에 그에 따른 처리를 진행한다.
 - if문을 이용하여 예외처리를 하면 예외처리를 위한 코드와 프로그램의 흐름을 구성하는 코드를 쉽게 구분하지 못한다.


# 15-2 c++의 예외처리 매커니즘
 - c++은 구조적으로 예외를 처리할 수 있는 매커니즘을 제공한다. 이 매커니즘을 이용하면 코드의 가독성과 유지보수성을 높일 수 있다. 예외의 처리를 프로그램의 일반적인 흐름에서 독립시킬 수 있기 때문이다.

## c++의 예외처리 매커니즘: try, catch, throw
 - try블록
   - try블록은 예외발생에 대한 검사의 범위를 지정할 때 사용된다. 즉 try블록 내에서 예외가 발생하면 이는 c++의 예외처리 매커니즘에 의해 처리가 된다.
 - catch블록
   - catch블록은 try블록에서 발생한 예외를 처리하는 코드가 담기는 영역으로써, 그 형태가 마치 반환형 없는 함수와 유사하다.
```
try
{
    // 예외발생 예상지역
}
catch(처리할 예외의 종류 명시)
{
    // 예외처리 코드 삽입
}
```
 - try와 catch는 하나의 문장이다. 따라서 항상 이어서 등장해야 하고, 중간에 다른 문장이 오면 안된다.
 - throw 블록
   - throw 블록은 예외가 발생했음을 알리는 문장의 구성에 사용된다.  
`throw expn;`
   - 위의 문장에서 expn은 변수, 상수 그리고 객체 등 표현 가능한 모든 데이터가 될 수 있으나, 예외상항에 대한 정보를 담은, 의미 있는 데이터이어야 한다.
   - 위 문장이 실행되면 c++의 예외처리 매커니즘이 동작해 일반적인 프로그램의 흐름과는 다른 예외처리의 흐름이 시작된다.
 - throw에 의해 던져진 '예외 데이터'는, '예외 데이터'를 감싸는 try블록에 의해 감지가 되어, 이어서 등장하는 catch 블록에 의해 처리된다.
```
try
{
    if (예외 발생)
        throw expn;
}
catch(type exn)
{
    // 예외처리
}
```

## 예외처리 매커니즘의 적용
```
int main(void)
{
	int num1, num2;
	cout<<"두 개의 숫자 입력: ";
	cin>>num1>>num2;

	try
	{
		if(num2==0)
			throw num2;
		cout<<"나눗셈의 몫: "<< num1/num2 <<endl;
		cout<<"나눗셈의 나머지: "<< num1%num2 <<endl;
	}
	catch(int expn)
	{
		cout<<"제수는 "<<expn<<"이 될 수 없습니다."<<endl;
		cout<<"프로그램을 다시 실행하세요."<<endl;
	}
	cout<<"end of main"<<endl;
	return 0;
}
```
 - 예외가 발생되면 catch블록이 실행된 후에, 예외가 발생한 지점 이후를 실행하는 것이 아니라, catch블록 이후가 실행된다.
 - throw절에 의해 던져진 예외 데이터의 자료형과 catch블록의 매개변수 자료형은 일치해야 한다. 만약 일치하지 않으면 던져진 예외 데이터는 catch블록으로 전달되지 않는다.

## try블록을 묶는 기준
 - 앞서 보인 예제를 통해 알 수 있는 실행흐름은 다음과 같다.
   - try블록을 만나면 그 안에 삽입된 문장이 순서대로 실행된다.
   - try블록 내에서 예외가 발생하지 않으면 catch블록 이후를 실행한다.
   - try블록 내에서 예외가 발생하면, 예외가 발생한 지점 이후의 나머지 try영역을 건너뛴다.
 - try블록을 묶는 기준은 예외가 발생할만한 영역만 묶는 것이 아니라 그와 관련된 모든 문장을 함께 묶어 이를 하나의 일(work)의 단위로 구성하는 것이다.
   - 만일 예외가 발생할만한 영역만 묶으면, 예외처리를 한 이후에 실행해서는 안 되는 문장이 실행될 수도 있다.


# 15-3 스택 풀기(Stack Unwinding)
#include <iostream>
using namespace std;

void Divide(int num1, int num2)
{
	if(num2==0)
		throw num2;
	
	cout<<"나눗셈의 몫: "<< num1/num2 <<endl;
	cout<<"나눗셈의 나머지: "<< num1%num2 <<endl;
}

int main(void)
{
	int num1, num2;	
	cout<<"두 개의 숫자 입력: ";
	cin>>num1>>num2;

	try
	{
		Divide(num1, num2);
		cout<<"나눗셈을 마쳤습니다."<<endl;
	}
	catch(int expn)
	{
		cout<<"제수는 "<<expn<<"이 될 수 없습니다."<<endl;
		cout<<"프로그램을 다시 실행하세요."<<endl;
	}

	return 0;
}
 - 만일 Divide함수 내에 try-catch문이 없다면, 이러한 경우에는 Divide함수를 호출한 위치로 예외 데이터가 전달된다. 따라서 메인함수 내의 try-catch문에서 예외 데이터를 처리하게 된다.
   - 이 때에도 예외처리가 된 후에는 catch블록의 다음 문장을 실행하게 된다.
 - **예외가 처리되지 않으면, 예외가 발생한 함수를 호출한 영역으로 예외 데이터와 더불어 예외에 대한 책임까지 전달된다.**
 - **함수 내에서 함수를 호출한 영역으로 예외 데이터를 전달하면, 해당 함수는 더 이상 실행되지 않고 종료가 된다.**

## 스택 풀기
 - 예외가 처리되지 않아서 함수를 호출한 영역으로 예외 데이터가 전달되는 현상을 스택 풀기라고 한다.
```
#include <iostream>
using namespace std;

void SimpleFuncOne(void);
void SimpleFuncTwo(void);
void SimpleFuncThree(void);

int main(void)
{
	try
	{
		SimpleFuncOne();
	}
	catch(int expn)
	{
		cout<<"예외코드: "<< expn <<endl;
	}
	return 0;
}
void SimpleFuncOne(void) 
{ 
	cout<<"SimpleFuncOne(void)"<<endl;
	SimpleFuncTwo(); 
}
void SimpleFuncTwo(void) 
{ 
	cout<<"SimpleFuncTwo(void)"<<endl;
	SimpleFuncThree(); 
}
void SimpleFuncThree(void) 
{ 
	cout<<"SimpleFuncThree(void)"<<endl;
	throw -1; 
}
```
 - 위의 예제의 함수호출 순서는 main -> SimpleFuncOne -> SimpleFuncTwo -> SimpleFuncThree이다.
 - 예외 데이터가 전달되며 스택이 해체되는 순서는 함수호출 순서의 정반대이다. 결과적으로 예외는 three에서 발생했지만 예외의 처리는 main함수에서 이뤄지게 된다.
 - 예외 데이터가 main까지 도달했는데, main함수에서조차 예외를 처리하지 않으면 terminate함수(프로그램 종료 함수)가 호출되면서 프로그램이 종료된다.

## 자료형이 불일치해도 예외 데이터는 전달된다.
```
int SimpleFunc(void)
{
    ...
    try
    {
        if (...)
            throw -1;   // int형 예외 데이터 발생
    }
    catch(char expn) { ... }    // char형 예외 데이터 
}
```
 - 위의 예시에서처럼 자료형이 불일치하면 catch블록으로 값이 전달되지 않으며 예외는 처리되지 않는다. 따라서 SimpleFunc함수를 호출한 영역으로 예외 데이터가 전달된다.

## 하나의 try블록과 다수의 catch블록
 - 하나의 try블록 내에서 유형이 다른 둘 이상의 예외상황이 발생할 수 있고, 이러한 경우 각각의 예외를 표현하기 위해 사용되는 예외 데이터의 자료형이 다를 수 있기 때문에, try블록에 이어서 등장하는 catch블록은 둘 이상이 될 수 있따.

## 전달되는 예외의 명시
 - 함수 내에서 발생할 수 있는 예외의 종류도 함수의 특징으로 간주된다. 따라서 이미 정의된 특정 함수의 호출을 위해서는 함수의 이름, 매개변수 선언, 반환형 정보에 더해 함수 내에서 전달될 수 있는 예외의 종류와 그 상황도 알아야 한다.
 - 함수에서 전달되는 예외의 명시는 다음과 같이 할 수 있다.  
`int ThrowFunc(int num) throw (int, char) { ... }`
   - 위의 throw 선언은 ThrowFunc함수 내에서 예외상황의 발생으로 인해 int형 예외 데이터와 char형 예외 데이터가 전달될 수 있음을 알리는 것이다.
   - 따라서 위의 원형 선언을 본 프로그래머는 int형과 char형 예외 데이터를 받는 catch블록을 각각 구성하게 된다.
   - 위와 같이 전달되는 예외의 형이 선언되면, 선언된 자료형과 다른 자료형의 데이터가 전달될 경우 terminate함수의 호출로 인해 프로그램이 종료되고 만다.
 - 다음은 어떠한 예외도 전달하지 않는 함수의 선언이다.  
`int ThrowFunc(int num) throw () { ... }`
   - 만일 위의 함수가 예외를 전달하면 프로그램은 그냥 종료된다.


# 15-4 예외 상황을 표현하는 예외 클래스의 설계
 - 클래스의 객체도 예외 데이터가 될 수 있고 이것이 좀 더 보편적인 방법이다.
 - 예외 클래스라고 해서 일반 클래스와 특별히 다른 것은 없다. 다만 해당 예외상황을 잘 표현할 수 있도록 정의하고 너무 복잡하게 정의하지 않으면 된다.
 - 예외 클래스도 상속관계를 구성할 수 있다.
```
#include <iostream>
#include <cstring>
using namespace std;

class AccountException
{
public:
	virtual void ShowExceptionReason() =0;  // 순수 가상함수
};

class DepositException : public AccountException
{
private:
	int reqDep;		// 요청 입금액
public:
	DepositException(int money) : reqDep(money)
	{ }
	void ShowExceptionReason()
	{
		cout<<"[예외 메시지: "<<reqDep<<"는 입금불가]"<<endl;
	}
};

class WithdrawException : public AccountException
{
private:
	int	balance;	// 잔고
public:
	WithdrawException(int money) : balance(money)
	{ }
	void ShowExceptionReason()
	{
		cout<<"[예외 메시지: 잔액 "<<balance<<", 잔액부족]"<<endl;
	}
};

class Account
{
private:
	char accNum[50];	// 계좌번호
	int	balance;		// 잔고
public:
	Account(char * acc, int money) : balance(money)
	{
		strcpy(accNum, acc);
	}
	void Deposit(int money) throw (AccountException)    // 상속에 의해 DepositException객체도 AccountException 객체로 간주되기 때문에 해당 선언이 가능
	{
		if(money<0)
		{
			DepositException expn(money);
			throw expn;	
		}
		balance+=money;
	}
	void Withdraw(int money) throw (AccountException)
	{
		if(money>balance)
			throw WithdrawException(balance);
		balance-=money;
	}
	void ShowMyMoney()
	{
		cout<<"잔고: "<<balance<<endl<<endl;
	}
};

int main(void)
{
	Account myAcc("56789-827120", 5000);

	try
	{
		myAcc.Deposit(2000);
		myAcc.Deposit(-300);
	}
	catch(AccountException &expn)
	{
		expn.ShowExceptionReason();
	}
	myAcc.ShowMyMoney();

	try
	{
		myAcc.Withdraw(3500);
		myAcc.Withdraw(4500);
	}
	catch(AccountException &expn)
	{
		expn.ShowExceptionReason();
	}	
	myAcc.ShowMyMoney();
	return 0;
}
```

## 예외의 전달방식에 따른 주의사항
 - try블록의 뒤를 이어 등장하는 catch블록이 둘 이상일 경우 적절한 catch블록을 찾는 과정은 if~else구문과 동일하다.
 - 따라서 여러 개의 catch 블록을 사용할 때 가장 작은 조건부터, 가장 최후미의 유도 클래스부터 처리하는 블록을 작성하고, 점점 조건 등을 키워나가며 catch블록을 작성해야 한다.


# 15-5 예외처리와 관련된 또 다른 특성들

## new연산자에 의해 발생하는 예외
 - new 연산에 의한 메모리 공간의 할당이 실패하면 bad_alloc라는 예외가 발생한다. bad_alloc은 헤더파일 <new>에 선언된 예외 클래스로써 메모리 공간의 할당이 실패했음을 알리는 의도로 정의되었다.

## 모든 예외를 처리하는 catch블록
 - 다음과 같이 catch블록을 선언하면 try블록 내에서 전달되는 모든 예외가 자료형에 상관없이 걸려든다.
```
try
{ ... }
catch(...)      // ...은 전달된는 모든 예외를 다 받아주겠다는 선언
{ ... }
```
 - 따라서 마지막 catch블록에 덧붙여지는 경우가 많은데, 대신 catch의 매개변수 선언에서 보이듯이 발생한 예외와 관련해서 그 어떠한 정보도 전달받을 수 없으며 전달된 예외의 종류도 구분이 불가능하다.

## 예외 던지기
 - catch블록에 전달된 예외는 다시 던져질 수 있다. 그리고 이로 인해 하나의 예외가 둘 이상의 catch블록에 의해 처리될 수 있다.
```
#include <iostream>
using namespace std;

void Divide(int num1, int num2)
{
	try
	{
		if(num2==0)
			throw 0;
		cout<<"몫: "<<num1/num2<<endl;
		cout<<"나머지: "<<num1%num2<<endl;
	}
	catch(int expn)
	{
		cout<<"first catch"<<endl;
		throw;  // 예외 다시 던지기
	}
}

int main(void)
{	
	try
	{
		Divide(9, 2);
		Divide(4, 0);
	}
	catch(int expn)
	{
		cout<<"second catch"<<endl;
	}
	return 0;
}
```
 - catch블록 안의 `throw;`구문에 의해 cathc블록으로 전달된 예외가 소멸되지 않고 다시 던져진다. 따라서 이 함수를 호출한 영역으로 예외가 전달된다. 결과적으로 메인 함수의 catch문까지 실행되게 된다.
 - 예외처리는 가급적 간결한 구조를 따르는 것이 좋다. 따라서 정말로 필요한 상황이 아니라면, 굳이 예외를 다시 던지기 위해 노력할 필요는 없다.