Console.WriteLine("Hello, World!");

int add(int i, int j) => i + j;

int sub(int i, int j) => i - j;

int mult(int i, int j) => i * j;

int div(int i, int j) => i / j;

int mod(int i, int j) => i - (i / j * j);

intFn[] funcs = { add, sub, mult, div, mod };

foreach(intFn fn in funcs) {
    Console.WriteLine($"{fn(12, 3)}");
}

intFn Rebecca = mod;
Console.WriteLine(Rebecca(7, 3));


delegate int intFn(int i, int j);
