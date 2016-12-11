# Generic Methods

A *generic method* is declared by specifying the generic type in the method declaration.

```cs
void Swap<T>(ref T p, ref T q) {
  T temp;

  temp = p;
  p = q;
  q = temp;
}
```
