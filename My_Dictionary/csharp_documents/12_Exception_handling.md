# 예외 처리하기

### 예외에 대하여
 - 프로그래머가 생각하는 시나리오에서 벗어나는 사건을 예외라고 한다.
 - 예외가 프로그램의 오류나 다운으로 이어지지 않도록 적절하게 처리하는 것을 예외처리라고 한다.
 - 프로그래머가 예외처리를 하지 않으면 예외가 발생했을 때 CLR이 프로그램을 강제로 종료하게 된다.

### try~catch로 예외 받기
 - 예외를 받는 방법은 다음과 같다.
```
try
{
    // 실행하고자 하는 코드
}
catch (예외_객체1)
{
    // 예외 발생시 처리
}
catch (예외_객체2)
{
    // 예외 발생시 처리
}
```

### System.Exception 클래스
 - `System.Exception` 클래스는 모든 예외 클래스의 조상 클래스이다.
 - 따라서 catch문 속 예외 객체를 `System.Exception`으로 선언하여 모든 예외를 받아낼 수 있다.
   - 하지만 프로그래머가 예측한 예외 말고 다른 예외까지 받아버릴 수 있으며, 이는 버그를 유발할 수 있다.

### 예외 던지기
 - catch구문이 받을 수 있는 예외를 프로그래머가 던질 수도 있따. `throw`구문을 이용하여 예외를 던진다.
```
try
{
    // ...
    throw new Exception("예외 던지기");
}
catch(Exception e)
{
    Console.WriteLine(e.Message);
}
```
 - `throw`는 보통 문(statement)으로 사용하지만, 식(expression)으로도 사용할 수 있다.
```
int? a = null;
int b = a ?? throw new ArgumentNullException();
```

### try~catch와 finally
 - 예외 처리로 인해 try블록의 자원 해제 같은 중요 코드를 실행하지 못하면 그것도 버그를 만드는 원인이 된다.
 - C#에서는 예외 처리를 할 때 자원 해제 같은 뒷마무리를 깔끔하게 실행할 수 있도록 `finally`절을 제공한다.
   - finally절은 자신이 소속된 try절이 실행되면 어떤 경우에서도 실행된다. 심지어 return이나 throw처럼 흐름 제어를 외부 코드로 옮기는 상황에서도 말이다.
 - finally안에서 예외가 일어나면 해당 예외는 '처리되지 않은 예외'가 된다. 이를 방지하거나, finally구문 안에 try~catch를 실행하여 처리할 수 있다.
```
try
{
    // 실행하고자 하는 코드
}
catch (예외_객체1)
{
    // 예외 발생시 처리
}
finally
{
    // 예외가 발생해도 실행해야 되는 코드
}
```

### 사용자 정의 예외 클래스 만들기
 - 사용자 정의 예외는 특별한 데이터를 담아 예외 처리 루틴에 추가 정보를 제공하고 싶거나, 예외 상황을 더 잘 설명하고 싶을 때 필요하다.
 - 사용자 정의 예외 클래스 또한 `Exception`클래스를 상속받으므로 다음과 같이 선언할 수 있다.
```
// 구현
class MyException : Exception
{
    // 기본 생성자
    public MyException() {}

    // 예외문구를 포함하는 생성자
    public MyException(string message) : base(message) {}

    public object Argument { get; set; }

    public string Range { get; set; }
}

// 사용 예시
try
{
    // ...
    if (arg > 255)
    {
        throw new MyException() { Argument = arg, Range = "0~255"};
    }
}
catch (MyException e)
{
    Console.WriteLine(e.Message);
    Console.WriteLine($"Argument:{e.Argument}, Range:{e.Range}");
}
```

### 예외 필터하기
 - catch절이 받아들일 예외 객체에 제약 사항을 명시해서 해당 조건을 만족하는 예외객체에 대해서만 예외처리를 실행할 수 있다.
 - 조건을 만족하지 않은 예외는 처리되지 않은 상태 그대로 현재 코드의 호출자에게 던져진다.
```
class FilterableException : Exception
{
    public int ErrorNo {get; set;}
}

try
{
    // ...
    throw new FilterableException() {ErrorNo = num};

}
catch (FilterableException e) when (e.ErrorNo < 0)
{
    // ...
}
```

### 예외 처리를 하는 이유
 - 예외 처리는 실제 일을 하는 코드와 문제를 처리하는 코드르 분리시켜 코드를 간결하게 만들어준다.
 - 예외 객체의 트레이스 프로퍼티를 통해 문제가 발생한 부분의 소스 코드 위치를 알 수 있어 디버깅에 용이하다.
 - 예외 처리는 여러 문제점들을 하나로 묶거나 코드에서 발생할 수 있는 오류를 종류별로 정리해주는 효과가 있다.