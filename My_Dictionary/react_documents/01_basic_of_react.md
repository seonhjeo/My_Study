### Before Reack
 - 리액트와 같은 프레임워크들은 최종적으로 CSS, JS, HTML을 편리하게 사용하기 위함이다.
 - 바닐라 JS로 어떠한 입력을 받아 그 결과를 나타내는 데에는 다음과 같은 과정을 거친다.
   - 입력값을 받을 HTML의 요소를 가져온다.
   - 요소에 이벤터리스너를 연결한다.
   - 이벤트리스너가 작동되었을 때 실행될 함수와 업데이트될 값을 만든다.
   - 실행된 결과를 다시 HTML에 보일 수 있게 출력창의 요소에 연결한다.
 - 리액트는 위의 과정을 줄일 수 있다.
 - 리액트를 HTML에 설치 및 실행하기 위해서는 다음과 같이 하면 실시하면 된다.
```
<!DOCTYPE html>
<html>
<body></body>
<script src="https://unpkg.com/react@17.0.2/umd/react.production.min.js"></script>
<script src="https://unpkg.com/react-dom@17.0.2/umd/react-dom.production.min.js"></script>
<script>
	// start srcipt here
</script>
</html>
```

### First React Element
 - 리액트의 첫 번째 규칙은 HTML의 구성요소들을 JS로 만든다는 것이다.
   - 리액트는 바닐라JS와 반대로 작성한다. HTML을 작성하고 그 안에 스크립트를 임포트하는 바닐라JS와 반대로, 리액트JS 스크립트를 생성한 후에 그 스크립트 안에서 HTML요소를 추가해준다.
   - 이는 유저에게 보여질 요소를 리액트가 능동적으로 컨트롤할 수 있다는 뜻이다.
 - 아래는 리액트의 작동 원리를 이용해 간단하게 구현한 웹페이지이다. 실제로 현업에서는 아래와 같은 방식은 복잡해서 잘 사용되지 않는다.
```
<!DOCTYPE html>
<html>
<body>
	// 리액트에서 만들어진 요소가 추가될 루트 div
	<div id="root"></div>
</body>
<script src="https://unpkg.com/react@17.0.2/umd/react.production.min.js"></script>
<script src="https://unpkg.com/react-dom@17.0.2/umd/react-dom.production.min.js"></script>
<script>
	const root = document.getElementById("root");
	// span요소 생성
	const span = React.createElement("span", {id:"my-span", style: {color: "red"}}, "Hello World!");
	// 만든 요소를 ReackDOM을 이용해 렌더링
	ReactDOM.render(span, root);
</script>
</html>
```

### Events in React
 - 리액트는 vanillaJS보다 이벤트 작성을 축약할 수도 있다.
   - 다음은 JS와 리액트로 동일한 결과를 구현한 것이다.
```
// JS
<!DOCTYPE html>
<html lang="en">
	<body>
		<span>Total clicks: 0</span>
		<button id="btn">Click me</button>
	</body>
	<script>
		let counter = 0;
		const button = document.getElementById("btn");
		const span = document.querySelector("span");
		function handleClick() {
			console.log("I have been clicked");
			counter = counter + 1;
			span.innerText = `Total clicks: ${counter}`;
		}
		button.addEventListener("click", handleClick);
	</script>
</html>
```
```
// React
<!DOCTYPE html>
<html>
<body>
	<div id="root"></div>
</body>
<script src="https://unpkg.com/react@17.0.2/umd/react.production.min.js"></script>
<script src="https://unpkg.com/react-dom@17.0.2/umd/react-dom.production.min.js"></script>
<script>
	const root = document.getElementById("root");
	const h3 = React.createElement("h3", null, "Hello World!");
	const btn = React.createElement("button", {
		onClick: () => console.log("I'm clcked"),
	}, "Click me");
	const container = React.createElement("div", null, [h3, btn]);
	ReactDOM.render(container, root);
</script>
</html>
```
 - JS에서는 버튼을 구현하기 위해 4줄의 코드가 필요하지만, 리액트에서는 단 한줄로 가능하다.
   - 이는 요소를 가져오는 것이 필요가 없기 때문이다.

### JSX
 - [참고 문서](https://velog.io/@gyumin_2/React-JSX%EB%9E%80%EC%A0%95%EC%9D%98-%EC%9E%A5%EC%A0%90-%EB%AC%B8%EB%B2%95-%ED%8A%B9%EC%A7%95-%EB%93%B1)
 - 리액트의 createElement함수를 JSX를 이용해 더욱 편리하게 만들 수 있다.
   - JSX는 어플리케이션을 여러가지 작은 요소로 나누어 관리할 수 있게 해준다.
   - 생성이 필요한 여러 요소들이 복합적으로 모여있을 때 JSX는 이를 직관적으로 생성할 수 있게 해준다.
   - 따라서 코드의 재활용이 매우 편해지게 된다.
 - 브라우저가 JSX를 이해하기 위해서는 JSX문법을 리액트 코드로 변환시켜주는 프로그램을 설치해주어야 한다.
   - 이후 스크립트에 바벨로 작성되는 것이라고 명시해주어야 한다.
   - 각각의 컴포넌트는 대문자로 작성해 HTML태그와의 중복을 피해준다.
```
<script src="https://unpkg.com/@babel/standalone/babel.min.js"></script>
<script type="text/babel"> ... </script>
```
 - 다음은 위의 예시를 JSX로 작성한 것이다.
```
<!DOCTYPE html>
<html>
<body>
	<div id="root"></div>
</body>
<script src="https://unpkg.com/react@17.0.2/umd/react.production.min.js"></script>
<script src="https://unpkg.com/react-dom@17.0.2/umd/react-dom.production.min.js"></script>
<script src="https://unpkg.com/@babel/standalone/babel.min.js"></script>
<script type="text/babel">
	const root = document.getElementById("root");
	function Title() {
		return (
			<h3 id = "title"
			onMouseEnter={() => console.log("mouse enter")}>
				Hello I'm a Title
			</h3>
		);
	}
	// JSX에게 임의의 버튼 컴포넌트임을 알리기 위해 대문자로 선언
	// Arrow Funcion 사용
	const Button = () =>
		<button style={{ backgroundColor: "tomato", }}
		onClick={() => console.log("I'm clicked")}>
			Click me
		</button>

	const Container = () => <div>
		<Title />
		<Button />
		</div>
	ReactDOM.render(<Container />, root);
</script>
</html>
```
 - [Arrow Function](https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Functions/Arrow_functions)
