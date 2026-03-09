# Device of Hermes

## Impl for System

`Hermes`

```cs
Hermes.Say("Hey, Rien.");

Hermes.Say("The prescript cannot be defied", MessageLevel.Warn);

Hermes.Say("*beep*", MessageLevel.Error);
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
