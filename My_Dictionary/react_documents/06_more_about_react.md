# TO-DO list
 - 추가적인 배열 사용법을 익혔다.

### 배열 setState
 - 다음과 같이 선언이 가능하다.
```
const [toDos, setTodos] = useState([]);
```
 - 이 때 초기값을 빈 배열로 넣어주어 리액트가 해당 변수가 배열임을 알게 하자.
   - 만일 아무 것도 넣어주지 않아 undefined상태가 되면 초기값 때 배열의 기능을 사용할 수 없게 된다.

### 새 배열에 기존 배열 요소 추가
 - 새로운 배열에 기존 a배열의 모든 값과 추가적인 값을 같이 넣고 싶으면, a배열 앞에 `...`을 붙이면 된다.
 - useState를 사용한 배열 변수의 값을 변화할 때 애용된다.
```
const numarr = [1, 2, 3, 4];
const numarrplus = [...numarr, 5, 6];
// numarrplus = [1, 2, 3, 4, 5, 6]
```

### 배열 요소 전체 접근
 - `array.maps()`를 사용하면 array배열의 모든 요소에 접근할 수 있다.
   - 값을 접근할 때에는 맨 처음부터 순차적으로 접근한다.
 - 매개변수로 변경할 값을 넣어주면 모든 요소가 해당 값으로 변경된다.
   - 매개변수는 단순한 값일수도 있고, 상태를 변화시켜주는 함수일 수도 있다.
```
// 배열 내 모든 요소를 temp로 변경한다.
array.map("temp");
// 배열 내 모든 요소를 대문자로 만들어준다.
array.map((item) => item.toUpperCase());
// 배열 내 모든 요소를 HTML의 리스트 형식으로 출력해준다.
{ toDos.map(((item) => <li>{item}</li>)) }
```

### TO-DO list 작성 코드
 - HTML에서 li키워드로 만든 리스트들은 키 값이 필요하다. 해당 코드에서는 배열의 인덱스를 키 값으로 부여해주었다.
 - useState는 비동기 방식의 함수이기 때문에 useState안에서 값을 바꾼 것이 즉각적으로 반영되지 않는다.
   - 따라서 onSubmit함수 내의 콘솔 출력은 최신 값을 출력하지 않는다.

```
function App() {
  const [toDo, setTodo] = useState("");
  const [toDos, setTodos] = useState([]);
  const onChange = (event) => setTodo(event.target.value);
  const onSubmit = (event) => {
    event.preventDefault();
    if (toDo === "")
      return;
    else
    {
      setTodos((currentArray) => [...currentArray, toDo])
      console.log(toDos);
    }

  }
  return (
    <div>
      <h1>My ToDos ({toDos.length})</h1>
      <form onSubmit={onSubmit}>
        <input onChange={onChange} value={toDo} type="text" placeholder="Write your to do..." />
        <button>Add To Do</button>
      </form>
      <hr/>
      { toDos.map(((item, index) => <li key={index}>{item}</li>)) }
    </div>
  )
}
```

# Coin Tracker
 - 추가적인 useEffect 사용법을 익혔다.
 - 가상화폐 API를 끌어와 이를 출력해주는 프로그램을 작성하였다.

### fetch
 - fetch() 컴포넌트로 데이터를 가져올 수 있다. 매개변수는 url의 주소 문자열이다.
   - then()함수를 이용해 fetch()로 가져온 json형식의 데이터를 추가할 수 있다.

```
function App() {
  const [loading, setLoading] = useState(true);
  const [coins, setCoint] = useState([]);

  useEffect(() => {
    fetch("https://api.coinpaprika.com/v1/tickers")
    .then(responce => responce.json())
    .then((json) => {
      setCoint(json);
      setLoading(false);
    });
  }, [])

  return (
    <div>
      <h1>The Coins! {loading ? "" : `(${coins.length})`}</h1>
      { loading ?
        <strong>Loading...</strong> : (
        <select>
          {coins.map((coin) => <option key={coin.rank}>{coin.name} ({coin.symbol}): ${coin.quotes.USD.price}</option>)}
        </select>
      )}

    </div>
  )
}
```

# Movie App
 - 요소를 클릭하면 다른 페이지로 넘어가는 방법을 배운다.
 - [리액트 컴포넌트 키](https://velog.io/@yeonbot/React%EC%97%90%EC%84%9C-key%EC%9D%98-%EC%97%AD%ED%95%A0-%EC%BB%B4%ED%8F%AC%EB%84%8C%ED%8A%B8%EB%A5%BC-%EB%8B%A4%EC%8B%9C%EA%B7%B8%EB%A6%AC%EB%8A%94-%EA%B3%BC%EC%A0%95)

### 여러 컴포넌트를 사용하여 구현
```
// Movie.js
function Movie({coverImg, title, summary, genres}) {
  return (
    <div>
      <img src={coverImg} alt={title} />
      <h2>{title}</h2>
      <ul>
        {genres.map(g => <li key={g}>{g}</li>)}
      </ul>
      <p>{summary}</p>
    </div>
  );
}

Movie.prototype = {
  coverImg: PropTypes.string.isRequired,
  title: PropTypes.string.isRequired,
  summary: PropTypes.string.isRequired,
  genres: PropTypes.arrayOf(PropTypes.string).isRequired,
};


// App.js
function App() {
  const [loading, setLoading] = useState(true);
  const [movies, setMovies] = useState([]);
  const getMovies = async() => {
    const response = await fetch(`https://yts.mx/api/v2/list_movies.json?minimum_rating=10&sort_by=year`);
    const json = await response.json();
    setMovies(json.data.movies);
    setLoading(false);
  }

  useEffect(() => {
    getMovies();
  }, []);
  console.log(movies);

  return (
    <div>
      <div>{ loading ?
        <h1>Loading...</h1> :
        <div>
          { movies.map((movie) => <Movie
            key={movie.id}
            coverImg={movie.medium_cover_image}
            title={movie.title}
            summary={movie.summary}
            genres={movie.genres}
            />) }
          </div>}
        </div>
    </div>
  )
}
```

### react Route
 - [리액트 라우트](https://goddaehee.tistory.com/305)
 - 리액트 라우트는 기본적으로 Home URL뒤의 추가 URL에 대항하는 해당 컴포넌트를 로드해준다.
 - 이를 사용하기 위해서는 우선 `npm i react-route-dom`으로 라우트를 설치한 후 스크립트에 임포트해줘야 한다.
 - 자세한 코드와 주석은 [내 깃허브](https://github.com/seonhjeo/web_FrontEnd_Study/tree/main/my_react_app2) 혹은 [원본 깃허브](https://github.com/nomadcoders/react-for-beginners/tree/master/src)에 있다.

### Publishing
 - 코드를 올리면 해당 결과물을 깃허브 페이지에서 무료로 배포해주는 기능이다.
 - `npm i gh-pages`를 실행해 깃허브 페이지를 설치한다.
 - `npm run build`를 실행해 소스를 빌드한다.
   - `build`는 만든 소스코드를 배포하기 편하게끔 압축하고 최적화해준다.
 - `package.json`의 요소로 다음과 같이 작성해준다.
```
"homepage": "https://MY_GITHUB_USERNAME.github.io/MY_PROJECT_REPOSITORY_NAME"
```
 - `package.json`의 `script`의 마지막 요소에 다음을 추가해준다.
   - deploy를 실행하기 전에 자동으로 predeploy를 해준다.
```
"deploy": "gh-pages -d build",
"predeploy": "npm run build
```
 - `npm run deploy`를 실행한다.
