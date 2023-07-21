# 리플렉션
 - 리플렉션은 객체의 형식 정보를 들여다보는 기능이다.
 - 이 기능을 사용하면 프로그램 실행 중 객체의 형식 이름부터 프로퍼티 목록, 메소드 목록, 필드, 이벤트 목록까지 모두 열어볼 수 있다.
 - 또한 새로운 데이터 형식을 동적으로 만들 수도 있다.

### Object.GetType()메소드와 Type클래스
 - `Object`는 모든 데이터 형식의 조상이다. 즉 모든 데이터 형식은 다음의 메소드를 물려받고 있다.
   - `Equals()`, `GetHashCode()`, `GetType()`, `ReferenceEqual()`, `ToString()`
 - 이 중 `GetType()`메소드는 `Type`형식의 결과를 반환한다. Type형식은 .NET에서 사용되는 데이터 형식의 모든 정보를 담고 있다.
   - 데이터 형식의 정보에는 형식 이름, 소속된 어셈블리 이름, 프로퍼티 목록, 메소드 목록, 필드 목록, 이벤트 목록, 심지어는 이 형식이 상속하는 인터페이스 목록도 있다.
 - `Object.GetType()`메소드와 `Type` 형식을 사용하는 방법은 다음과 같다.
   - MSDN에서 `System.type`의 매뉴얼을 찾아보면 사용할 수 있는 메소드들을 확인할 수 있다.
```
int a = 0;

Type type = a.GetType();
FieldInfo[] fields = type.GetFields(); // 필드 목록 조회
```
 - 비교적 사용도가 높은 조회 메소드들은 다음과 같다.
```
메소드                  반환 형식                설명
GetConstructors()     ConstructorInfo[]      해당 형식의 모든 생성자 목록 반환
GetEvents()           EventInfo[]            해당 형식의 이벤트 목록 반환
GetFields()           FieldInfo[]            해당 형식의 필드 목록 반환
GetGenericArguments() Type[]                 해당 형식의 형식 매개변수 목록 반환
GetInterfaces()       Type[]                 해당 형식이 상속하는 인터페이스 목록 반환
GetMembers()          MemberInfo[]           해당 형식의 멤버 목록 반환
GetMethods()          MethodInfo[]           해당 형식의 메소드 목록 반환
GetNestedTypes()      Type[]                 해당 형식의 내장 형식 목록 반환
GetProperties()       PropertyInfo[]         해당 형식의 프로퍼티 목록 반환
```
 - `System.Reflection.BindingFlags`열거형을 이용해 특정 조건을 포함하는 학목만 조회할 수 있다.
```
Type type = a.GetType();

// Public 인스턴스 필드 조회
var fields1 = type.GetFields(BindingFlags.Public | BindingFlags.Instance);
```

 - `typeof`연산자와 `Type.GetType()`메소드로도 형식 정보를 얻을 수 있다.
```
Type a = typeof(int); // 형식의 식별자 자체를 인수로 받는다.

Type b = Type.GetType("System.Int32"); // 네임스페이스를 포함하는 전체 이름을 인수로 받는다.
```

### 리플렉션을 이용해 객체 생성하고 이용하기
 - 리플렉션을 이용해 코드 안에서 런타임에 특정 형식의 인스턴스를 만들어 프로그램이 좀 더 동적으로 동작할 수 있도록 구성할 수 있다.
 - 리플렉션을 이용해 동적으로 인스턴스를 만들기 위해서는 다음과 같이 수행하면 된다.
```
object a = Activator.CreateInstance(typeof(int));
List<int> list = Activator.CreateInstance<List<int>>();
```
 - `SetValue()`와 `GetValue()`를 이용해 프로퍼티의 값을 호출하고 변경할 수 있다.
```
class Profile
{
  public string Name { get; set; }
  public string Phone { get; set; }
}

static void Main()
{
  Type type = typeof(Profile);
  // 프로필 타입의 동적 객체 생성
  Object profile = Activator.CreateInstance(type);

  // Name이란 이름의 프로퍼티 반환
  PropertyInfo name = type.GetProperty("Name");

  // 값 설정, 마지막 인수는 인덱서 정보지만 필요없으므로 null
  name.SetValue(profile, "홍길동", null);

  // 값 불러오기, 마지막 인수 역시 인덱스
  Console.WriteLine(name.GetValue(profile, null));
}
```
 - 메소드의 정보를 담은 `MethodInfo` 클래스의 `Invoke()`메소드를 이용해 동적으로 메소드를 호출할 수 있다.
```
class Profile
{
  public string Name{ get; set; }

  public voi Print()
  {
    Console.WriteLine(Name);
  }
}

static void Main()
{
  Type type = typeof(Profile);
  Profile profile = (Profile)Activator.CreateInstance(type);
  profile.Name = "홍길동";

  // 메소드 정보 가져와 함수 실행하기
  MethodInfo method = type.GetMethod("Print");
  method.Invoke(profile, null);
}
```

### 형식 내보내기
 - 리플렉션을 이용해 프로그램 실행 중에 새로운 형식을 만들어낼 수 있다.
 - `System.Reflection.Emit` 네임스페이스의 클래스들을 통해 동적으로 새로운 형식을 만들 수 있다.
 - 사용 방식은 다음과 같다.(p561 참조)
   1. AssemplyBuilder를 이용해 어셈블리를 만든다.
   2. ModuleBuilder를 이용해 1에서 생성한 어셈블리 안에 모듈을 만들어 넣는다.
   3. 2에서 생성한 모듈 안에 TypeBuilder로 클래스를 만들어 넣는다
   4. 3에서 생성한 클래스 안에 MethodBuilder로 메소드나 PropertyBuilder로 프로퍼티를 만들어 넣는다.
   5. 4에서 생선한 것이 메소드라면 ILGenerator를 이용해 메소드 안에 CPU가 실행할 IL명령들을 넣는다.
 - 이는 어셈블리 -> 모듈 -> 클래스 -> 메소드 또는 프로퍼티 로 이루어지는 .NET프로그램의 계층 구조를 따르기 때문이다.

# 애트리뷰트

### 애트리뷰트
 - 애트리뷰트는 코드에 대한 부가 정보를 기록하고 읽을 수 있는 기능이다. 다만 주석과 다르게 사람이 작성하고 컴퓨터가 읽어들인다.
 - 애트리뷰트를 이용해 클래스나 구조체, 메소드, 프로퍼티 등에 데이터를 기록해두면 C#컴파일러나 C#으로 작성된 프로그램이 해당 정보를 읽고 사용할 수 있다.
 - 리플렉션과 애트리뷰트를 통해 얻는 정보들도 C#코드의 메타데이터라 할 수 있다.
   - 메타데이터는 데이터의 데이터. 가령 C#코드도 데이터지만 이 코드에 대한 정보 등을 뜻한다.

### 애트리뷰트 사용하기
 - 애트비류트를 사용할 때는 설명하고자 하는 코드 요소 앞에 대괄호 쌍[]을 붙이고 그 안에 애트리뷰트의 이름을 넣으면 된다.
```
[ 애트리뷰트_이름( 애트리뷰트_매개변수 )]
public void MyMethod()
{
  ...
}
```
 - 코드 사용시 경고를 띄워주는 `Obsolete` 애트리뷰트를 이용해 다음과 같은 예제를 작성할 수 있다.
```
class MyClass
{
  [Obsolete("OldMethod는 폐기되었습니다. "NewMethod()를 이용하세요.")]
  public void OldMethod() {...}
  public void NewMethod() {...}

  static void Main(string[] args)
  {
    OldMethod(); // 실행 시 컴파일러에 경고 문구 발생
  }
}
```
 - 애트리뷰트를 이용해 호출자 정보를 불러올 수 있다.
   - `CallerMemberNameAttribute` : 현재 메소드를 호출한 메소드 또는 프로퍼티의 이름을 나타낸다.
   - `CallerFilePathAttribute`   : 현재 메소드가 호출된 소스 파일 경로를 나타낸다. 이 때 경로는 소스코드를 컴파일할 때의 전체 경로를 나타낸다.
   - `CallerLineNumberAttribute` : 현재 메소드가 호출된 소스 파일 행의 번호를 나타낸다.

### 내가 만드는 애트리뷰트
 - `System.Attribute`클래스를 상속받아 커스텀 애트리뷰트를 만들 수 있다.
 - 커스텀 애트리뷰트를 여러 번 사용하기 위해서는 수정 가능 여부를 참으로 해야 한다. 이는 `System.AttributeUsage`라는 애트리뷰트의 도움을 받아야 한다.
   - `System.AttributeUsage`는 애트리뷰트의 애트리뷰트로 그것이 어떤 대상을 설명할지, 이 애트리뷰트를 중복해서 사용할 수 있는지의 여부 등을 설명한다.
```
using System;

namespace HistoryAttribute
{
  // 커스텀 애트리뷰트 선언
  [System.AttributeUsage(System.AttributeTargets.Class, AllowMultiple=true)]
  class History : System.Attribute
  {
    private string programmer;
    public double version;
    public string changes;

    public History(string programmer)
    {
      this.programmer = programmer;
      version = 1.0;
      changes = "First release";
    }

    public string GetProgrammer()
    {
      return programmer;
    }
  }

  // 애트리뷰트를 적용한 코드
  [History("Sean", verison = 0.1, changes = "2017-11-01 Created class stub")]
  [History("Bob", version = 0.2, changes = "2020-12-03 Added Func() Method")]
  class MyClass
  {
    public void Func() { ... }
  }

  // 애트리뷰트 값 불러오기
  class MainApp
  {
    static void Main(string[] args)
    {
      Type type = typeof(MyClass);
      Attribute[] attributes = Attribute.GetCustomAttributes(type);

      Console.WriteLine("MyClass changes history...");

      foreach (Attribute a in attributes)
      {
        History h = a as History;
        if (h != null)
        {
          Console.WriteLine(h.version);
        }
      }
    }
  }
}
```

