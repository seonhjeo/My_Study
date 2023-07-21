### Effect의 필요성
 - 상태가 변경되면 그 상태를 포함하고 있는 컴포넌트들이 업데이트-리렌더링 된다. 이는 타 API같은 무거운 요소들을 리렌더링 할 여지가 존재한다.
 - 따라서 컴포넌트 내의 몇몇 요소들은 프로그래머가 원할 때만 리렌더링 하게 할 수 있다.

### useEffect
 - 어떠한 함수를 시작 시에만 단 한번만 실행하게 하려면 useEffect함수를 사용하면 된다.
 - useEffect함수는 다음과 같이 사용된다.
```
const iRunOnlyOnce = () => {
  console.log("I Run only once");
}
useEffect(iRunOnlyOnce, []);

useEffect(() => {
  console.log("CALL THE API....");
}, []);
```

### useEffect Deps
 - useEffect는 두 가지 인자를 받는 함수이다.
   - 첫 번째 인자는 프로그래머가 실행 조건을 걸고 싶어하는 코드이다.
   - 두 번째 인자는 첫 번째 인자가 실행될 때의 조건들을 모아둔 배열이다.
     - 따라서 빈 배열을 입력받으면 첫 번째 인자가 처음 시작 때 한 번만 실행되게 된다.
 - 이를 이용해 검색 구현 등 특정 조건에 따른 함수 실행이 가능하게 할 수 있다.
 - 다음은 useEffect 사용 예제이다.
```
function App() {
  const [counter, setValue] = useState(0);
  const [keyword, setKeyword] = useState("");

  const onClick = () => { setValue((prev) => prev + 1); }
  const onChange = (event) => setKeyword(event.target.value);

  console.log("I Run all the time");
  const iRunOnlyOnce = () => { console.log("I Run only once"); }

  useEffect(iRunOnlyOnce, []);
  useEffect(() => { console.log("I Run onle once too"); }, []);
  useEffect(() => {
    console.log("I Run when keyword changes");
  }, [keyword]);
  useEffect(() => {
    console.log("I Run when counter changes");
  }, [counter]);
  useEffect(() => {
    console.log("I Run when keyword or counter change");
  }, [keyword, counter]);

  return (
    <div>
      <input value={keyword} onChange={onChange} type="text" placeholder="Search here..." />
      <h1>{counter}</h1>
      <button onClick={onClick}>press this</button>
    </div>
  );
}
```

### cleanup function
 - useEffect로 실행한 함수나 컴포넌트를 리턴하면 해당 요소들이 파괴될 때 반환이 된다. 이를 cleanup Function이라고 한다.
 - 다음은 cleanup을 사용한 useEffect함수이다.
```
function Hello() {
  useEffect(() => {
    console.log("Created :)");
    return () => console.log("Destroyed :<");
  }, [])
  return <h1>Hello</h1>;
}
```
 - 이를 풀어쓰면 다음과 동일하다.
```
function byeFn() {
	console.log("Destroyed :<");
}
function HiFn() {
	console.log("Created :)");
	return byeFn;
}
useEffect(HiFn, []);
```
