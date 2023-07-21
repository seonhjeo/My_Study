# 배열과 컬렉션 그리고 인덱서

### 배열
 - 배열은 같은 타입의 데이터들의 집합을 저장 및 관리하기 위한 구조체이다.
 - 배열은 다음과 같이 선언 가능하다.
```
데이터형식[] 배열이름 = new 데이터형식[용량];
```
 - 배열의 첫 번째 인덱스는 0, 배열의 마지막 인덱스는 길이 - 1이다.
   - `^`연산자는 컬렉션의 마지막부터 역순으로 인덱스를 저장한다. `^1`은 마지막 요소, `^2`는 마지막에서 두 번째 요소를 가리킨다.
   - `^`연산자 인덱서를 `System.Index`형식의 값에 저장할 수 있다.
```
System.Index last = ^1;
scores[last] = 34; // scores[scores.Length - 1] = 34; 와 동일
```

### 배열의 초기화 방법
 - 배열의 초기화 방법은 총 3가지가 있다. 결과는 모두 동일하다.
   - 이 때 객체 선언 뒤의 중괄호{} 블록을 컬렉션 초기자(Collection Initializer)이라 한다.
```
// 기본적인 방법
string[] array1 = new string[3]{"1", "2", "3"};

// 배열의 용량을 생략함
string[] array2 = new string[]{"1", "2", "3"};

// 형식을 모두 생략함
string[] array3 = {"1", "2", "3"};
```

### System.Array
 - .NET의 CTS에서 배열은 System.Array 클래스에 대응된다. 위에서 선언한 string형식의 배열은 System.Array가 string형식에 맡게 일반화된 것이다.
 - System.Array 클래스에는 배열의 정렬, 탐색, 추가 및 삭제 등 여러 작업을 간단하게 할 수 있다.
 - `..`연산자를 이용해 배열의 시작과 끝 인덱스를 지정해줄 수 있다. `..`연산자는 `System.Range`형식의 값에 저장할 수 있다.
   - `..`연산자의 시작과 끝 인덱스는 생략될 수 있다. 이 때 생략된 인덱스 대신 맨 처음과 맨 끝의 인덱스가 자동으로 삽입된다.
   - `..`연산자의 시작과 끝 인덱스에는 `System.Index`타입의 값도 들어갈 수 있다.
```
System.Range r1 = 0..3; // 시작 인덱스 0, 마지막 인덱스 3
int[] sliced = scores[r1];

int[] sliced2 = scores[0..3];

// 첫 번째부터 세 번째 요소까지
int[] sliced3 = scores[..3];

// 두 번째부터 마지막 요소까지
int[] sliced3 = scores[1..];

// 전체 요소
int[] sliced3 = scores[..];

// 마지막 요소까지
int[] sliced3 = scores[..^1];
```

### 다차원 배열
 - 다차원 배열이란 차원이 둘 이상인 배열을 말한다.
 - 다차원 배열의 선언 방법은 다음과 같다.
```
데이터형식[,] 배열이름 = new 데이터형식[2차원길이, 1차원길이];

int[,] arr = { { 1, 2, 3}, { 4, 5, 6 }};
```

### 가변 배열
 - 가변 배열은 다양한 길이의 배열을 요소로 갖는 다타원 배열로 이용될 수 있다.
 - 가변 배열은 다차원 배열과 달리 배열을 요소로 사용해 접근할 수 있다.
 - 가변 배열의 선언은 다음과 같다.
```
데이터형식[][] 배열이름 = new 데이터형식[가변 배열의 용량][];

int[][] jagged = new int[2][] {
    new int[] {1, 2}
    new int[4] {3, 4, 5, 6}
};
```

### 컬렉션 맛보기
 - 배열 또한 .NET이 제공하는 다양한 컬렉션 자료구조의 일부이다.
 - .NET의 `System.Array`는 다음과 같이 상속을 받는다.
```
public abstract class Array : ICloneable, IList, ICollection, IEnumerable
```
 - .NET의 컬렉션에는 `Array`, `List`, `Stack`, `Queue`, `Hash`등 여러 종류의 자료구조들이 있다.

### 인덱서
 - 인덱서는 인덱스를 이용해 객체 내의 데이터에 접근하게 해주는 프로퍼티라 생각하면 이해하기 쉽다.
 - 인덱서의 선언 형식은 다음과 같다.
   - 인덱서도 프로퍼티와 마찬가지로 객체 내의 데이터에 접근할 수 있는 통로이지만, `index`를 이용한다는 사실이 다르다.
```
class 클래스이름
{
    한정자 인덱서형식 this[형식 index]
    {
        get
        {
            // index를 이용해 내부 데이터 반환
        }
        set
        {
            // index를 이용해 내부 데이터 저장
        }
    }
}
```

### foreach가 가능한 객체 만들기
 - `foreach` 구문은 `IEnumerable`을 상속하는 형식만 지원한다.
 - `IEnumerable` 인터페이스가 갖고 있는 메소드는 다음과 같다.
   - `IEnumerator GetEnumerator()` : IEnumerator 형식의 객체를 반환
 - `IEnumerator`형식의 객체를 반환하기 위해서 `yield return`문을 사용한다.
 - `IEnumerator` 인터페이스가 갖고 있는 메소드는 다음과 같다.
   - `boolean MoveNext()` : 다음 요소로 이동한다. 컬렉션의 끝을 지난 경우 false, 이동에 성공한 경우 true를 반환
   - `void Reset()` : 컬렉션의 첫 번째 위치의 앞으로 이동한다. 첫 번째 위치가 0인 경우 Reset()을 호출 시 -1번으로 이동
   - `Object Current{get;}` : 컬렉션의 현재 요소를 반환한다.

### 인덱서와 foreach가 가능한 객체 예제
```
using System;
using System.Collections;

namespace Enumerable
{
    class MyList : IEnumerable, IEnumerator
    {
        private int[] array;
        int position = -1; // 컬렉션의 현재 위치를 가리키는 요소, Reset()을 고려하여 -1로 지정

        Public MyList()
        {
            array = new int[3];
        }

        public int this[int index]
        {
            get
            {
                return array[index];
            }
            set
            {
                if (index >= array.Length)
                {
                    Array.Resize<int>(ref aaray, index + 1);
                }
                array[index] = value;
            }
        }

        // IEnumerator 멤버
        public object Current
        {
            get
            {
                return array[position];
            }
        }

        public bool MoveNext()
        {
            if (position == array.Length - 1)
            {
                Reset();
                return false;
            }

            position++;
            return(position < array.Length);
        }

        public void Reset()
        {
            position = -1;
        }

        // IEnumerable 멤버
        public IEnumerator GetEnumerator()
        {
            return this;
        }
    }
}
```