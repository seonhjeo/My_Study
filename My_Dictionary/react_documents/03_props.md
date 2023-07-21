### Props
 - Props는 부모 컴포넌트에서 자식 컴포넌트로 데이터를 보내는 방법이다.
   - 부모 컴포넌트에서 값을 HTML스타일로 설정해주면 자식 컴포넌트는 첫 번째 매개변수로 부모가 제공한 값들이 순서대로 저장되어 있는 key-value배열을 받게 된다.
 - props를 이용하면 모양이 비슷하지만 내용이 다른 버튼을 하나의 컴포넌트로 사용하는 등 코드의 재활용율을 높여 준다.
```
function Btn(props) {
	return <button style={{
		backgroundColor: "tomato",
		color:"white",
		padding:"10px 20px",
		border:0,
		borderRadius:20,
	}}>{props.text}</button>
}

function App() {
	return (
		<div>
			<Btn text="Save Changes"/>
			<Btn text="Continue"/>
		</div>
	)
}
```
 - 위의 예시에서 매개변수에 중괄호를 열어 확인하고 싶은 값의 키를 입력해도 접근이 가능하다. 이를 Shortcut이라 한다.
   - ShortCut에 props에 있는 각각의 키 값을 모두 넣으면 C계열의 함수 선언과 유사하게 사용이 된다.
   - 또한 Shortcut은 C계열처럼 기본값을 지정해줄 수 있다.
```
function Btn({text}) {
	...
	}}{text}</button>
}
...
```
 - props를 이용해 단순 자료형부터 함수까지 보낼 수 있다.
   - 함수를 보내는 특성을 이용해 컴포넌트 내의 이벤트를 커스터마이징할 수 있다.
```
// 함수 선언
const changeValue = () => {... };

// 함수 전달
function App() {
	return (
		<Btn text={value} changeValue={changeValue}/>
	)
}

// 전달한 함수를 태그의 프로퍼티에 적용
function Btn({text, changeValue}) {
	return <button
		onClick={changeValue}
		style={{...}}>
		{text}
	</button>
}
```

### Memo
 - 리액트의 특성 상 값이 바뀐 컴포넌트를 리렌더링하기 때문에 자식 컴포넌트 하나의 값이 바뀌어도 그것의 부모 컴포넌트와 연관된 모든 자식 컴포넌트들을 리렌더링하게 된다.
 - 이를 방지하기 위해 `React.memo`선언으로 부모 컴포넌트를 메모하여 업데이트된 자식 컴포넌트만 리렌더링하게 할 수 있다.
```
// 버튼 부모 컴포넌트 선언
function Btn(...) { ... }

// 버튼 메모 선언 ()
const MemorizedBtn = React.memo(Btn)

// 선언된 메모 실행
<MemorizedBtn text={value} changeValue={changeValue}/>
```
