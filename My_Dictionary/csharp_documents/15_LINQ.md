# LINQ
 - [MS LINQ 문서](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/keywords/query-keywords)
 - [LINQ의 메소드적 활용](https://mentum.tistory.com/268)
 - [LINQ의 최적화](https://afsdzvcx123.tistory.com/entry/C-%EB%AC%B8%EB%B2%95-Linq-%EC%84%B1%EB%8A%A5-%EA%B0%9C%EC%84%A0)

### LINQ란?
 - LINQ는 Language INtegrated Query의 약자로 C#에 통합된 데이터 질의 기능을 말한다.
 - LINQ를 이용해 데이터 정리를 깔끔하게 할 수 있다.
 - LINQ를 사용할 데이터 집합의 양이 클 수록 for문에 대해 성능 이슈가 발생한다. 이를 유의해라.
 - LINQ는 다음과 같이 선언된다.
```
var profiles = from     profile in arrProfile  // arrProfile에 있는 각 profile 데이터로부터
               where    profile.Height < 175   // Height가 175미만인 객체만 골라
               orderby  profile.Height         // 키순으로 정렬하여
               select   profile;               // profile 객체 추출
```

### LINQ 기초
 - `from`
   - 모든 LINQ 쿼리식은 반드시 `from`절로 시작한다.
   - 쿼리식이 대상이 될 데이터 원본과 데이터 원본 안에 들어 있는 각 요소 데이터를 나타내는 범위 변수를 from절에 지정해 주어야 한다.
   - `from`의 데이터 원본은 `IEnumerable<T>`인터페이스를 상속하는 형식이어야만 한다.
   - `foreach`와 `from`의 차이점은 반복 변수와 범위 변수의 차이에 있다. 반복 변수에는 실제 객체가 들어가지만, 범위 변수에는 실제로 데이터가 담기지 않는다.
 - `where`
   - 필터 역할을 하는 연산자이다.
   - `from`절이 데이터 원본으로부터 뽑아낸 범위 변수가 가져야 하는 조건을 `where`연산자에 인수로 입력하면 LINQ는 해당 조건에 부합하는 데이터만을 걸러낸다.
 - `orderby`
   - 데이터의 정렬을 수행하는 연산자이다.
   - `ascending`, `descending`키워드를 이용해 오름차순과 내림차순 정렬을 선택할 수 있다.
 - `select`
   - 최종 결과를 추출하는 쿼리식의 마침표 같은 존재이다.
   - LINQ의 최종 질의 결과는 `IEnumerable<T>`형식으로 반환되며, T에 들어가는 형식은 사용자가 원하는 결과에 맞춰진다. 무명 형식으로 임의로 생성할 수도 있다.

### 여러 개의 데이터 원본에 질의하기
 - `from`문을 중첩해서 사용하면 된다.
 - 클래스 객체 내에 포함된 클래스 등 중첩되어 있는 객체의 안쪽을 질의할 때 용이하다.

### group by
 - 데이터를 기준에 따라 분류해주는 작업으로, `group by`절을 사용하여 실행한다.
 - 다음과 같이 사용할 수 있다.
```
group A by B into C
```
 - 이 때 A는 from절에서 뽑아낸 변수, B는 분류 기준, C는 결과를 담을 그룹 변수이다.

### join
 - `join`은 두 데이터 원본을 연결하는 연산이다.
 - 막무가내로 연결하는 것은 아니고 각 데이터 원본에서 특정 필드의 값을 비교하여 일치하는 데이터끼리 연결을 수행한다.
 - `join`은 다음과 같이 사용한다.
   - A의 a 필드와 B의 b 필드가 일치한 값들을 합친다는 뜻이다.
```
from a in A
join b in B on a.XXXX equals b.YYYY
```
 - `join`은 두 가지의 종류가 있다.
   - 내부 조인 : 교집합과 비슷하며, 두 데이터 원본 사이에서 일치하는 데이터들만 연결한 후 반환한다.
   - 외부 조인 : 내부 조인과 비슷하지만, 조인 결과에 기준이 되는 데이터 원본이 모두 포함된다.
 - 내부 및 외부 조인의 예시는 다음과 같다.
```
// 내부 조인
var listProfile =
    from profile in arrProfile
    join product in arrProduct on profile.Name equals pruduct.Star
    select new
    {
        Name = profile.Name,
        Work = product.Title,
        Height = profile.Height
    };

// 외부 조인
var listProfile =
    from profile in arrProfile
    join product in arrProduct on profile.Name equals pruduct.Star into ps
    from product in ps.DefaultIfEmpty(new Product(){Title = "Null"})
    select new
    {
        Name = profile.Name,
        Work = product.Title,
        Height = profile.Height
    };
```

### LINQ의 비밀과 LINQ 표준 연산자
 - LINQ는 .NET 언어 중 C#과 VB에서만 사용할 수 있다.
 - C#의 LINQ는 컴파일러가 컴파일 도중 일반적인 메소드 호출 코드로 변환하여 컴파일한다.
 - C#에서 제공하는 메소드 53개 중 쿼리식을 지원하는 것은 총 11가지가 있으며 이는 공식 문서에서 확인 가능하다.
   - 메소드는 책 537p에서 확인 가능하다.