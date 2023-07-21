# 클래스와 인터페이스

### 클래스
 - TS는 JS의 클래스에 C언어계열의 클래스 문법을 도입하였다.
   - 클래스 내의 접근 제한자는 C와 Java계열과 동일하다.
   - JS는 private이 없이 오로지 public만 있다. 접근 제한자 개념은 TS의 사용자 보호를 위해 있다.
 - TS의 클래스는 다음과 같이 선언할 수 있다.
```
class Player {
	// 생성자 안에 인자를 추가해주면 JS로 변환되면서 인자가 저절로 멤버변수가 된다
	constructor (
		private firstname:string,
		private lastname:string,
		public nickname:string
	) {}
	public getFullName() { return `${this.firstname} ${this.lastname}`}
}

const hoya = new Player("seonho", "jeong", "hoya");
hoya.getFullName();
```
 - TS는 추상 클래스와 추상 메소드를 생성할 수 있다.
   - 추상 클래스는 자기 자신은 인스턴스를 만들 수 없는 대신 다른 클래스가 본인을 상속받을 수 있는 클래스이다.
   - 추상 메소드는 상속되는 다른 클래스에서 추가로 정의되어 작동하게 되는 함수이다. 추상 메소드를 만들기 위해서는 함수의 몸통부를 없애고 콜 시그니처를 정의해주면 된다.
 - 추상 클래스와 추상 메소드는 다음과 같이 작성될 수 있다.
```
// 추상 클래스
abstract class User {
	constructor (
		private firstname:string,
		private lastname:string,
		public nickname:string
	) {}

	// 추상 메소드
	abstract getNickName():void

	getFullName() { return `${this.firstname} ${this.lastname}`}
}

// 추상 클래스를 상속받는 클래스, 추상 클래스가 갖는 멤버함수 또한 갖고 있다.
class Player extends User {
	// 추상 메소드 구현부
	getNickName() {
		console.log(this.nickname);
	}
}

const hoya = new Player("seonho", "jeong", "hoya");
hoya.getFullName();
hoya.getNickName();
```
 - 인덱스 시그니처
   - [인덱스 시그니처](https://bobbyhadz.com/blog/typescript-key-string-string)
   - {[key: string]: string} 구문은 TypeScript의 인덱스 서명이며 형식 속성의 이름을 미리 알 수는 없지만 값의 모양을 알 때 사용된다. 인덱스 서명은 유형 문자열의 키 및 값을 지정한다.

 - 클래스 예시
   - 클래스의 멤버변수를 생성자 바깥에서 선언하고, 생성자에서 초기화시킬 수도 있다.
   - 클래스를 인자 혹은 반환자의 타입으로 사용할 수 있다.
   - C#처럼 정적(static)메소드를 생성할 수 있다. JS에서 지원하는 형식이다.
```
// 인덱스 시그니처
interface Words {
	[key: string]: string;
}

class Dict {
	// 맴버변수를 수동으로 만들어줄 수 있다. 멤버변수는 생성자에서 초기화
	private words: Words;
	constructor() {
		this.words = {};
	}
	// 클래스를 인자형으로 사용할 수 있다.
	add(word: Word) {
		if (this.words[word.term] === undefined) {
			this.words[word.term] = word.def;
		}
	}
	update(word: Word) {
		if (this.words[word.term] !== undefined) {
			this.words[word.term] = word.def
		}
	}
	remove(term: string) {
		if (this.words[term] !== undefined) {
			delete this.words[term]
		}
	}
	def(term: string) {
		return this.words[term] ?? '없음';
	}
	// 정적 메소드
	static hello() {
		return "hello"
	}
}
class Word {
	// 읽기 전용(선언 이후 변경 불가)
	constructor(public readonly term: string, public readonly def: string) { }
}
const dict = new Dict();
const kimchi = new Word('kimchi', '한국의 음식');
dict.add(kimchi);
```

### 인터페이스
 - 타입의 사용 방법은 다음과 같다.
   - TS에게 객체의 형식(모양)을 알려주는 것
   - c언어의 define처럼 사용하는 것
   - alias를 선언하는 데에도 사용할 수 있다.
```
// 특정 명칭을 타입으로 정의
type Nickname = string
type Health = number
type Friends = Array<string>

// 특정 콘크리트 값을 타입으로 정의
type Team = "red" | "blue" | "yellow"

// TS에게 Player라는 객체의 모양을 알려주는 것
type Player = {
	nickname:Nickname,
	team:Team,
	healthBar:Health
}

const hoya: Player = {
	nickname:"hoya",
	team:"blue",
	health:10
}
```
 - 인터페이스는 타입스크립트에게 오브젝트의 모양을 설명해 주기 위해 존재한다.
 - 인터페이스는 생성자 등이 없고 주로 단순한 변수와 함수의 선언만 한다.
 - 인터페이스는 JS변환과정에서 사라지는 가벼운 기능이다
 - 인터페이스는 다음과 같이 선언이 가능하다.
```
interface Player {
	nickname:Nickname,
	team:Team,
	healthBar:Health
}

// 다음과 동일하다.
type Player = {
	nickname:Nickname,
	team:Team,
	healthBar:Health
}
```
 - 인터페이스는 클래스와 마찬가지로 상속이 가능하다
```
interface User {
	name.string
}
interface Player extends User {
}

// 다음과 동일하다.
type User = {
	name.string
}
type Player = User & {
}
```
 - 동일한 이름의 인터페이스는 축적이 가능하다. 이는 타입으로 불가능하다.
```
interface User { firstname:string }
interface User { lastname:string }
interface User { health:number}

const hoya : user = {
	firstname:"seonho",
	lastname:"jeong",
	health:10
}
```
 - 인터페이스로 상속된 변수는 무조건 public이어야 한다.
 - 인터페이스 형 또한 함수의 인자나 반환값이 될 수 있다.
```
interface User { firstname:string }

function makeUser(user:User) { return "hi" }

makeUser( { firstname:"hoya"})
```

### 다형성을 포함한 종합 클래스
```
interface SStorage<T> {
	[key: string]: T
}
class LocalStorage<T> {
	private storage: SStorage<T> = {}
	//Create
	set(key: string, value: T) {
		if (this.storage[key] !== undefined) {
			return console.log(`${key}가 이미 존재합니다. update 호출 바랍니다.`)
		}
		this.storage[key] = value
	}
	//Read
	get(key: string): T | void {
		if (this.storage[key] === undefined) {
			return console.log(`${key}가 존재하지 않습니다.`)
		}
		return this.storage[key]
	}
	//Update
	update(key: string, value: T) {
		if (this.storage[key] !== undefined) {
			this.storage[key] = value
		} else {
			console.log(`${key}가 존재하지 않아 새로 만듭니다.`)
			this.storage[key] = value
		}
	}
	//Delete
	remove(key: string) {
		if (this.storage[key] === undefined) {
			return console.log(`${key}가 존재하지 않습니다.`)
		}
		delete this.storage[key]
	}
	clear() {
		this.storage = {}
	}
}
const stringsStorage = new LocalStorage()
const booleanStorage = new LocalStorage()
```
