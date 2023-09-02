Bu paket, asp.net uygulamalarında tek tek servis kayıt etmek yerine, program.cs içerisinde tek bir metot ile
Tüm servislerinizi kaydetmeniz için bir yöntem içerir.

# Çalışma prensibi:

Uygulama build edilmeden önce aşağıdaki gibi paketin içerisinde yer alan metot çağrılmalıdır.

```
builder.Services.RegisterDependencies();
```

## Attribute Yöntemi

Servis sınıfınıza [DIClass] Attribute'ini eklemeniz yeterli olacaktır.

Attribute için gerekli 2 parametre vardır: "Type" ve "DependencyInjectionScope"

Type => Buraya hangi interface ile bu servisi kaydetmek istiyorsanız onun tipini girmelisiniz.

DependencyInjectionScope => Enum. Servisin hangi lifetime(Singleton, scoped, transient) değerine sahip olacağını
belirtiyorsunuz.

### Örnek kullanım:
```
[DIClass(typeof(ITestService), DependencyInjectionScope.Scoped)]
public class TestService : ITestService
{
}
```

### Yer alan türler
```
public enum DependencyInjectionScope
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
