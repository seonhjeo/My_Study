# STL
 - 참고 자료 : https://velog.io/@chaewonkang/CPP-Module-08#stl%EC%9D%98-%EA%B5%AC%EC%84%B1-%EC%9A%94%EC%86%8C

## STL이란?
 - STL은 STandard Library의 약자로써 자료구조와 그에 관련된 알고리즘을 구현해 놓은 템플릿의 모임이다.
 - STL은 알고리즘, 컨테이너, 함수자, 반복자라 불리는 네 가지 구성요소가 있다.
 - STL은 컨테이너와 연관 배열 같은 c++을 위한 일반 클래스들의 미리 만들어진 집합을 제공하는데, 이것들은 어떤 빌트인 혹은 사용자 타입과도 같이 사용될 수 있다. STL의 알고리즘들은 컨테이너에 독립적이다.
 - STL은 결과를 템플릿을 통해 달성한다. 이 접근법은 전통적인 런타임 다형성에 비해 훨씬 효과적인 컴파일 타임 다형성을 제공한다. 현대의 C++ 컴파일러들은 STL의 많은 사용에 의해 야기되는 어떤 추상화 페널티도 최소화하도록 튜닝되었다.
 - STL은 제네릭 알고리즘과 C++을 위한 데이터 구조체들의 첫 번째 라이브러리로서 만들어졌다. 이것은 다음의 네 가지를 기초로 한다. 제네릭 프로그래밍, 효율성을 잃지 않은 추상화, 폰 노이만 구조 그리고 밸류 시멘틱스(value semantics)가 그것이다.

## STL의 구성 요소

### Container(자료구조)
 - 참고자료 : https://modoocode.com/174
 - 종류 및 사용법 : https://indirect91.tistory.com/27
 - 자신이 보관하는 원소들의 메모리를 관리하며, 각각의 원소에 접근할 수 있도록 멤버함수를 제공해준다.
 - 컨테이너는 직접 함수를 호출하거나 반복자를 이요해 원소에 접근할 수 있다.
 - 컨테이너 내 요소에 대한 아양한 작업을 도와주는 여러 멤버함수가 포함되어 있다.
 - STL은 자료를 저장하는 방식과 관리하는 방식에 따라 여러 형태로 나뉜다.
 - 컨테이너의 세 가지 유형
   - 시퀀스 컨테이너(Sequence Container)
     - 데이터를 선형으로 저장하며, 특별한 제약이나 규칙이 없는 가장 일반적인 컨테이너.
     - 삽입된 요소의 순서가 그대로 유지된다.
     - 명확한 순서가 유지되는 요소들이므로 특정 위치를 참조하는 연산이 가능해야 한다.
     - Vector, Deque, List, Forward_list등이 있다.
   - 연관 컨테이너(Associative Container)
     - key와 value처럼 관련 있는 데이터를 하나의 쌍으로 저장하는 컨테이너
     - set, multiset, map, multimap등이 있다.
   - 컨테이너 어답터(Adapter Container)
     - 간결함과 명료성을 위해 인터페이스를 제한한 시퀀스 컨테이너나 연관 컨테이너의 변형.
	 - 반복자를 지원하지 않으므로 STL 알고리즘에서는 사용할 수 없다.
     - stack, queue, priority_queue등이 있다.

### Iterator(반복자)
 - 참고 자료 : https://ansohxxn.github.io/stl/chapter16-2/
 - 반복자는 컨테이너의 원소에 접근할 수 있는 포인터와 같은 객체. 알고리즘 라이브러리의 경우 대부분이 반복자를 인자로 받아 알고리즘을 수행함
 - 반복자는 컨테이너에 iterator 멤버 타입으로 정의되어 있다. iterator 타입의 객체를 선언해 컨테이너의 특정 위치에 접근할 수 있다.
 - 모든 자로구조의 원소에 대한 동일한 접근 방법을 제공한다.
   - 지금까지 우리는 배열의 요소에 접근할 때 i 같은 인덱스를 선언해 사용해 왔다. 반복자는 그런 연산의 과정을 어떤 객체에서나 동일하게 사용할 수 있도록 통일해서 cpp에서 제공하는 generic 포인터 객체라고 생각하면 된다.
   - iterator 클래스 내부에는 배열의 요소를 쉽게 가리킬 수 있도록 몇가지 멤버함수들이 정의되어 있다. iterator 메서드와 컨테이너 메서드를 적절히 사용하면 좋은 알고리즘을 가진 클래스와 함수를 작성할 수 있을 것이다.

### Functor(함수자)
 - 함수자는 객체지만 함수처럼 동작하고, ()연산자에 의해 정의된다.
 - 함수가 아닌 객체이기 때문에 연산자()를 제외한 다른 멤버 함수와 멤버 변수들을 가질 수 있다.

### 알고리즘
