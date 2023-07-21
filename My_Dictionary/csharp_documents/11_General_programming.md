### 일반화 프로그래밍
 - 특수한 개념으로부터 공통된 개념을 찾아 묶는 것을 일반화라고 한다.
 - 일반화 프로그래밍에서 일반화하는 대상은 데이터 형식이다.

### 일반화 메소드
 - 일반화 메소드는 이름처럼 데이터 형식을 일반화한 메소드이다.
 - 일반화 메소드는 다음과 같이 선언한다.
```
한정자 반환_형식 메소드이름<형식_매개변수> (매개변수_목록)
{
    // ...
}

// 예시
void copyArray<T> (T[] source, T[] target)
{
    for (int i = 0; i < source.Length; i++)
        target[i] = source[i];
}

// 사용
CopyArray<int>(soucce, target);
```

### 일반화 클래스
 - 일반화 클래스는 데이터 형식을 일반화한 클래스이다.
 - 일반화 클래스의 선언은 다음과 같다.
```
class 클래스이름 <형식_매개변수>
{

}

// 예시
class Array_Generic<T>
{
    private T[] array;
    // ...
    public T GetElement(int index) { return array[index]; }
}

// 사용
Array_Generic<double> dblArr = new Array_Generic<double>();
```

### 형식 매개변수 제약
 - 일반화 메소드나 일반화 클래스가 입력받는 형식 매개변수 T는 모든 데이터 형식을 대신할 수 있다.
 - 특정 조건을 갖춘 형식에만 대응하는 형식 매개변수를 만들기 위해 조건을 제약할 수 있다.
 - 형식 매개변수의 제약은 `where`구문을 사용하게 된다.
```
where 형식_매개변수 : 제약조건
```
 - 제약 조건들은 다음과 같다.
   - `where T : struct`         : T는 값 형식이어야 한다.
   - `where T : class`          : T는 클래스 형식이어야 한다.
   - `where T : new()`          : T는 반드시 매개변수가 없는 생성자가 있어야 한다.
   - `where T : 기반_클래스_이름`: T는 명시한 기반 클래스의 파생 클리스여야 한다.
   - `where T : 인터페이스_이름` : T는 명시한 인터페이스를 반드시 구현해야 한다. 인터페이스 이름은 여러 개가 명시될 수 있다.
   - `where T : U`              : T는 또 다른 형식 매개변수 U로부터 상속받은 클래스여야 한다.

### 일반화 컬렉션
 - 이전에 사용했던 object형식 기반의 컬렉션 클래스들은 컬렉션의 요소에 접근할 때마다 형식 변환이 일어나므로 성능에 문제가 있었다.
 - 일반화 컬렉션은 일반화에 기반하여 만들어져 있기 때문에 컴파일할 때 컬렉션에서 사용할 형식이 결정되고, 쓸데없는 형식 변환을 일으키지 않는다. 또한 잘못된 형식의 객체를 담게 될 위험도 피할 수 있다.
 - 일반화 컬렉션 클래스들은 `System.Collection.Generic` 네임스페이스에 존재한다.

### foreach를 사용하는 일반화 컬렉션
 - 일반화 컬렉션의 `foreach` 구문은 `IEnumerable<T>`을 상속하는 형식만 지원한다.
 - `IEnumerable<T>` 인터페이스가 갖고 있는 메소드는 다음과 같다.
   - `IEnumerator GetEnumerator()` : IEnumerator 형식의 객체를 반환. IEnumerable로부터 상속받은 메소드.
   - `IEnumerator<T> GetEnumerator()` : `IEnumerator<T>` 형식의 객체를 반환
 - `IEnumerator<T>` 인터페이스가 갖고 있는 메소드는 다음과 같다.
   - `boolean MoveNext()` : 다음 요소로 이동한다. 컬렉션의 끝을 지난 경우 false, 이동에 성공한 경우 true를 반환
   - `void Reset()` : 컬렉션의 첫 번째 위치의 앞으로 이동한다. 첫 번째 위치가 0인 경우 Reset()을 호출 시 -1번으로 이동
   - `Object Current{get;}` : 컬렉션의 현재 요소를 반환한다. IEnumerator로부터 상속받은 메소드
   - `T Current{get;}` : 컬렉션의 현재 요소를 반환한다.