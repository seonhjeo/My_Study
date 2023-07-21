### State
 - 리액트는 정확히 바뀐 요소만을 업데이트해준다.
   - 만일 프로그래머가 무언가를 변화하고 모든 요소를 리렌더링을 시도해도, 리액트는 정확히 바뀐 요소만을 업데이트해준다.
   - 바닐라JS는 모든 요소를 리렌더링한다.
   - 하지만 이는 성능면에서 좋지 않으므로 프로그래머는 가능하면 리액트의 자동 랜더링을 사용하는 것이 좋다.
 - 리액트는 값이 바뀌는 변수에 대한 State기능을 제공한다.
   - State는 값을 저장하는 변수와 그 값을 변경해 리랜더링해주는 변경함수가 포함된 배열로 구성되어 있다.
   - 해당 값만을 따로 리랜더링 해주므로 `ReactDOMl.render`등의 전체 렌더링 함수를 호출할 필요가 없다.
   - 값을 선언할 때 각각 요소의 이름을 할당하는 변수로 선언하면 접근하기 쉽다.
```
<script type="text/babel">
	const root = document.getElementById("root");
	function App() {
		// 초기값(counter)과 그 값을 변경할 때 사용하는 함수(modifier)를 제공한다.
    // 변경함수의 값은 대체로 변수 앞에 set을 붙여 이름짓는다.
		// 배열 형식으로 제공되는 것을 각 요소마다 이름을 붙여주었다.
		const [counter, setCounter] = React.useState(0);
		const onClick = () => {
			// 함수가 스스로 값을 변경하고 해당 부분만 리렌더링 해준다.
			setCounter(counter + 1);
		};
		// 리액트에서 요소 텍스트 내에 변수 입력 방법 : 중괄호 안에 변수 넣기
		return (
			<div>
				<h3>Total clicks: {counter}</h3>
				<button onClick={onClick}>Click me</button>
			</div>
		);
	}
	ReactDOM.render(<App />, root);
</script>
```

### State Function

 - state를 바꾸는 방법은 2가지가 있다.
   - 변경 함수의 인자에 원하는 값을 넣어준다.
   - 변경 함수의 인자에 이전 값에 대한 계산을 넣어준다.
 - 이전 값에 대한 계산을 추가할 때는 `current`를 사용한 함수 형식으로 추가해주는 것이 안전한다.
   - 리액트에서 `current`함수는 인자가 확실한 현재 값이라는 것을 보장하며, 항상 해당 state를 얻게 해준다.
   - 따라서 여러 곳에서 변경되는 변수에 대해 중간에 계산이 잘못될 염려가 없어진다.
```
const [counter, setCounter] = React.useState(0);

// 계산식 형태, 비추천
setCounter(counter + 1);
// 함수 형태, 추천
setCounter((current) => current + 1);
```

### Inputs and State
 - 리액트는 HTML과 JS의 문법을 둘 다 사용하기 때문에 겹치게 되는 키워드들이 존재한다.(for, class 등)
   - 이는 대체 키워드(htmlFor, className 등)으로 회피할 수 있다.
 - 리액트에서 값 입력은 바닐라JS에서 했던 것처럼 HTML-JSX를 이용하여 편하게 처리할 수 있다.
```
// 입력 필드 선언 및 함수 연결
<input value={minutes} id="minutes" placeholder="Minutes" type="number" onChange={onChange}/>
// 입력 함수, event인자로 필드에서 발생한 이벤트를 가져옴
const onChange = (event) => {
	...
}
```
 - 다음은 분과 시간을 변환해주는 간단한 변환기이다.
```
function App() {
	const [amount, setAmount] = React.useState(0);
	const [flipped, setFlipped] = React.useState(false);
	const onChange = (event) => { setAmount(event.target.value); }
	const Reset = () => { setAmount(0); }
	const Flipped = () => {
		Reset();
		setFlipped((current) => !current);
	}
	return (
		<div>
			<h1 className="hi">Super Converter</h1>
			<div>
				<label htmlFor="minutes">Minutes</label>
				<input value={flipped ? Math.round(amount * 60) : amount}
				id="minutes" placeholder="Minutes" type="number" onChange={onChange} disabled={flipped}/>
			</div>
			<div>
				<label htmlFor="hours">Hours</label>
				<input value={flipped ? amount : Math.round(amount / 60)}
				id="hours" placeholder="Hours" type="number" onChange={onChange} disabled={!flipped}/>
			</div>
			<button onClick={Reset}>Reset</button>
			<button onClick={Flipped}>{flipped ? "Turn back" : "Invert"}</button>
		</div>
	);
}
```

### Select State
 - 리액트는 여러 컴포넌트들을 만들어 관리할 수 있다.
 - 원하는 컴포넌트를 렌더링하기 위해서는 HTML의 `Select`문을 사용할 수 있다.
 - HTML문 안에서 중괄호를 사용해 JS코드를 작성할 수 있다.
```
// 선택될 컴포넌트들
function KMToMiles() { ... }
function MinutesToHours() { ... }

// 컴포넌트를 선택해 렌더링해주는 컴포넌트
function App() {
	const [index, setIndex] = React.useState("xx");
	const onSelect = (event) => {
		setIndex(event.target.value);
	}
	return (
		<div>
			<h1>Super Converter</h1>
			<select value={index} onChange={onSelect}>
					<option value="xx">Select your units</option>
					<option value="0">Minutes & Hours</option>
					<option value="1">Km & miles</option>
				</select>
				<hr />
				{ index === "xx" ? "Please select your units" : null }
				{ index === "0" ? <MinutesToHours /> : null }
				{ index === "1" ? <KMToMiles /> : null }
		</div>
	)
}
const root = document.getElementById("root");
ReactDOM.render(<App />, root);
```
