이 문서는 [Learn CSS in 20 Minuites](https://www.youtube.com/watch?v=1PnVor36_40&t=7s) 을 토대로 작성했습니다.  

# What is CSS
 - css프로그래밍 언어가 아니라, 웹페이지의 스타일을 변경해주는 스타일링 언어이다.
 - css는 웹페이지의 내용을 표현(presentation)하고 디자인(design)하기 위한 언어이다.
 - css의 특성상 같은 결과를 내기 위한 방법이 여러가지가 있다.
 - css는 창의성(Creativity)에 초점이 맞춰져 있다.

# CSS Syntax
```
// 기본적인 문법
selector {
    property1: value;
    property2: value;
}
```
 - selector(선택자)
   - css에는 수많은 종류의 selector들이 있다.
   - selector은 스타일들을 묶어놓은 묶음의 이름이다.
 - property-value pair(프로퍼티-값 쌍)
   - 이 쌍은 컬러나 폰트와 같은 프로퍼티의 값을 value로 정하는 역할을 한다.

# Selector
 - css에는 수많은 선택자들이 있다. 그 중 주로 Element, Class, ID Selector가 쓰인다.

### Element Selector
 - HTML의 해당 요(Element)에 직접적으로 적용되는 선택자이다.
   - 선택자가 div이면 HTML의 div 내 모든 요소가 바뀐다.
```
// css 구현부
div {
    property: value;
}

// HTML 선언부
<div> ... </div>
```

### Class Selector
 - 가장 유용하고 많이 쓰이는 선택자이다.
   - 재사용 및 재활용이 원활한 선택자이다.
 - 여러 프로퍼티를 하나의 클래스명으로 묶어놓고, HTML상에서 선언하여 사용한다.
   - 클래스명을 스페이스로 띄워서 하나의 요소에 여러 클래스를 추가할 수 있다.
 - 클래스 선택자를 선언할 때에는 선택자 이름 앞에 `.`을 붙인다.
```
// css 구현부
.class_name {
    property: value;
}

// HTML 선언부
<div class="class_name"> ... </div>
```

### ID Selector
 - 클래스 선택자와 비슷하지만, 여러 개를 가질 수 있는 클래스 선택자와 다르게 HTML의 요소는 단 하나의 ID 선택자를 가질 수 있다.
 - 따라서 ID 선택자는 매우 특수한 표현을 위해서 쓰이곤 하지만, 강제적으로 쓸 필요는 없다.

```
// css 구현부
#id_name {
    property: value;
}

// HTML 선언부
<h1 id="id_name"> ... </h1>
```

# CSS Selector Combination
 - [선택자에 관한 참고 자료](https://lktprogrammer.tistory.com/94)
 - css는 선택자를 혼합하여 특정 요소에 쓰이는 선택자를 만들 수 있다.

 - 한 요소가 갖고 있는 모든 선택자를 지정할 수 있다.
   - 이 때 선언한 선택자간에는 띄어쓰기를 사용하지 않는다.
```
// 기본적인 사용법
selector1selector2 {
    property: value;
}

// 실제 예시
// large-header클래스 선택자를 갖고 있는 모든 h1요소의 스타일을 바꾼다.
h1.large-header {
    property: value;
}

// big-blue ID선택자, large 클래스 선택자, blue 클래스 선택자를 모두 갖고 있는 모든 요소의 스타일을 바꾼다.
#big-blue.large.blue {
    property: value;
}
```

 - 부모와 자식 선택자를 지정하여 HTML상에서 부모 브라켓에 속한 자식 브라켓에 접근할 수 있다.
   - 이 때 부모와 자식 간의 선택자에는 
```
// 기본적인 사용법
ancestor child {
    property: value;
}

// 실제 예시
// <div> 브라켓 안의 모든 <p> 브라켓 요소 스타일을 바꾼다
div p {
    property:value;
}

// main_header클래스 선택자를 가진 <header> 브라켓 안의 모든 brown클래스 선택자를 가진 <h1> 브라켓 요소 스타일을 바꾼다
header.main_header h1.brown {
    property:value;
}
```

 - 동일한 프로퍼티를 여러 개의 선택자가 공유할 수 있다.
   - 코드의 중복을 방지해 코드의 양을 줄일 수 있다.
```
// 기본적인 사용법
selector1, selector2 {
    property: value;
}

// 실제 예시
// big 클래스 선택자와 large 클래스 선택자의 property 공유
.big, .large {
    property:value;
}
```

 - 웹페이지 내 모든 요소에 대해 접근할 수도 있다.
   - 웹페이지에서 기본적으로 모든 요소가 사용할 스타일을 지정할 수 있다.
```
// 기본적인 사용법
* {
    property: value;
}
```

# How to Load CSS
 - css를 로드하는 방법은 크게 3가지가 있다
 - css를 여러번 로드할 때 최종적으로 가장 마지막에 로드된 스타일 혹은 선택자들이 실제로 적용이 된다.

### Inline
 - HTML상에서 직접적으로 로드하는 방법이다.
   - HTML 코드 내에서 css를 직접 작성하는 방식으로 선택자가 필요 없다.
   - 코드가 복잡해지고 재사용이 불가능하며, HTML이 너무 많은 걸 처리하기 때문에 매우 좋지 않은 방법이다.
```
<h1 style="color: blue;"> <h1>
```

### Style Element
 - HTML상에 `Style` 요소를 추가하여 작성하는 방식이다.
   - 코드의 재사용은 가능하지만 여전히 복잡하고, HTML이 너무 많은 걸 처리하기 때문에 여전히 좋지 않다.
```
<style>
    .blue {
        color: blue;
    }
</style>
```

### external CSS
 - 외부의 css 코드를 가져오는 방식이다.
 - 가장 좋은 방법
```
<link rel="stylesheet" href="style.css" />
```
 - rel은 HTML도큐먼트의 링크될 파일의 관계를 정의한다.
   - 이 때 css파일은 "stylesheet"라는 관계로 정의된다.
 - href는 실제 css파일의 디렉토리 경로 혹은 url 경로를 지정한다.