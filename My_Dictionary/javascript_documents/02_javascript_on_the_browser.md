[참고 자료](https://developer.mozilla.org/en-US/docs/Web/API) 

# 웹서버의 구동 원리
 - 인터렉티브 웹페이지의 프론트엔드는 HTML, CSS, Javascript의 세 가지 언어로 구성된다.
 - 브라우저는 HTML 파일을 실행하고, HTML파일은 브라우저에 의해 실행되는 도중 CSS와 Javascript를 실행한다.
   - HTML상에서 CSS는 `<link rel="stylesheet" href="style.css">`와 같이, Javascript는 `<script src="app.js"></script>`와 같이 불러올 수 있다.
 - 크롬 기준 f12로 팝업되는 개발자 도구에서 요소(Elements)로 HTML 코드를 스타일(Style)로 CSS의 적용 결과를, 콘솔(Console)은 Javascript의 실행 결과를 보여준다.
 - Javascript를 사용하는 이유는 HTML과 상호작용을 하기 위해서이다. 즉 Javascript로 HTML의 요소들을 변경하고 읽을 수 있다.


# Documents 객체
 - javascript상의 document 객체는 접근할 수 있는 HTML을 가리킨다.
   - 브라우저 콘솔 창에 `document`를 입력하면 해당 자바스크립트 소스코드와 연결되어있는 HTML코드를 보여준다.
   - 브라우저 콘솔 창에 `console.dir(document)`를 입력하면 HTML코드를 javascript의 관점(객체)로 보여준다.
 - document 객체도 일반 객체처럼 객체 내 값에 접근해 읽고 쓸 수 있다.
   - HTML의 값과 JS의 값 중 JS의 값이 우선시된다.
```
console.log(document.title); // 해당 HTML의 타이틀을 출력한다.
document.title = "Hello"     // 해당 HTML의 타이틀을 Hello로 변경한다.
// 브라우저 콘솔 창에서 변경된 객체값은 브라우저를 새로고침하면 HTML에 기재된 원래 값으로 돌아간다.
```
 - HTML과 JS를 연결하기 위해서는 HTML에 JS파일을 임포트해주어야 한다.
   - document는 HTML이 JS파일을 로드하기 때문에 존재하며 로드된 이후에 브라우저가 프로그래머를 document에 접근할 수 있게 해준다.
```
// app.js라는 이름의 스크립트 임포트
<script src="app.js"></script>
```

# 자바스크립트 내의 HTML
 - javascript 상으로 HTML 데이터를 가져오는 방법은 `document`객체와 `element`객체의 수많은 함수들을 이용하는 것이다.
   - HTML상에서 변경되는 값 또한 javascript의 document/elements객체에 저장되게 된다.
```
// HTML에서 ID가 title인 elements객체를 가져와 객체 내 innerText값을 변경하는 함수
const title = document.getElementById("title");
title.innerText = "GotCha!";
```

# Elements를 찾는 방법들

### GetElements 함수들
 - Elements를 ID로 찾기 위해서는 `getElementById()`함수를 이용한다.
   - 이 함수는 HTML내의 모든 조건에 맞는 요소 중 맨 처음 나온 단 하나의 요소만 반환한다.
 - Elements를 Classname으로 찾기 위해서는 `getElementsByClassName()`함수를 이용한다.
   - 이 함수는 HTML내의 모든 조건에 맞는 요소를 배열 형식으로 묶어 반환한다.
 - Elements를 Tagname으로 찾기 위해서는 `getElementsByTagName()`함수를 이용한다.
   - 이 함수는 HTML내의 모든 조건에 맞는 요소를 배열 형식으로 묶어 반환한다.

# quarySelector 함수들
 - quarySelector류 함수들은 elements를 css방식(선택자)으로 검색할 수 있다.
   - `quarySelectorAll()`함수는 조건에 부합하는 모든 요소를 배열 형식으로, `quarySelector()`함수는 조건에 부합하는 요소 중 맨 처음 나온 요소를 반환한다.
   - [css선택자](https://developer.mozilla.org/ko/docs/Web/CSS/CSS_Selectors)
```
// hello라는 클래스 내부에 있는 h1을 가져온다
const title = document.querySelector(".hello h1");
```

# 이벤트(Events)
 - JS에서 대부분 작업할 것들은 이벤트를 듣는 것(listen)이다.
   - 이벤트는 클릭, 마우스를 올리는 것, 입력, 연결 해제 등등 모든 것이 될 수 있다.


### 이벤트리스너
 - 목표 elements에 `addEventListener()`함수로 이벤트리스너를 추가해줄 수 있다.
   - `addEventListener()`의 매개인자로는 click, type 등 다양한 이벤트 입력이 들어갈 수 있다.
   - `addEventListener()`의 또 다른 매개인자로는 이벤트 발생 시 실행할 함수가 들어갈 수 있다.
```
// title에 클릭 이벤트를 추가시켜 이벤트 발생시 handleTitleClick()을 실행하게 한다.
const h1 = document.querySelector("div.hello:first-child h1");
function handleh1Click() { console.log("title was clicked!"); }
h1.addEventListener("click", handleTitleClick);
```

### HTMLEvents
 - [HTMLElements 공식 문서](https://developer.mozilla.org/en-US/docs/Web/API/HTMLElement)
   - elements 내의 수많은 변수와 함수에 대한 레퍼런스 문서이다.
   - `console.dir(elements)`으로 콘솔 창에서도 확인할 수 있다.
 - elements 내에는 이벤트에 대한 다양한 프로퍼티가 있고, 프로그래머는 `addEventListener()`를 여러 개를 사용해 하나의 elements에 여러 개의 이벤트를 듣게 할 수 있다.
```
// h1에 마우스를 올리면 파란색, 마우스를 내리면 검정색, 마우스를 클릭하면 콘솔 출력이 되게 하기
const h1 = document.querySelector("div.hello:first-child h1");

function handleTitleClick() { console.log("title was clicked!"); }
function handleMouseEnter() { h1.style.color = "blue"; }
function handleMouseLeave() { h1.style.color = "black"; }

h1.addEventListener("click", handleTitleClick);
h1.addEventListener("mouseenter", handleMouseEnter);
h1.addEventListener("mouseleave", handleMouseLeave);
```

### More Events
 - `addEventListener()`함수 뿐만 아니라 elements 내의 onEvent에 직접 함수를 연결하여 이벤트를 연동시킬 수 있다.
   - `addEventListener()`함수로 설정된 이벤트는 추후에 `removeEventListener()`함수로 설정된 이벤트를 제거할 수 있다.
```
// 둘이 같은 역할을 한다.
h1.addEventListener("click", handleTitleClick);
h1.onclick = handleTitleClick;
```
 - document의 요소 중 head, body, title은 직접적으로 끌고 올 수 있다. 다른 요소들은 함수를 사용해 찾아야 한다.

### Window Events
 - JS가 HTML요소를 document로 기본적으로 제공하듯, 윈도우 요소도 window로 기본적으로 제공한다.
   - window 객체에 접근해 브라우저 창에 대한 작업을 수행할 수 있다.
```
// 브라우저 창 크기를 변경하면 배경색을 토마토색으로 바꾼다.
function handleWindowResize() {
    document.body.style.backgroundColor = "tomato";
}

window.addEventListener("resize", handleWindowResize);
```

# CSS in Javascript
 - `elements.style`을 이용해 JS에서 HTML의 스타일적 요소들을 변경할 수 있다.
   - 하지만 이는 HTML이 아닌 CSS에서 변경하는 것이 더 효율적이다.
   - CSS가 웹페이지의 전반적인 스타일을 담당하기 떄문

### CSS와 HTML의 연결
 - css는 여러 스타일을 묶어놓은 클래스를 선언할 수 있다.
   - css에서 클래스는 .으로 시작한다.
   - 선언된 클래스는 HTML의 꺾쇠 안에서 적용된다.
```
// CSS 파일에 만들어진 font클래스
.font {
	font: 3em "Fira Sans", sans-serif;
}

// HTML의 h1에 폰트 클래스를 적용시킨 상황
<h1 class="font">Grab me!</h1>
```
 - JS상에서 CSS에 선언된 클래스를 HTML에 추가, 삭제, 변경할 수 있다.
   - 이 때 추가할 클래스의 이름 문자열을 다르게 적으면 오류가 발생한다.
```
function handleh1Click() {
    const activeClass = "active";
    if (h1.className === activeClass) {
        h1.className = "";
    } else {
        h1.className = activeClass;
    }   
}
```

### 원활할 CSS Class의 수정
 - 위에서 한 것처럼 직접적으로 클래스의 이름을 수정하는 것보다 `ClassList`를 활용하는 것이 훨신 세련된다.
   - `ClassList`는 css 클래스들의 리스트이다. c++의 컨테이너처럼 사용이 가능하다.
   - 클래스의 검색, 추가, 삭제 등 수많은 기능을 제공하고, 여러 클래스를 중첩하여 사용할 수 있다.
```
function handleh1Click() {
    const activeClass = "active";
    if (h1.classList.contains(activeClass)) {
        h1.classList.remove(activeClass);
    } else {
        h1.classList.add(activeClass);
    }   
}

// 위의 함수와 동일한 기능을 한다.
function handleh1Click() {
  h1.classList.toggle("active");
}
```