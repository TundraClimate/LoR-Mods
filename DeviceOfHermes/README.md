# Device of Hermes

## Impl for System

`Hermes.Say(message, lvl = MessageLevel.Info)`:

```cs
Hermes.Say("Hey, Rien.");

Hermes.Say("The prescript cannot be defied", MessageLevel.Warn);

Hermes.Say("*beep*", MessageLevel.Error);
```

`Hermes.Store(data)`, `Hermes.Load()`:

```
Hermes.Store("Hi, Hermes!");

// "Hi, Hermes!"
Console.WriteLine(Hermes.Load<string>());

Hermes.Store<int>(12);

// "12"
Console.WriteLine(Hermes.Load<int>());
```

## Impl for System.Collction.Generic

`Peekable`

```cs
var peekable = new Peekable<string>(new List<string>() { "Foo", "Bar" });
string? elem = null;

peekable.Peek(out elem);

// "Foo"
Console.Write(elem);

peekable.MoveNext(out elem);

// "Foo"
Console.Write(elem);

peekable.MoveNext(out elem);

// "Bar"
Console.Write(elem);

peekable.MoveNext(out elem);

// null
Console.Write(elem);
```

## Impl for BattleUnitModel

`TryGetBuf<T>(this BattleUnitModel model, [NotNullWhen(true)] out T? buf)`:

```cs
using BattleBuf;

if (base.owner.TryGetBuf<BattleUnitBuf_burn>(out BattleUnitBuf_burn burn))
{
    // burn is Non-null
}

// burn is Nullable
```

`GetAndInitIfNull<T>(this BattleUnitModel model, Func<T> bufMake)`:

```cs
using BattleBuf;

// Throws the NullReferenceException if base.owner is null
BattleUnitBuf_burn buf = base.owner.GetAndInitIfNull<BattleUnitBuf_burn>(() => new BattleUnitBuf_burn());
```
