[Node.js의 기본 개념](https://hanamon.kr/nodejs-%EA%B0%9C%EB%85%90-%EC%9D%B4%ED%95%B4%ED%95%98%EA%B8%B0/)

### React App 설치
 - 1. [Node.js설치](https://nodejs.org/ko/download/package-manager/#macos), 홈페이지에서도 설치 가능
 - 2. 터미널에서 `npx create-react-app APP_NAME` 실행
 - 설치가 완료되면 다음과 같은 명령어들을 입력할 수 있다.
   - npm start : Starts the development server.
   - npm build : Bundles the app into static files for production.
   - npm test : Starts the test runner.
   - npm run eject : Removes this tool and copies build dependencies, configuration files and scripts into the app directory. If you do this, you can’t go back!

### React App 들여보기
 - src폴더에는 우리가 작성한 모든 소스들이 들어가게 된다.
 - create-react-app은 src폴더의 소스들을 이용해 최종적으로 public/index.html에 작성하게 된다.
 - `XXX.module.css`파일을 만들면 css의 요소를 모듈별로 적용할 수 있다.
   - 이 때 리액트는 해당 css 요소를 클론하여 만든 랜덤 css 요소를 HTML요소에 붙이게 된다.
```
import styles from "./Button.module.css"

function Button({text}){
	return (<button className={styles.btn}>{text}</button>);
}
```
