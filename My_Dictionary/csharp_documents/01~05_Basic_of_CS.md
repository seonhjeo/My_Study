 - 이 문서는 C/C++에도 존재하는 선언, 변수, 연산, 흐름 제어, 메소드 사용법 중 C/C++과 다른 방식인 것들만 간추려 작성한 문서입니다.


# 01_프로그래밍을_시작합니다.
 - window-visual studio로 C# 프로그래밍 환경 세팅

# 02_처음_만드는_C#_프로그램.

### using
 - `using`은 c#의 키워드 중 하나.
 - `using` 이후 네임스페이스를 붙이면 해당 네임스페이스를 전부 사용하겠다는 뜻.
 - `using static`은 어떤 데이터 형식(클래스 등)의 정적 멤버를 데이터 형식의 이름을 명시하지 않고 참조하겠다고 선언하는 기능

### 네임스페이스
 - 네임스페이스는 성격이나 하는 일이 비슷한 클래스, 구조체, 인터페이스, 대리자, 열거 형식 등을 하나의 이름 아래 묶는 역할을 수행
```
namespace '네임스페이스_이름'
{
	// 클래스
	// 구조체
	// 인터페이스 등...
}
```

### static void Main()
 - `main`
   - c#프로그램의 진입점이 되는 특수한 메소드.
   - 모든 프로그램은 반드시 `Main`이라는 이름의 메소드를 하나 가져야 됨.
 - `static`
   - 한정자(modifier)로서 메소드나 변수 등을 수식하는 단어.
   - `static`키워드로 수식되는 코드는 프로그램이 처음 구동될 때부터 메모리에 할당해 놓음.

### CLR
 - Common Language Runtime의 약자이며 Java 가상 머신과 비슷한 역할을 함
 - C# 컴파일러는 C#소스코드를 컴파일해 IL(intermediate Language)라는 중간 언어로 작성된 실행 파일을 만들어 냄
 - 사용자가 이 파일을 실행시키면 CLR이 중간 코드를 읽어 들여 다시 하드웨어가 이해할 수 있는 네이티브 코드로 컴파일한 후 실행시킴. 이를 JIT컴파일이라고 부른다.
 - JIT컴파일은 실행에 필요한 코드를 실행할 때마다 실시간으로 컴파일해 실행한다는 뜻
 - 이렇게 작동하는 이유는 C#뿐 아니라 다른 언어도 지원하도록 설계되었기 때문. 장점으로는 플랫폼에 최적화된 코드 제작이 가능하고, 단점으로는 컴파일 비용이 부담된다는 점.
 - CLR은 단순히 언어를 동작시키는 환경 기능 외에도 프로그램의 예외 발생 시 처리, 언어간의 상속 지원, COM과의 상호 운반성 연결, 자동 메모리 관리 등의 기능 제공


# 03_데이터_관리

### 값과 참조
 - C#의 데이터 기본 데이터 형식과 복합 데이터로 나뉘고 이들을 값 형식과 참조 형식으로 또 나눌 수 있다.
 - 값 형식은 변수가 값을 담는 데이터 형식을 말하고, 참조 형식은 변수가 값 대신 값이 있는 곳의 위치를 담는 데이터 형식을 말한다.
   - 값 형식 변수는 선언될 시 스택에 적재되고, 해당 변수가 사용되는 함수가 종료될 시 스택에서 해제된다.
   - 참조 형식 변수는 힙과 스택을 함께 이용하며, 힙 영역에는 데이터를 저장하고, 스택 영역에 데이터가 저장된 힙 메모리의 주소를 저장한다.
   - 함수 내에서 사용이 종료되 스택 메모리가 해제되어도 힙에 있는 데이터는 사라지지 않게 된다. 힙에서 사용되지 않는 변수는 CLR의 가비지 컬렉터가 판단 하에 수거한다.

### object 형식
 - object는 물건, 객체라는 뜻. 프로그래밍 언어에서의 object는 어떠한 데이터든 다룰 수 있는 형식을 의미
 - c#은 object가 모든 데이터를 다룰 수 있도록 하기 위해 모든 데이터 형식이 자동으로 object형을 상속받게 하였다.
 - ojbect형식은 참조 형식이므로 데이터를 힙에 할당한다. 또한 object형은 값 형식의 데이터를 힙에 할당하기 위해 박싱과 언박싱 기능을 제공한다.
   - 박싱은 값을 힙에 복사해 저장하고, 그 힙의 주소를 스택에 저장한다.
   - 언박싱은 힙에 복사된 값을 꺼내 다른 변수에 복사 혹은 저장한다.

### 열거형과 Nullable, var
 - 열거형은 `enum`으로 선언하며 작성시 컴파일러가 0부터 상수를 자동으로 대입해준다.
   - 이 때 상수값을 프로그래머가 수동으로 할당할 수 있다. 수동 할당시 다른 요소에 같은 값이 할당될 수 있으므로 주의해야 한다.
 - nullable형은 변수 선언시에 해당 값이 `null`이 될 수 있다는 것을 컴파일러에 알려주는 역할을 한다.
   - nullable형의 선언은 변수형식 뒤에 ?을 붙여 선언한다. 또한 각 변수형식이 갖고 있는 `HasValue`속성으로 null값 여부를 확인할 수 있다.
```
int? a = null;
```
 - c#은 강타입 언어지만 `var`키워드를 이용한 약형식 검사도 지원한다.
   - 단 `var`키워드를 사용한 변수는 반드시 선언과 동시에 초기화해줘야 한다.
   - 또한 `var`은 오로지 지역변수로만 사용 가능하다.
   - `var`형식은 `object`형과 다르게 컴파일 단계에서 컴파일러가 적합한 형식으로 자연스럽게 변환해준다.

### 공용 형식과 문자열 다루기
 - C#은 CLR에서 다른 언어와 소통하기 위해 .NET의 공용 형식을 따른다.
 - `System.Int16`과 같이 선언하여 사용할 수 있다.
 - 자세한 내용은 책의 p91~p111참조


# 04_연산자

### null 조건부 연산자
 - `A?.B`로 사용하며 해당 객체의 멤버에 접근하기 전에 해당 객체가 null인지 검사하여, 그 결과가 참이면 null을 반환하고, 아니면 멤버를 반환한다.
 - 객체의 멤버가 아닌 배열의 컬렉션 객체를 참조하기위한 `?[]`연산자도 있다.

### null 병합 연산자
 - `A ?? B`로 사용하며 A가 null이 아닐 시 A를, 아니면 B를 반환한다.


# 05_코드의_흐름제어

### switch식
 - switch식은 어떤 작업의 분기를 거쳐 값을 내놓아야 하는 경우에 사용된다.
 - switch문보다 더욱 간결하여 가독성이 상승된다.
```
// switch문을 사용한 코드
switch(score)
{
  case 90:
    grade = "A";
    break;
  case 80:
    grade = "B";
    break;
  case 70:
    grade = "C";
    break;
  case 60:
    grade = "D";
    break;
  default:
    grade = "F";
}

// switch식을 사용한 코드
string grade = score switch
{
  90 => "A",
  80 => "B",
  70 => "C",
  60 => "D",
  _ => "F"
};
```

### 점프문
 - 흐름 제어문들의 흐름을 분기하거나 끊을 때 사용한다.
 - C#에서 제공하는 점프문은 5가지가 있다.
   - break, continue, goto, return, throw
 - break
   - 현재 실행 중인 반복문이나 switch문의 실행을 중단할 때 사용한다.
```
int i = 0;
while (i >= 0) {
  if (i == 10) break; // i가 10이 되면 반복문 탈출
  ...
}
```
 - continue
   - 현재 실행 중인 반복문을 한 회 건너뛰어 반복을 계속 수행하게 한다.
```
for (int i = 0; i < 5; i++) {
  if (i == 3) continue; // i가 3일 때 현재 실행 중인 반복문을 건너뜀
  ...
}
```
 - goto
   - 해당 레이블이 가리키는 곳으로 바로 뛰어넘어감
   - 그다지 추천하지 않지만 중첩된 반복문을 단번에 탈출할 때에는 편리함
```
...
if (true) goto LABEL; // LABLE이라는 이름의 LABLE로 건너뛰어짐
...
LABEL: // 레이블 선언 시 콜론 필수
...
```
