# 타입 스크립트의 함수

### call signatures
 - 함수의 콜 시그니처는 함수의 인자 타입과 함수의 반환 타입을 알려주는 것이다.
```
// call signature : const add: (a:number, b:number) => number
// number형인 인자 a, b를 받아 number형의 값을 반환한다는 것을 알려준다.
const add = (a:number, b:number) => a + b
```
 - 사용자가 임의로 콜 시그니처를 만들 수 있다.
```
// 1번 타입
type Add = (a:number, b:number) => number;
// 2번 타입
type Add = {
	(a: number, b: number) : number
}

// 콜 시그니처 적용
const add:Add = (a, b) => a + b;
```

### overloading
 - 오버로딩은 함수가 서로 다른 여러 개의 콜 시그니처를 가질 때 발생한다.
 - 인자의 개수는 같지만, 인자의 타입이 다를 때

```
type Add = {
	(a: number, b: number) : number
	(a: number, b: string) : number
}

const add: Add = (a, b) => {
	if (typeof b === "string") return a
	return a + b
}
```
```
type Config = {
	path: string,
	state: object
}

type Push = {
	(path:string):void
	(config:Config):void
}

const push:Push = (config) => {
	if (typeof config === "string") {

	}else {

	}
}
```
 - 인자의 개수가 다를 때
```
type Add = {
	(a: number, b: number) : number
	(a: number, b: number, c:number) : number
}

// 인자 c는 가 선택사항(optional)이란걸 컴파일러에게 알려줘야 한다.
const add: Add = (a, b, c?:number) => {
	if (c) return a + b + c
	return a + b
}
```

##### polymorhpism(다형성)
 - generic(제네릭) 타입을 이용해 인자와 반환값의 타입을 임의로 하여 함수의 다형성을 극대화시킬 수 있다.
   - 제네릭 타입은 타입의 placeholder같은 역할을 한다.
   - 제네릭 타입이 사용된 인자는 TS가 코드를 읽어들여 알아낸 타입으로 대체해준다.
 - 원하는대로 타입이 정해지니 any와 유사하다 느낄 수 있지만, 제네릭 타입은 각 타입에 대한 오류 방지를 해준다는 차이가 있다.
 - 제네릭 타입은 콜 시그니처의 인자에 꺾쇠를 이용해 선언할 수 있다.
 - 제네릭은 라이브러리나 프레임워크를 만들 때 자주 사용된다.
```
type SuperPrint = {
	<T>(arr: T[]):T
}

const superPrint: SuperPrint = (arr) => {
	return (arr[0])
}

const val = superPrint([1, "2", true])
```
 - 제네릭 타입은 중복해서 사용할 수 있다.
```
type SuperPrint = {
	<T, M>(arr: T[], b:M) => T
}
```
 - 제네릭은 콜 시그니처뿐만 아니라 함수에 직접적으로 사용할 수도 있다.
   - 함수를 사용할 때 타입 정의처럼 제네릭 값 또한 임의로 정해줄 수 있다. 다만 TS가 임의로 제네릭 값을 지정하게 두는 것이 일반적으로 더 낫다.
```
function superPrint<T>(a: T[]) {
	return a[0];
}

const a = superPrint<int>([1, 2, 3, 4]);
const b = superPrint([true, false]);
```
 - 제네릭은 Alias를 만들 때도 사용할 수 있다. 이를 이용해 재사용가능한 여러 타입의 객체를 만들 수 있다.
```
type Player<E> = {
	name:string
	extraInfo:T
}

const hoya: Player<{favFood:string}> = {
	name:"hoya",
	extraInfo: {
		favFood:"chocolate"
	}
}

// 위의 hoya 구현부는 다음과 같이 확장할 수 있다.
type hoyaExtra = {
	favFood:string
}

type hoyaPlayer = Player<hoyaExtra>

const hoya: hoyaPlayer = {
	hame:"hoya",
	extraInfo: {
		favFood:"chocolate"
	}
}
```
