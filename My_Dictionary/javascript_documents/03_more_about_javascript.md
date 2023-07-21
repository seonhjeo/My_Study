이 문서는 4장 이후 실시하는 클론코딩에서 추가로 배우는 내용을 정리한 곳입니다.

### HTML FORM
 - 입력 양식 제한, 단순한 버튼 클릭 등을 할 때에는 JS의 if구문보다 HTML의 form을 사용하는 것이 좀 더 효율적이다.
```
<form id="login-form">
	<input
		required
		maxlength="15"
		type="text"
		placeholder="What is your name?"
	/>
	<input type="submit" value="Log in" />
</form>
```

### Eventlistener의 매개변수
 - 이벤트리스너로 실행시킬 함수는 매개변수를 가질 수 있다.
 - 매개변수를 선언한 채 함수를 이벤트리스너에 추가할 경우, 이벤트를 실행할 때 JS가 알아서 이벤트 상황 및 변경점 등을 매개변수에 저장해 준다.
```
const loginForm = document.querySelector("#login-form");

function onLoginSubmit(event) {
	event.preventDefault(); // 브라우저의 기본 동작 실행을 저지하는 함수
	console.log(event);
}

loginForm.addEventListener("submit", onLoginSubmit);
```

### JS의 문자열 통합 방법
 - 더하기 사용
```
const name = "JSH";
const text = "Hello " + name;
```
 - 변수 직접대입
```
const name = "JSH";
const text = `Hello ${username}`;
```

### JS localStorage
[docuement link](https://developer.mozilla.org/en-US/docs/Web/API/Window/localStorage)
 - 브라우저가 새로고침 등의 초기화 작업에도 데이터를 기억하기 위해 사용하는 API이다.

### setInterval(), setTimeout()
 - setInterval()
   - 특정 시간마다 주기적으로 함수를 실행시킬 때 사용한다.
   - 첫 번째 인자로 실행할 함수의 이름, 두 번째 인자로 실행 텀을 준다.
 - setTimeout()
   - 특정 시간 후에 함수를 한 번 실행시킬 때 사용한다.
   - setInterval()과 동일하게 인자를 받는다.

### JS date
[document link](https://developer.mozilla.org/en-US/docs/Web/Javascript/Reference/Global_Objects/Date)
 - JS에서 현재 날짜와 시간에 관련된 여러 값들을 불러오기 위한 객체이다.
 - `const date = new Date();`로 선언하여 사용한다.

### JS createElement(), appendChild()
 - createElement()
   - JS코드상에서 HTML의 요소를 생성할 때 사용한다.
   - 단순하게 만들 요소의 종류 태그만을 인자로 받는다.
 - appendChild()
   - JS에서 만들어진 요소를 다른 요소의 자식으로 부착할 때 사용한다.
```
const bgImage = document.createElement("img");

document.body.appendChild(bgImage);
```

### JSON.stringify(), JSON.parse()
 - JSON.stringify()
   - 어떠한 오브젝트든 스트링으로 바꿔주는 함수
   - 문자열만을 허락하는 브라우저 로컬 스토리지에 여러 데이터를 저장할 때 사용한다.
 - JSON.parse()
   - 문자열들을 적절한 오브젝트형으로 파싱해주는 함수
   - 로컬 스토리지에 저장한 데이터를 파싱할 때 사용

### foreach와 item
 - foreach구문으로 여러 배열의 요소에 순차적으로 처음부터 끝까지 접근할 수 있다.
 - 이 때 배열의 개별 요소에 접근하면서 이에 관한 함수를 실행하려면 매개인자를 통해 함수에 배열의 요소를 전달해줄 수 있다.
```
// 1번 예시와 2번 예시는 동일한 코드이다
// 1번
function sayHello(item) {
	console.log(item);
}

parsedToDos.forEach(sayHello);

// 2번
parsedTodos.forEach((item) => console.log(item));
```
