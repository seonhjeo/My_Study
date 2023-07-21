# 변수
 - 어떠한 값을 변경하거나 유지하기 위해 쓰임
   - 일반적으로 `upperCamelCase`를 사용한다.
 - 데이터 타입들을 저장할 수 있다.

### const
 - 상수(Constant), 선언 이후로 값이 절대 바뀌지 않는다.
```
const myName = "Seonho Jeong";
```

### let
 - 비상수, 선언 이후에 값을 바꿀 수 있다.
```
let myName = "Seonho";  myName = "seonhjeo";
```

### var
 - const와 let이 생기기 이전에 사용된 변수형
   - 상수 구분이 없어 값을 바꾸면 안되는 변수의 값을 바꾸는 오류를 범할 수 있다.

# 자바스크립트의 데이터 타입들

### 숫자
 - 정수형과 실수형이 혼합되어 있다.
 - 두 형식간의 연산이 자유롭다.
```
2.5 + 4; // 6.5
```

### 문자열
 - 따옴표('') 혹은 쌍따옴표("")로 가두어져 있는 문자들의 집합이다.
   - 따옴표 혹은 쌍따옴표 안의 모든 기호들은 문자로 간주한다.
 - 문자열간의 덧셈 연산이 가능하다.  
```
"Hello" + " World"; // "Hello World"
```

### 불린형
 - 참(true)과 거짓(false)으로만 이루어진 값이다.  
```
const amIFat = false;
```

### null
 - 변수 안에 값이 없음을 뜻한다. 모든 데이터 타입에 null을 이용해 값이 없음을 표현할 수 있다.
```
let amIFat = null;
```

### undefined
 - 변수가 메모리는 할당되었지만, 그 메모리에 값이 주입되지 않았음을 뜻한다. 모든 데이터 타입의 변수 선언에 값을 할당하지 않음으로써 undefined상태를 만들 수 있다.  
```
let amIFat; // undefined
```

### 자료형 변환
 - javascript 내에 내장된 변환함수들을 사용해 데이터형을 변환할 수 있다.

# 배열(Array)
 - 데이터들의 집합체이다. C 계얼의 언어와 다르게 서로 다른 자료형이 하나의 배열에 저장될 수 있다.
   - 상수, 비상수, 변수, null이나 undefined와 같은 특수 데이터 타입까지 하나의 배열이 저장 가능하다.  
```
const array = [1, 2, "Hello", false, true, null, undefined];
```
 - 배열은 대괄호`[]`로 감싸여지고, 배열의 각 요소는 쉼표로 구분되어야 한다.
 - 배열의 각 요소에 접근하기 위해서는 배열의 이름 뒤에 대괄호로 요소의 위치를 특정짓는다.
   - 배열의 범위를 넘어가는 접근은 undefined를 반환한다.  
```
Console.log(array[3]);
```
 - javascript의 배열은 클래스화 되어있다. 삽입, 삭제 등의 여러 작업을 배열의 멤버함수로 실행할 수 있다.  
```
ex) array.push("thing to add");
```

# 객체(Object)
 - 여러 데이터와 그 데이터를 가공 및 유지보수해주는 기능들의 집합체이다.
 - 객체는 중괄호로 선언하고, 객체 내의 변수 선언은 =이 아닌 :로 한다. 변수의 구분은 배열과 같이 쉼표로 해준다.  
```
const player = {
  name: "jsh",
  points: 10,
  isAvailable: true,
};
```
 - 객체 내 값의 접근은 `.`혹은 `["변수명"]`으로 가능하다.  
```
player.name; 
player["name"];
```
 - 객체를 const로 선언했어도 객체 내 변수들이 const선언이 안 되어 있으면 자유롭게 객체 내 변수들의 값을 바꿀 수 있다.
```
player.isAvailable = false; // 가능
player = false;             // 불가능
```
 - 객체 외부에서 객체 내에 새로운 변수를 추가해줄 수 있다.  
```
player.favorite = "hamburger"; // player객체에 favorite변수를 추가함
```

# 함수(Function)
 - 함수는 프로그래머가 계속해서 재사용할 수 있는 코드조각을 뜻한다.
```
function sayHello(name, age){
  console.log("Hello this is " + name + " and I'm " + age);
}

sayHello("jsh", 25); // Hello this is jsh and I'm 25
```
 - 객체 내의 함수는 다음과 같이 선언 및 사용할 수 있다.
```
const player = {
  name: "jsh",
  sayHello: function(otherName) {
    console.log("Hello " + otherName);
  },
};

player.sayHello("Kim"); // Hello Kim
```
 - 함수 내에서 생산된 값은 return을 이용해 반환할 수 있다.
   - 함수 내 return구문이 실행되면 해당 함수는 종료된다.
```
function add(a, b){
  return a + b;
}

const res = add(3, 5); // res에 add의 반환값이 저장됨
```

# 조건문(Conditional)
 - 조건문은 다음과 같이 작성할 수 있다.
   - 조건문의 조건은 단순 변수뿐 아니라 함수(의 결과) 혹은 and/or 연산자의 연산 결과도 들어갈 수 있다.
   - 조건문의 `else if`와 `else`는 선택적이다. 필요에 따라 작성하지 않을 수도 있다.
   - 자바스크립트는 조건을 맨 위에서부터 차례로 검사해 맞는 조건을 발견하면 해당하는 블럭만 실행한 후 나머지 블럭을 건너뛴다.
```
if (condition1){
  
}else if(condition2){

}else{

}
```
 - and 연산자는 `&&`, or연산자는 `||`을 사용한다.
 - 비교 연산자는 `==` 과 `===`을 사용한다.
   - `==`연산자는 두 피연산자의 자료형이 다를 시 일부 피연산자의 자료형을 변환한 후 값을 비교한다.
   - `===`연산자는 두 피연산자의 자료형이 다를 시 무조건 false를 반환한다.
