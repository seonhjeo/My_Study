# 19_1. 프로세스(Process)와 스레드(Thread)

## 프로세스와 스레드
 - 프로세스
   - 실행 파일이 실행되어 메모리에 적재된 인스턴스
   - word.exe가 실행 파일이면, 그 실행 파일에 담겨있는 데이터와 코드가 메모리에 적재되어 동작하는 것이 프로세스.
   - 프로세스는 반드시 하나 이상의 스레드로 구성됨
 - 스레드
   - 운영체제가 CPU시간을 할당하는 기본 단위
   - 프로세스가 밧줄이라면 스레드는 밧줄을 이루는 실과 같은 관계

## 멀티 스레드의 장단점
 - 장점
   - 사용자 대화형 프로그램에서 멀티 스레드를 이용하면 응답성을 높일 수 있다.
   - 멀티 프로세스 방식에 비해 멀티 스레드 방식이 자원 공유가 쉽다.
     - 프로세스끼리의 데이터 공유는 소켓이나 공유 메모리같은 IPC가 필요하지만 스레드끼리는 코드 내 변수를 공유하면 된다.
   - 경제성이 매우 좋다.
     - 프로세스를 띄우기 위해 메모리와 자원을 할당하는 작업은 비용이 비싸다.
     - 스레드를 띄울 때는 이미 프로세스에 할당된 메모리와 자원을 그대로 사용하므로 메모리와 자월을 할당하는 비용을 지불하지 않아도 된다.
 - 단점
   - 구현이 매우 까다롭다.
   - 멀티스레드 기반의 소프트웨어는 자식 스레드중 하나에 문제가 생기면 전체 프로세스에 영향을 준다.
   - 스레드를 너무 많이 사용하면 성능이 저하된다.
     - 스레드가 cpu를 사용하기 위해서는 작업 간 전환(Context Switching)을 해야 하는데, 이것이 적잖은 비용을 소모하게 된다.


# 19_1_1. 스레드 시작하기

 - .NET은 스레드를 제어하는 클래스로 `System.Threading.Thread`를 제공한다. 사용 방법은 다음과 같다.
   - Thread인스턴스를 생성한다. 이 때 생성자의 인수로 스레드가 실행할 메소드를 넘긴다.
   - Thread.Start()메소드를 호출해 스레드를 시작한다.
   - Thread.Join()메소드를 호출해 스레드가 끝날 때까지 기다린다.
```
// 스레드가 실행할 메소드
static void DoSomething()
{
    for (int i = 0; i < 5; i++)
    {
        Console.WriteLine("DoSomething : {0}", i);
    }
}

// 메인 함수
static void Main(string[] args)
{
    // Thread 인스턴스 생성
    Thread t1 = new Thread(new ThreadStart(DoSomething));
    // Thread 시작
    t1.Start();
    // Thread의 종료 대기
    t1.Join();
}
```
 - 위 코드에서 실제 스레드가 메모리에 적재되는 시점은 `t1.Start()`메소드를 호출했을 때이다. Thread 클래스 인스턴스는 준비만 해둘 뿐이다.
 - `t1.Start()` 메소드 호출시 CLR이 스레드를 실제로 생성하여 DoSomeThing()메소드를 호출하게 되고, `t1.Join()`메소드는 블록되어 있다가 DoSomething()메소드의 실행이 끝나면 반환되어 다음 코드를 실행할 수 있게 한다.
 - `Start()`메소드로 프로세스 밧줄에서 스레드라는 하나의 실이 빠져나왔다가 `Join()`메소드가 반환되는 시점에서 실이 밧줄로 다시 합류한다고 생각하면 이해하기 편하다.


# 19_1_2. 스레드 임의로 종료시키기
 - 살아있는 스레드를 죽이려면 해당 스레드 객체의 `Abort()`메소드를 호출해줘야 한다.
   - .NET 5 이상에서는 사용되지 않는 메서드이다.
```
// 스레드가 실행할 메소드
static void DoSomething()
{
    try
    {
        for (int i = 0; i < 10000; i++)
        {
            Console.WriteLine("DoSomething : {0}", i);
        }
    }
    catch (ThreaedAbortedException)
    {
        // ...
    }
    finally
    {
        // ...
    }
}

// 메인 함수
static void Main(string[] args)
{
    // Thread 인스턴스 생성
    Thread t1 = new Thread(new ThreadStart(DoSomething));
    // Thread 시작
    t1.Start();
    // 스레드 취소(종료)
    t1.Abort();
    // Thread의 종료 대기
    t1.Join();
}
```
 - `Abort()`메소드는 호출과 동시에 스레드를 즉시 종료하지 안흔ㄴ다.
   - Thread 객체에 `Abort()`메소드를 호출하면 CLR은 해당 스레드가 실행 중이던 코드에 `ThreadAbortExcpetion`을 덙진다.
   - 이 때 이 예외를 catch하는 코드가 있으면 이 예외를 처리한 다음 finally블록까지 실행한 후에 해당 스레드는 완전히 종료된다.
 - `Thread.Abort()`는 되도록 사용하지 않는 것이 좋다.
   - 한 스레드가 동기화를 위해 어떤 자원을 독점하고자 잠근 후 자원을 해제하지 못한 채 죽어버리면 그 자원에 접근하고자 하는 다른 쓰레드는 아무것도 할 수 없게 된다.


# 19_1_3. 스레드의 일생과 상태 변화

## 스레드의 상태 종류
 - Unstarted : 스레드 객체를 생성한 후 `Thread.Start()`메소드가 호출되기 전의 상태
 - Running : 스레드가 시작하여 동작 중인 상태
   - Unstarted 상태의 스레드를 `Thread.Start()`메소드를 통해 이 상태로 만들 수 있다.
 - Suspended : 스레드가 일시 중단 상태.
   - Running 중인 스레드를 `Thread.Suspend()` 메소드를 통해 Suspended상태로 만들고, Suspended중인 스레드를 `Thread.Resume()`메소드를 통해 다시 Running 상태로 만들 수 있다.
 - WaitSleepJoin : 스레드가 블록(Block)된 상태
   - Running 중인 스레드에 대해 `Monitor.Enter()`, `Thread.Sleep()`, `Thread.Join()`메소드를 호출하면 해당 상태로 변환
 - Aborted : 스레드가 취소된 상태
   - Running 중인 `Thread.Abort()`메소드를 호출 시 해당 상태로 변환되고, Aborted 상태의 스레드는 다시 Stopped 상태로 전화되어 완전히 중지됨
 - Stopped : 중지된 스레드의 상태
   - `Abort()`메소드를 호출하거나 스레드가 실행 중인 메소드가 종료되면 해당 상태로 변환
 - Background : 스레드가 백그라운드로 동작하고 있음을 나타냄
   - 포그라운드(Foreground) 스레드는 하나라도 살아있는 한 프로세스가 죽지 않지만 백그라운드는 스레드의 생존이 프로세스의 생존에 영향을 미치지 않고, 프로세스가 죽으면 백그라운드 스레드들이 다 죽게 된다.
   - `Thread.IsBackground`속성에 true값을 입력해 스레드를 이 상태로 바꿀 수 있다.

## 스레드의 상태 표현
 - 스레드는 동시에 여러 가지 상태일 수 있다.
 - 두 가시 이상의 상태를 동시에 표현하고자 `ThreadState`에 `Flags`애트리뷰트가 있다.
```
상태                10진수      2진수
Running             0          000000000
StopRequrestind     1          000000001
SuspendRequested    2          000000010
Background          4          000000100
Unstarted           8          000001000
Stopped             16         000010000
WaitSleepJoin       32         000100000
Suspended           64         001000000
AbortRequested      128        010000000
Aborted             256        100000000
```
 - `Flags`애트리뷰트에 비트연산자를 이용해 상태를 지정하거나 알아낼 수 있다.


# 19_1_4. 인터럽트: 스레드를 임의로 종료하는 다른 방법

## Thread.Interrupt()
 - `Thread.Interrupt()`메소드는 스레드가 Running 상태일 때를 피해서 WaitJoinSleep 상태에 들어갔을 때 ThreadInterruptedExcpetion 예외를 던져 스레드를 중지시킨다.
   - 스레드가 WaitJoinSleep 상태일 때에는 즉시 중단시키지만 해당 상태가 아니면 해당 상태가 될 때가지 기다린 후에 중지시킨다.
   - 따라서 프로그래머는 최소한 코드가 절대로 중단되면 안 되는 작업을 하고 있을때는 중단되지 않는다는 보장을 받을 수 있다.
 - `Thread.Interrupt()`메소드는 `Thread.Abort()`메소드와 사용 방법이 동일하다.


# 19_1_5. 스레드 간의 동기화

## 스레드 동기화의 필요성
 - 스레드는 여러 자원을 공유하는 경우가 많다.
 - 그러나 스레드는 이기적이기 떄문에 다른 스레드가 사용하는 자원을 제멋대로 사용해버리는 경우가 많다.
 - 스레드들이 순서를 갖춰 자원을 사용하게 하는 것을 일컬어 동기화라고 한다. 이를 제대로 하는 것이 멀티스레딩을 완벽하게 하는 길이다.
 - 스레드 동기화에서 가장 중요한 사명은 자원을 한 번에 하나의 스레드가 사용하도록 보장하는 것

## lock키워드로 동기화하기
```
class Counter
{
  public int count = 0;
  private readonly object thisLock = new object();

  public void Increase()
  {
    lock(thisLock)
    {
      count = count + 1;
    }
  }
}
```
 - lock 키워드와 중괄호로 둘러싼 부분이 크리티컬 섹션이 된다.
   - 한 스레드가 이 코드를 실행하다가 lock블록이 끝나는 괄호를 만나기 전까지 다른 스레드는 절대 이 코드를 실행할 수 없다.
 - lock이 만든 크리티컬 섹션으로 인해 다른 스레드들이 lock이 풀릴 때까지 대기 상태에 돌입할 수 있다.
   - 이러한 성능 저하를 방지하기 위해 크리티컬 섹션은 반드시 필요한 곳에만 사용하도록 해야 한다.
 - lock 키워드의 매개변수로 사용하는 객체는 참조형이면 어느 것이든 쓸 수 있지만 public키워드 등을 통해 외부 코드에서도 접근할 수 있는 다음 세 가지는 절대 사용하지 마라
   - this : 클래스 인스턴스는 클래스 내부뿐 아니라 외부에서도 자주 사용된다.
   - Type 형식 : typeof나 GetType()메소드는 코드 어느곳에서나 특정 형식에 대한 type객체를 얻기 위한 코드이다.
   - string 형식 : 리터럴 문자열은 인턴 풀에 등록되어 어디서든 접근할 수 있는 문자열이 된다.
     - 인턴 풀 : http://taeyo.net/columns/View.aspx?SEQ=253&PSEQ=23&IDX=5