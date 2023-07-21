# 프로퍼티

### 프로퍼티란?
 - 프로퍼티는 접근자를 간소화 하기 위해 C#에서 제공하는 기능이다.
 - 기존의 접근 메소드 선언 및 사용을 최소화하여 `=` 할당 연산자를 통해 값을 불러오고 변경할 수 있게 해준다.
 - 프로퍼티 선언은 다음과 같이 할 수 있다.
```
// 기본 프로퍼티 선언
class MyClass
{
  private int myField;
  public int MyField { get { return myField; } set { myField = value; }}
}

// 자동 구현 프로퍼티 선언
class MyClass
{
  private int MyField { get; set; };
}
```

### 프로퍼티와 생성자
 - 프로퍼티를 이용해 객체를 초기화 할 수 있다.
   - 프로그래머가 초기화하고 싶은 프로퍼티만 넣어서 초기화할 수 있다.
```
클래스이름 인스턴스 = new 클래스이름() {
  프로퍼티1 = 값, 프로퍼티2 = 값, ...
}
```

### 초기화 전용(Init-Only) 자동 구현 프로퍼티
 - 프로퍼티를 초기화만 하고 읽기 전용으로 선언할 때에는 `init`키워드를 사용하면 된다.
   - 이렇게 선언한 프로퍼티를 초기화 자동 구현 프로퍼티라고 한다.
 - 초기화 전용 프로퍼티는 위에서 언급한 '프로퍼티를 이용한 객체 초기화'방법을 사용하여 초기화해줄 수 있다.
```
public class Transaction
{
	public string From   { get; init; }
	public string To     { get; init; }
	public int    Amount { get; init; }
}
```

### 레코드 형식으로 만드는 불변 개체
 - 불변 객체는 내부 상태를 변경할 수 없는 객체를 의미한다. 이러한 특성 때문에 불변 객체에서는 데이터 복사와 비교가 많이 이루어진다.
 - 레코드는 불변 객체에서 빈번하게 이뤄지는 이 두 가지 연산을 편리하게 수행할 수 있도록 만들어진 형식이다.
   - 값 형식처럼 다룰 수 있는 불변 참소 형식으로, 참조 형식의 비용 효율과 값 형식이 주는 편리함을 모두 제공한다.
 - 레코드 선언은 `record`키워드를 이용해 다음과 같이 할 수 있다. 이렇게 선언한 레코드로 인스턴스를 만들면 불변 객체가 만들어진다.
```
record RTransaction
{
	public string From     { get; init; }
	public string To       { get; init; }
	public int    Amount   { get; init; }
}
```
 - c#컴파일러는 레코드 형식을 위한 복사 생성자를 자동으로 작성한다. 단 이 복사 생성자는 명시적으로 호출할 수 없고, `with`식을 이용해야 한다.
   - 복사와 동시에 일부 혹은 전체의 프로퍼티 값을 다시 초기화해줄 수 있다.
```
RTransaction tr1 = new RTransaction{From="Alice", To="Bob", Amount=100};
RTransaction tr2 = with tr1 {To="Charlie"};
```
 - 컴파일러는 레코드의 상태를 비교하는 `Equals()`메소드를 자동으로 구현한다.
   - 두 레코드의 모든 프로퍼티가 동일하면 True를, 아니면 False를 반환한다.


### 무명 형식
 - 형식에 이름이 필요한 이유는 그 형식의 이름을 이용해 인스턴스를 만들기 때문이다.
 - 무명 형식은 선언과 동시에 인스턴스를 할당한다. 이 때문에 인스턴스를 만들고 다시는 사용하지 않을 때 용이하다.
   - 이와 같이 선언한 무명 형식의 인스턴스는 여느 객체처럼 프로퍼티에 접근하여 사용할 수 있다.
```
var myInstance = new { name="홍길동", age= "17" };

Console.WriteLine(myInstance.name, myInstance.age);
```
 - 무명 형식의 프로퍼티에 할당된 값은 변경이 불가능하다.

### 인터페이스와 프로퍼티
 - 인터페이스는 프로퍼티와 인덱서를 가질 수 있다. 
   - 프로퍼티나 인덱서를 가진 인터페이스를 상속하는 클래스는 반드시 해당 프로퍼디와 인덱서를 구현해야 한다.
	 - 인터페이스에 들어가는 프로퍼티는 구현부를 갖지 않으므로 상속받는 클래스에서 구현해주어야 한다.
	 - 인터페이스의 프로퍼티 선언은 클래스의 자동 구현 프로퍼티 선언과 모습이 동일하다.
```
interface IProduct
{
	string ProductName { get; set; }
}

class Product : IProduct
{
	private string productName;
	public string ProductName{
		get{ return productName; }
		set{ productName = value; }
	}
}
```

### 추상 클래스와 프로퍼티
 - 추상 클래스는 클래스처럼 구현된 프로퍼티와 인터페이스처럼 구현되지 않는 프로퍼티를 둘 다 가질 수 있다. 추상 클래스의 프로퍼티를 추상 프로퍼티라고 한다.
 - 추상 프로퍼티 또한 추상 메소드처럼 파생 클래스가 해당 프로퍼티를 구현 및 재정의하도록 강제하는 역할을 한다.
 - 추상 프로퍼티는 자동 구현 프로퍼티와의 혼동을 방지하기 위해 `abstract`한정자를 이용해 선언한다.
```
abstract class 추상_클래스_이름
{
	abstract 데이터_형식 데이터_이름 { get; set; }
}
```