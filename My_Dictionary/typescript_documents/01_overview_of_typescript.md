[타입스크립트 핸드북](https://www.typescriptlang.org/docs/)
[타입스크립트 핸드북 번역본](https://typescript-kr.github.io/)

# 왜 타입스크립트를?
 - 타입스크립트는 마이크로소프트가 자바스크립트에 [타입 안정성](https://tlonist-sang.github.io/Today-I-learned/jekyll/update/2020/09/29/typed-language.html)을 더해 만들었다.
   - 타입 안정성이 더해짐으로써 실수를 바로잡고 버그가 줄어든다.
 - 함수 등을 실행할 때 받는 인자의 타입에 엄격하고, 객체 안의 함수 및 변수에 대해 컴파일 전에 오류를 표시해준다.
   - 개발자의 오류를 실행 전에 미리 잡을 수 있어 코드 수정 시간이 줄어든다.

# 타입스크립트 개요
 - 타입스크립트는 강타입(strongly typed)프로그래밍 언어이다.
 - 타입스크립트는 컴파일 시 브라우저가 이해할 수 있는 자바스크립트 코드로 변환된다.
   - 타입스크립트는 컴파일 이전 우리의 코드를 확인한 후 개발자가 범한 오류를 우선적으로 확인해준다.

### 타입 시스템 개요
 - 타입스크립트는 C처럼 변수의 타입을 정확히 명시할 수도, 아니면 JS처럼 단순히 변수만 생성할 수도 있다.
   - JS식의 변수 생성시, TS는 변수의 타입을 추론하게 된다.
 - 변수의 타입이 정해지면, TS는 개발자가 다른 타입의 값으로 변수를 초기화하는 것을 막아준다.
 - 변수의 선언과 동시에 값 초기화를 하면 상관없지만, 값 초기화를 추후에 해주면 타입을 명시해주는 것이 좋다.
 - TS에서 중요한 포인트는 타입 체커와 소통한다는 점이다.
```
// 타입 명시
let a : boolean = false;
// 타입 비명시(TS에서 타입 추론)
let b = "hello";
```

### 타입스크립트의 타입들 1

##### 기본적인 타입 지정

 - TS에서 타입을 지정해줄 때는 변수명 뒤에 콜론과 타입명을 작성해주면 된다.
```
const num : number = 1;
```
 - TS의 기본적 타입의 정의는 다음과 같이 할 수 있다.
```
const num : number = 1;
const str : string = "hello";
const bool : boolean = true;

const numarr : number[] = [1, 2, 3];
const strarr : string[] = ["hello", "world"];
const boolarr : boolean[] = [true, false];
```
 - TS에서 객체 내 변수들의 타입을 정의할 때는 다음과 같이 할 수 있다.
   - 변수 타입을 선언할 때 콜론 앞에 ?를 추가함으로써 해당 변수의 존재 유무를 선택할 수 있게끔 할 수 있다. ?가 붙은 변수는 값이 없으면 undefined로 정의한다.
```
const player : {
  name:string,
  age?:number
} = {
  name = "hoya";
}
```

##### Alias

 - 동일한 유형의 객체를 여러 개 생성할 때는 Alias를 지정하여 코드 수를 줄일 수 있다.
   - Alias는 C++의 typedef와 비슷한 일을 한다.
```
type Player = {
  name:string,
  age?:number
}

const A : Player = {
  name : "A"
}
```
 - 함수의 반환타입을 특정하는 것도 가능하다.
```
// Player형을 반환하는 함수
function playerMaker(name:string) : Player{
  return {
    name
  }
}

// Player형의 변수를 정의하는 구문
const playerMaker = (name:string) : Player => ({name})
```

### 타입스크립트의 타입들2

##### readonly
 - 변수명 앞에 `readonly`를 추가해 변수들을 읽기 전용으로 만들 수 있다.
   - `readonly` 변수들의 값을 변경하려 하면 오류가 발생한다.
```
type Player = {
  readonly name:string,
  age?:number
}

const A : Player = {
  name = "A"
}

A.name = "B" // 오류 발생
```
 - 타입명 앞에 `readonly`를 적어 해당 변수의 변경을 방지할 수도 있다.
```
const num : readonly number[] = [1, 2, 3];
num.push(1);  // 오류 발생
```

##### Tuple
[참고 링크](https://ahnheejong.gitbook.io/ts-for-jsdev/03-basic-grammar/array-and-tuple)
 - 튜플은 특정한 형식의 배열을 생성할 수 있게 하는데, 최소한의 길이를 가지고, 특정 위치에 특정 타입이 있어야 한다.
   - 변수나 객체와 마찬가지로 `readonly`를 붙여 수정을 불가하게 만들 수 있다.
```
const player : [string, number, boolean] = ["hoya", 1, true]
const players : [string, number, boolean][] = [["hello", 1, true], ["world", 2, false]]
```

##### null, undefined, any
 - `null`과 `undefined`또한 타입의 일종으로 변수에 지정해줄 수 있다.
```
const a : undefined = undefined;
const b : null = null;
```
 - `any`는 어떠한 타입이든 될 수 있는 타입 지정자로, TS의 강타입 제한에서 벗어나고 싶을 때 사용한다.
   - `any`를 사용한 변수는 TS의 보호에서 벗어나 순수 JS형 변수가 된다.

### 타입스크립트의 타입들 3
 - 이 타입들은 TS에만 존재하는 것이다.

##### unknown
 - 어떤 타입인지 모르는 변수를 선언하거나 받아올 때 사용한다.
   - `unkndown`선언된 변수를 사용하기 위해서는 우선 해당 변수의 타입을 특정해야 한다.
```
let a: unknown;

if (typeof a === 'number) {
  let b = a + 1;
}
```

##### void
 - 아무런 값도 반환하지 않는 함수에 사용된다.
   - C계열 언어의 `void`와 유사하게 사용된다.
   - 평상시에는 `void`형을 따로 지정해줄 필요가 없다.
```
// funtion hello():void 으로 void형이 자동으로 적용됨
function hello() {
  colsole.log('x');
}
```

##### never
 - never은 함수가 값을 절대 반환하지 않을 때(주로 오류를 발생시킬때) 사용된다.
```
function hello():never {
  throw new Error("this is error")
}
```
 - 혹은 타입이 여러가지 일 수도 있는 상황에 발생할 수 있다.
```
// else구문까지 온 name변수의 타입은 never이 되게 된다.
function hello(name:string|number) {
  if (typeof name === "string") {

  } else if (typeof name === "number") {

  } else {

  }
}
```
