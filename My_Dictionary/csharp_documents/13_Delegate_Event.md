# 대리자와 이벤트

### 대리자란?
 - 프로그래머는 함수를 호출할 때 콜(Call)을 하여 호출한다. 예를 들어, Run()`함수를 실행하기 위해 콜(Call)을 하여 함수를 실행해 달라고 요청하는 것.
 - 콜백(Callback)은 콜의 반대되는 개념이다. 사장과 비서의 관계와 같다고 할 수 있다.
   - 비서(Delegate)에게 사장(Function)이 돌아온다면, 내가(프로그래머) 말한 요청사항을 대신 전해달라고 하는 것
 - C#의 대리자는 다음과 같이 선언할 수 있다.
```
한정자 delegate 반환_형식 대리자_이름(매개변수_목록);
```
 - 대리자는 인스턴스가 아닌 형식으로, 메소드를 참조하는 무언가를 만들려면 대리자의 인스턴스르 따로 만들어야 한다.
 - 대리자의 사용 예시는 다음과 같다.
```
using System;

namespace Delegate
{
    // 반환 형식이 int, 매개변수가 (int, int)형인 대리자 선언
    delegate int MyDelegate(int a, int b);

    class Calculator
    {
        // 대리자는 인스턴스 메소드와 정적 메소드를 둘 다 참조할 수 있다.
        public int Plus (int a, int b) { return a + b; }
        public static int Minus(int a, int b) { return a - b; }
    }

    class MainApp
    {
        static void Main(string args[])
        {
            calculator Calc = new Calculator();
            MyDelegate Callback;

            Callback = new MyDelegate(Calc.Plus);
            Console.WriteLine(Callback(3, 4));

            Callback = new MyDelegate(Calculator.Minus);
            Console.WriteLine(Callback(7, 5));
        }
    }
}
```

### 대리자의 사용 이유와 상황
 - 프로그래밍을 하다 보면 '값'이 아닌 '코드'자체를 매개변수에 넘기고 싶을 때가 많다.
   - 예를 들어 정렬 수행 중 프로그램이 정렬할 때 사용하는 비교 루틴을 넣어 오름차순/내림차순 등을 유연하게 사용할 때
 - 대리자는 메소드에 대한 참조이므로 비교 메소드를 참조할 대리자를 매개변수에 받을 수 있도록 정렬 메소드를 작성해놓으면 된다.

### 일반화 대리자
 - 대리자는 일반화 메소드도 참조할 수 있다. 이 경우에는 대리자도 일반화 메소드를 참조할 수 있도록 형식 매개변수를 이용해 선언해야 한다.
 - 일반화 대리자는 다음과 같이 선언된다.
```
한정자 delegate 반환_형식 대리자_이름<형식_매개변수>(매개변수_목록);

delegate int Compare<T>(T a, T b);
```

### 일반화 대리자를 사용한 버블정렬 예시
```
using System;

namespace GenericDelegate
{
    delegate int Compare<T>(T a, T b);

    class mainApp
    {
        static int AscendCompare<T>(T a, T b) where T : ICompareable<T> { return a.CompareTo(b); }
        static int DescendCompare<T>(T a, T b) where T : ICompareable<T> { return a.CompareTo(b) * -1; }

        static void BubbleSort<T>(T[] DataSet, Compare<T> Comparer)
        {
            T temp;

            for (int i = 0; i < DataSet.Length - 1; i++)
            {
                for (int j = 0; j < DataSet.Length - ( i - 1 ); j++)
                {
                    if (Comparer(DataSet[j], DataSet[j + 1]) > 0)
                    {
                        temp = DataSet[j + 1];
                        DataSet[j + 1] = DataSet[j];
                        DataSet[j] = temp;;
                    }
                }
            }
        }

        static void Main(string args)
        {
            int[] array = {3, 7, 4, 2, 10};

            // 오름차순 정렬
            BubbleSort<int>(array, new Compare<int>(AscendCompare));
            // 내림차순 정렬
            BubbleSort<int>(array, new Compare<int>(DescendCompare));
        }
    }
}
```

### 대리자 체인
 - 대리자 하나는 여러 개의 메소드를 동시에 참조할 수 있다. 여러 개의 메소드가 하나의 대리자에 연결되어 있는 것을 대리자 체인이라고 한다.
 - 대리자 체인은 여러 개의 콜백을 체인에 연결된 순서에 따라 차례대로 호출한다.
   - `+=`연산자, `Delegate.Combine()`함수를 이용해 체인에 메서드를 추가할 수 있다.
   - `-=`연산자, `Delegate.Remobe()`함수를 이용해 체인에서 메서드를 제거할 수 있다.

### 익명 메소드
 - 익명 메소드는 이름이 없는 메소드를 말한다.
 - 선언한 대리자의 인스턴스르 만들고, 이 인스턴스가 메소드의 구현이 담겨 있는 코드를 익명 메소드라고 한다.
 - 익명 메소드는 자신을 참조할 대리자의 형식과 동일한 형식으로 선언되어야 한다.
 - 익명 메소드는 대리자가 참조할 메소드가 두 번 다시 사용되지 않을 것이라 판단될 때 유용하게 사용된다.
 - 익명 메소드의 사용 방법은 다음과 같다.
```
대리자_인스턴스 = delegate (매개변수_목록) {
    // 실행 코드..
}
```
 - 익명 메소드의 사용 예시는 다음과 같다.
```
delegate int Calculate(int a, int b);

public static void Main()
{
    Calculate Calc;

    // 익명 메소드 선언
    Calc = delegate(int a, int b) { return a + b; };

    // 익명 메소드 사용
    Console.WriteLine(Calc(3, 4));
}
```

### 이벤트
 - 이벤트는 객체에서 일어난 사건을 알리는 역할을 한다.
 - 이벤트는 대리자를 `event` 한정자로 수식해 만든다.
 - 이벤트의 선언 및 사용절차는 다음과 같다.
   1. 대리자를 선언한다. 이 대리자는 클래스 밖 혹은 안에 선언해도 된다.
   2. 클래스 내에 1번에서 선언한 대리자의 인스턴스를 `event` 한정자로 수식해 선언한다.
   3. 이벤트 핸들러를 작성한다. 이벤트 핸들러는 1에서 선언한 대리자와 일치하는 메소드면 된다.
   4. 클래스의 인스턴스를 생성하고 이 객체의 이벤트에 3에서 작성한 이벤트 핸들러를 등록한다.
   5. 이벤트가 발생하면 이벤트 핸들러가 호출된다.
 - 다음은 이벤트를 사용한 예시이다.
```
using System;

namespace EventTest
{
    // 대리자 선언
    delegate void EventHandler(string message);

    class MyNotifier
    {
        // event로 대리자 인스턴스 선언
        public event EventHandler SomethingHappened;
        public void DoSomeThing(int number)
        {
            int temp = number % 10;
            if (temp != 0 && temp% 3 == 0)
            {
                SomethingHappened(String.Format("{0} : 짝", number));
            }
        }
    }

    class MainApp
    {
        // 이벤트 핸들러 작성
        static public void MyHandler(string message)
        {
            Console.WriteLine(message);
        }

        static void Main(string[] args)
        {
            // 클래스 인스턴스 생성 후 이벤트 핸들러 등록
            MyNotifier notifier = mew MyNotifier();
            notifier.SomethingHappened += new EventHandler(MyHandler);

            // 이벤트 발생시 이벤트핸들러 호출
            for (int i = 1; i < 30; i++)
            {
                notifier.DoSomething(i);
            }
        }
    }
}
```

### 대리자와 이벤트
 - 이벤트는 대리자에 `event`키워드로 수식하여 선언한 것.
 - 이벤트가 대리자와 가장 크게 다른 점은 바로 이벤트를 외부에서 직접 사용할 수 없다는 것에 있다.
   - 이벤트는 public 한정자로 선언되어 있어도 자신이 선언된 클래스 외부에서는 호출이 불가능하다.
   - 대리자는 public이나 internal로 수식되어 있으면 클래스 외부에서라도 얼마든지 호출이 가능하다.
 - 따라서 대리자는 대리자대로 콜백 용도로 사용하고, 이벤트는 이벤트대로 객체의 상태 변화나 사건의 발생을 알리는 용도로 구분해서 사용해야 한다.