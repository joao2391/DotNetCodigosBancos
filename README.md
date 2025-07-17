# DotNetCodigosBancos [![Nuget](https://img.shields.io/nuget/v/DotNetCodigosBancos)](https://www.nuget.org/packages/DotNetCodigosBancos/) ![Nuget](https://img.shields.io/nuget/dt/DotNetCodigosBancos)

DotNet.CodigosBancos is a .Net library that helps you to get brazilian's offset codes .

## Notes
Version 2.0.0:

- Upgrade to .NET 9

## Installation

Use the package manager to install.

```bash
Install-Package DotNetCodigosBancos  -Version 2.0.0
```

## Usage

```C#
services.<ChooseYours><IHttpClientWrapper, HttpClientWrapper>();
services.<ChooseYours><ICodigosBanco, CodigoBanco>();

```

### Features
```C#
var bancosByNome = GetBancosByNome("itau");
// bancosByNome.BancoArray[0].Codigo -> 341
// bancosByNome.BancoArray[0].Nome -> Itaú Unibanco S.A.
// bancosByNome.BancoArray[0].Site -> www.itau.com.br

var bancos = GetBancos();
// It returns all banks.

var bancosByCodigo = GetBancoByCodigo(654);
// bancosByCodigo.BancoArray[0].Codigo -> 654
// bancosByCodigo.BancoArray[0].Nome -> Banco A.J.Renner S.A.
// bancosByCodigo.BancoArray[0].Site -> www.bancorenner.com.br

```

## Contributing
Pull requests are welcome. For major changes, please open an issue first to discuss what you would like to change.

Please make sure to update tests as appropriate.

## License
[MIT](https://choosealicense.com/licenses/mit/)
