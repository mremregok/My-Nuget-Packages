Bu paket, asp.net uygulamalarında tek tek servis kayıt etmek yerine, program.cs içerisinde tek bir metot ile
Tüm servislerinizi kaydetmeniz için bir yöntem içerir.

# Çalışma prensibi:

Uygulama build edilmeden önce aşağıdaki gibi paketin içerisinde yer alan metot çağrılmalıdır.

```
builder.Services.RegisterServices();
```

## Attribute Yöntemi

Servis sınıfınıza [DependencyInjection] Attribute'ini eklemeniz yeterli olacaktır.

Attribute için gerekli 2 parametre vardır: "Type" ve "LifeTime"

Type => Buraya hangi interface ile bu servisi kaydetmek istiyorsanız onun tipini girmelisiniz. Zorunlu bir alan değildir. 
Eğer bu alanı belirtmezseniz, Servisinizin kalıtmış olduğu ve isminin başında "I" olan ilk arayüzle beraber kaydetmeyi dener.

LifeTime => Enum. Servisin hangi lifetime(Singleton, scoped, transient) değerine sahip olacağını
belirtiyorsunuz.

### Örnek kullanım:
```
[DependencyInjection(Lifetime.Scoped)]
public class TestService : ITestService
{
}
```

### Örnek Kullanım 2:
```
[DependencyInjection(typeof(IGenericService), Lifetime.Scoped)]
public class BookService : IGenericService
{
}
```

### Yer alan türler
```
public enum Lifetime
    {
        Singleton,
        Transient,
        Scoped
    }
```


## Marker Pattern

Interface Marker yöntemi ile Dependency Injection yapılacak servisi belirtebilirsiniz.

Servisinize pakette yer alan bir lifetime metot adlarına sahip interface kalıtımı yapmanız gerekli.

Her interface, bir generic tipte, kalıtmak istediğiniz Interface'i alır.

### Örnek kullanım:

```
public class InterfaceMarkerTest : IInterfaceMarkerTest, IScopedService<IInterfaceMarkerTest>
{
}
```

### Yer alan arayüzler:
```
public interface IScopedService<T> : IServiceLifetime where T : class { }
public interface ISingletonService<T> : IServiceLifetime where T : class { }
public interface ITransientService<T> : IServiceLifetime where T : class { }
```


# Changes

## 1.0.3: 
### Attribute yöntemi için, tek bir sınıf için 1 defa kullanım olacak şekilde sınırlandırıldı. Duplicate
### edilmesi engellendi.