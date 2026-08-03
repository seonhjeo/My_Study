# SOLID 원칙

SOLID 원칙은 객체지향 프로그래밍에서 유지보수성과 확장성이 높은 소프트웨어를 설계하기 위한 다섯 가지 원칙이다.

SOLID는 다음 다섯 가지 원칙의 앞 글자를 조합한 이름이다.

* **SRP**: 단일 책임 원칙
* **OCP**: 개방-폐쇄 원칙
* **LSP**: 리스코프 치환 원칙
* **ISP**: 인터페이스 분리 원칙
* **DIP**: 의존 역전 원칙

SOLID 원칙을 잘 적용하면 다음과 같은 장점을 얻을 수 있다.

* 코드의 변경 범위를 줄일 수 있다.
* 새로운 기능을 쉽게 추가할 수 있다.
* 클래스 간 결합도를 낮출 수 있다.
* 각 클래스의 책임을 명확하게 만들 수 있다.
* 테스트와 유지보수가 쉬워진다.

다만 모든 클래스와 모든 기능에 SOLID 원칙을 기계적으로 적용해야 하는 것은 아니다. 지나친 추상화와 클래스 분리는 오히려 코드의 복잡도를 높일 수 있으므로, 변경 가능성과 시스템의 규모를 고려하여 적절하게 적용해야 한다.

---

## 1. SRP(Single Responsibility Principle), 단일 책임 원칙

클래스는 하나의 책임만 가져야 한다.

여기서 말하는 책임은 단순히 클래스가 하나의 기능이나 메서드만 가져야 한다는 뜻이 아니다. 더 정확하게는 다음과 같이 설명할 수 있다.

> 클래스는 변경되어야 하는 이유를 하나만 가져야 한다.

하나의 클래스가 서로 다른 이유로 자주 변경된다면 그 클래스는 여러 책임을 동시에 가지고 있을 가능성이 높다.

예를 들어 게임의 `Player` 클래스가 다음과 같은 기능을 모두 담당한다고 가정해 보자.

* 플레이어 이동
* 입력 처리
* 체력 관리
* 공격 처리
* 애니메이션 재생
* 사운드 재생
* 데이터 저장
* 네트워크 동기화

이 경우 이동 방식이 변경되어도 `Player`를 수정해야 하고, 저장 방식이 변경되어도 `Player`를 수정해야 하며, 네트워크 구조가 변경되어도 같은 클래스를 수정해야 한다.

즉 `Player` 클래스에는 변경의 이유가 너무 많다.

### SRP의 자세한 의미

단일 책임 원칙에서 말하는 책임은 특정 기능 하나가 아니라, 특정 역할이나 변경의 원인을 의미한다.

예를 들어 게임 캐릭터의 체력과 관련된 기능은 다음과 같이 여러 메서드를 가질 수 있다.

```csharp
public class Health
{
    private int currentHealth;
    private int maxHealth;

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
    }

    public bool IsDead()
    {
        return currentHealth <= 0;
    }
}
```

이 클래스에는 여러 메서드가 존재하지만 모두 체력 관리라는 하나의 책임에 집중되어 있다.

따라서 메서드가 여러 개라고 해서 SRP를 위반하는 것은 아니다.

반면 다음 클래스는 서로 관련이 적은 기능을 함께 가지고 있다.

```csharp
public class Player
{
    public void Move()
    {
    }

    public void SaveData()
    {
    }

    public void SendNetworkPacket()
    {
    }

    public void PlayBackgroundMusic()
    {
    }
}
```

이 클래스는 이동, 저장, 네트워크, 사운드라는 여러 책임을 동시에 가지고 있으므로 SRP를 위반한다.

### SRP를 적용하지 않은 예시

```csharp
public class Player : MonoBehaviour
{
    private int health = 100;

    private void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        transform.Translate(Vector3.right * horizontal * Time.deltaTime);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        Debug.Log($"현재 체력: {health}");

        SavePlayerData();

        SendHealthToServer();

        PlayHitSound();
    }

    private void SavePlayerData()
    {
        // 플레이어 데이터 저장
    }

    private void SendHealthToServer()
    {
        // 네트워크 전송
    }

    private void PlayHitSound()
    {
        // 피격 사운드 재생
    }
}
```

`Player` 클래스가 다음 책임을 모두 가지고 있다.

* 입력 처리
* 이동
* 체력 관리
* 저장
* 네트워크
* 사운드

이 구조에서는 기능 하나를 변경할 때 다른 기능까지 영향을 받을 가능성이 높아진다.

### SRP를 적용한 예시

```csharp
public class PlayerMovement : MonoBehaviour
{
    public void Move(Vector2 direction)
    {
        transform.Translate(direction * Time.deltaTime);
    }
}
```

```csharp
public class PlayerHealth : MonoBehaviour
{
    private int health = 100;

    public void TakeDamage(int damage)
    {
        health -= damage;
    }
}
```

```csharp
public class PlayerSaveService
{
    public void Save(PlayerData playerData)
    {
        // 플레이어 데이터 저장
    }
}
```

```csharp
public class PlayerNetworkService
{
    public void SendHealth(int health)
    {
        // 체력 정보를 서버에 전송
    }
}
```

```csharp
public class PlayerSound : MonoBehaviour
{
    public void PlayHitSound()
    {
        // 피격 사운드 재생
    }
}
```

각 클래스가 하나의 책임만 가지도록 분리되었다.

```text
Player
 ├── PlayerMovement
 ├── PlayerHealth
 ├── PlayerSound
 ├── PlayerSaveService
 └── PlayerNetworkService
```

### SRP의 장점

* 클래스의 역할이 명확해진다.
* 수정 범위를 예측하기 쉬워진다.
* 클래스의 크기가 작아진다.
* 단위 테스트가 쉬워진다.
* 다른 시스템에서 재사용하기 쉬워진다.
* 여러 개발자가 서로 다른 기능을 동시에 작업하기 쉬워진다.

### SRP 원칙 적용 주의점

SRP를 지키기 위해 무조건 클래스를 잘게 나누는 것은 좋지 않다.

예를 들어 하나의 간단한 기능을 위해 지나치게 많은 클래스를 만들면 다음과 같은 문제가 발생할 수 있다.

* 클래스 수가 지나치게 많아진다.
* 기능의 전체 흐름을 이해하기 어려워진다.
* 객체 간 호출 관계가 복잡해진다.
* 작은 기능에도 과도한 구조가 필요해진다.

따라서 책임을 분리할 때는 다음을 고려해야 한다.

* 서로 다른 이유로 변경되는가?
* 서로 독립적으로 테스트할 필요가 있는가?
* 다른 시스템에서도 재사용될 가능성이 있는가?
* 하나의 클래스가 여러 외부 시스템에 의존하고 있는가?

같은 이유로 함께 변경되는 기능까지 억지로 분리할 필요는 없다.

---

## 2. OCP(Open-Closed Principle), 개방-폐쇄 원칙

클래스는 확장에 대해서는 열려 있어야 하지만 코드 변경에 대해서는 닫혀 있어야 한다.

클래스에 기능을 추가해야 할 때 기존 코드를 직접 수정하지 않은 채 확장을 통해 새로운 행동을 추가할 수 있어야 한다.

OCP를 준수하면 매우 유연하면서도 급변하는 주변 환경에 잘 적응할 수 있다. 그로 인해 변경에 강하고 튼튼한 설계를 만들 수 있다.

그렇다고 모든 설계에서 OCP를 반드시 준수해야 하는 것은 아니다. 모든 변경 가능성을 미리 예측해 추상화하려면 많은 시간과 비용이 필요하기 때문이다. 또한 굳이 확장 구조가 필요하지 않은 단순한 코드도 많다.

그러므로 설계한 것들 중에서 **변경될 가능성이 높은 부분**을 중점적으로 살펴보고 OCP를 적용하는 것이 합리적이다.

### OCP의 자세한 의미

OCP는 어렵게 생각할 필요 없이 추상화를 활용하는 원칙으로 이해할 수 있다.

OCP는 다형성과 확장을 가능하게 하는 객체지향의 장점을 극대화하는 설계 원칙이다. 객체를 추상화하면 새로운 구현을 추가하면서도 기존 코드를 크게 변경하지 않는 유연한 구조를 만들 수 있다.

새로운 클래스를 추가해야 할 때 기존 코드를 수정하는 대신, 적절한 인터페이스나 추상 클래스의 구현체를 추가하여 기능을 확장할 수 있다.

#### 확장에 열려 있다

* 모듈의 확장성을 보장하는 것을 의미한다.
* 새로운 요구사항이 발생했을 때 새로운 클래스를 추가하여 기능을 확장할 수 있어야 한다.
* 기존 기능에 영향을 최소화하면서 새로운 행동을 추가할 수 있어야 한다.

#### 변경에 닫혀 있다

* 이미 정상적으로 동작하는 코드를 직접 수정하는 일을 최소화해야 한다는 뜻이다.
* 새로운 요구사항이 생길 때마다 기존 조건문이나 핵심 로직을 계속 수정한다면 변경에 닫혀 있지 않은 구조이다.
* 기존 코드의 잦은 수정은 예상하지 못한 오류와 회귀 버그를 발생시킬 수 있다.
* 따라서 기존 코드를 변경하지 않고 새로운 구현을 추가할 수 있도록 설계해야 한다.

### OCP를 적용하지 않은 예시

게임에 여러 종류의 무기가 있다고 가정해 보자.

```csharp
public enum WeaponType
{
    Sword,
    Bow,
    Gun
}
```

```csharp
public class PlayerAttack
{
    public void Attack(WeaponType weaponType)
    {
        switch (weaponType)
        {
            case WeaponType.Sword:
                Debug.Log("검으로 공격");
                break;

            case WeaponType.Bow:
                Debug.Log("활로 공격");
                break;

            case WeaponType.Gun:
                Debug.Log("총으로 공격");
                break;
        }
    }
}
```

새로운 무기인 `MagicStaff`를 추가하려면 다음 부분을 모두 수정해야 한다.

* `WeaponType` 열거형
* `Attack()` 메서드의 조건문
* 무기별 부가 처리 코드

무기가 추가될 때마다 기존 코드가 변경되므로 OCP를 위반한다.

### OCP를 적용한 예시

```csharp
public interface IWeapon
{
    void Attack();
}
```

```csharp
public class Sword : IWeapon
{
    public void Attack()
    {
        Debug.Log("검으로 공격");
    }
}
```

```csharp
public class Bow : IWeapon
{
    public void Attack()
    {
        Debug.Log("활로 공격");
    }
}
```

```csharp
public class Gun : IWeapon
{
    public void Attack()
    {
        Debug.Log("총으로 공격");
    }
}
```

```csharp
public class PlayerAttack
{
    private IWeapon weapon;

    public PlayerAttack(IWeapon weapon)
    {
        this.weapon = weapon;
    }

    public void Attack()
    {
        weapon.Attack();
    }
}
```

새로운 마법 지팡이를 추가하더라도 기존 클래스는 수정하지 않는다.

```csharp
public class MagicStaff : IWeapon
{
    public void Attack()
    {
        Debug.Log("마법 공격");
    }
}
```

새로운 구현체만 추가하면 되므로 확장에는 열려 있고 기존 코드의 변경에는 닫혀 있다.

### OCP 원칙 적용 주의점

확장에는 열려 있고 변경에는 닫히게 만들기 위해서는 추상화를 적절하게 설계해야 한다.

추상 클래스나 인터페이스를 정의할 때는 다음을 고려해야 한다.

* 여러 구현체가 공통으로 가져야 하는 본질적인 기능은 무엇인가?
* 구현체마다 달라지는 부분은 무엇인가?
* 실제로 변경될 가능성이 높은 지점은 어디인가?
* 인터페이스가 특정 구현에 종속되어 있지는 않은가?

그래디 부치(Grady Booch)는 추상화를 다음과 같이 설명한다.

> 다른 모든 종류의 객체로부터 식별될 수 있는 객체의 본질적인 특징

즉 추상화는 단순히 구체적이지 않게 만드는 것이 아니라, 여러 구현체가 공유하는 본질적인 특징을 정의하는 것이다.

추상화 수준을 잘못 선택하면 다음과 같은 문제가 발생할 수 있다.

* 자식 클래스가 부모 클래스의 규칙을 지키지 못한다.
* 불필요한 메서드를 구현해야 한다.
* 특정 구현체에만 필요한 기능이 공통 인터페이스에 들어간다.
* LSP와 ISP를 함께 위반하게 된다.

또한 아직 변경 가능성이 확인되지 않은 부분까지 미리 추상화하면 과도한 설계가 될 수 있다.

따라서 OCP는 모든 부분에 적용하기보다 변경 가능성이 높거나 구현체가 여러 개 존재하는 부분에 우선적으로 적용하는 것이 좋다.

---

## 3. LSP(Liskov Substitution Principle), 리스코프 치환 원칙

자식 클래스는 부모 클래스를 대체하여 사용하더라도 프로그램의 동작에 문제가 없어야 한다.

즉 다음과 같이 표현할 수 있다.

> 상위 타입의 객체가 사용되는 모든 위치에서 하위 타입의 객체로 바꾸어도 프로그램의 올바른 동작이 유지되어야 한다.

상속 관계를 만들었다면 자식 클래스는 부모 클래스가 약속한 동작과 규칙을 지켜야 한다.

단순히 문법적으로 상속이 가능하다고 해서 올바른 상속 관계인 것은 아니다.

### LSP의 자세한 의미

LSP는 상속과 다형성을 안전하게 사용하기 위한 원칙이다.

다음과 같은 구조가 있다고 가정해 보자.

```text
Character
   ▲
   │
Player
```

`Player`가 `Character`를 상속했다면, `Character`를 사용하는 코드에 `Player`를 전달해도 정상적으로 동작해야 한다.

```csharp
public void HealCharacter(Character character)
{
    character.Heal(10);
}
```

다음 코드가 모두 정상적으로 동작해야 한다.

```csharp
HealCharacter(new Player());
HealCharacter(new Enemy());
HealCharacter(new NPC());
```

만약 특정 자식 클래스가 부모의 기능을 사용할 수 없거나, 부모와 전혀 다른 동작을 한다면 잘못된 상속 관계일 가능성이 높다.

### LSP를 위반한 예시

모든 캐릭터가 점프할 수 있다고 가정한 부모 클래스가 있다.

```csharp
public abstract class Character
{
    public abstract void Jump();
}
```

플레이어는 정상적으로 점프할 수 있다.

```csharp
public class Player : Character
{
    public override void Jump()
    {
        Debug.Log("플레이어가 점프한다.");
    }
}
```

하지만 바닥에 고정된 포탑은 점프할 수 없다.

```csharp
public class Turret : Character
{
    public override void Jump()
    {
        throw new NotSupportedException("포탑은 점프할 수 없습니다.");
    }
}
```

다음 코드는 `Character`라면 모두 점프할 수 있다고 기대한다.

```csharp
public void MakeCharacterJump(Character character)
{
    character.Jump();
}
```

하지만 `Turret`을 전달하면 예외가 발생한다.

```csharp
MakeCharacterJump(new Turret());
```

`Turret`은 `Character`를 완전히 대체할 수 없으므로 LSP를 위반한다.

### LSP를 적용한 예시

점프 기능을 별도의 인터페이스로 분리한다.

```csharp
public abstract class Character
{
    public abstract void Attack();
}
```

```csharp
public interface IJumpable
{
    void Jump();
}
```

```csharp
public class Player : Character, IJumpable
{
    public override void Attack()
    {
        Debug.Log("플레이어 공격");
    }

    public void Jump()
    {
        Debug.Log("플레이어가 점프한다.");
    }
}
```

```csharp
public class Turret : Character
{
    public override void Attack()
    {
        Debug.Log("포탑이 공격한다.");
    }
}
```

이제 점프할 수 있는 객체만 `IJumpable`을 구현한다.

```csharp
public void MakeObjectJump(IJumpable jumpable)
{
    jumpable.Jump();
}
```

`Turret`은 점프 기능을 약속하지 않으므로 잘못된 호출 자체가 발생하지 않는다.

### 계약 관점에서의 LSP

LSP는 부모 클래스가 제공하는 계약을 자식 클래스가 지켜야 한다는 의미로도 볼 수 있다.

계약은 다음 세 가지 관점으로 설명할 수 있다.

#### 사전 조건을 더 강하게 만들지 않아야 한다

부모 클래스가 어떤 범위의 입력을 허용한다면 자식 클래스는 그보다 더 제한적인 입력만 허용해서는 안 된다.

```csharp
public class Weapon
{
    public virtual void SetDamage(int damage)
    {
        if (damage < 0)
            throw new ArgumentOutOfRangeException(nameof(damage));
    }
}
```

잘못된 자식 클래스

```csharp
public class Sword : Weapon
{
    public override void SetDamage(int damage)
    {
        if (damage < 10)
            throw new ArgumentOutOfRangeException(nameof(damage));
    }
}
```

부모는 `0` 이상의 값을 허용하지만 자식은 `10` 이상의 값만 허용한다. 자식이 부모보다 더 강한 사전 조건을 요구하므로 대체 가능성이 깨진다.

#### 사후 조건을 더 약하게 만들지 않아야 한다

부모 클래스가 보장하는 결과를 자식 클래스도 최소한 동일하게 보장해야 한다.

부모가 공격 후 반드시 데미지를 적용한다고 약속했다면 자식 클래스가 아무 이유 없이 공격을 무시해서는 안 된다.

#### 부모 클래스의 불변식을 유지해야 한다

부모 클래스가 항상 유지해야 하는 상태 규칙을 자식 클래스가 깨뜨려서는 안 된다.

예를 들어 `Health` 값이 항상 `0` 이상이어야 한다면 자식 클래스도 이 규칙을 지켜야 한다.

### LSP의 장점

* 안전한 다형성을 사용할 수 있다.
* 부모 타입을 사용하는 코드가 예측 가능해진다.
* 잘못된 상속 구조를 방지할 수 있다.
* 런타임 예외 발생 가능성을 줄일 수 있다.
* OCP를 안정적으로 적용할 수 있다.

### LSP 원칙 적용 주의점

두 객체가 비슷한 속성을 가지고 있다고 해서 반드시 상속 관계로 만들어야 하는 것은 아니다.

다음 질문을 통해 올바른 상속인지 판단할 수 있다.

* 자식 객체를 부모 객체 대신 사용해도 되는가?
* 부모의 모든 공개 기능을 자식도 자연스럽게 지원하는가?
* 자식이 부모 메서드에서 예외를 던지거나 아무 동작도 하지 않는가?
* 자식이 부모의 동작 의미를 완전히 바꾸고 있지는 않은가?
* 단순한 코드 재사용을 위해 상속하고 있지는 않은가?

상속 관계가 부자연스럽다면 인터페이스, 구성 또는 위임을 사용하는 것이 더 적절할 수 있다.

---

## 4. ISP(Interface Segregation Principle), 인터페이스 분리 원칙

클라이언트는 자신이 사용하지 않는 메서드에 의존하도록 강요받아서는 안 된다.

즉 하나의 거대한 인터페이스를 제공하기보다, 사용 목적에 따라 작고 구체적인 인터페이스로 분리해야 한다.

여기서 클라이언트는 인터페이스를 구현하는 클래스나 해당 인터페이스를 사용하는 객체를 의미한다.

### ISP의 자세한 의미

인터페이스가 너무 많은 기능을 포함하면 구현 클래스는 자신에게 필요하지 않은 메서드까지 구현해야 한다.

이러한 인터페이스를 흔히 비대한 인터페이스 또는 범용 인터페이스라고 부른다.

예를 들어 모든 게임 유닛의 기능을 하나의 인터페이스에 정의했다고 가정해 보자.

```csharp
public interface IGameUnit
{
    void Move();
    void Jump();
    void Fly();
    void Swim();
    void Attack();
    void CastMagic();
}
```

플레이어는 대부분의 기능을 사용할 수 있을지 모르지만, 포탑이나 물고기 같은 객체에는 필요하지 않은 기능이 많다.

```csharp
public class Turret : IGameUnit
{
    public void Move()
    {
        throw new NotSupportedException();
    }

    public void Jump()
    {
        throw new NotSupportedException();
    }

    public void Fly()
    {
        throw new NotSupportedException();
    }

    public void Swim()
    {
        throw new NotSupportedException();
    }

    public void Attack()
    {
        Debug.Log("포탑 공격");
    }

    public void CastMagic()
    {
        throw new NotSupportedException();
    }
}
```

사용하지 않는 메서드를 억지로 구현하고 있으므로 ISP를 위반한다.

### ISP를 적용한 예시

기능별로 인터페이스를 분리한다.

```csharp
public interface IMovable
{
    void Move();
}
```

```csharp
public interface IJumpable
{
    void Jump();
}
```

```csharp
public interface IFlyable
{
    void Fly();
}
```

```csharp
public interface ISwimmable
{
    void Swim();
}
```

```csharp
public interface IAttackable
{
    void Attack();
}
```

```csharp
public interface IMagicCaster
{
    void CastMagic();
}
```

플레이어는 필요한 기능만 구현한다.

```csharp
public class Player :
    IMovable,
    IJumpable,
    IAttackable
{
    public void Move()
    {
        Debug.Log("플레이어 이동");
    }

    public void Jump()
    {
        Debug.Log("플레이어 점프");
    }

    public void Attack()
    {
        Debug.Log("플레이어 공격");
    }
}
```

포탑은 공격 기능만 구현한다.

```csharp
public class Turret : IAttackable
{
    public void Attack()
    {
        Debug.Log("포탑 공격");
    }
}
```

물고기는 이동과 수영 기능만 구현할 수 있다.

```csharp
public class Fish : IMovable, ISwimmable
{
    public void Move()
    {
        Debug.Log("물고기 이동");
    }

    public void Swim()
    {
        Debug.Log("물고기가 헤엄친다.");
    }
}
```

각 클래스가 필요한 인터페이스만 구현하므로 불필요한 의존성이 사라진다.

### 게임 시스템에서의 ISP 예시

데미지를 받을 수 있는 객체와 회복할 수 있는 객체를 하나의 인터페이스로 묶었다고 가정해 보자.

```csharp
public interface IHealthObject
{
    void TakeDamage(int damage);
    void Heal(int amount);
}
```

파괴 가능한 상자는 데미지를 받을 수 있지만 회복은 필요하지 않을 수 있다.

```csharp
public class BreakableBox : IHealthObject
{
    public void TakeDamage(int damage)
    {
        Debug.Log("상자가 피해를 입었다.");
    }

    public void Heal(int amount)
    {
        throw new NotSupportedException();
    }
}
```

이를 다음처럼 분리할 수 있다.

```csharp
public interface IDamageable
{
    void TakeDamage(int damage);
}
```

```csharp
public interface IHealable
{
    void Heal(int amount);
}
```

```csharp
public class PlayerHealth : IDamageable, IHealable
{
    public void TakeDamage(int damage)
    {
    }

    public void Heal(int amount)
    {
    }
}
```

```csharp
public class BreakableBox : IDamageable
{
    public void TakeDamage(int damage)
    {
    }
}
```

### ISP의 장점

* 불필요한 메서드 구현을 방지할 수 있다.
* 클래스의 역할이 명확해진다.
* 인터페이스 변경의 영향을 줄일 수 있다.
* 작은 단위로 기능을 조합할 수 있다.
* 테스트용 객체나 Mock 구현을 만들기 쉬워진다.
* LSP 위반 가능성을 줄일 수 있다.

### ISP 원칙 적용 주의점

인터페이스는 작을수록 무조건 좋은 것은 아니다.

메서드 하나마다 인터페이스를 만들면 다음과 같은 문제가 생길 수 있다.

* 인터페이스 수가 지나치게 많아진다.
* 객체의 역할을 파악하기 어려워진다.
* 비슷한 인터페이스가 중복될 수 있다.
* 코드 탐색과 관리가 복잡해질 수 있다.

따라서 인터페이스를 분리할 때는 사용하는 클라이언트의 관점에서 판단해야 한다.

다음 조건을 중심으로 인터페이스 분리를 고려하는 것이 좋다.

* 일부 구현체가 특정 메서드를 사용하지 않는가?
* 특정 메서드에서 `NotSupportedException`을 던지고 있는가?
* 인터페이스 변경 시 관련 없는 클래스까지 수정되는가?
* 기능들이 서로 다른 이유로 변경되는가?
* 각 기능을 독립적으로 조합할 필요가 있는가?

ISP는 단순히 인터페이스를 작게 만드는 원칙이 아니라, 클라이언트가 필요로 하는 기능만 의존하게 만드는 원칙이다.

---

## 5. DIP(Dependency Inversion Principle), 의존 역전 원칙

상위 수준 모듈은 하위 수준 모듈에 의존해서는 안 된다. 둘 모두 추상화에 의존해야 한다.

또한 추상화는 세부 사항에 의존해서는 안 되며, 세부 사항이 추상화에 의존해야 한다.

이를 다음과 같이 정리할 수 있다.

> 구체적인 구현에 의존하지 말고 인터페이스나 추상 클래스에 의존해야 한다.

### DIP의 자세한 의미

일반적인 절차적 구조에서는 상위 수준의 비즈니스 로직이 하위 수준의 구체적인 기능을 직접 사용한다.

예를 들어 `Player`가 `Sword`를 직접 생성한다고 가정해 보자.

```csharp
public class Sword
{
    public void Attack()
    {
        Debug.Log("검 공격");
    }
}
```

```csharp
public class Player
{
    private readonly Sword sword = new Sword();

    public void Attack()
    {
        sword.Attack();
    }
}
```

이 구조의 의존 방향은 다음과 같다.

```text
Player
   │
   ▼
Sword
```

`Player`는 게임의 핵심 로직을 담당하는 상위 수준 모듈이고, `Sword`는 구체적인 공격 방식을 담당하는 하위 수준 모듈이다.

하지만 상위 모듈이 구체 구현인 `Sword`에 직접 의존하기 때문에 무기를 변경하려면 `Player` 코드도 수정해야 한다.

### DIP를 적용한 예시

무기의 추상화를 정의한다.

```csharp
public interface IWeapon
{
    void Attack();
}
```

구체적인 무기는 인터페이스를 구현한다.

```csharp
public class Sword : IWeapon
{
    public void Attack()
    {
        Debug.Log("검 공격");
    }
}
```

```csharp
public class Bow : IWeapon
{
    public void Attack()
    {
        Debug.Log("활 공격");
    }
}
```

`Player`는 구체 클래스가 아니라 `IWeapon`에 의존한다.

```csharp
public class Player
{
    private readonly IWeapon weapon;

    public Player(IWeapon weapon)
    {
        this.weapon = weapon;
    }

    public void Attack()
    {
        weapon.Attack();
    }
}
```

의존 관계는 다음과 같이 변경된다.

```text
Player ─────▶ IWeapon
                ▲
                │
           Sword, Bow
```

상위 모듈인 `Player`와 하위 모듈인 `Sword`, `Bow`가 모두 추상화인 `IWeapon`에 의존한다.

이것을 의존성이 역전되었다고 표현한다.

기존 구조에서는 상위 모듈이 하위 구현을 직접 가리켰다.

```text
Player → Sword
```

DIP 적용 후에는 구체 구현이 상위 모듈이 요구하는 추상화를 구현한다.

```text
Player → IWeapon ← Sword
```

### 의존성 주입과 DIP의 차이

DIP와 의존성 주입(Dependency Injection, DI)은 함께 사용되는 경우가 많지만 같은 개념은 아니다.

#### DIP

* 객체가 구체 구현이 아니라 추상화에 의존하도록 만드는 설계 원칙이다.
* 의존 관계를 어떤 방향으로 설계할지를 다룬다.

#### DI

* 객체가 필요한 의존성을 외부에서 전달받는 구현 기법이다.
* 의존 객체를 어떻게 제공할지를 다룬다.

다음 코드는 DIP와 생성자 주입을 함께 사용한다.

```csharp
public class Player
{
    private readonly IWeapon weapon;

    public Player(IWeapon weapon)
    {
        this.weapon = weapon;
    }
}
```

`Player`가 `IWeapon`에 의존하는 것은 DIP이고, 생성자를 통해 외부에서 `IWeapon`을 받는 것은 DI이다.

추상화를 사용하지 않고 구체 클래스를 주입하는 경우도 DI라고 부를 수 있다.

```csharp
public class Player
{
    private readonly Sword sword;

    public Player(Sword sword)
    {
        this.sword = sword;
    }
}
```

이 코드는 객체를 외부에서 전달받으므로 DI는 사용했지만, 여전히 `Sword`라는 구체 클래스에 의존하므로 DIP를 충분히 적용했다고 보기 어렵다.

### 게임 시스템에서의 DIP 예시

플레이어 사망 시 데이터를 저장하는 시스템을 생각해 보자.

DIP를 적용하지 않은 구조

```csharp
public class JsonSaveSystem
{
    public void Save()
    {
        Debug.Log("JSON 파일로 저장");
    }
}
```

```csharp
public class PlayerDeathHandler
{
    private readonly JsonSaveSystem saveSystem = new JsonSaveSystem();

    public void HandleDeath()
    {
        saveSystem.Save();
    }
}
```

저장 방식을 클라우드나 데이터베이스로 변경하려면 `PlayerDeathHandler`를 수정해야 한다.

추상화를 적용한다.

```csharp
public interface ISaveService
{
    void Save();
}
```

```csharp
public class JsonSaveService : ISaveService
{
    public void Save()
    {
        Debug.Log("JSON 파일로 저장");
    }
}
```

```csharp
public class CloudSaveService : ISaveService
{
    public void Save()
    {
        Debug.Log("클라우드에 저장");
    }
}
```

```csharp
public class PlayerDeathHandler
{
    private readonly ISaveService saveService;

    public PlayerDeathHandler(ISaveService saveService)
    {
        this.saveService = saveService;
    }

    public void HandleDeath()
    {
        saveService.Save();
    }
}
```

저장 방식이 변경되어도 `PlayerDeathHandler`는 수정할 필요가 없다.

```csharp
var localHandler =
    new PlayerDeathHandler(new JsonSaveService());

var cloudHandler =
    new PlayerDeathHandler(new CloudSaveService());
```

### Unity에서의 DIP

Unity에서는 인스펙터 참조, 팩토리, 서비스 로케이터 또는 DI 프레임워크를 통해 의존성을 연결할 수 있다.

간단한 수동 주입 예시

```csharp
public interface IAttackStrategy
{
    void Attack();
}
```

```csharp
public class MeleeAttackStrategy : IAttackStrategy
{
    public void Attack()
    {
        Debug.Log("근접 공격");
    }
}
```

```csharp
public class PlayerAttack : MonoBehaviour
{
    private IAttackStrategy attackStrategy;

    public void Initialize(IAttackStrategy attackStrategy)
    {
        this.attackStrategy = attackStrategy;
    }

    public void Attack()
    {
        attackStrategy.Attack();
    }
}
```

```csharp
public class PlayerInstaller : MonoBehaviour
{
    [SerializeField]
    private PlayerAttack playerAttack;

    private void Awake()
    {
        IAttackStrategy strategy = new MeleeAttackStrategy();
        playerAttack.Initialize(strategy);
    }
}
```

Unity에서는 VContainer나 Zenject 같은 DI 컨테이너를 이용해 객체 생성을 자동화할 수도 있다.

다만 DI 컨테이너를 사용한다고 해서 자동으로 DIP를 준수하는 것은 아니다. 등록 구조와 추상화가 잘못되어 있다면 여전히 구체 구현에 강하게 결합될 수 있다.

### DIP의 장점

* 구체 구현을 쉽게 교체할 수 있다.
* 상위 수준의 핵심 로직이 외부 기술에 덜 영향을 받는다.
* 테스트용 객체를 주입하기 쉽다.
* 코드의 결합도가 낮아진다.
* OCP를 적용하기 쉬워진다.
* 저장소, 네트워크, 데이터베이스 등의 변경에 유연하게 대응할 수 있다.

### DIP 원칙 적용 주의점

모든 클래스에 인터페이스를 만드는 것은 바람직하지 않을 수 있다.

구현체가 하나뿐이고 변경 가능성이 낮은 단순한 클래스까지 무조건 추상화하면 다음과 같은 문제가 발생한다.

* 코드 파일 수가 증가한다.
* 실제 구현을 찾기 어려워진다.
* 의존 관계를 파악하기 어려워진다.
* 간단한 기능에 과도한 설계가 적용된다.

DIP는 다음과 같은 경계에 적용할 때 특히 효과적이다.

* 데이터베이스
* 파일 저장
* 네트워크 통신
* 결제 시스템
* 입력 시스템
* 게임 플랫폼별 기능
* 무기 및 스킬 전략
* AI 행동 전략
* 외부 SDK
* 테스트에서 대체해야 하는 기능

즉 변동 가능성이 크거나 외부 환경에 영향을 많이 받는 부분을 추상화하는 것이 좋다.

---

## SOLID 원칙 정리

| 원칙      | 이름          | 핵심 내용                              | 게임 시스템 예시                              |
| ------- | ----------- | ---------------------------------- | -------------------------------------- |
| **SRP** | 단일 책임 원칙    | 클래스는 하나의 변경 이유만 가져야 한다.            | 이동, 체력, 공격, 저장 기능을 각각 분리한다.            |
| **OCP** | 개방-폐쇄 원칙    | 확장에는 열려 있고 기존 코드 변경에는 닫혀 있어야 한다.   | `IWeapon` 구현체를 추가하여 새로운 무기를 확장한다.      |
| **LSP** | 리스코프 치환 원칙  | 자식 클래스는 부모 클래스를 안전하게 대체할 수 있어야 한다. | 점프할 수 없는 포탑을 `IJumpable`로 강제하지 않는다.    |
| **ISP** | 인터페이스 분리 원칙 | 사용하지 않는 메서드에 의존하도록 강요해서는 안 된다.     | 이동, 점프, 공격 인터페이스를 각각 분리한다.             |
| **DIP** | 의존 역전 원칙    | 구체 구현이 아닌 추상화에 의존해야 한다.            | `Player`가 `Sword`가 아닌 `IWeapon`에 의존한다. |

---

## SOLID 원칙 간의 관계

SOLID 원칙은 각각 독립적인 규칙처럼 보이지만 서로 밀접하게 연결되어 있다.

```text
SRP
 └── 책임을 분리하여 변경 범위를 줄인다.

OCP
 └── 새로운 기능을 기존 코드 수정 없이 확장한다.

LSP
 └── 확장된 하위 타입이 기존 타입을 안전하게 대체하도록 한다.

ISP
 └── 각 객체가 필요한 기능만 의존하도록 인터페이스를 분리한다.

DIP
 └── 구체 구현 대신 추상화에 의존하도록 방향을 설계한다.
```

예를 들어 무기 시스템에서 `IWeapon`을 사용하면 다음 원칙들이 함께 적용될 수 있다.

* `Player`와 무기 기능을 분리하므로 SRP에 도움이 된다.
* 새로운 무기를 구현체 추가로 확장하므로 OCP를 만족한다.
* 모든 무기가 `IWeapon`의 계약을 지키면 LSP를 만족한다.
* 무기 인터페이스에 불필요한 기능이 없다면 ISP를 만족한다.
* `Player`가 구체적인 무기가 아닌 `IWeapon`에 의존하므로 DIP를 만족한다.

---

## SOLID 원칙 적용 시 주의사항

SOLID 원칙은 반드시 지켜야 하는 절대적인 규칙이 아니라, 변경에 강한 코드를 설계하기 위한 판단 기준이다.

원칙을 지나치게 적용하면 다음과 같은 문제가 생길 수 있다.

* 추상 클래스와 인터페이스가 과도하게 많아진다.
* 간단한 기능의 구조가 불필요하게 복잡해진다.
* 실제 기능보다 설계 구조를 이해하는 데 더 많은 시간이 필요해진다.
* 아직 발생하지 않은 요구사항을 위해 과도한 확장을 준비하게 된다.

따라서 다음과 같은 기준으로 적용하는 것이 좋다.

* 변경 가능성이 높은 부분인가?
* 구현체가 여러 개 존재하는가?
* 외부 시스템과 연결되는 부분인가?
* 테스트에서 대체할 필요가 있는가?
* 클래스가 여러 이유로 자주 변경되는가?
* 조건문이 새로운 기능이 추가될 때마다 계속 늘어나는가?
* 상속받은 메서드를 사용할 수 없어 예외를 던지고 있는가?

SOLID의 목적은 인터페이스나 클래스를 많이 만드는 것이 아니라, 코드의 변경 비용을 낮추고 시스템을 이해하기 쉽게 만드는 것이다.
