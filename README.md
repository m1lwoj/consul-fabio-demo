# consul-fabio-demo

Windows env:
- fabio https://github.com/fabiolb/fabio/releases
- consul https://www.consul.io/downloads.html
- ASP.NET Core 2.2 https://dotnet.microsoft.com/download/dotnet-core/2.2

Run consul and fabio
- ./consul.exe agent -dev
- ./fabio.exe

Run commands for each service:
- dotnet restore
- dotnet run --environment=dev

