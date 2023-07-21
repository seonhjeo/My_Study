# 102.SQL-DDL

## DDL(데이터 정의어)
- **DB를 구축하거나 수정할 목적으로 사용하는 언어**
- 번역한 결과가 데이터 사전이라는 특별한 파일에 여러 개의 테이블로 저장됨
- DDL의 3가지 유형
  - CREATE : SCHEMA, DOMAIN, TABLE, VIEW, INDEX를 정의함
  - ALTER : TABLE에 대한 정의를 변경하는 데 사용함
  - DROP : SCHEMA, DOMAIN, TABLE, VIEW, INDEX를 삭제함

## CREATE SCHEMA
- **스키마를 정의하는 명령문**
```
CREATE SCHEMA 스키마명 AUTHORIZATION 사용자_ID;
```

## CREATE DOMAIN
- **도메인을 정의하는 명령문**
```
CREATE DOMAIN 도메인명 [AS] 데이터_타입
       [DEFAULT 기본값]
       [CONSTRAINT 제약조건명 CHECK (범위값)];
```
- 데이터 타입 : SQL에서 지원하는 데이터 타입
- 기본값 : 데이터를 입력하지 않았을 때 자동으로 입력되는 값

## CREATE TABLE
- **테이블을 정의하는 명령문**
```
CREATE TABLE 테이블명
        (속성명 데이터_타입 [DEFAULT 기본값] [NOT NULL], ...
        [, PRIMARY KEY(기본키_속성명, ...)]
        [, UNIQUE(대체키_속성명, ...)]
        [, FOREIGN KEY(외래키_속성명, ...)
                    REFERENCE 참조테이블(기본키_속성명, ...)]
                    [ON DELETE 옵션]
                    [ON UPDATE 옵션]
        [, CONSTRAINT 제약조건명] [CHECK (조건식)]);
```
- 기본 테이블에 포함될 모든 속성에 대하여 속성명과 그 속성의 데이터 타입, 기본값, NOT NULL 여부 지정
- PRIMARY KEY : 기본키로 사용할 속성 지정
- UNIQUE : 대체키로 사용할 속성을 지정, 중복된 값을 가질 수 없음
- FOREIGN KEY ~ REFERENCE ~ : 외래키로 사용할 속성 지정
  - ON DELETE 옵션 : 참조 테이블의 튜플이 삭제되었을 때 기본 테이블에 취해야 할 사항 지정
  - ON UPDATE 옵션 : 참조 테이블의 참조 속성 값이 변경되었을 때 기본 테이블에 취해야 할 사항 지정
- CONSTRAINT : 제약 조건의 이름 지정
- CHECK : 속성 값에 대한 제약 조건 정의

## CREATE VIEW
- **뷰를 정의하는 명령문**
  - 뷰는 하나 이상의 기본 테이블로부터 유도되는 이름을 갖는 가상 테이블
```
CREATE VIEW 뷰명[(속성명[, 속성명, ...])]
AS SELECT문;
```

## CREATE INDEX
- **인덱스를 정의하는 명령문**
```
CREATE [UNIQUE] INDEX 인덱스명
ON 테이블명(속성명 [ASC | DESC] [,속성명 [ASC | DESC]])
[CLUSTER];
```
- UNIQUE
  - 사용된 경우 : 중복 값이 없는 속성으로 인덱스 생성
  - 생략된 경우 : 중복 값을 허용하는 속성으로 인덱스 생성
- 정렬 여부 지정
  - ASC : 오름차순 정렬
  - DESC : 내림차순 정렬
  - 생략된 경우 : 오름차순으로 정렬됨
- CLUSTER : 사용하면 인덱스가 클러스터드 인덱스로 설정됨

## ALTER TABLE
- **테이블에 대한 정의를 변경하는 명령문**
```
ALTER TABLE 테이블명 ADD 속성명 데이터_타입 [DEFAULT '기본값'];
ALTER TABLE 테이블명 ALTER 속성명 [SET DEFAULT '기본값'];
ALTER TABLE 테이블명 DROP COLUMN 속성명 [CASCADE];
```
- ADD : 새로운 속성(열)을 추가할 때 사용
- ALTER : 특정 속성의 디폴트 값을 변경할 때 사용
- DROP COLUMN : 특정 속성을 삭제할 때 사용

## DROP
- **스키마, 도메인, 기본 테이블, 뷰 테이블, 인덱스, 제약 조건 등을 제거하는 명령문**
```
DROP SCHEMA 스키마명 [CASCADE | RESTRICT];
DROP DOMAIN 도메인명 [CASCADE | RESTRICT];
DROP TABLE 테이블명 [CASCADE | RESTRICT];
DROP VIEW 뷰명 [CASCADE | RESTRICT];
DROP INDEX 인덱스명 [CASCADE | RESTRICT];
DROP CONSTRAINT 제약조건명;
```
- CASCADE : 제거할 요소를 참조하는 다른 모든 개체를 함께 제거
- RESTRICT : 다른 개체가 제거할 요소를 참조중일 때는 제거 취소

1. ALTER / ADD
2. CREATE INDEX idx_name ON student(name)
3. CREATE TABLE patient(
  id CHAR(5), name CHAR(10), sex CHAR(1), phone CHAR(20),
  PRIMARY KEY(id),
  CONSTRAINT sex_ck CHECK (sex = 'f' or sex = 'm'),
  CONSTRAINT id_fk FOREIGN KEY(id) REFERENCE docker(doc_id)
);
4. CREATE TABLE Instructor(
  id CHAR(5) NOT NULL, name CHAR(15), dept CHAR(15),
  PRIMARY KEY(id),
  FOREIGN KEY(dept) REFERENCE Department(dept)
  ON UPDATE CASCADE
  ON DELETE SET NULL
);
5. ALTER TABLE patient ADD (job CHAR(20))
6. CREATE VIEW CC(ccid, ccname, instname)
  AS SELECT (Course.id, Course.name, Instructor.name)
  FROM (Course, Instructor)
  WHERE (Course.instructor = Instructor.id);
7. REFERENCE / CASCADE
8. CREATE UNIQUE INDEX Stud_idx
  ON Student(ssn ASC);
9. DEFAULT '사원' / CONSTRAINT / VALUE IN('사원', '대리', '과장', '부장', '이사', '사장')
10. CREATE INDEX 직원_name on 직원(이름);
11. CASCADE
12. Create, Alter, Drop
13. CHECK / IN


# 103.SQL-DCL

## DCL(데이터 제어어)
- **데이터의 보안, 무결성, 회복, 병행 제어 등을 정의하는 데 사용하는 언어**
- 데이터베이스 관리자가 데이터 관리를 목적으로 사용함
- 종류
  - COMMIT : 명령에 의해 수행된 결과를 실제 물리적 디스크로 저장하고, 데이터베이스 조작 작업이 정상적으로 완료되었음을 관리자에게 알려줌
  - ROLLBACK : 데이터베이스 조작 작업이 비정상적으로 종료되었을 때 원래의 상태로 복구
  - GRANT : 데이터베이스 사용자에게 사용 권한 부여
  - REVOKE : 데이터베이스 사용자의 사용 권한 취소

## GRANT / REVOKE
- GRANT : 권한 부여 명령어, REVOKE : 권한 취소 명령어
```
// 사용자등급 지정 및 해제
GRANT 사용자등급 TO 사용자_ID_리스트 [IDENTIFIED BY 암호];
REVOKE 사용자등급 FROM 사용자_ID_리스트;

// 테이블 및 속성에 대한 권한 부여 및 취소
GRANT 권한_리스트 ON 개체 TO 사용자 [WITH GRANT OPTION];
REVOKE [GRANT OPTION FOR] 권한_리스트 ON 개체 FROM 사용자 [CASCADE];
```
- 권한 종류 : ALL, SELECT, INSERT, DELETE, UPDATE 등
- WITH GRANT OPTION : 부여받은 권한을 다른 사용자에게 다시 부여할 수 있는 권한 부여
- GRANT OPTION FOR : 다른 사용자에게 권한을 부여할 수 있는 권한 취소
- CASCADE : 권한 취소 시 권한을 부여받았던 사용자가 다른 사용자에게 부여한 권한도 연쇄적으로 취소

## COMMIT
- **트랜잭션이 수행한 내용을 데이터베이스에 반영하는 명령**
- 자동으로 COMMIT과 ROLLBACK이 되게 오토커밋 기능을 설정할 수 있다.

## ROLLBACK
- **변경되었으나 아직 COMMIT되지 않은 모든 내용들을 취소하고 데이터베이스를 이전 상태로 되돌리는 명령**

## SAVEPOINT
- ** 트랜잭션 내에 ROLLBACK할 위치인 저장점을 지정하는 명령어

```
SAVEPOINT S1;
DELETE FROM 사원 WHERE 사원번호=20;
COMMIT;
SAVEPOINT S2;
ROLLBACK TO S1;
```

1. 변경되었으나 아직 커밋되지 않은 모든 내용들을 취소하고 데이터베이스를 이전 상태로 되돌리는 명령어
2. 사용자에게 사용자 권한 또는 테이블에 대한 접근 권한을 부여하는 명령어
3. GRANT / ON
4. GRANT ALL ON 학생 TO 김하늘;
5. GRANT DELETE ON 강좌 TO 김하늘 WITH GRANT OPTION;
6. REVOKE (SELECT, INSERT, DELETE) ON 교수 FROM 임꺽정;
7. REVOKE UPDATE ON 수강 FROM 임꺽정 CASCADE;
8. 모든 트랜잭션의 연산이 정상적으로 수행되어 결과값을 데이터베이스에 저장하는 것
9. ROLLBACK TO P1;


# 104.SQL-DML

## DML(데이터 조작어)
- **저장된 데이터를 실질적으로 관리하는데 사용되는 언어**
- DB사용자와 DB관리시스템 간의 인터페이스 제공
- DML의 유형
  - SELECT : 테이블에서 튜플 검색
  - INSERT : 테이블에 새 튜플 삽입
  - DELETE : 테이블에서 튜플 삭제
  - UPDATE : 테이블에서 튜플 내용 갱신

## 삽입문(INSERT INTO~)
- 기본 테이블에 새로운 튜플을 삽입할 때 사용
- 대응하는 속성과 데이터는 개수와 유형이 일치해야 함
- 기본 테이블의 모든 속성을 사용할 때는 속성명 생략 가능
- SELECT문을 사용하여 다른 테이블의 검색 결과 삽입 가능
```
// 일반 형식
INSERT INTO 테이블명([속성명1, 속성명2, ...])
VALUES (데이터1, 데이터2, ...);

// SELECT 사용 형식
INSERT INTO 테이블명([속성명1, 속성명2, ...])
SELECT (데이터명1, 데이터명2, ...)
FROM 테이블_이름
WHERE 조건;
```

## 삭제문(DELETE FROM~)
- 기본 테이블에 있는 튜플들 중에서 특정 튜플을 삭제할 때 사용
- 모든 레코드를 삭제할 때에는 WHERE절 생략
- 모든 레코드를 삭제해도 테이블 구조는 남아있기 때문에 디스크에서 테이블을 완전히 제거하는 DROP과는 다름
```
DELETE FROM 테이블명
[WHERE 조건];
```

## 갱신문(UPDATE~ SET~)
- 기본 테이블에 있는 튜플들 중에서 특정 튜플의 내용을 변경할 때 사용
```
UPDATE 테이블명
SET 속성명=데이터[, 속성명=데이터, ...]
[WHERE 조건];
```

1. DELETE FROM 학생 WHERE 이름='민수';
2. INSERT INTO 학생(학번, 성명, 학년, 과목, 연락처)
   VALUES(98170823, '한국산', 3, '경영학개론', '?-1234-1234');
3. DELETE FROM 학생 WHERE 이름='Scott';
4. SET / WHERE
5. UPDATE / SET
6. INSERT INTO 기획부(성명, 경력, 주소, 기본급)
   SELECT(성명, 경력, 주소, 기본급)
   FROM 사원
   WHERE 부서 = '기획';


# 105.DML-SELECT-1

## 일반 형식
```
SELECT [PREDICATE] [테이블명.]속성명 [AS 별칭][,[테이블명.]속성명, ...]
// [, 그룹함수(속성명) [AS 별칭]]
// [, Window함수 OVER (PARTITION BY 속성명1, 속성명2, ...
//                     ORDER BY 속성명3, 속성명4, ...)]
FROM 테이블명[, 테이블명, ...]
[WHERE 조건]
// [GROUP BY 속성명, 속성명, ...]
// [HAVING 조건]
[ORDER BY 속성명 [ASC|DESC]];
```
- SELECT절
  - PREDICATE : 검색할 튜플 수를 제한하는 명령어를 기술함
    - DISTINCT : 중복된 튜플이 있으면 그 중 첫 번째 한 개만 표시함
  - 속성명 : 검색하여 불러올 속성 또는 속성을 이용한 수식 지정
  - AS : 속성이나 연산의 이름을 다른 이름으로 표시하기 위해 사용함
- FROM절 : 검색할 데이터가 들어있을 테이블 이름을 기술함
- WHERE절 : 검색할 조건을 기술함
- ORDER BY절 : 데이터를 정렬하여 검색할 때 사용함
  - 속성명 : 정렬의 기준이 되는 속성명
  - ASC|DESC : 각각 오름차순|내림차순, 생략 시 오름차순 정렬

## 조건 연산자

- 비교 연산자
  - `= : 같다`
  - `<> : 같지 않다`
  - `> : 크다`
  - `< : 작다`
  - `>= : 크거나 같다`
  - `<= : 작거나 같다`
- LIKE연산자 : 대표문자를 이용해 지정된 속성의 값이 문자 패턴과 일치하는 튜플을 검색하기 위해 사용
  - `% : 모든 문자 대표`
  - `_ : 문자 하나 대표`
  - `# : 숫자 하나 대표`


## 기본 검색
```
// 사원 테이블의 모든 튜플 검색
SELECT *
FROM 사원;

// 사원 테이블에서 주소만 검색하되 같은 주소는 한 번만 출력
SELECT DISTINCT 주소
FROM 사원;

// 사원 테이블에서 기본급에 특별수당 10을 더한 월급을 XX부서의 XXX의 월급 XXX형태로 출력
SELECT 부서 + '부서의' AS 부서2, 이름 + '의 월급' AS 이름2, 기본급 + 10 AS 기본급2
FROM 사원;
```

## 조건 지정 검색
```
// 사원 테이블에서 기획부의 모든 튜플 검색
SELECT *
FROM 사원
WHERE 부서 = '기획';

// 사원 테이블에서 기획 부서에 근무하면서 대흥동에 사는 사람의 튜플을 검색
SELECT *
FROM 사원
WHERE 부서='기획' AND 주소='대흥동';

// 사원 테이블에서 성이 김인 사람의 튜플 검색
SELECT *
FROM 사원
WHERE 이름 LIKE "김%";

// 사원 테이블에서 생일이 01/01/69에서 12/31/73사이인 튜플을 검색
SELECT *
FROM 사원
WHERE 생일 BETWEEN #01/01/69# AND #12/31/73#;
```

## 정렬 검색
```
// 사원 테이블에서 부서를 기준으로 오름차순 정렬하고, 같은 부서에 대해서는 이름을 기준으로 내림차순 정렬시켜서 검색
SELECT *
FROM 사원
ORDER BY 부서 ASC, 이름 DESC;
```

## 하위 질의
```
// 취미활동을 하지 않는 사원들을 검색
SELECT *
FROM 사원
WHERE 이름 NOT IN(SELECT 이름 FROM 여가활동);

// 망원동에 거주하는 사원들의 기본급보다 적은 기본급을 받는 사원의 정보 검색
SELECT 이름, 기본급, 주소
FROM 사원
WHERE 기본급 < ALL (SELECT 기본급 FROM 사원 WHERE 주소 = "망원동");
```

1. 3
2. 200 / 3 / 1
3. ORDER / score / DESC
4. ALL
5. 이% / DESC
6. SELECT pid
   FROM Sale
   WHERE psale >= 10 AND psale <= 20;
7. SELECT (학번, 이름, 결제여부)
   FROM 학생정보, 결제
   WHERE (학생정보.학번 = 신청정보.학번 AND 신청정보.신청번호 = 결제.신청번호 and 신청정보.신청과목 = 'OpenGL');
8. 
```
// 1번
SELECT (ID, NAME)
FROM CUSTOMER;

// 2번
SELECT DISTINCT GRADE
FROM CUSTOMER;

// 3번
SELECT *
FROM CUSTOMER
ORDER BY ID DESC;

// 4번
SELECT NAME
FROM CUSTOMER
WHERE AGE IS NULL;

// 5번
SELECT NAME
FROM CUSTOMER
WHER AGE IS NOT NULL;
```
9. 연락번호 IS NOT NULL
10. SELECT 팀코드 FROM 직원 WHERE 이름='정도일'


# 106.DML-SELECT-2

## 일반 형식
```
SELECT [PREDICATE] [테이블명.]속성명 [AS 별칭][,[테이블명.]속성명, ...]
 [, 그룹함수(속성명) [AS 별칭]]
 [, Window함수 OVER (PARTITION BY 속성명1, 속성명2, ...
                     ORDER BY 속성명3, 속성명4, ...)]
//FROM 테이블명[, 테이블명, ...]
//[WHERE 조건]
 [GROUP BY 속성명, 속성명, ...]
 [HAVING 조건]
//[ORDER BY 속성명 [ASC|DESC]];
```
- 그룹함수 : GROUP BY 절에 지정된 그룹별로 속성의 값을 집계할 함수를 기술
- WINDOW 함수 : GROUP BY절을 이용하지 않고 속성의 값을 집계할 함수를 기술함
  - PARTITION BY : 윈도우 함수의 적용 범위가 될 속성 지정
  - ORDER BY : PARTITION 안에서 정렬 기준으로 사용할 속성 지정
- GROUP BY절 : 특정 속성을 기준으로 그룹화하여 검색할 때 사용, 일반적으로 GROUP BY절은 그룹함수와 함께 사용
- HAVING절 : GROUP BY와 함께 사용되며, 그룹에 대한 조건 지정

## 그룹 함수
- GROUP BY절에 지정된 그룹별로 속성의 값을 집계할 때 사용됨
  - COUNT(속성명) : 그룹별 튜플 수를 구하는 함수
  - SUM(속성명) : 그룹별 합계를 구하는 함수
  - AVG(속성명) : 그룹별 평균을 구하는 함수
  - MAX(속성명) : 그룹별 최대값을 구하는 함수
  - MIN(속성명) : 그룹별 최소값을 구하는 함수
  - STDDEV(속성명) : 그룹별 표준편차를 구하는 함수
  - VARIANCE(속성명) : 그룹별 분산을 구하는 함수
  - ROLLUP(속성명, ...) : 인수로 주어진 속성을 대상으로 그룹별 소계를 구하는 함수
  - CUBE(속성명, ...) : ROLLUP과 유사한 형태지만 CUBE는 인수로 주어진 속성을 대상으로 모든 조합의 그룹별 소계를 구함

## WINDOW함수
- GROUP BY절을 이용하지 않고 함수의 인수로 지정한 속성의 값을 집계
- 함수의 인수로 지정한 속성이 집계할 범위가 되는데 이를 윈도우라고 부름
- 함수 종류
  - ROW_NUMBER() : 윈도우별로 각 레코드에 대한 일련번호 반환
  - RANK() : 윈도우별로 순위를 반환하며, 공동 순위 반영
  - DENSE_RANK() : 윈도우별로 순위를 반환하며, 공동 순위를 무시하고 순위 부여


## WINDOW함수 이용 검색
```
// 상여금 테이블에서 상여내역 별로 상여금에 대한 일련 번호를 구하라, 순서는 내림차순에 속성명은 NO
SELECT 상여내역, 상여금, ROW_NUMBER() OVER (PARTITION BY 상여내역 ORDER BY 상여금 DESC) AS NO
FROM 상여금;

// 상여금 테이블에서 상여내역별로 상여금에 대한 순위 구하기, 순서는 내림순, 속성명은 상여금순위, RANK함수 이용
SELECT 상여내역, 상여금, RANK() OVER (PARTITION BY 상여내역 ORDER BY 상여금 DESC) AS 상여금순위
FROM 상여금;
```

## 그룹 지정 검색
```
// 상여금 테이블에서 부서별 상여금의 평균 구하기
SELECT 부서, AVG(상여금) AS 평균
FROM 상여금
GROUP BY 부서;

// 상여금 테이블에서 부서별 튜플 수 검색
SELECT 부서, COUNT(*) AS 사원수
FROM 상여금
GROUP BY 부서;

// 상여금 테이블에서 상여금이 100 이상인 사원이 2명 이상인 부서의 튜플 수를 구하기
SELECT 부서, COUNT(*) AS 사원수
FROM 상여금
WHERE 상여금 >= 100
GROUP BY 부서
HAVING COUNT(*) >= 2;

// 상여금 테이블의 부서, 상여내역, 상여금에 대해 부서별 상여내역별 소계와 전체 합계 검색, 속성명은 상여금합계, ROLLUP함수 사용
SELECT 부서, 상여내역, SUM(상여금) AS 상여금합계
FROM 상여금
GROUP BY ROLLUP(부서, 상여내역);
```

## 집합 연산자를 이용한 통합 질의
```
SELECT 속성명1, 속성명2, ...
FROM 테이블명
UNION | UNION ALL | INTERSECT | EXPERT
SELECT 속성명1, 속성명2, ...
FROM 테이블명
[ORDER BY 속성명 [ASC | DESC]];
```
- 두 개의 SELECT문에 기술한 속성들은 개수와 데이터 유형이 서로 동일해야 한다
- 집합 연산자의 종류(통합 질의의 종류)
  - UNION : 두 SELECT문의 조회 결과를 통합하여 중복된 행은 한 번만 출력
  - UNION ALL : 두 SELECT문의 조회 결과를 통합하여 중복된 행도 그대로 출력
  - INTERSECT : 두 SELECT문의 조회 결과 중 공통된 행만 출력
  - EXCEPT : 첫 번째 SELECT문의 조회 결과에서 두 번째 SELECT문의 조회 결과를 제외한 행 출력

1. ```
   SELECT 학과, COUNT(*) AS 학과별튜플수
   FROM 학생
   GROUP BY 학과;
   ```
2. ```
   SELECT 과목이름, MIN(*) AS 최소점수, MAX(*)AS 최대점수
   FROM 성적
   GROUP BY 점수
   HAVING AVG(점수) >= 90;
   ```
3. 3 / 4
4. 1
5. ```
   SELECT SUM(Sale.psale)
   FROM Sale
   WHERE pid IN(
     SELECT id
     FROM Product
     WHERE name LIKE "USB%"
   );
   ```
6. 매출액 > 1000 / 소속도시 / 3
7. UNION ALL
8. 장학내역, 장학금, NUM


# 107.DML-JOIN

## JOIN
- **연관된 튜플들을 결합하여 하나의 새로운 릴레이션을 반환**
- 일반적으로 FROM절에 기술하지만 릴레이션이 사용되는 곳 어디에나 사용 가능
- 크게 INNER JOIN과 OUTER JOIN으로 구분됨

## INNER JOIN
- 일반적으로 EQUI JOIN과 NON-EQUI JOIN으로 구분됨
- 조건이 없는 INNER JOIN을 수행하면 CROSS JOIN과 동일한 결과를 얻을 수 있음
- EQUI JOIN
  - JOIN 대상 테이블에서 공통 속성을 기준으로 =비교에 의해 같은 값을 가지는 행을 연결하여 결과를 생성하는 JOIN 방법
  - =비교시 동일 속성이 두 번 나타나는데 이 때 중복 속성을 제거하는 것을 NATURAL JOIN이라 함
  - EQUI JOIN에서 연결 고리가 되는 공통 속성을 JOIN속성이라고 한다
```
// WHERE절을 이용한 EQUI JOIN 표기 형식
SELECT [테이블명.]속성명, [테이블명.]속성명, ...
FROM 테이블명1, 테이블명2, ...
WHERE 테이블명1.속성명 = 테이블명2.속성명;

// NATURAL JOIN을 이용한 EQUI JOIN 표기 형식
SELECT [테이블명.]속성명, [테이블명.]속성명, ...
FROM 테이블명1 NATURAL JOIN 테이블명2;

// JOIN~USING절을 이용한 EQUI JOIN의 표기 형식
SELECT [테이블명.]속성명, [테이블명.]속성명, ...
FROM 테이블명1 JOIN 테이블명2 USING(속성명);
```
- NON-EQUI JOIN
  - JOIN조건에 =이 아닌 나머니 비교연산자를 사용하는 방식
```
SELECT [테이블명.]속성명, [테이블명.]속성명, ...
FROM 테이블명1, 테이블명2, ...
WHERE (NON-EQUI JOIN 조건);
```

## OUTER JOIN
- 릴레이션에서 JOIN조건에 만족하지 않는 튜플도 결과로 출력하기 위한 방법으로 LEFT/RIGHT/FULL OUTER JOIN이 있다
- LEFT OUTER JOIN
  - INNER JOIN의 결과를 구한 후 우측 항 릴레이션의 어던 튜플과도 맞지 않는 좌측항의 릴레이션에 있는 튜플들에 NULL값을 붙여 INNER JOIN에 추가
```
SELECT [테이블명.]속성명, [테이블명.]속성명, ...
FROM 테이블명1, LEFT OUTER JOIN 테이블명2
ON 테이블명1.속성명 = 테이블명2.속성명;

SELECT [테이블명.]속성명, [테이블명.]속성명, ...
FROM 테이블명1, LEFT OUTER JOIN 테이블명2
WHERE 테이블명1.속성명 = 테이블명2.속성명(+);
```
- RIGHT OUTER JOIN
  - INNER JOIN의 결과를 구한 후 좌측 항 릴레이션의 어던 튜플과도 맞지 않는 우측항의 릴레이션에 있는 튜플들에 NULL값을 붙여 INNER JOIN에 추가
```
SELECT [테이블명.]속성명, [테이블명.]속성명, ...
FROM 테이블명1, RIGHT OUTER JOIN 테이블명2
ON 테이블명1.속성명 = 테이블명2.속성명;

SELECT [테이블명.]속성명, [테이블명.]속성명, ...
FROM 테이블명1, LEFT OUTER JOIN 테이블명2
WHERE 테이블명1.속성명(+) = 테이블명2.속성명;
```
- FULL OUTER JOIN
  - INNER JOIN후 LEFT와 RIGHT OUTER JOIN을 둘 다 하는 것
```
SELECT [테이블명.]속성명, [테이블명.]속성명, ...
FROM 테이블명1, FULL OUTER JOIN 테이블명2
ON 테이블명1.속성명 = 테이블명2.속성명;
```

1. JOIN
2. WHERE / NATURAL JOIN / USING
3. ON / 코드


# 108.트리거

## 트리거
- 데이터의 삽입, 갱신, 삭제 등의 **이벤트가 발생할 때 관련 작업이 자동으로 수행되게 하는 절차형 SQL**
- DB에 저장되며 데이터 변경 및 무결성 유지, 로그 메시지 출력 등의 목적으로 사용됨
- 트리거의 구문에는 DCL을 사용할 수 없다

## 트리거의 구성도
```
DECLARE(필수)
EVENT(필수)
BEGIN(필수)
   CONTROL
   SQL
   EXCEPTION
END(필수)
```
- DECLARE : 트리거의 명칭, 변수 및 상수, 데이터 타입을 정의하는 선언부
- EVENT : 트리거가 실행되는 조건 명시
- BEGIN/END : 트리거의 시작과 종료 의미
- CONTROL : 조건문 또는 반복문이 삽입되어 순차적으로 처리됨
- SQL : DML문이 삽입되어 데이터 관리를 위한 조회, 추가, 수정, 삭제 작업을 수행
- EXCEPTION : BEGIN~END 안의 구문 시 예외가 발생하면 이를 처리하는 방법 정의

## 트리거 생성
```
CREATE [OR REPLACE] TRIGGER 트리거명 [동작시기 옵션][동작 옵션] ON 테이블명
REFERENCING [NEW | OLD] AS 테이블명
FOR EACH ROW
[WHEN 조건식]
BEGIN
  트리거 BODY;
END;
```
- OR REPLACE : 동일한 이름의 트리거가 이미 존재할 시 기존 트리거 대체
- 동작시기 옵션 : 트리거가 실행될 때를 지정
  - AFTER : 테이블이 변경된 후 트리거가 실행됨
  - BEFORE : 테이블이 변경되기 전 트리거가 실행됨
- 동작 옵션 : 트리거가 실행되게 할 작업의 종류를 지정
  - INSERT : 테이블에 새 튜플이 삽입될 때 트리거가 실행됨
  - DELETE : 테이블의 튜플을 삭제할 때 트리거가 실행됨
  - UPDATE : 테이블의 튜플을 수정할 때 트리거가 실행됨
- NEW | OLD : 트리거가 적용될 테이블의 별칭을 지정함
  - NEW : 추가되거나 수정에 참여할 튜플들의 집합을 의미
  - OLD : 수정되거나 삭제 전 대상이 되는 튜플들의 집합을 의미함
- FOR EACH ROW : 각 튜플마다 트리거를 적용한다는 의미
- WHEN 조건식 : 트리거를 적용할 튜플의 조건 지정
- 트리거 BODY : 트리거의 본문 코드를 입력하는 부분으로 적어도 하나 이상의 SQL문이 있어야 한다

## 트리거의 제거
```
DROP TRIGGER 트리거명
```

1. XMFLRJ
2. UPDATE / FOR EACH ROW

---

1. DROP TABLE 직원;
2. 
```
CREATE TABLE 직원(사번 CHAR(15), 이름 CHAR(4) NOT NULL, 전화번호 CHAR(20), 부서번호 CHAR(10), 경력 NUMBER, 기본급 NUMBER,
PRIMARY KEY 사번,
FOREIGN KEY 부서번호 REFERENCE 부서.부서번호,
UNIQUE 전화번호,
CHECK (기본급 >= 1000000));
```
3. SELECT * FROM 사원;
4. 
```
SELECT DISTINCT 이름
FROM 자격증
WHERE 경력 >= 3;
```
5. 
```
SELECT (이름, 재직년도, 기본급)
FROM 사원
WHERE 이름 NOT IN (SELECT 이름 FROM 자격증);
```
 
6. 
```
SELECT 이름
FROM 자격증
GROUP BY(이름)
HAVING COUNT(*) >= 2;
```
7. 
```
CREATE VIEW 3학년학생
AS SELECT * FROM 학생 WHERE 학년 = 3;
```
8.
```
CREATE VIEW 강좌교수(강좌명, 강의실, 수강제한인원, 교수이름)
AS SELECT 강좌명, 강의실, 수강제한인원, 이름
FROM 강좌, 교수
WHERE 강좌.교수번호 = 교수.교수번호;
```
9. COMMIT / ROLLBACK / GRANT / REVOKE
10. GRANT SELECT ON 강좌 TO 홍길동;
11. GRANT ALL ON 강좌 TO 홍길동 WITH GRANT OPTION;
12. REVOKE INSERT ON 교수 FROM 박문수;
13. REVOKE SELECT ON 수강 FROM 박문수 CASCADE;
14.
```
// 1번
DELETE
FROM 상품
WHERE 제품코드 = "P-20";

INSERT INTO 상품
VALUES("P-20", "PLAYER", 8800, 6600);
```
15.
```
SELECT 상호, 총액
FROM 거래내역
WHERE 총액 IN (SELECT MAX(총액) FROM 거래내역);
```
16. 450 / 3 / 1
17. 송윤아 / 24 / 사원
18. 학번이 S로 시작하는 세 글자의 문자열 표시
19. 2 / 2 / 4
20. 장학내역 / 학과 / AVG(장학금)
21. 59 / 지원학과 ASC, 점수 DESC
22. ALTER TABLE / ADD
23. %신%
24. 15000
25. UPDATE / SET / WHERE
26. 4
27. 63