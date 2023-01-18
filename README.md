# Geometry

Objects and helpers to provide easy calculations of objects in 2D-space and time. 
These objects can be (de)serialized to/from JSON.
This library extensively uses the new generic math functionality of .NET 7 so each object can contain the numeric types you prefer.

> Check out [CodeChops projects](https://www.CodeChops.nl/projects) for more projects.

# Space

## Point
A 2-dimensional location value object with TNumber as type of the underlying values of `X` and `Y`. Contains:
- Operators to perform calculations.
- Default casts (also from/to `ValueTuple`).
- Converters to convert to a different `TNumber`.
- Some simple predefined calculations: `Sum()`, `Multiply()`.

## Line
A line holds two points: a starting point and end point. Contains:
- Casts to and from `Size`.
- Converters to convert to a different `TNumber`.
- Some simple predefined calculations: `Distance()`, `Difference()`.
- An iterator to iterate all points in the line `foreach(var point in new Line<int>((0,0), (10, 10))`

## Size
A 2-dimensional measurement value object with TNumber as type of the underlying values of `Width` and `Height`. Contains:
- Operators to perform calculations.
- Default casts (also from/to `ValueTuple`).
- Converters to convert to a different `TNumber`.
- Some simple predefined calculations: `Circumference()`, `Area()`.
- An iterator to iterate all points in the size: `GetAllPoints()` or use `foreach(var point in new Size<int>(10, 10))`.
- Extension to convert size to inline css.

## Surface 
An Euclidean plane of a specific size (and offset). Contains bound checks using the size offset.
- Some simple predefined calculations: `Circumference`, `Area`.
- Converters to convert to a different `TNumber`.
- A way to (try to) get the address of a point on the plan. 
- An iterator to iterate all points in a certain direction.
- An iterator to iterate all points of a line.
- An iterator to iterate all points in the dimensions.

## Directions
A direction holds a point which contains the delta in X and Y. There are different types of directions. 
Every direction type is a value object (and therefore immutable).

### FreeDirection
A direction in every possible delta point. Contains:
- An `Angle`, which also can be consumed by the constructor of `FreeDirection`.
- Default casts.
- Converters to convert to a different `TNumber`.
- Methods to take a (random) turn.

### StrictDirection
A strongly typed direction that resides in a specific enum which holds other direction values.
This object makes it easy to enumerate over the directions and search for them.
No free directions can be used. Contains:
- Predefined definitions: 
  - `DiagonalDirection`
  - `EveryDirection`
  - `HorizontalDirection`
  - `OrthogonalDirection`
  - `VerticalDirection`
- Default casts.
- Converters to convert to a different `TNumber`.
- Converters to (try to) convert to a different StrictDirection.
- Methods to take a (random) turn using `RotationType`.

> StrictDirections make use of the [MagicEnums library](https://github.com/Code-Chops/MagicEnums/) to easily enumerate directions in one definition.

> StrictDirections make use of the [ImplementationDiscovery library](https://github.com/Code-Chops/ImplementationDiscovery/) to easily enumerate different definitions.

> An example of predefined definition `DiagonalDirection`
> ```csharp
> public static readonly DiagonalDirection<TNumber> NorthEast = CreatePoint( 1, -1);
> public static readonly DiagonalDirection<TNumber> SouthEast = CreatePoint( 1,  1);
> public static readonly DiagonalDirection<TNumber> SouthWest = CreatePoint(-1,  1);
> public static readonly DiagonalDirection<TNumber> NorthWest = CreatePoint(-1, -1);
> ```

### NoDirection
A singleton object that holds a delta point of (0, 0).

## Movements
Describes in what direction an object moves from a starting point, using a [timer](#Timer). 
There are different types of movements. Every movement type is a value object (and therefore immutable).

### StraightMovement
A movement that only goes into one straight direction over its lifetime.

### DynamicMovement
A movement in which the direction and (therefore the location over time) can be determined by using a formula.

### NoMovement
Should be used for objects that don't move. It can still hold a direction.

# Time

## Timer
A timer helps calculating the direction and location of a moving object deterministically. It uses `Stopwatch` behind the scenes for accuracy.
The speed and value of the time can be influenced by using a [custom speed timer](#CustomSpeedTimer).

## CustomSpeedTimer
A timer (wraps `Stopwatch`) in which the speed or time can be increased manually.  
The timer can be stopped, reset and (re)started.

## TimerScope
Controls access to a timer in the ambient context. This way a statically shared timer can be accessed and controlled from the outside.

> This functionality makes use of Ambient Context pattern as implemented by TheArchitectDev. See the [Architect.AmbientContexts package](https://github.com/TheArchitectDev/Architect.AmbientContexts).

## UnixEpochConverter
A small helper to convert DateTimes to Unix time stamps and vice versa.
